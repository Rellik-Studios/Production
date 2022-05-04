using System;
using System.Collections;
using rachael;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu.SmartObjective
{
    public class Padlock : MonoBehaviour
    {
        public bool m_isLocked = true;
        [SerializeField] private Door m_door;
        [SerializeField] private TMP_Text m_text;

        private void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var i1 = i;
                    var j1 = j;
                    transform.GetChild(1).GetChild(i).GetChild(j).GetComponent<Button>().onClick
                        .AddListener(() => TextEnter((i1 * 3 + (j1 + 1))));
                }
            }
        }

        public void TextEnter(int _number)
        {
            m_text.text += _number.ToString();
        }

        private void Update()
        {
            if (m_text.text.Length == 4)
            {
                if (m_text.text == "1969")
                {
                    m_isLocked = false;
                    m_door.OpenTheDoor();
                    gameObject.SetActive(false);
                }
                else
                {
                    m_isLocked = true;
                    m_text.text = "";
                }
            }
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            FindObjectOfType<PlayerFollow>().m_mouseInput = false;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FindObjectOfType<PlayerFollow>().m_mouseInput = true;
        }
    }
}