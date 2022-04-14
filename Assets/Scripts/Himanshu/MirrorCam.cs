using System;
using System.Collections.Generic;
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

        public List<GameObject> m_objects;

        public GameObject m_bathroomObjective;
        public void EnableMirror()
        {
            m_objects.ForEach(t=>t.SetActive(false));
            m_bathroomObjective.SetActive(true);
        }
        
        public void DisableMirror()
        {
             m_objects.ForEach(t=>t.SetActive(true));
            m_bathroomObjective.SetActive(false);
        }

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
            if(m_beginTracking) {

                transform.position = m_playerCam.transform.position;
                transform.rotation = m_playerCam.transform.rotation;
            }
        }
    }
}