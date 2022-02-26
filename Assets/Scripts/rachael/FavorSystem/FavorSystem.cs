using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavorSystem : MonoBehaviour
{
    private bool isOpen = false;
    public bool isDanger = false;
    public GameObject commandPrompt;

    public Text[] commandFeatures;
    public Text commandText;

    // Start is called before the first frame update
    void Start()
    {
        if (!isOpen)
        {
            commandPrompt.SetActive(false);
            Debug.Log(isDanger);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            OpenCommandPromptWindow();
    }

    void OpenCommandPromptWindow()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            Debug.Log("Command Prompt is open");
            commandPrompt.SetActive(true);
            DisplayingCommands();


        }
        else
        {
            Debug.Log("Command Prompt is close");
            commandPrompt.SetActive(false);

        }
    }

    public void DisplayingCommands()
    {
        //Displaying different commands depending on the user

        if (isDanger)
        {
            //Opening special commands
            commandText.text = commandFeatures[1].text;
        }
        else
        {
            //Opening normal commands
            commandText.text = commandFeatures[0].text;
        }


    }


    public void AddCommmandLine(int i)
    {
        commandText.text += "\n\n";
        commandText.text += commandText.text = commandFeatures[i].text;
    }
}
