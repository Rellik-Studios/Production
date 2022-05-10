using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Himanshu
{
    public class gameManager : MonoBehaviour
    {
        public static gameManager Instance => m_instance;

        private static gameManager m_instance;

        public string m_currentRoom = "";
        public int m_deathCounter;
        public bool m_isSafeRoom;

        public bool isTutorialRunning = false;
        public float m_triggerDistance = 10f;
        
        public bool? m_objTutorialPlayed = null;
        public bool? m_bookTutorialPlayed = null;
        public bool? m_endTutorialPlayed = null;
        [CanBeNull] public string m_username = null;

        [HideInInspector]public List<string> m_oneTimeTextAlreadyPlayed = null;

        private int m_objTut;
        private int m_bookTut;
        private int m_endTut;
        public string m_timeEra = "";

        private void Awake()
        {
            if (m_instance == null)
            {
                DontDestroyOnLoad(this);
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            var objs = GameObject.FindGameObjectsWithTag("EnemyBlocker");
            foreach (var obj in objs)
            {
                obj.AddComponent<EnemyBlocker>();
            }
        }

        public void ResetManager()
        {
            m_objTutorialPlayed = null;
            m_bookTutorialPlayed = null;
            m_endTutorialPlayed = null;
            isTutorialRunning = false;
            m_oneTimeTextAlreadyPlayed = new List<string>();
            m_username = null;
        }

        private void Update()
        {

            m_objTut = m_objTutorialPlayed == true ? 1 : m_objTutorialPlayed == false ? 0 : 2;
            m_bookTut = m_bookTutorialPlayed == true ? 1 : m_objTutorialPlayed == false ? 0 : 2;
            m_endTut = m_endTutorialPlayed == true ? 1 : m_objTutorialPlayed == false ? 0 : 2;
            
        }

    }
}