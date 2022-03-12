using System;
using UnityEngine;

namespace Himanshu
{
    public class gameManager : MonoBehaviour
    {
        public static gameManager Instance => m_instance;

        private static gameManager m_instance;

        public int m_deathCounter;
        public bool m_isSafeRoom;

        public bool isTutorialRunning = false;
        public float m_triggerDistance = 10f;
        
        public bool? m_objTutorialPlayed = null;
        public bool? m_bookTutorialPlayed = null;

        private int m_objTut;
        private int m_bookTut;
        
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

        public void ResetManager()
        {
            m_objTutorialPlayed = null;
            m_bookTutorialPlayed = null;
            isTutorialRunning = false;
        }

        private void Update()
        {

            m_objTut = m_objTutorialPlayed == true ? 1 : m_objTutorialPlayed == false ? 0 : 2;
            m_bookTut = m_bookTutorialPlayed == true ? 1 : m_objTutorialPlayed == false ? 0 : 2;

        }

    }
}