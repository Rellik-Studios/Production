using System;
using System.Collections;
using System.Collections.Generic;
using rachael.FavorSystem;
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

        private PlayerInteract m_player;

        private void Start()
        {

            m_player = FindObjectOfType<PlayerInteract>();
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
            
            //m_waitTimer = Random.Range(25f, 40f);
            if(GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>()?.Play();

            m_player.m_invincible = false;
            gameManager.Instance.m_currentRoom = gameObject.name;

            if (gameObject.name != "Hub" && _collider.CompareTag("Player")) 
            {
                // Debug.Log("Player entered new room");
                FavorSystem.startTimer = true;
                if(!m_player.m_isDying)
                    FavorSystem.m_grantSpecial = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            m_player.m_invincible = true;
            if(GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>()?.Stop();
            gameManager.Instance.m_currentRoom = "";
        }

        private void OnTriggerStay(Collider _collider)
        {

            if (!gameManager.Instance.m_objTutorialPlayed ?? true && m_objTutorial) return;
            
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
                        // Debug.Log(rand);
                        if  (rand < 6) m_hospitalIdle?.Invoke(); else m_idleEvent?.Invoke();
                    }
                }
            }
        }
    }
}