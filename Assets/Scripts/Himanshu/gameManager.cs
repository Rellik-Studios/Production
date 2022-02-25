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

    }
}