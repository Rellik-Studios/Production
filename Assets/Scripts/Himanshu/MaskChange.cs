using System;
using System.Collections;
using UnityEngine;
namespace Himanshu
{
    public class MaskChange : MonoBehaviour
    {
        public Texture[] m_whiteToYellow;
        public Texture[] m_yellowToRed;

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

                m_dangerLevel = value;

            }
        }

        private IEnumerator eYellowToRed()
        {
            foreach (var texture in m_yellowToRed)
            {
                m_renderer.material.mainTexture = texture;
                yield return new WaitForSeconds(1f / 30f);
            }
        }

        private IEnumerator eWhiteToYellow(bool _reverse)
        {
            foreach (var texture in m_whiteToYellow)
            {
                m_renderer.material.mainTexture = texture;
                yield return new WaitForSeconds(1f / 30f);
            }
        }

        private void Start()
        {
            m_renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            throw new NotImplementedException();
        }
    }
}
