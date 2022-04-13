using System;
using UnityEngine;

namespace Himanshu
{
    public class MirrorCam : MonoBehaviour
    {
        [SerializeField] private GameObject m_playerCam;

        public Vector3 m_defPlayerPos;
        public Quaternion m_defPlayerRot;
        private static MirrorCam m_instance;
        private static bool m_beginTracking;

        private Vector3 m_defPos;
        private Quaternion m_defRot;

        public static bool beginTracking
        {
            get => m_beginTracking;
            set
            {
                m_beginTracking = value;
                if (value)
                {
                    m_instance.m_defPlayerPos = m_instance.m_playerCam.transform.position;
                    m_instance.m_defPlayerRot = m_instance.m_playerCam.transform.rotation;
                }
            }
        }

        private void Start()
        {
            m_instance = this;
            m_defPos = transform.position;
            m_defRot = transform.rotation;
        }

        private void Update()
        {
            if(m_beginTracking)
            {
                transform.position = m_defPos + (m_playerCam.transform.position - m_defPlayerPos);
                transform.rotation = m_playerCam.transform.rotation;
            }
        }
    }
}