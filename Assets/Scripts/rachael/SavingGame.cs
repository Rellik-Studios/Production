using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
     public class SavingGame : MonoBehaviour
     {}
    // {
    //     [FormerlySerializedAs("eraChanging")] public ChangeFurniture m_eraChanging;
    //     [FormerlySerializedAs("respawnManager")] public RespawnManager m_respawnManager;
    //     [FormerlySerializedAs("player")] public PlayerInteract m_player;
    //
    //     [FormerlySerializedAs("testing_Mode")] [SerializeField] bool m_testingMode = false;
    //
    //     // Start is called before the first frame update
    //     void Start()
    //     {
    //         m_player = GetComponent<PlayerInteract>();
    //         //for the saving purposes---------------
    //
    //         if (!m_testingMode)
    //         {
    //             if (!PlayerPrefs.HasKey("Player_X"))
    //             {
    //                 PlayerPrefs.SetFloat("Player_X", gameObject.transform.position.x);
    //                 PlayerPrefs.SetFloat("Player_Y", gameObject.transform.position.y);
    //                 PlayerPrefs.SetFloat("Player_Z", gameObject.transform.position.z);
    //
    //
    //                 PlayerPrefs.SetInt("pieces", m_player.m_numOfPieces);
    //
    //                 PlayerPrefs.SetInt("Index", m_eraChanging.index);
    //
    //                 //PlayerPrefs.SetInt("NumLine", 0);
    //
    //                 //PlayerPrefs.SetString("TimeEra", "Place");
    //
    //                 PlayerPrefs.SetInt("Death", m_player.m_deathCount);
    //
    //                 //PlayerPrefs.Save();
    //
    //             }
    //             else
    //             {
    //                 LoadPoint();
    //
    //                 //if(GetComponent<SavingGame>() != null)
    //                 //{
    //                 //    GetComponent<Player>().LoadPlayer();
    //                 //}
    //             }
    //         }
    //     }
    //
    //     // Update is called once per frame
    //     void Update()
    //     {
    //         // if(Input.GetKeyDown(KeyCode.Z))
    //         // {
    //         //     SavePoint();
    //         // }
    //         // if (Input.GetKeyDown(KeyCode.X))
    //         // {
    //         //     SceneManager.LoadScene(0);
    //         //     Cursor.lockState = CursorLockMode.None;
    //         // }
    //         // if(Input.GetKeyDown(KeyCode.Q))
    //         // {
    //         //
    //         //     PlayerPrefs.DeleteAll();
    //         // }
    //     }
    //
    //     public void SavePoint()
    //     {
    //         PlayerPrefs.SetFloat("Player_X", gameObject.transform.position.x);
    //         PlayerPrefs.SetFloat("Player_Y", gameObject.transform.position.y);
    //         PlayerPrefs.SetFloat("Player_Z", gameObject.transform.position.z);
    //
    //
    //         PlayerPrefs.SetInt("pieces", m_player.m_numOfPieces);
    //
    //         PlayerPrefs.SetInt("Index", m_eraChanging.index);
    //
    //
    //         PlayerPrefs.SetInt("Death", m_player.m_deathCount);
    //     }
    //     void LoadPoint()
    //     {
    //         Cursor.lockState = CursorLockMode.Locked;
    //         GetComponent<CharacterController>().enabled = false;
    //
    //         gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_X"), PlayerPrefs.GetFloat("Player_Y"), PlayerPrefs.GetFloat("Player_Z"));
    //
    //         m_player.m_numOfPieces = PlayerPrefs.GetInt("pieces");
    //
    //         m_eraChanging.SavingTimeEra();
    //
    //         m_player.m_deathCount = PlayerPrefs.GetInt("Death");
    //
    //
    //         GetComponent<RespawnManager>().SetPosition(gameObject.transform);
    //         GetComponent<RespawnManager>().Respawn();
    //     
    //
    //         GetComponent<CharacterController>().enabled = true;
    //     }
    // }
}
