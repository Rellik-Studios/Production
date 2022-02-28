using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

namespace rachael.FavorSystem
{
    public enum ConsoleDisplay
    {
        defaultMenu = 0,
        SpecialMenu = 1
    }

    public class FavorSystem : MonoBehaviour
    {
        private bool m_isOpen = false;
        [FormerlySerializedAs("isDanger")] public bool m_isDanger = false;
        [FormerlySerializedAs("commandPrompt")] public GameObject m_commandPrompt;

        [FormerlySerializedAs("commandFeatures")] public Text[] m_commandFeatures;
        [FormerlySerializedAs("commandText")] public Text m_commandText;

        [SerializeField] private TMP_InputField m_inputField;

        

        public ConsoleDisplay consoleDisplay;

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
            if (Input.GetKeyDown(KeyCode.C) && !m_isOpen)
            {
                m_inputField.text = "";
                CommandPromptWindow();
            }
        }

        void CommandPromptWindow()
        {
            m_isOpen = !m_isOpen;
            if (m_isOpen)
            {
                Debug.Log("Command Prompt is open");
                m_commandPrompt.SetActive(true);
                DisplayingMainMenu();
                m_inputField.ActivateInputField();
                m_inputField.Select();


            }
            else
            {
                Debug.Log("Command Prompt is close");
                m_commandPrompt.SetActive(false);

            }
        }

        public void OpenCommandPrompt()
        {
            Debug.Log("Command Prompt is open");
            m_commandPrompt.SetActive(true);
            DisplayingMainMenu();
            m_inputField.ActivateInputField();
            m_inputField.Select();
            m_isOpen = true;
        }

        public void CloseCommandPrompt()
        {
            Debug.Log("Command Prompt is close");
            m_isOpen = false;
            m_commandPrompt.SetActive(false);
        }

        private IEnumerator EKeyboardInput()
        {
        
        
            yield return null;
        }

        public void DisplayingMainMenu()
        {
            //Displaying different commands depending on the user
            if (m_isDanger)
            {
                //Opening special commands
                m_commandText.text = m_commandFeatures[1].text;
                consoleDisplay = ConsoleDisplay.SpecialMenu;
            }
            else
            {
                //Opening normal commands
                m_commandText.text = m_commandFeatures[0].text;
                consoleDisplay = ConsoleDisplay.defaultMenu;
            }


        }

        public int getEnumConsoleNum()
        {
            return ((int)consoleDisplay);
        }


        public void DisplayScreen()
        {
            m_commandText.text = m_commandFeatures[getEnumConsoleNum()].text;
        }

        public void SetConsoleScreen(ConsoleDisplay _displayConsole)
        {
            m_commandText.text = m_commandFeatures[(int)_displayConsole].text;
            consoleDisplay = _displayConsole;

        }

        public void AddCommmandLine(int i)
        {
            m_commandText.text += "\n\n";
            m_commandText.text += m_commandText.text = m_commandFeatures[i].text;
        }
    }

}
