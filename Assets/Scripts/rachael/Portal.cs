using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class Portal : MonoBehaviour
    {
        public PlayerInteract m_player;
        [FormerlySerializedAs("changeFurniture")] public ChangeFurniture m_changeFurniture;
        [FormerlySerializedAs("portalObject")] public GameObject m_portalObject;

        public GameObject m_requiredCollectable;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            // if(m_player.m_numOfPieces != m_changeFurniture.index)
            if(m_requiredCollectable != null && m_requiredCollectable.gameObject.activeSelf)
            {
                GetComponent<BoxCollider>().enabled = false;

                if (m_portalObject != null)
                    m_portalObject.SetActive(false);
            }
            else
            {
                GetComponent<BoxCollider>().enabled = true;

                if(m_portalObject != null)
                    m_portalObject.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider _other)
        {
            Debug.Log("Contact");
            if (_other.CompareTag("Player"))
            {
                foreach (var choosingPath in FindObjectsOfType<ChoosingPath>())
                {
                    choosingPath.CheckCollect();
                    Tutorial.m_portalEntry = true;
                }

                m_changeFurniture.EndofTimeEra(_other);
            }
        }
    }
}
