using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using rachael.FavorSystem;
using Himanshu;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer m_audioMixer;
    [SerializeField] private Slider m_bgSlider;
    [SerializeField] private Slider m_sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        float sfxValue = 0;
        float bgValue = 0;
        m_audioMixer.GetFloat("sfxAudio", out sfxValue);
        m_audioMixer.GetFloat("bgAudio", out bgValue);
        m_sfxSlider.value = sfxValue;
        m_bgSlider.value = bgValue;
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<PlayerInteract>().enabled = false;

    }

    public void Unpause()
    {
        if (FindObjectOfType<FavorSystem>().m_timeStop == false)
        {
            Time.timeScale = 1f;
        }
        else
        {
            FindObjectOfType<FavorSystem>().m_continueCounting = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<PlayerInteract>().enabled = true;
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
