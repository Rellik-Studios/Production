using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using rachael.FavorSystem;
using rachael;
using Himanshu;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameCommandPrompt : MonoBehaviour
{
    [SerializeField] FavorSystem favorSystem;

    [SerializeField] private TMP_InputField m_inputField;
    //[SerializeField] private GameObject m_caretImage;
    private float m_defaultPosition;

    private int m_failedAttempts = 0;

    int counter = 0;

    bool changingName = false;

    bool enableTyping = true;

    string favorName;


    private Dictionary<string, Func<bool>> m_commands;
    private bool m_timeStop;

    // Start is called before the first frame update
    private void Awake()
    {
        var comparer = StringComparer.OrdinalIgnoreCase;

        m_commands = new Dictionary<string, Func<bool>>(comparer)
        {
            //{"HELP", FavorCommand},
            {"TALK", TalkCommand},
            {"TIME", TimeCommand},
            {"USER", UserCommand},
            {"QUIT", QuitCommand}
        };
    }

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
        counter = 0;
        changingName = false;
        m_inputField.enabled = true;
        enableTyping = true;
        favorName = "";
    }
    private void OnDisable()
    {
        Debug.Log("closing");
        if (!timeStop)
            Time.timeScale = 1f;
        else
        {
            favorSystem.ResetTime();
            //this.Invoke(()=>timeStop = false, 5, true);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Random.Range(1, 3));
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
    public void DisableTypingForPlayer()
    {
        if(!enableTyping && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Return))
        {
            m_inputField.text = "";
            enableTyping = true;
            selectInputField();
            favorSystem.DisplayingMainMenu();
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
            m_failedAttempts = 0;
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
        enableTyping = false;
        //favorSystem.DisplayScreen();
        Debug.Log("Talk!");
        return true;
    }

    bool TimeCommand()
    {
        favorSystem.m_commandText.text = "the current time is " + NarratorScript.Time +  "\n\nPress any key to continue";
        favorSystem.consoleDisplay = ConsoleDisplay.timeMenu;
        enableTyping = false;
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

    bool DoesTimeGrantFavor()
    {
        int x = Random.Range(0, 2);
        favorSystem.m_defaultPoints = x / 2.0f;
        favorSystem.m_result = favorSystem.m_defaultPoints + favorSystem.m_favorPoints;
        Debug.Log(x);
        Debug.Log(favorSystem.m_defaultPoints);
        Debug.Log(favorSystem.m_favorPoints);
        Debug.Log(favorSystem.m_result);

        //return true;
        return favorSystem.m_result >= 0.5f;
    }

    string GrantTypeofFavor()
    {
        int x = Random.Range(1, 3);
        string[] ListOfAbilities = new string[] { "Teleport", "Rewind", "Stop" };

        //return "Stop";
        return ListOfAbilities[x - 1];
    }

    void FavorDecision()
    {
        if (DoesTimeGrantFavor())
        {
            favorName = GrantTypeofFavor();
            favorSystem.m_commandText.text += " Favor Accepted.";
            favorSystem.m_commandText.text += "\nGrant " + NarratorScript.UserName + " the ability of " + favorName;
            favorSystem.m_commandText.text += "\n\nCommence shut down process.";

        }
        else
        {
            favorName = "";
            favorSystem.m_commandText.text += " Favor Denied.\n\nCommence shut down process";
        }
    }

    /// <summary>
    /// Grand Rewind favor
    /// NOTE: needs the rewind reference aka teleport and restarting from the last checkpoint
    /// </summary>
    void GrantTeleport()
    {
        gameManager.Instance.m_isSafeRoom = true;
        SceneManager.LoadScene("Path_face 3");
        Debug.Log("Grant Teleport Time");
    }

    void GrantRewind()
    {
        FindObjectsOfType<TimeBody>().All((t) =>
        {
            t.isRewinding = true;
            return true;
        });
        Debug.Log("Grant Rewind Time");
    }

    void GrantStop()
    {
        Time.timeScale = 0;
        timeStop = true;
        Debug.Log("Grant Stop Time");
    }

    public bool timeStop
    {
        get => m_timeStop;
        set
        {
            m_timeStop = value;
            //Time.timeScale = value ? 0f : 1f;
        }
    }

    void AskToChangeName(string answer)
    {
        if(answer == "YES" || answer == "Y")
        {
            Debug.Log("it works");
            favorSystem.m_commandText.text = "Current Username:\n" + NarratorScript.UserName + "\n\nInput New Username";
            changingName = true;
            favorSystem.m_commandFeatures[favorSystem.getEnumConsoleNum()].text = favorSystem.m_commandText.text;
        }
        else if(answer == "NO" || answer == "N")
        {
            favorSystem.m_commandText.text = "You did not change your username\n" + NarratorScript.UserName;

            m_inputField.enabled = false;
            changingName = false;
            StartCoroutine(ReturnToMenuCommandProcess());
        }
        else
        {
            InvalidInput();
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
                        AskToChangeName(textInput.ToUpper());
                    }
                    m_inputField.text = "";
                    Debug.Log("CANAPLE");
                }
                break;
            case ConsoleDisplay.talkMenu:
                {
                    enableTyping = true;
                    selectInputField();
                    favorSystem.DisplayingMainMenu();
                    Debug.Log("CANAPLE");
                }
                break;
            case ConsoleDisplay.timeMenu:
                {
                    enableTyping = true;
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
        m_failedAttempts = 0;
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
        
        favorSystem.m_commandText.text = "Process done!";
        
        counter = 0;
        FavorDecision();
        yield return new WaitForSecondsRealtime(3);

        if (favorName != null)
        {
            if(favorName == "Teleport")
            {
                GrantTeleport();
            }
            else if(favorName == "Rewind")
            {
                GrantRewind();
            }
            else
            {
                GrantStop();
            }
        }

        m_inputField.enabled = true;
        favorSystem.m_commandText.resizeTextForBestFit = true;
        //favorSystem.isDanger = false;
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
