using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Himanshu
{
    public class OneTimeText : MonoBehaviour
    {
        private static Func<bool> m_turnOff;
        private static TMP_Text m_tmpText;

        public static List<string> alreadyUsed
        {
            get => gameManager.Instance.m_oneTimeTextAlreadyPlayed;
            set => gameManager.Instance.m_oneTimeTextAlreadyPlayed = value;
        }

        private static IEnumerator StopText()
        {
            yield return new WaitForSeconds(5f);
            m_tmpText.text = "";
        }
        public static void SetText(string _text, Func<bool> _turnOff)
        {
            var inst = FindObjectOfType<OneTimeText>();
            if(alreadyUsed.Contains(_text))
                return;
            m_tmpText.text = _text;
            m_turnOff = _turnOff;
            alreadyUsed.Add(_text);
            inst.StopAllCoroutines();
            inst.StartCoroutine(StopText());
        }

        public static void SetTextRepeated(string _text, Func<bool> _turnOff)
        {
            var inst = FindObjectOfType<OneTimeText>();
                
            m_tmpText.text = _text;
            m_turnOff = _turnOff;
            if(!alreadyUsed.Contains(_text))
                alreadyUsed.Add(_text);
            inst.StopAllCoroutines();
            inst.StartCoroutine(StopText());
        }

        private void Start()
        {
            alreadyUsed ??= new List<string>();
            m_tmpText = GetComponent<TMP_Text>();
            m_turnOff = () => false;
        }

        private void Update()
        {
            if (m_turnOff())
            {
                m_tmpText.text = "";
            }
        }
    }
}