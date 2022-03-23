using System;
using System.Collections;
using UnityEngine;
namespace Himanshu
{
    public class MaskChange : MonoBehaviour
    {
        public Texture[] m_whiteToYellow;
        public Texture[] m_yellowToRed;

        public Texture[] m_emWhiteToYellow;
        public Texture[] m_emYellowToRed;

        private EnemyController.eDanger m_dangerLevel;

        private Renderer m_renderer;

        public EnemyController.eDanger dangerLevel
        {
            get => m_dangerLevel;
            set
            {
                if (value == EnemyController.eDanger.red)
                {
                    StartCoroutine(eYellowToRed());
                }
                else if (value == EnemyController.eDanger.yellow)
                {
                    if (m_dangerLevel == EnemyController.eDanger.white)
                        StartCoroutine(eWhiteToYellow());
                    if (m_dangerLevel == EnemyController.eDanger.red)
                        StartCoroutine(eRedToYellow());
                } 
                else 
                {
                    StartCoroutine(eYellowToWhite());
                }

                m_dangerLevel = value;

            }
        }

        private IEnumerator eYellowToRed()
        {
            foreach (var texture in m_yellowToRed)
            {
                m_renderer.material.mainTexture = texture;
                //m_renderer.material.SetTexture("_EmmisionMap", );
                yield return new WaitForSeconds(1f / 30f);
            }
        }
        
        private IEnumerator eRedToYellow()
        {
            for (int  i = 4; i >= 0; i--)
            {
                m_renderer.material.mainTexture = m_yellowToRed[i];
                yield return new WaitForSeconds(1f / 30f);
            }
        }
        
        
        private IEnumerator eYellowToWhite()
        {
            for (int  i = 5; i >= 0; i--)
            {
                m_renderer.material.mainTexture = m_whiteToYellow[i];
                yield return new WaitForSeconds(1f / 30f);
            }
        }

        private IEnumerator eWhiteToYellow()
        {
            foreach (var texture in m_whiteToYellow)
            {
                m_renderer.material.mainTexture = texture;
                yield return new WaitForSeconds(1f / 30f);
            }

            while (m_dangerLevel == EnemyController.eDanger.yellow)
            {
                for (int i = 3; i < 6; i++)
                {
                    m_renderer.material.mainTexture = m_whiteToYellow[i];
                    yield return new WaitForSeconds(1f / 30f);
                }
                yield return new WaitForSeconds(1f / 30f);

            }
        }

        private void Start()
        {
            m_renderer = transform.GetChild(0).GetComponent<Renderer>();
        }

        private void Update()
        {
        }
    }
}
