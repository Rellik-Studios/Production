using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

namespace rachael.FavorSystem
{
    public class FavorSystem : MonoBehaviour
    {
        private bool m_isOpen = false;
        [FormerlySerializedAs("isDanger")] public bool m_isDanger = false;
        [FormerlySerializedAs("commandPrompt")] public GameObject m_commandPrompt;

        [FormerlySerializedAs("commandFeatures")] public Text[] m_commandFeatures;
        [FormerlySerializedAs("commandText")] public Text m_commandText;

        [SerializeField] private TMP_InputField m_inputField;


        // Start is called before the first frame update
        void Start()
        {
            if (!m_isOpen)
            {
                m_commandPrompt.SetActive(false);
                Debug.Log(m_isDanger);
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                m_inputField.ActivateInputField();
                m_inputField.Select();
                m_inputField.text = "";
                OpenCommandPromptWindow();
            }
        }

        void OpenCommandPromptWindow()
        {
            m_isOpen = !m_isOpen;
            if (m_isOpen)
            {
                Debug.Log("Command Prompt is open");
                m_commandPrompt.SetActive(true);
                DisplayingCommands();


            }
            else
            {
                Debug.Log("Command Prompt is close");
                m_commandPrompt.SetActive(false);

            }
        }

        private IEnumerator EKeyboardInput()
        {
        
        
            yield return null;
        }

        public void DisplayingCommands()
        {
            //Displaying different commands depending on the user

            if (m_isDanger)
            {
                //Opening special commands
                m_commandText.text = m_commandFeatures[1].text;
            }
            else
            {
                //Opening normal commands
                m_commandText.text = m_commandFeatures[0].text;
            }


        }


        public void AddCommmandLine(int i)
        {
            m_commandText.text += "\n\n";
            m_commandText.text += m_commandText.text = m_commandFeatures[i].text;
        }
    }

}
