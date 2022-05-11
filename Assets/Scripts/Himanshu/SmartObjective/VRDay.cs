using System.Collections;
using rachael;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Himanshu.SmartObjective
{
    public class VRDay : MonoBehaviour
    {
        private Text m_text;
        private IEnumerator Start()
        {

            yield return new WaitForEndOfFrame();
            m_text = GetComponent<Text>();
            m_text.text = NarratorScript.WeekDay;
        }

    }
}
