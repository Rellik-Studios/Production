using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Himanshu
{
    public class MainMenuAudio : MonoBehaviour
    {
        [SerializeField] private AudioMixer m_audioMixer;
        [SerializeField] private Slider m_bgSlider;
        [SerializeField] private Slider m_sfxSlider;

        private void Start()
        {
            SetBGVolume(0);
            SetSFXVolume(0);
            
            gameManager.Instance?.ResetManager();
        }

        public void SetBGVolume(float _value)
        {
            m_audioMixer.SetFloat("bgAudio", m_bgSlider.value);
        }

        public void SetSFXVolume(float _value)
        {
            m_audioMixer.SetFloat("sfxAudio", m_sfxSlider.value);
        }
    }
}