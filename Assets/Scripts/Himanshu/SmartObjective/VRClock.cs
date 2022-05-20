using System;
using rachael;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Himanshu.SmartObjective
{
    public class VRClock : MonoBehaviour
    {
        private Text m_text;
        private PlayerSmartObjectives m_playerSmartObjectives;
        private void Start()
        {
            m_playerSmartObjectives = FindObjectOfType<PlayerSmartObjectives>();
            m_text = GetComponent<Text>();
        }

        private void Update()
        {
            m_text.text = NarratorScript.Time;
        }
    }
}
