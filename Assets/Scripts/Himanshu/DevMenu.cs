using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Ludiq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Himanshu
{
    public class DevMenu : MonoBehaviour
    {

        private GameObject[] m_enemies;
        private static float m_enemySpeed;
        private static float m_enemyTriggerDistance;

        [SerializeField] private List<Transform> m_loop1TeleportPoints; //Hand
        [SerializeField] private List<Transform> m_loop2TeleportPoints; //Face
        [SerializeField] private List<Transform> m_loop3TeleportPoints; //Mouth
        [SerializeField] private List<Transform> m_loop4TeleportPoints; //Gear

        private TMP_Text m_textBox;
        
        public static DevMenu Instance;
        
        private TMP_InputField m_inputField;
        public bool inputSubmit { get; set; }
        
        [SerializeField] private GameObject m_player;
        private float m_teleport;
        private bool m_isMenuOpen;
        
        private List<int> m_selection;

        public bool isMenuOpen
        {
            get => m_isMenuOpen;
            set
            {
                m_isMenuOpen = value;
                transform.GetChild(0).gameObject.SetActive(value);
                transform.GetChild(1).gameObject.SetActive(value);
                transform.GetChild(2).gameObject.SetActive(value);
                if (value)
                    StartCoroutine(MainMenu());
            }
        }

        private List<string> m_menuOptions;

        public float teleport
        {
            get => m_teleport;
            set
            {
                switch (value)
                {
                    case 1.0f:
                        m_player.transform.position = m_loop1TeleportPoints[0].position;
                        break;
                    case 1.1f:
                        m_player.transform.position = m_loop1TeleportPoints[1].position;
                        break;
                    case 1.2f:
                        m_player.transform.position = m_loop1TeleportPoints[2].position;
                        break;
                    case 1.3f:
                        m_player.transform.position = m_loop1TeleportPoints[3].position;
                        break;
                    case 1.4f:
                        m_player.transform.position = m_loop1TeleportPoints[4].position;
                        break;
                    case 2.0f:
                        m_player.transform.position = m_loop2TeleportPoints[0].position;
                        break;
                    case 2.1f:
                        m_player.transform.position = m_loop2TeleportPoints[1].position;
                        break;
                    case 2.2f:
                        m_player.transform.position = m_loop2TeleportPoints[2].position;
                        break;
                    case 2.3f:
                        m_player.transform.position = m_loop2TeleportPoints[3].position;
                        break;
                    case 2.4f:
                        m_player.transform.position = m_loop2TeleportPoints[4].position;
                        break;
                    case 3.0f:
                        m_player.transform.position = m_loop3TeleportPoints[0].position;
                        break;
                    case 3.1f:
                        m_player.transform.position = m_loop3TeleportPoints[1].position;
                        break;
                    case 3.2f:
                        m_player.transform.position = m_loop3TeleportPoints[2].position;
                        break;
                    case 3.3f:
                        m_player.transform.position = m_loop3TeleportPoints[3].position;
                        break;
                    case 3.4f:
                        m_player.transform.position = m_loop3TeleportPoints[4].position;
                        break;
                    case 4.0f:
                        m_player.transform.position = m_loop4TeleportPoints[0].position;
                        break;
                    case 4.1f:
                        m_player.transform.position = m_loop4TeleportPoints[1].position;
                        break;
                    case 4.2f:
                        m_player.transform.position = m_loop4TeleportPoints[2].position;
                        break;
                    case 4.3f:
                        m_player.transform.position = m_loop4TeleportPoints[3].position;
                        break;
                    case 4.4f:
                        m_player.transform.position = m_loop4TeleportPoints[4].position;
                        break;
                }
                m_teleport = value;
            }
        }

        private static float enemySpeed
        {
            get => m_enemySpeed;
            set
            {
                m_enemySpeed = value;
                
                Instance.m_enemies.All((t) =>
                {
                    t.GetComponent<NavMeshAgent>().speed = value;
                    return true;
                });
            }
        }

        public static float enemyTriggerDistance
        {
            get => m_enemyTriggerDistance;
            set
            {
                m_enemyTriggerDistance = value;
                Instance.m_enemies.All((t) =>
                {
                    t.GetComponent<EnemyController>().m_hearingRadius = value / 3f;
                    return true;
                });
            }
        }
        


        void Start()
        {
            Instance = this;
            m_enemies = FindObjectsOfType<EnemyController>(true).Select((t)=>t.gameObject).ToArray();
            m_menuOptions = new List<string>();
            m_menuOptions.Add("Press 1 to change enemy speed.\n" +
                                    "Press 2 to change enemy trigger Distance.\n" +
                                    "Press 3 to teleport");
            m_menuOptions.Add("Press 1 for Hand Loop\n" +
                                   "Press 2 for Face Loop\n" +
                                   "Press 3 for Mouth Loop\n" +
                                   "Press 4 for Gear Loop\n" +
                                   "Press 5 for Main Menu\n");
            m_menuOptions.Add("Press 1 for room 1\n" +
                                   "Press 2 for room 2\n" +
                                   "Press 3 for room 3\n" +
                                   "Press 4 for room 4\n" + 
                                   "Press 5 for Main Menu\n");
            
            m_textBox = transform.GetChild(1).GetComponent<TMP_Text>();
            m_textBox.text = m_menuOptions[0];
            m_inputField = transform.GetChild(2).GetComponent<TMP_InputField>();
        }

        IEnumerator MainMenu()
        {
            m_textBox.text = m_menuOptions[0];
            while (true)
            {
                
                if(!isMenuOpen)
                    yield break;
                // if (Input.GetKeyDown(KeyCode.Alpha1))
                // {
                //     StartCoroutine(EnemyTweaker());
                //     yield break;
                // }
                //
                // if (Input.GetKeyDown(KeyCode.Alpha2))
                // {
                //     StartCoroutine(EnemyTweaker(true));
                //     yield break;
                // }
                //
                // if (Input.GetKeyDown(KeyCode.Alpha3))
                // {
                //     StartCoroutine(TeleportMenu());
                //     yield break;
                // }
                //
                // yield return null;
            }
        }

        private IEnumerator TeleportMenu()
        {
            string output = "";
            m_textBox.text = m_menuOptions[1];
            while (true)
            {
                if (!isMenuOpen)
                    yield break;

                yield return new WaitUntil(() => !Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha2) &&
                                                 !Input.GetKey(KeyCode.Alpha3) && !Input.GetKey(KeyCode.Alpha4));

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    output += "1.";
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    output += "2.";
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    output += "3.";
                    break;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    output += "4.";
                    break;
                }
               
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    StartCoroutine(MainMenu());
                    yield break;
                }
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    StartCoroutine(MainMenu());
                    yield break;
                }

                yield return null;
            }

            yield return new WaitUntil(() =>
            {
                return !Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha2) &&
                       !Input.GetKey(KeyCode.Alpha3) && !Input.GetKey(KeyCode.Alpha4);
            });

            m_textBox.text = m_menuOptions[2];
            
            while (true)
            {
                if (!isMenuOpen)
                    yield break;
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    output += "1";
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    output += "2";
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    output += "3";
                    break;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    output += "4";
                    break;
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    StartCoroutine(MainMenu());
                    yield break;
                    
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    StartCoroutine(TeleportMenu());
                    yield break;
                }

                yield return null;
            }

            teleport = float.Parse(output);

            
            yield return new WaitUntil(() =>
            {
                return !Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha2) &&
                       !Input.GetKey(KeyCode.Alpha3) && !Input.GetKey(KeyCode.Alpha4);
            });
            
            StartCoroutine(MainMenu());
            yield break;
            
            yield return null;
        }

        private IEnumerator EnemyTriggerChanger()
        {
            yield return null;
        }

        public  static void EnemyTweaker(bool _trigger = false, float _value = 0.0f)
        {

            if (!_trigger)
            {
                if (_value > 15f)
                {
                    _value = 15f;
                }
                else if (_value < 0.1f)
                {
                    _value = 0.1f;
                }

                enemySpeed = _value;
            }
            else
            {
                if (_value > 30f)
                {
                    _value = 30f;
                }
                else if (_value < 0.1f)
                {
                    _value = 0.1f;
                }

                enemyTriggerDistance = _value;
            }

        }

        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.V))
            //     isMenuOpen = !isMenuOpen;
        }
    }
}
