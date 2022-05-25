using System;
using System.Collections.Generic;
using Himanshu.SmartObjective;
using rachael;
using rachael.FavorSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Himanshu
{

    public class Objective : MonoBehaviour, IInteract
    {
        [SerializeField] private GameObject m_dissapearingDoor;
        [SerializeField] private GameObject m_unlockVFX;
        [SerializeField] private List<Transform> m_possibleLocations;
        [SerializeField] private List<GameObject> m_glitchedCubes;
        [SerializeField] private List<TMP_Text> m_heartRateTexts;
        private AudioSource m_audioSource;
        [SerializeField] private Padlock m_padlock;
        public bool m_locked = false;
        [SerializeField] private UnityEvent m_onComplete;

        private void Start()
        {
            m_heartRateTexts.ForEach(t=>t.text = Random.Range(66, 99).ToString());
            
            if (m_possibleLocations.Count > 0) {
                var t = Random.Range(0, 6);
                transform.position = m_possibleLocations[t].position;
                transform.rotation = m_possibleLocations[t].rotation;
                m_glitchedCubes[t].GetComponent<Renderer>().material.SetInt("_UseFillPercent", 0);
                m_audioSource = GetComponent<AudioSource>();
                m_padlock.m_lockCode = "0" + (t + 1).ToString() + m_heartRateTexts[t].text;
            }
            
        }

        public void Execute(PlayerInteract _player)
        {
            if(m_locked)    return;
            FavorSystem.startTimer = false;
            m_unlockVFX.SetActive(true);
            m_audioSource?.Play();
            m_onComplete?.Invoke();
            m_glitchedCubes.ForEach(t=>t.GetComponent<Renderer>().material.SetInt("_UseFillPercent", 1));
            m_dissapearingDoor.SetActive(false);
            gameObject.SetActive(false);
        }

        public void CanExecute(Raycast _raycast)
        {
            if (m_locked)
            {
                _raycast.m_indication.enabled = false;
                return;
            }
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
        }
    }
}
