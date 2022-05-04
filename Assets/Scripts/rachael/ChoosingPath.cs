using UnityEngine;
using UnityEngine.Serialization;
using Himanshu;
namespace rachael
{
    public class ChoosingPath : MonoBehaviour
    {
        public GameObject m_player;
        // [FormerlySerializedAs("assignRoom")] public GameObject m_assignRoom;
        [FormerlySerializedAs("assignDoor")] public GameObject m_assignDoor;
        // [FormerlySerializedAs("eraChanging")] public ChangeFurniture m_eraChanging;
        // [FormerlySerializedAs("assignedName")] public string m_assignedName;
        //
        // [FormerlySerializedAs("route")] [SerializeField] private int m_route;

        [SerializeField] private GameObject m_assignedObject;

        // Start is called before the first frame update
        void Start()
        {
      
            if (!m_assignedObject.activeSelf)
            {
                m_assignDoor.GetComponent<DoorToWall>().TransformDoorToWall();
                Destroy(this);
            }
        
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider _other)
        {
            Debug.Log("Contact");
            if (_other.GetComponent<PlayerInteract>() != null)
            {
                m_assignDoor.GetComponent<DoorToWall>().FadeHubWall();
                    Destroy(this);
            }
        }

        public void CheckCollect()
        {
            Debug.Log(m_assignedObject);

            if (m_assignedObject == null)
            {
                m_assignDoor.GetComponent<DoorToWall>().TransformDoorToWall();
                Destroy(this);
            }
        }
    }
}
