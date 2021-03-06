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
        [SerializeField] private Slider m_dSlider;

        private void Start()
        {
            m_audioMixer.GetFloat("bgAudio", out float _bgVol);
            m_audioMixer.GetFloat("sfxAudio", out float _sfxVol);
            m_audioMixer.GetFloat("dialogueAudio", out float _dValue);
            SetDVolume(_dValue);
            m_dSlider.value = _dValue;
            SetBGVolume(_bgVol);
            m_bgSlider.value = _bgVol;
            SetSFXVolume(_sfxVol);
            m_sfxSlider.value = _sfxVol;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1f;

        }

        public void SetBGVolume(float _value)
        {
            if (m_bgSlider.value > -25f)
                m_audioMixer.SetFloat("bgAudio", m_bgSlider.value);
            else
                m_audioMixer.SetFloat("bgAudio", -80f);
        }

        public void SetSFXVolume(float _value)
        {
            if(m_sfxSlider.value > -25f)
                m_audioMixer.SetFloat("sfxAudio", m_sfxSlider.value);
            else 
                m_audioMixer.SetFloat("sfxAudio", -80f);
        }

        public void SetDVolume(float _value)
        {
            if(m_dSlider.value > -25f)
                m_audioMixer.SetFloat("dialogueAudio", m_dSlider.value);
            else 
                m_audioMixer.SetFloat("dialogueAudio", -80f);
        }
    }

}