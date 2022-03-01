using System.Collections;
using System.Collections.Generic;
using rachael.FavorSystem;
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
        Debug.Log("help!");
    }

    void TalkCommand()
    {
        favorSystem.DisplayScreen();
        Debug.Log("Talk!");
    }

    void TimeCommand()
    {
        favorSystem.DisplayScreen();
        Debug.Log("Time!");
    }

    void UserCommand()
    {
        favorSystem.DisplayScreen();
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

    private IEnumerator ShutDownCommandProcess()
    {
        yield return new WaitForSecondsRealtime(3);
        m_inputField.enabled = true;
        favorSystem.CloseCommandPrompt();
       

        yield return null;
    }

    public void playerEnterCommand(string textInput)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectInputField();
            CheckInput(textInput.ToUpper());
            m_inputField.text = "";
        }
    }
}
