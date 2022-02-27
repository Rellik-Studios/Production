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

    public string here;

    // Start is called before the first frame update
    void Start()
    {
        //m_defaultPosition = m_caretImage.transform.position.x;
    }

    private void OnEnable()
    {
        m_inputField.ActivateInputField();
        m_inputField.Select();
    }
    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputCommand();

        //m_caretImage.transform.position = new Vector2(m_defaultPosition + m_inputField.caretPosition * 60f, 0f);
    }

    void PlayerInputCommand()
    {
        //if (!favorSystem.m_isDanger)
        //{
        //    DefaultCommandList();
        //}
        //else
        //{
        //    SpecialCommandList();
        //}
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
            FavorCommand();
        }
        else 
        {
            switch (input)
            {
                case"TALK":
                    {
                        TalkCommand();
                        break;
                    }
                case"TIME":
                    {
                        TimeCommand();
                        break;
                    }
                case"USER":
                    {
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
                        Debug.Log("INVALID INPUT! TRY AGAIN");
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
        Debug.Log("Talk!");
    }

    void TimeCommand()
    {
        Debug.Log("Time!");
    }

    void UserCommand()
    {
        Debug.Log("User!");
    }

    void QuitCommand()
    {
        Debug.Log("Quit!");
    }

    public void playerEnterCommand(string textInput)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_inputField.ActivateInputField();
            m_inputField.Select();

            CheckInput(textInput.ToUpper());
            m_inputField.text = "";
        }
    }
}
