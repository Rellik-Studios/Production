using UnityEngine;
namespace Himanshu
{
    public class CryoChamber : MonoBehaviour
    {
        [SerializeField] private Material m_cryoOnlineLit;
        [SerializeField] private Material m_cryoOnlineUnlit;
        [SerializeField] private Material m_cryoOffline;
        [SerializeField] private Material m_screenLit;
        private bool m_broken = false;
        private MeshRenderer m_cryoRenderer;
        private MeshRenderer m_screenRenderer;
        

        private void Start()
        {
            m_cryoRenderer = GetComponent<MeshRenderer>();    
            m_cryoRenderer.material = m_cryoOffline;
            m_broken = Random.Range(0, 100) > 92;
            m_cryoRenderer.material = !m_broken ? m_cryoOnlineUnlit : m_cryoOffline;   
            m_screenRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (_collider.GetComponent<PlayerInteract>() != null) {
                if (m_cryoRenderer.material.name == m_cryoOnlineUnlit.name + " (Instance)")
                {
                    m_cryoRenderer.material = m_cryoOnlineLit; 
                    // this.Invoke(()=>m_cryoRenderer.material = m_cryoOffline, 5f);
                    // this.Invoke(()=>m_cryoRenderer.material = m_cryoOnlineUnlit, 10f);
                } 
                
                m_screenRenderer.material = m_screenLit;
                
            }
        }


    }
}
