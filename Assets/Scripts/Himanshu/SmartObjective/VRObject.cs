using System;
using System.Collections;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class VRObject : MonoBehaviour
    {
        private MeshCollider m_meshCollider;
        MeshRenderer m_meshRenderer;
        MeshRenderer m_childMeshRenderer;
        private PlayerSmartObjectives m_playerSmartObjectives;
        [SerializeField] private bool m_isObjective;
        private static GameObject m_vrCam;
        private void Start()
        {
            m_childMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
            m_playerSmartObjectives = FindObjectOfType<PlayerSmartObjectives>(true);
            m_meshCollider = GetComponent<MeshCollider>();
            m_meshRenderer = GetComponent<MeshRenderer>();
            if (m_meshCollider != null)
                m_meshCollider.enabled = false;
            if (m_vrCam == null) {
                m_vrCam = GameObject.FindGameObjectWithTag("VRCam");
                m_vrCam.GetComponent<Camera>().enabled = false;
            }
        }

        private void Update()
        {
            if (m_playerSmartObjectives.hasVRHeadset) {
                if (m_meshCollider != null)
                    m_meshCollider.enabled = true;
                if(!m_vrCam.activeSelf) 
                    m_vrCam.GetComponent<Camera>().enabled = true; 
            } 
            else {
                if (m_meshCollider != null)
                    m_meshCollider.enabled = false;
                if(m_vrCam.activeSelf)
                    m_vrCam.GetComponent<Camera>().enabled = false;
            }

            if (m_isObjective && m_playerSmartObjectives.hasVRHeadset && Vector3.Distance(transform.position, m_playerSmartObjectives.transform.position) < 8f) {
                Materialize();
            }
        }
        private void Materialize()
        {
            IEnumerator MaterializeObject()
            {
                while (m_childMeshRenderer.materials[0].GetFloat("_MaterialiseLevel") < 1f) {

                    m_childMeshRenderer.materials[0].SetFloat("_MaterialiseLevel", m_childMeshRenderer.materials[0].GetFloat("_MaterialiseLevel") + Time.deltaTime);
                    yield return null;

                }
            }
            
            StartCoroutine(MaterializeObject());
        }
    }
}
