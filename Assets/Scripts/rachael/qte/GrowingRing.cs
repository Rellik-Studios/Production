using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace rachael.qte
{
    public class GrowingRing : MonoBehaviour
    {
        Image m_playerRing;

        GameObject m_gameRing;

        bool m_isInRing = false;

        [FormerlySerializedAs("speed")] public float m_speed = 1.0f;

        [FormerlySerializedAs("lowestRing")] public float m_lowestRing = 1350.0f;
        [FormerlySerializedAs("highestRing")] public float m_highestRing = 1550.0f;

        float m_ringSize;

        float m_betweenValues; 

        // Start is called before the first frame update
        void Start()
        {
            m_playerRing = GetComponent<Image>();
            if(FindObjectOfType<QteRing>() != null)
                m_gameRing = FindObjectOfType<QteRing>().gameObject;
            m_betweenValues = m_highestRing - m_lowestRing;
            m_ringSize = m_gameRing.GetComponent<Image>().rectTransform.sizeDelta.x;
        }

        // Update is called once per frame
        void Update()
        {
            m_playerRing.rectTransform.sizeDelta = new Vector2(m_playerRing.rectTransform.sizeDelta.x + (m_speed), m_playerRing.rectTransform.sizeDelta.y + (m_speed));
            if (OuterRing() && InnerRing())
            {
                m_isInRing = true;
            }
            else
            {
                m_isInRing = false;
            }
            if(m_playerRing.rectTransform.sizeDelta.x >= 2000.0f || m_playerRing.rectTransform.sizeDelta.y >= 2000.0f)
            {
                m_gameRing.GetComponent<QteRing>().LateRing();
                Destroy(gameObject);
            }
        }
        public bool IsRingInisde()
        {
            return m_isInRing;
        }
        //public bool OuterRing()
        //{
        //    return (playerRing.rectTransform.sizeDelta.x >= lowestRing && playerRing.rectTransform.sizeDelta.y >= lowestRing);

        //}
        //public bool InnerRing()
        //{
        //    return (playerRing.rectTransform.sizeDelta.x <= highestRing && playerRing.rectTransform.sizeDelta.y <= highestRing);

        //}

        public bool OuterRing()
        {
            return (m_playerRing.rectTransform.sizeDelta.x >= (m_ringSize -m_betweenValues) && m_playerRing.rectTransform.sizeDelta.y >= (m_ringSize - m_betweenValues));

        }
        public bool InnerRing()
        {
            return (m_playerRing.rectTransform.sizeDelta.x <= (m_ringSize + m_betweenValues) && m_playerRing.rectTransform.sizeDelta.y <= (m_ringSize + m_betweenValues));

        }
    }
}
