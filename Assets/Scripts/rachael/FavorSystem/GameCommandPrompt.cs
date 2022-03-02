using System.Collections;
using System.Collections.Generic;
using rachael.FavorSystem;
using rachael;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCommandPrompt : MonoBehaviour
{
    [SerializeField] FavorSystem favorSystem;

    [SerializeField] private TMP_InputField m_inputField;
    //[SerializeField] private GameObject m_caretImage;
    private float m_defaultPosition;

    private int m_failedAttempts = 0;

    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        //m_defaultPosition = m_caretImage.transform.position.x;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        selectInputField();
        Debug.Log("opening");
        m_failedAttempts = 0;
    }
    private void OnDisable()
    {
        Debug.Log("closing");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void DefaultCommandList()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Talk");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Time");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("User");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Quit");
        }
    }

    void SpecialCommandList()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Talk");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Time");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("User");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Help");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Quit");
        }
    }


    void CheckInput(string input)
    { 

        if (input == "HELP" && favorSystem.m_isDanger)
        {
            m_failedAttempts = 0;
            FavorCommand();
        }
        else 
        {
            switch (input)
            {
                case"TALK":
                    {
                        m_failedAttempts = 0;
                        TalkCommand();
                        break;
                    }
                case"TIME":
                    {
                        m_failedAttempts = 0;
                        TimeCommand();
                        break;
                    }
                case"USER":
                    {
                        m_failedAttempts = 0;
                        UserCommand();
                        break;
                    }
                case"QUIT":
                    {
                        QuitCommand();
                        break;
                    }
                default:
                    {
                        InvalidInput();
                        break;
                    }
            }

        }

    }

    void FavorCommand()
    {
        favorSystem.m_commandText.text = "PROCESSING";
        favorSystem.m_commandText.resizeTextForBestFit = false;
        m_inputField.enabled = false;
        StartCoroutine(HelpCommandProcess());
        Debug.Log("help!");
    }

    void TalkCommand()
    {
        favorSystem.m_commandText.text = "Hello world\n\nPress any key to continue";
        favorSystem.consoleDisplay = ConsoleDisplay.talkMenu;
        //favorSystem.DisplayScreen();
        Debug.Log("Talk!");
    }

    void TimeCommand()
    {
        string here = NarratorScript.Time;
        favorSystem.m_commandText.text = "the current time is " + NarratorScript.Time +  "\n\nPress any key to continue";
        favorSystem.consoleDisplay = ConsoleDisplay.timeMenu;
        //favorSystem.DisplayScreen();
        Debug.Log("Time!");
        Debug.Log(here);
    }

    void UserCommand()
    {
        favorSystem.m_commandText.text = "Your username is " + NarratorScript.UserName + "\n\nPress any key to continue";
        favorSystem.consoleDisplay = ConsoleDisplay.userMenu;
        //favorSystem.DisplayScreen();
        Debug.Log("User!");
    }

    void QuitCommand()
    {
        favorSystem.CloseCommandPrompt();
        Debug.Log("Quit!");
    }

    void InvalidInput()
    {
        Debug.Log("INVALID INPUT! TRY AGAIN");

        m_failedAttempts++;


        if (m_failedAttempts > 3)
        {
            favorSystem.m_commandText.text = "TOO MANY FAILED ATTEMPTS\n\n SHUTTING DOWN COMMAND PROMPT";
            Debug.Log("TOO MANY FAILED ATTEMPTS, COMMENCE COMMAND PROMPT CLOSE DOWN");
            m_inputField.enabled = false;
            StartCoroutine(ShutDownCommandProcess());
            
        }
        else
        {
            favorSystem.m_commandText.text = (m_failedAttempts != 1 ? ("[" + m_failedAttempts + "] ") : "") + "Error: INVALID INPUT! TRY AGAIN";
            favorSystem.AddCommmandLine(favorSystem.getEnumConsoleNum());
        }

      
    }

    private void selectInputField()
    {
        m_inputField.ActivateInputField();
        m_inputField.Select();
    }

    public void playerEnterCommand(string textInput)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            CheckMenuForInput(textInput);
            //selectInputField();
            //CheckInput(textInput.ToUpper());
            //m_inputField.text = "";


        }
    }
    

    public void CheckMenuForInput(string textInput)
    {
        switch (favorSystem.consoleDisplay)
        {
            case ConsoleDisplay.defaultMenu:
                {
                    selectInputField();
                    CheckInput(textInput.ToUpper());
                    m_inputField.text = "";
                    Debug.Log("APPLE");
                }
                break;
            case ConsoleDisplay.SpecialMenu:
                {
                    selectInputField();
                    CheckInput(textInput.ToUpper());
                    m_inputField.text = "";
                    Debug.Log("BANANA");
                }
                break;
            case ConsoleDisplay.userMenu:
                {
                    selectInputField();
                    favorSystem.DisplayingMainMenu();
                    Debug.Log("CANAPLE");
                }
                break;
            case ConsoleDisplay.talkMenu:
                {
                    selectInputField();
                    favorSystem.DisplayingMainMenu();
                    Debug.Log("CANAPLE");
                }
                break;
            case ConsoleDisplay.timeMenu:
                {
                    selectInputField();
                    favorSystem.DisplayingMainMenu();
                    Debug.Log("CANAPLE");
                }
                break;
            default:
                Debug.Log("NOTHING");
                CheckInput(textInput.ToUpper());
                break;
        }
    }


    private IEnumerator ShutDownCommandProcess()
    {
        yield return new WaitForSecondsRealtime(3);
        m_inputField.enabled = true;
        favorSystem.CloseCommandPrompt();


        yield return null;
    }


    private IEnumerator HelpCommandProcess()
    {
        
        while (counter < 30)
        {
            favorSystem.m_commandText.text += ".";
            counter++;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        favorSystem.m_commandText.resizeTextForBestFit = true;
        favorSystem.m_commandText.text = "Process done goodbye";
        counter = 0;
        yield return new WaitForSecondsRealtime(3);
        m_inputField.enabled = true;

        favorSystem.m_isDanger = false;
        favorSystem.CloseCommandPrompt();


        yield return null;
    }
}
