using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace rachael
{
    public class SceneChanger : MonoBehaviour
    {
        //public GameObject m_player;
        [FormerlySerializedAs("ContinueBlock")] public GameObject m_continueBlock;
        // Start is called before the first frame update
        void Start()
        {
        
            if(m_continueBlock != null)
            {
                ButtonPresent();

            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ButtonPresent()
        {
            if (Directory.Exists(Application.persistentDataPath + "/player/"))
            {
                m_continueBlock.SetActive(true);
            }
            else
            {
                m_continueBlock.SetActive(false);
            }
        }
        public void MainScene()
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }
        public void Quit()
        {
            Application.Quit();
        }
        public void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("Death", 0);
        }
        public void Play(bool _newGame)
        {
            if (_newGame)
            {
                if(Directory.Exists(Application.persistentDataPath + "/player/"))
                    SaveSystem.SaveSystem.DeletePlayer();
                if(Directory.Exists(Application.persistentDataPath + "/narrator/"))
                    SaveSystem.SaveSystem.DeleteNarrator();
            }
            SceneManager.LoadScene(1);
        }
        public void Ending()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(2);
        }
        public void DeleteFile()
        {

            if (Directory.Exists(Application.persistentDataPath + "/player/"))
                SaveSystem.SaveSystem.DeletePlayer();
            if (Directory.Exists(Application.persistentDataPath + "/narrator/"))
                SaveSystem.SaveSystem.DeleteNarrator();

            ButtonPresent();
            
        }
        //public void Continue()
        //{
        //    //m_player.SetActive(true);
        //    //MIGHT NEED TO REDO
        //    Cursor.lockState = CursorLockMode.Locked;
        //    if (m_player.GetComponent<RespawnManager>() != null)
        //        m_player.GetComponent<RespawnManager>().Respawn();
        //    m_player.GetComponent<PlayerInteract>().m_timeRewind.fillAmount = 0;
        //    m_player.GetComponent<PlayerInteract>().m_timeStop.fillAmount = 0;
        //    //m_player.GetComponent<PlayerInteract>().m_hiding = true;
        //}

        public void LoseScreen()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);

        }
        private void OnTriggerEnter(Collider _other)
        {
            if (_other.CompareTag("Player"))
            {
                Ending();
            }
        
        }

        public void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
