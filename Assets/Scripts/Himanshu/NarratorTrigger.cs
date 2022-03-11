using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Himanshu
{
    public class NarratorTrigger : MonoBehaviour
    {
        public UnityEvent m_event;

        public bool m_objTutorial;
        
        private float m_waitTimer;

        private int counter;
        public UnityEvent m_idleEvent;

        public bool m_isHospital;
        public UnityEvent m_hospitalIdle;
        [SerializeField] private GameObject m_objective;

        private void Start()
        {
            counter = 0;
            GetComponent<BoxCollider>().enabled = false;
        }

        private void Update()
        {
            m_waitTimer -= Time.deltaTime;
            if (!GetComponent<BoxCollider>().enabled && !gameManager.Instance.isTutorialRunning)
            {
                GetComponent<BoxCollider>().enabled = true;
            }
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (m_objTutorial && (!gameManager.Instance.m_objTutorialPlayed ?? false))
            {
                Tutorial.RunObjTutorial(m_objective);
            }
            //m_waitTimer = Random.Range(25f, 40f);
            if(GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>()?.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if(GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>()?.Stop();
        }

        private void OnTriggerStay(Collider _collider)
        {

            if (!gameManager.Instance.m_objTutorialPlayed ?? false) return;
            
            if (_collider.CompareTag("Player") && m_waitTimer < 0f)
            {
                if (counter < 1)
                {
                    counter++;
                    m_waitTimer = Random.Range(30f, 45f);
                    m_event?.Invoke();
                }
                else
                {
                    m_waitTimer = Random.Range(30f, 45f);
                    if (!m_isHospital)
                        m_idleEvent?.Invoke();
                    else
                    {
                        var rand = Random.Range(1, 11);
                        Debug.Log(rand);
                        if  (rand < 6) m_hospitalIdle?.Invoke(); else m_idleEvent?.Invoke();
                    }
                }
            }
        }
    }
}