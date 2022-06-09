using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Himanshu
{
    public class QTE : MonoBehaviour
    {
        [SerializeField]private Sprite m_ring;

        private int m_passCounter;
        private int m_failCounter;

        [SerializeField] private int m_maxPass = 3;
        [SerializeField] private int m_maxFail = 1;

        public bool m_result;
        private IEnumerator Start()
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            int index = 0;
            while (m_passCounter < m_maxPass)
            {
                var obj = Instantiate(new GameObject(), transform);
                obj.AddComponent<Image>();
                obj.transform.localPosition = new Vector2(Random.Range(-800.0f, 800.0f), Random.Range(-440.0f, 440.0f));
                obj.GetComponent<Image>().sprite = m_ring;
                var diameter = Random.Range(0f, 240f);
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(diameter, diameter);

                while (obj.GetComponent<Image>().color.a > 0)
                {
                    var color = obj.GetComponent<Image>().color;
                    obj.GetComponent<Image>().color =
                        new Color(color.r, color.b, color.g, color.a - Time.unscaledDeltaTime / 3f);
                    if (Vector2.Distance(Input.mousePosition, obj.transform.position) <
                        obj.GetComponent<RectTransform>().rect.width)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Destroy(obj);
                            m_passCounter++;
                            break;
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Destroy(obj);
                            m_failCounter++;
                            break;
                        }
                    }


                    yield return null;
                }

             
                index++;
                if (m_passCounter + m_failCounter != index)
                {
                    m_failCounter++;
                    
                }
                if (m_failCounter == m_maxFail)
                    break;
                Destroy(obj);
                yield return null;
            }
        }
        
        

        private void Update()
        {
            if (m_passCounter == m_maxPass)
            {
                // Debug.Log("QTE Successful");
                m_result = true;
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                FindObjectOfType<PlayerInteract>().enabled = true;
                gameObject.SetActive(false);
            }

            if (m_failCounter >= m_maxFail)
            {
                m_result = false;
                // Debug.Log("QTE Fail");
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
        }
    }
}