using System.Collections;
using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

namespace rachael.FavorSystem
{
    public enum ConsoleDisplay
    {
        defaultMenu = 0,
        SpecialMenu = 1,
        talkMenu = 2,
        timeMenu = 3,
        userMenu = 4,
        quitMenu = 5,
        helpMenu = 6,
    }

    public class FavorSystem : MonoBehaviour
    {
        private bool m_isOpen = false;

        public bool m_timeStop = false;

        public static bool m_grantSpecial = false; 

        private PlayerInteract m_playerInteract;


        public bool isDanger
        {
            get
            {
                if (m_playerInteract)
                {
                    m_isDanger = (m_playerInteract.playerDanger == EnemyController.eDanger.red ||
                                 m_playerInteract.playerDanger == EnemyController.eDanger.yellow) && 
                                 (m_grantSpecial);
                }

                m_gameCommandPrompt.HelpActive(m_isDanger);
                return m_isDanger;
            }
        }


        [FormerlySerializedAs("commandPrompt")] public GameObject m_commandPrompt;

        [FormerlySerializedAs("commandFeatures")] public Text[] m_commandFeatures;
        [FormerlySerializedAs("commandText")] public Text m_commandText;

        [SerializeField] private TMP_InputField m_inputField;

        [FormerlySerializedAs("defaultValue")] public float m_defaultPoints;
        [FormerlySerializedAs("favorPoint")] public float m_favorPoints;
        [FormerlySerializedAs("resultant")] public float m_result;

        private GameCommandPrompt m_gameCommandPrompt;
        

        public ConsoleDisplay consoleDisplay;
        private bool m_isDanger;

        private float m_waitTimer;
        public static bool startTimer = false;



        public GameObject CommandIcon;
        private Animator m_notifAnimator;
        private bool aNotifEnabled {
            get => m_notifAnimator.GetBool("IsEnabled");
            set
            {
                if(m_notifAnimator.GetBool("IsEnabled") != value)
                    m_notifAnimator.SetBool("IsEnabled", value);
            }
        }

        public Image NotifIcon;

        // Start is called before the first frame update
        void Start()
        {
            m_gameCommandPrompt = m_commandPrompt.GetComponent<GameCommandPrompt>();
            m_playerInteract = FindObjectOfType<PlayerInteract>();
            m_waitTimer = 0.0f;
            m_notifAnimator = CommandIcon.GetComponent<Animator>();

            if (!m_isOpen)
            {
                m_commandPrompt.SetActive(false);
                //Debug.Log(m_isDanger);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(!CommandIcon.activeSelf && (gameManager.Instance.m_objTutorialPlayed ?? false))
            {
                CommandIcon.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.C) && !m_isOpen && (gameManager.Instance.m_objTutorialPlayed ?? false))
            {
                m_inputField.text = "";
                CommandPromptWindow();
            }
            aNotifEnabled = (m_playerInteract?.playerDanger != EnemyController.eDanger.white && m_grantSpecial);


            //starting timer for how long will  the objective be completed
            if (startTimer)
                m_waitTimer += Time.deltaTime;
            else if(!startTimer && m_waitTimer != 0.0f)
            {
                ConvertingToFavorPoints();
                m_waitTimer = 0.0f;
            }

            // if (Input.GetKeyDown(KeyCode.Alpha5) && !m_isOpen)
            // {
            //     isDanger = !isDanger;
            // }
        }

        void ConvertingToFavorPoints()
        {
            Debug.Log(m_waitTimer);
            Debug.Log(Time.deltaTime);
            if(m_waitTimer <= 10 )
            {
                m_favorPoints = 0.0f;
            }
            else if (m_waitTimer <= 15)
            {
                m_favorPoints = 0.25f;
            }
            else
            {
                m_favorPoints = 0.5f;
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
            if (isDanger)
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

        public void ResetTime()
        {
            
            IEnumerator Reset()
            {

                m_timeStop = true;
                float counter = 0f;
                while (counter < 5f)
                {
                    counter += Time.unscaledDeltaTime;
                    yield return null;
                }

                Time.timeScale = 1f;
                m_timeStop = false;

            }

            StartCoroutine(Reset());
        }
    }

}
