using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace rachael.qte
{
    public class QteSystem : MonoBehaviour
    {

        [FormerlySerializedAs("DisplayBox")] public GameObject m_displayBox;
        [FormerlySerializedAs("PassBox")] public GameObject m_passBox;
        [FormerlySerializedAs("QTEGen")] public int m_qteGen;
        [FormerlySerializedAs("WaitingForKey")] public int m_waitingForKey;
        [FormerlySerializedAs("CorrectKey")] public int m_correctKey;
        [FormerlySerializedAs("CountingDown")] public int m_countingDown;


        // Update is called once per frame
        void Update()
        {
            if (m_waitingForKey == 0)
            {
                m_qteGen = Random.Range(1, 4);
                m_countingDown = 1;
                //StartCoroutine(CountDown());
                if (m_qteGen == 1)
                {
                    m_waitingForKey = 1;
                    m_displayBox.GetComponent<Text>().text = "[P]";
                }
            }

            if (m_qteGen == 1)
            {
                if(Input.anyKeyDown)
                {
                    if(Input.GetKeyDown(KeyCode.P))
                    {
                        m_correctKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        m_correctKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }

            
        }

        IEnumerator KeyPressing()
        {
            m_qteGen = 4;
            if(m_correctKey ==1)
            {
                m_countingDown = 2;
                m_passBox.GetComponent<Text>().text = "Pass!";
                yield return new WaitForSeconds(1.5f);
                m_correctKey = 0;
                m_passBox.GetComponent<Text>().text = "";
                m_displayBox.GetComponent<Text>().text = "";
                yield return new WaitForSeconds(1.5f);
                m_waitingForKey = 0;
                m_countingDown = 1;

            }
            if (m_correctKey == 2)
            {
                m_countingDown = 2;
                m_passBox.GetComponent<Text>().text = "Fail!";
                yield return new WaitForSeconds(1.5f);
                m_correctKey = 0;
                m_passBox.GetComponent<Text>().text = "";
                m_displayBox.GetComponent<Text>().text = "";
                yield return new WaitForSeconds(1.5f);
                m_waitingForKey = 0;
                m_countingDown = 1;

            }
        }
        IEnumerator CountDown()
        {
            yield return new WaitForSeconds(3.5f);
            if(m_countingDown == 1)
            {
                m_qteGen = 4;
                m_countingDown = 2;
                m_passBox.GetComponent<Text>().text = "Too slow";
                yield return new WaitForSeconds(1.5f);
                m_correctKey = 0;
                m_passBox.GetComponent<Text>().text = "";
                m_displayBox.GetComponent<Text>().text = "";
                yield return new WaitForSeconds(1.5f);
                m_waitingForKey = 0;
                m_countingDown = 1;
            }
        }

    }
}


