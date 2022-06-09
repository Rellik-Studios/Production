using System.Collections;
using rachael.SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class ChangeFurniture : MonoBehaviour
    {
        [FormerlySerializedAs("LoopObjects")] public GameObject[] m_loopObjects;
        [FormerlySerializedAs("Rooms")] public GameObject[] m_rooms;
        [FormerlySerializedAs("HUB_Doors")] public GameObject[] m_hubDoors;
        [FormerlySerializedAs("roomMain")] public GameObject m_roomMain;
        [FormerlySerializedAs("roomNoDoor")] public GameObject m_roomNoDoor;
        [FormerlySerializedAs("LocationPosition")] public GameObject m_locationPosition;

        [FormerlySerializedAs("noDoor")] public bool m_noDoor = false;

        public int index
        {
            get;
            private set;
        }
        // Start is called before the first frame update
        void Start()
        {
            foreach (GameObject obj in m_loopObjects)
            {
                obj.SetActive(false);
            }
            if (m_loopObjects.Length != 0)
            {
                m_loopObjects[index].SetActive(true);
            }

            //SavingTimeEra();

        }

        public void EndofTimeEra(Collider _other)
        {
            if (_other.GetComponentInParent<RespawnManager>() != null)
            {
            
                _other.GetComponentInParent<RespawnManager>().Teleport(m_locationPosition.transform);


                // Debug.Log("You have moved to a new location");
            }
            if (index <= (m_loopObjects.Length - 2))
            {
                //disable the object
                m_loopObjects[index].SetActive(false);

                index++;

                m_loopObjects[index].SetActive(true);

            }

            StartCoroutine(SavingProgress(_other));
            //closing the doors in the gameplay after each loop
            foreach (GameObject door in m_hubDoors)
            {

                door.GetComponent<Door>().CloseTheDoor(true);
            }

            // Debug.Log("Player has finished this time era");
        }


        public void CloseDoors()
        {
            // foreach (GameObject door in m_hubDoors)
            // {
            //
            //     door.GetComponent<Door>().CloseTheDoor(true);
            // }
        }
        //to allow the saving of the rotation and position more properly in the
        IEnumerator SavingProgress(Collider _other)
        {
            yield return new WaitForEndOfFrame();
            if (_other.GetComponentInParent<PlayerSave>() != null)
            {
                //saves the player data into the system
                //other.GetComponentInParent<PlayerSave>().
                //(true);
            }
            yield return null;
        }
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * 200f);

        }
        private void OnTriggerEnter(Collider _other)
        {
            // Debug.Log("Contact");
            if (_other.CompareTag("Player"))
            {
                if (_other.GetComponentInParent<RespawnManager>() != null)
                {
                    _other.GetComponentInParent<RespawnManager>().Teleport(m_locationPosition.transform);
                
                    // Debug.Log("You have moved to a new location");
                }
                if (index <= (m_loopObjects.Length - 2))
                {
                    //disable the object
                    m_loopObjects[index].SetActive(false);

                    index++;

                    //Setting the new location for the player after they go through the hole

                    //after raising the index by one
                    m_loopObjects[index].SetActive(true);

                    ////making the player appear in a room with no door
                    //roomNoDoor.SetActive(true);
                    //roomMain.SetActive(false);

                }
                if(_other.GetComponentInParent<PlayerSave>() != null)
                {
                    //saves the player data into the system
                    //other.GetComponentInParent<PlayerSave>().SavePlayer();
                }

                // Debug.Log("Time Era has changed");
            }
        }
        public void MakeDoorAppear()
        {
            m_roomMain.SetActive(true);
            m_roomNoDoor.SetActive(false);
        }

        //ONLY FOR PLAYER PREF
        public void SavingTimeEra()
        {
            //for the saving purposes---------------
            if (PlayerPrefs.HasKey("Index") == true)
            {
                index = PlayerPrefs.GetInt("Index");
                if (index <= (m_loopObjects.Length - 2))
                {

                    foreach (GameObject obj in m_loopObjects)
                    {
                        obj.SetActive(false);
                    }
                    m_loopObjects[index].SetActive(true);

                }
            }
            else
            {
                PlayerPrefs.SetInt("Index", index);
            }
            //---------------------------------------
        }


        public void SaveIndex(int _num)
        {
            index = _num;
        }
        public void LoadTimeEra()
        {
            if (index <= (m_loopObjects.Length - 2))
            {

                foreach (GameObject obj in m_loopObjects)
                {
                    obj.SetActive(false);
                }

                m_loopObjects[index].SetActive(true);

            }
        }

    }
}
