using System;
using System.Collections;
using Bolt;
using UnityEngine;
namespace Himanshu
{
    public class MaskChange : MonoBehaviour
    {
        public Texture[] m_whiteToYellow;
        public Texture[] m_yellowToRed;

        public Texture2D[] m_emWhiteToYellow;
        public Texture2D[] m_emYellowToRed;

        public bool m_whiteToYellowTrigger;
        
        public bool m_yellowToRedTrigger;
        public bool m_whiteToRedTrigger;
        
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
            int index = 0;
            foreach (var texture in m_yellowToRed)
            {
                m_renderer.material.SetTexture("_EmissionMap", m_emYellowToRed[index]);
                m_renderer.material.mainTexture = texture;
                //m_renderer.material.SetTexture("_EmmisionMap", );
                index++;
                yield return new WaitForSeconds(1f / 30f);
            }
        }
        
        private IEnumerator eRedToYellow()
        {
            for (int  i = 4; i >= 0; i--)
            {
                m_renderer.material.SetTexture("_EmissionMap", m_emYellowToRed[i]);
                m_renderer.material.mainTexture = m_yellowToRed[i];
                yield return new WaitForSeconds(1f / 30f);
            }
        }
        
        
        private IEnumerator eYellowToWhite()
        {
            for (int  i = 5; i >= 0; i--)
            {
                m_renderer.material.SetTexture("_EmissionMap", m_emWhiteToYellow[i]);
                m_renderer.material.mainTexture = m_whiteToYellow[i];
                yield return new WaitForSeconds(1f / 30f);
            }
        }

        private IEnumerator eWhiteToYellow()
        {
            var index = 0;
            foreach (var texture in m_whiteToYellow)
            {
                m_renderer.material.SetTexture("_EmissionMap", m_emWhiteToYellow[index]);
                m_renderer.material.mainTexture = texture;
                index++;
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
            m_renderer.material.EnableKeyword("_EMISSION");
            DynamicGI.UpdateEnvironment();
        }

        private void Update()
        {
            if (m_whiteToYellowTrigger)
            {
                StartCoroutine(eWhiteToYellow());
                m_whiteToYellowTrigger = false;
            }
            if (m_yellowToRedTrigger)
            {
                StartCoroutine(eYellowToRed());
                m_yellowToRedTrigger = false;
            }
            if (m_whiteToRedTrigger)
            {
                StartCoroutine(eWhiteToYellow());
                m_whiteToRedTrigger = false;
            }
        }

        IEnumerator eWhiteToRed()
        {

            eWhiteToYellow();
            yield return new WaitForSeconds(6 / 30f);
            eYellowToRed();
        }
    }
}
