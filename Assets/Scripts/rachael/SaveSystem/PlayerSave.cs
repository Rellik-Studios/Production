using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace rachael.SaveSystem
{
    public class PlayerSave : MonoBehaviour
    {
        [FormerlySerializedAs("eraChanging")] public ChangeFurniture m_eraChanging;
        [FormerlySerializedAs("respawnManager")] public RespawnManager m_respawnManager;
        [FormerlySerializedAs("player")] public PlayerInteract m_player;

        public GrandfatherClock m_grandfatherClock;
        //public SavingGame saveFile;

        public List<CollectableObject> m_depositedToTheClock;

        public List<CollectableObject> m_dep;
        
        public Dictionary<CollectableObject, Wrapper<int>> m_inventory;
        [FormerlySerializedAs("Index")] public int m_index; //the number which index for each time era (for change furniture)
        //public int Death;

        // Start is called before the first frame update
        void Awake()
        {
            m_depositedToTheClock = new List<CollectableObject>();
            m_inventory = new Dictionary<CollectableObject, Wrapper<int>>();
            Debug.Log(Application.persistentDataPath);
            if (!gameManager.Instance.m_isSafeRoom && Directory.Exists(Application.persistentDataPath + "/player/"))
            {
                LoadPlayer();
            }
            else if (Directory.Exists(Application.persistentDataPath + "/player/"))
            {
                LoadPlayer(true);
                gameManager.Instance.m_isSafeRoom = false;
            }
            else
            {
                FindObjectOfType<Himanshu.Tutorial>().RunTutorial();
                SavePlayer();
                PlayerPrefs.SetInt("Death", 0);
                PlayerPrefs.SetInt("SaveFile", 1);
            }

            //m_dep = m_depositedToTheClock.Keys.ToList();
        }

        // Update is called once per frame
        void Update()
        {
            ////press key number 0
            //if (Input.GetKeyDown(KeyCode.Alpha0))
            //{
            //    SavePlayer();
            //}
            ////press key number 9
            //if (Input.GetKeyDown(KeyCode.Alpha9))
            //{
            //    LoadPlayer();
            //}

            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerPrefs.DeleteAll();
            }
            #endif

        }

        public void SavePlayer(bool _isSafeRoom = false)
        {
            //saveFile.SavePoint();
            SavingValues();
            SaveSystem.SavePlayer(this, _isSafeRoom);
        }


        public void SavingValues()
        {
            m_index = m_eraChanging.index;
            m_inventory = m_player.m_inventory;
            Debug.Log(m_depositedToTheClock);
            m_depositedToTheClock = m_grandfatherClock.m_depositedObjects;


        }
        public void LoadingValues(List<CollectableObjectWrapper> _depositedObjects, List<CollectableObjectWrapper> _inventory)
        {
            //making a transfer of data from the file to the scripts in gameplay
            m_eraChanging.SaveIndex(m_index);
            m_player.m_deathCount = PlayerPrefs.GetInt("Death");
            m_player.Load(_inventory);
            
            //m_player.m_inventory = m_inventory;
            //m_grandfatherClock.m_depositedObjects = m_depositedToTheClock.Keys.ToList();
            m_grandfatherClock.Load(_depositedObjects);

            //m_player.m_testInventory = m_inventory.Keys.ToList();


        }
        public void LoadPlayer(bool _isSafeRoom = false)
        {

            PlayerData data = SaveSystem.LoadPlayer(_isSafeRoom);

            if (data != null)
            {
                Cursor.lockState = CursorLockMode.Locked;
                GetComponent<CharacterController>().enabled = false;

                //m_inventory = data.m_inventory;
                m_index = data.m_index;
                //m_depositedToTheClock = data.m_depositedToTheClock;
                //Death = data.Death;

                LoadingValues(data.m_depositedToTheClock, data.m_inventory);

                m_eraChanging.LoadTimeEra();

                Vector3 position = new Vector3(0, 0, 0);
                position.x = data.m_position[0];
                position.y = data.m_position[1];
                position.z = data.m_position[2];

                Transform playerTransform = gameObject.transform;
                playerTransform.position = new Vector3(position.x, position.y, position.z);
            
            
                Vector3 rotation = new Vector3(0, 0, 0);
                rotation.x = data.m_rotation[0];
                rotation.y = data.m_rotation[1];
                rotation.z = data.m_rotation[2];

                Quaternion newrot = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

                playerTransform.rotation = new Quaternion(newrot.x, newrot.y, newrot.z, newrot.w);

                GetComponent<RespawnManager>().SetPosition(playerTransform);
                GetComponent<RespawnManager>().Respawn();

                NarratorScript.UserName = data.m_userName ?? Environment.UserName;



                GetComponent<CharacterController>().enabled = true;
            }
        }
 
    }
}