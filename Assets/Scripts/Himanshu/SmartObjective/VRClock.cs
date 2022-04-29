using System;
using rachael;
using TMPro;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class VRClock : MonoBehaviour
    {
        private TMP_Text m_text;
        private PlayerSmartObjectives m_playerSmartObjectives;
        private void Start()
        {
            m_playerSmartObjectives = FindObjectOfType<PlayerSmartObjectives>();
            m_text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            m_text.text = NarratorScript.Time;
        }
    }
}
