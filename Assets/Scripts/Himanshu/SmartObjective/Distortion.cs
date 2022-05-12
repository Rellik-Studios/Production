using System;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class Distortion : MonoBehaviour
    {
        [SerializeField] private Shader m_bandGlitch;
        [SerializeField] private Shader m_glitch;

        private void Start()
        {
            var mat = new Material(m_bandGlitch);
            mat.SetTexture("_MainTex", GetComponent<Renderer>().material.mainTexture);
        }
    }

}
