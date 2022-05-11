using rachael;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Himanshu.SmartObjective
{
    public class VRUserName : MonoBehaviour
    {
        private Text m_text;
        private void Start()
        {
            m_text = GetComponent<Text>();
        }

        private void Update()
        {
            m_text.text = "User: " + NarratorScript.UserName;
        }
    }
}
