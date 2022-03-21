using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Himanshu;
using rachael.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace rachael
{
    public class GrandfatherClock : MonoBehaviour, IInteract
    {
       public List<CollectableObject> m_depositedObjects;

        [SerializeField] private AudioClip m_pickUp;
        [SerializeField] private GameObject m_glow;

        private void OnEnable()
        {
            //m_depositedObjects = new List<CollectableObject>();

            //PlayerPrefs.DeleteAll();
        }

        // Start is called before the first frame update

        void Start()
        {

           
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void Execute(PlayerInteract _player)
        {
            var pieces = _player.m_inventory.Keys.Where(t => t.m_objectName.Contains("Clock_"));
            
            foreach (var piece in pieces)
            {
                if (!transform.parent.parent.Find(piece.m_objectName).gameObject.activeSelf)
                {
                    transform.parent.parent.Find(piece.m_objectName).gameObject.SetActive(true);
                    m_depositedObjects.Add(piece);
                    _player.m_inventory.Remove(piece);
                    _player.m_testInventory = _player.m_inventory.Keys.ToList();
                    FindObjectOfType<PlayerSave>().SavePlayer();
                    CheckVictory();
                    break;
                }
            }
            
            //CheckVictory();
        }

        private void CheckVictory()
        {
            IEnumerator WinRoutine()
            {
                m_glow.SetActive(true);
                var player = FindObjectOfType<PlayerMovement>();
                var playerCam = FindObjectOfType<PlayerFollow>();
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position -= player.transform.forward * 5f;
                yield return new WaitForEndOfFrame();
                player.GetComponent<CharacterController>().enabled = true;
                
                yield return new WaitUntil(() =>
                    Physics.OverlapSphere(player.transform.position, 1f).Any((t) => t.gameObject.name == "GlowCyl"));

                
                player.GetComponent<CharacterController>().enabled = false;
                float counter = 0f;
                playerCam.enabled = false;
                m_glow.SetActive(false);
                player.transform.position -= player.transform.forward * 2f;
                while(counter < 2f)
                {
                    counter += Time.deltaTime;
                    playerCam.transform.LookAt(transform.position + new Vector3(0f, 2f, 0f));
                    playerCam.transform.position += new Vector3(0f,Time.deltaTime * 2f, 0f);
                    yield return null;
                }

                FindObjectOfType<Fade>().color = Fade.eColor.white;

                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("WiningScreen");

                yield return null;
            }
            if (m_depositedObjects.Count == 4)
            {

                StartCoroutine(WinRoutine());
            }
        }

        public void CanExecute(Raycast _raycast)
        {
            //if the gear is within possession
             if (_raycast.m_player.GetComponent<PlayerInteract>().m_inventory.Count > 0)
             {
                 if (_raycast.m_indication != null)
                     _raycast.m_indication.sprite = Resources.Load<Sprite>("Place");
             }
             // no gear
             else
             {
                 if (_raycast.m_indication != null)
                     _raycast.m_indication.enabled = false;
             }
        }

        public void Load(List<CollectableObjectWrapper> _objs)
        {
            var collectables = GameObject.FindObjectsOfType<Collectable>();
            //collectables = collectables.Where(t => t.m_collectableObject.m_objectName.Contains("Clock_")).ToArray();
            foreach (var piece in _objs)
            {
                if (!transform.parent.parent.Find(piece.m_objectName).gameObject.activeSelf)
                {
                    transform.parent.parent.Find(piece.m_objectName).gameObject.SetActive(true);
                    var objToAdd = collectables.FirstOrDefault(t => t.m_collectableObject.m_objectName.Equals(piece.m_objectName));
                    
                    if (objToAdd == null) throw new Exception("object cannot be located");
                    
                    m_depositedObjects.Add(objToAdd.m_collectableObject);
                    objToAdd.gameObject.SetActive(false);
                }
            }
        }
    }
}
