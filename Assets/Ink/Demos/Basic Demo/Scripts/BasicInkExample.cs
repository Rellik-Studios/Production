using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Ink.Runtime;
using rachael;

public class BasicInkExample : MonoBehaviour 
{
    public static event Action<Story> OnCreateStory;
    
    [SerializeField] private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField] private Canvas canvas = null;

    [SerializeField] TMP_Text narratorText;


    private void Awake()
    {
	    story = new Story (inkJSONAsset.text);
	    story.EvaluateFunction($"SetUserName({NarratorScript.UserName})");
	    story.EvaluateFunction($"SetDeviceName({NarratorScript.DeviceName})");
    }

    void StartStory ()
    {
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	void RefreshView () 
	{
		
		while (story.canContinue) {
			string text = story.Continue ();
			text = text.Trim();
			CreateContentView(text);
		}
    }

	void CreateContentView (string text) 
	{
		narratorText.SetText(text);
	}



	public void Play(string _option)
	{
		story.EvaluateFunction("Start", _option);
		story.EvaluateFunction($"SetTime({NarratorScript.Time})");
		story.EvaluateFunction($"SetDate({NarratorScript.Date})");
	}
}
