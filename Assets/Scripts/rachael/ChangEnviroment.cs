using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class ChangEnviroment : MonoBehaviour
    {
        [FormerlySerializedAs("EnvirObject")] public GameObject[] m_envirObject;
        [FormerlySerializedAs("LocationObject")] public GameObject[] m_locationObject;
        [FormerlySerializedAs("DoorTrigger")] public GameObject m_doorTrigger;

        public int index
        {
            get;
            private set;
        }
        // Start is called before the first frame update
        void Start()
        {
            foreach(GameObject obj in m_envirObject)
            {
                obj.SetActive(false);
            }
            if(m_envirObject.Length !=0)
            {
                m_envirObject[0].SetActive(true);
            }
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * 200f);
        }
        private void OnTriggerEnter(Collider _other)
        {
            Debug.Log("Contact");
            if (_other.CompareTag("Player"))
            {
                if (_other.GetComponentInParent<RespawnManager>() != null)
                {
                    _other.GetComponentInParent<RespawnManager>().Teleport(m_locationObject[index].transform);
                    Debug.Log("You have moved to a new location");
                }
                if (index <= (m_envirObject.Length - 2))
                {
                    //disable the object
                    m_envirObject[index].SetActive(false);
                
                    index++;

                    //Setting the new location for the player after they go through the hole
                

                    //setting the trigger to disable so a certain door doesnt open
                    //NOTE: this door is the main room door with the clock
                    if(m_doorTrigger !=null)
                    {
                        m_doorTrigger.SetActive(false);
                    }
                    //after raising the index by one
                    m_envirObject[index].SetActive(true);
                }

                Debug.Log("Environment has changed");
            }
        }
    }
}
