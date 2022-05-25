using UnityEngine;
namespace Himanshu
{
    public class FireTv : MonoBehaviour
    {
        [SerializeField] private Texture2D[] m_anomalyTextures;
        [SerializeField] private Texture2D[] m_normalTextures;
        private Renderer m_renderer;
        public bool m_isAnomaly{ get; set; }
        private int m_index = 0;
        private float m_counter = 0f;
        private void Start()
        {
            m_renderer = GetComponent<Renderer>();
            m_isAnomaly = true;
        }

        private void Update()
        {
            m_counter += Time.deltaTime;
            if (m_isAnomaly && m_counter > 15f/60f) {
                m_renderer.material.SetTexture("_EmissionMap", m_anomalyTextures[m_index++]);
                if (m_index == 3) m_index = 0;
                m_counter = 0f;
            }
            else if (!m_isAnomaly && m_counter > 15f/60f) {
                m_renderer.material.SetTexture("_EmissionMap", m_normalTextures[m_index++]);
                if (m_index == 3) m_index = 0;
                m_counter = 0f;
            }
        }
    }
}
