using System.Collections.Generic;
using Himanshu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace rachael
{
    public class DoorScript : MonoBehaviour
    {
        [FormerlySerializedAs("myDoor")] [SerializeField] private Animator m_myDoor;
        [FormerlySerializedAs("openTrigger")] [SerializeField] private bool m_openTrigger = false;
        [FormerlySerializedAs("closeTrigger")] [SerializeField] private bool m_closeTrigger = false;
        [FormerlySerializedAs("isOpened")] [SerializeField] private bool m_isOpened = false;

        [SerializeField] private UnityEvent m_trigerredL1;
        [SerializeField] private UnityEvent m_trigerredL2;
        [SerializeField] private UnityEvent m_trigerredL3;
        [SerializeField] private UnityEvent m_trigerredL4;
    
        [FormerlySerializedAs("enemiesToEnable")] [SerializeField] private List<GameObject> m_enemiesToEnable;

    
        // Start is called before the first frame update
        void Start()
        {
        
            if (m_isOpened && m_openTrigger)
            {
                m_myDoor.SetBool("IsOpening", true);
            }
       
        }


        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider _other)
        {
            if(_other.CompareTag("Player"))
            {
                if((m_openTrigger && !m_isOpened) || (m_closeTrigger && m_isOpened))
                    GetComponent<AudioSource>().Play();
                if(m_closeTrigger)
                    _other.GetComponentInParent<PlayerInteract>().UnSpot();
                
                switch (FindObjectOfType<ChangEnviroment>().index)
                {
                    case 0:
                        m_trigerredL1?.Invoke();
                        break;
                    
                    case 1:
                        m_trigerredL2?.Invoke();
                        break;
                
                    case 2:
                        m_trigerredL3?.Invoke();
                        break;
                
                    case 3:
                        m_trigerredL4?.Invoke();
                        break;
                
                    default:
                    
                        break;
                }
                //old anim

                //if(openTrigger && DoneOnce)
                //{
                //    myDoor.Play("DoorOpening", 0, 0.0f);
                //    DoneOnce = false;

                //}
                //if (closeTrigger && DoneOnce)
                //{
                //    myDoor.Play("DoorClosing", 0, 0.0f);
                //    DoneOnce = false;

                //}
                if (m_openTrigger)
                {
                    m_myDoor.SetBool("IsOpening", true);
                    m_isOpened = true;
                }

                if (m_closeTrigger)
                {
                    m_myDoor.SetBool("IsOpening", false);
                    m_isOpened = false;
                }


            }
        }

        public void DoorOpening()
        {
            GetComponent<AudioSource>().Play();
            m_myDoor.SetBool("IsOpening", true);
        
        }
        public void DoorClosing()
        {
            GetComponent<AudioSource>().Play();
            m_myDoor.SetBool("IsOpening", false);
        }

    }
}
