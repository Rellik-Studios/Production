using System;
using System.Collections;
using UnityEngine;

namespace Himanshu
{
    public class Fade : MonoBehaviour
    {
        public enum eColor
        {
            black,
            white
        }

        [SerializeField] private Material m_blackMat;
        [SerializeField] private Material m_whiteMat;

        private GameObject m_plane;
        public eColor m_color;

        public bool m_setColor;
    
        public eColor color
        {
            get => m_color;
            set
            {
                m_color = value;
                StartCoroutine(eFade());
            }
        }

        void Start()
        {
            m_plane = transform.GetChild(0).gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_setColor)
            {
                m_setColor = false;
                StartCoroutine(eFade());
            }
        }

        private IEnumerator eFade()
        {
            var Rend = m_plane.GetComponent<Renderer>();

            m_plane.GetComponent<Renderer>().material = m_color == eColor.black ? m_blackMat : m_whiteMat;

            while (Math.Abs(Rend.material.color.a - 1f) > 0.0001f)
            {
                var alpha = Mathf.Lerp(Rend.material.color.a, 1f, Time.unscaledDeltaTime * (m_color == eColor.black ? 2f : 1f));
                Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b,alpha);
                yield return null;
            }

            yield return null;
        }
    }
}
