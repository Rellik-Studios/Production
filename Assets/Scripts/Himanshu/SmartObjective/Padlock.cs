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
        public string m_lockCode;
        

        
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
            transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(BackSpace);
            transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() => TextEnter(0));
            transform.GetChild(1).GetChild(3).GetChild(2).GetComponent<Button>().onClick.AddListener(Confirm);
        }

        public void TextEnter(int _number)
        {
            if(m_text.text.Length >= 4)
                return;
            
            m_text.text += _number.ToString();
        }
        public void BackSpace()
        {
            if (m_text.text.Length > 0) {
                m_text.text = m_text.text.Substring(0, m_text.text.Length - 1);
            }
        }

        public void Confirm()
        {
            if (m_text.text.Length == 4)
            {
                if (m_text.text == m_lockCode)
                {
                    m_isLocked = false;
                    m_door.OpenTheDoor();
                    gameObject.SetActive(false);
                }
                else
                {
                    m_isLocked = true;
                    //m_text.text = "";
                    StartCoroutine(eWrongPassword());
                }
            } 
            else {
                m_isLocked = true;
                //m_text.text = "";
                StartCoroutine(eWrongPassword());
            }
        }

        private void Update()
        {
            #if UNITY_EDITOR
                if(Input.GetKeyDown(KeyCode.Alpha0))
                    gameObject.SetActive(false);
            #else
                if(Input.GetKeyDown(KeyCode.Escape))
                    gameObject.SetActive(false);
            #endif
            
        }
        private IEnumerator eWrongPassword()
        {
            GetComponent<Animator>().SetTrigger("WrongPassword");
            yield return new WaitForSeconds(1f);
            m_text.text = "";
            yield return null;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            FindObjectOfType<PlayerFollow>().m_mouseInput = false;
            m_text.text = "";
            var player = FindObjectOfType<PlayerInteract>();
            player.enabled = false;
            FindObjectOfType<Raycast>().m_indication.enabled = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.GetComponent<PlayerInteract>() == null)    return;
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FindObjectOfType<PlayerFollow>().m_mouseInput = true;
            var player = FindObjectOfType<PlayerInteract>();
            player.enabled = true;
        }
    }
}