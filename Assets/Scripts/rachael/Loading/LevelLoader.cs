using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator LoadTransition;

    // Start is called before the first frame update
    void Start()
    {
        LoadTransition = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void LoadLevel(int sceneIndex)
    {
        LoadTransition.SetTrigger("Appear");
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    
        while(!operation.isDone)
        {
            // Debug.Log(operation.progress);
            yield return null;
        }
    
    }
}
