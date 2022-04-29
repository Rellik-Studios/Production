using System;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class VRObject : MonoBehaviour
    {
        private MeshCollider m_meshCollider;
        MeshRenderer m_meshRenderer;
        private PlayerSmartObjectives m_playerSmartObjectives;
        [SerializeField] private bool m_isObjective;
        private static GameObject m_vrCam;
        private void Start()
        {
            m_playerSmartObjectives = FindObjectOfType<PlayerSmartObjectives>();
            m_meshCollider = GetComponent<MeshCollider>();
            m_meshRenderer = GetComponent<MeshRenderer>();
            if (m_meshCollider != null)
                m_meshCollider.enabled = false;
            if (m_vrCam == null) {
                m_vrCam = GameObject.FindGameObjectWithTag("VRCam");
                m_vrCam.SetActive(false);
            }
        }

        private void Update()
        {
            if (m_playerSmartObjectives.m_hasVRHeadset) {
                if (m_meshCollider != null)
                    m_meshCollider.enabled = true;
                if(!m_vrCam.activeSelf) 
                    m_vrCam.SetActive(true);
            } 
            else {
                if (m_meshCollider != null)
                    m_meshCollider.enabled = false;
                if(m_vrCam.activeSelf)
                    m_vrCam.SetActive(false);
            }

            if (m_isObjective && m_playerSmartObjectives.m_hasVRHeadset && Vector3.Distance(transform.position, m_playerSmartObjectives.transform.position) < 8f) {
                Materialize();
            }
        }
        private void Materialize()
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
