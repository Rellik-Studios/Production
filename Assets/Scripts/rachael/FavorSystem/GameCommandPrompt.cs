using System;
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

    bool changingName = false;


    private Dictionary<string, Func<bool>> m_commands;

    // Start is called before the first frame update
    void Start()
    {
        var comparer = StringComparer.OrdinalIgnoreCase;
        m_commands = new Dictionary<string, Func<bool>>(comparer)
        {
            {"HELP", FavorCommand},
            {"TALK", TalkCommand},
            {"TIME", TimeCommand},
            {"USER", UserCommand},
            {"QUIT", QuitCommand}
        };



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
        counter = 0;
        changingName = false;
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


    void  CheckInput(string input)
    { 

        // if (input == "HELP" && favorSystem.m_isDanger)
        // {
        //     m_failedAttempts = 0;
        //     FavorCommand();
        // }
        // else 
        // {
        //     switch (input)
        //     {
        //         case"TALK":
        //             {
        //                 m_failedAttempts = 0;
        //                 TalkCommand();
        //                 break;
        //             }
        //         case"TIME":
        //             {
        //                 m_failedAttempts = 0;
        //                 TimeCommand();
        //                 break;
        //             }
        //         case"USER":
        //             {
        //                 m_failedAttempts = 0;
        //                 UserCommand();
        //                 break;
        //             }
        //         case"QUIT":
        //             {
        //                 QuitCommand();
        //                 break;
        //             }
        //         default:
        //             {
        //                 InvalidInput();
        //                 break;
        //             }
        //     }
        //
        // }
        
        if(m_commands.TryGetValue(input, out Func<bool> _value))
        {
            _value.Invoke();
        }
        else
        {
            InvalidInput();
        }

    }

    
    //public bool ConsistsOfWhiteSpace(string s)
    //{
    //    foreach (char c in s)
    //    {
    //        if (c != ' ') return false;
    //    }
    //    return true;
    //}

    void InputNewUsername(string answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            InvalidInput();
            Debug.Log("yes it does contain white space");
        }
        else
        {
            favorSystem.m_commandText.text = "Old Username:\n" + NarratorScript.UserName + "\n\nNew Username:\n" + answer;
            Debug.Log("yes it does not contain white space");
            NarratorScript.UserName = answer;
            changingName = false;
            StartCoroutine(ReturnToMenuCommandProcess());

        }
    }
#region Commands

    bool FavorCommand()
    {
        favorSystem.m_commandText.text = "PROCESSING";
        favorSystem.m_commandText.resizeTextForBestFit = false;
        m_inputField.enabled = false;
        StartCoroutine(HelpCommandProcess());
        Debug.Log("help!");
        return true;
    }

    bool TalkCommand()
    {
        favorSystem.m_commandText.text = "Hello world\n\nPress any key to continue";
        favorSystem.consoleDisplay = ConsoleDisplay.talkMenu;
        //favorSystem.DisplayScreen();
        Debug.Log("Talk!");
        return true;
    }

    bool TimeCommand()
    {
        favorSystem.m_commandText.text = "the current time is " + NarratorScript.Time +  "\n\nPress any key to continue";
        favorSystem.consoleDisplay = ConsoleDisplay.timeMenu;
        //favorSystem.DisplayScreen();
        Debug.Log("Time!");
        return true;
    }

    bool UserCommand()
    {
        favorSystem.m_commandText.text = "Your username is " + NarratorScript.UserName + "\n\nWould you like to change your name?\n[YES/NO]";
        favorSystem.consoleDisplay = ConsoleDisplay.userMenu;
        favorSystem.m_commandFeatures[favorSystem.getEnumConsoleNum()].text = favorSystem.m_commandText.text;
        //favorSystem.DisplayScreen();
        Debug.Log("User!");
        return true;
    }

    bool QuitCommand()
    {
        favorSystem.CloseCommandPrompt();
        Debug.Log("Quit!");
        return true;
    }

    #endregion
    

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
                    if (changingName)
                    {
                        selectInputField();
                        InputNewUsername(textInput);

                    }
                    else
                    {
                        selectInputField();
                        //favorSystem.DisplayingMainMenu();
                        //AskToChangeName(textInput.ToUpper());
                    }
                    m_inputField.text = "";
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
    private IEnumerator ReturnToMenuCommandProcess()
    {
        m_inputField.enabled = false;
        yield return new WaitForSecondsRealtime(3);
        m_inputField.enabled = true;
        favorSystem.DisplayingMainMenu();


        yield return null;
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

        favorSystem.isDanger = false;
        favorSystem.CloseCommandPrompt();


        yield return null;
    }

    public void HelpActive(bool _value)
    {
        if (_value)
        {
            if (m_commands.ContainsKey("HELP"))
                return;
            m_commands.Add("Help", FavorCommand);
        }
        else
        {
            if (!m_commands.ContainsKey("HELP"))
                return;
            m_commands.Remove("Help");
        }
    }
}
