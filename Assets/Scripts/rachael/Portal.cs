using System.Collections.Generic;
using System.Linq;
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
        private AudioClip m_audioClip;

        public GameObject[] m_requiredCollectable;

        private List<GameObject> m_reqCollectable;
        // Start is called before the first frame update
        void Start()
        {
            m_audioClip = Resources.Load<AudioClip>("SFX/TimeRift");
            m_reqCollectable = m_requiredCollectable.ToList();
        }

        // Update is called once per frame
        void Update()
        {
            m_reqCollectable.RemoveAll(t => t == null);
            // if(m_player.m_numOfPieces != m_changeFurniture.index)
            if(m_reqCollectable.Count > 0)
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
                PlayerInteract.PlaySound(m_audioClip);
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
