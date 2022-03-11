using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bolt;
using rachael;
using UnityEngine;
using UnityEngine.Serialization;

namespace Himanshu
{
    public class Tutorial : MonoBehaviour
    {
        public bool m_tutorialOver
        {
            get => !gameManager.Instance.isTutorialRunning;
            set => gameManager.Instance.isTutorialRunning = !value;
        }

        [SerializeField] private GameObject[] m_walls;

        public static bool m_tutorialSkipped = false;
        private int m_deathCounter;

        private Color wallcolor
        {
            get => m_walls[0].GetComponent<Renderer>().material.color;
            set => m_walls.All((t) =>
            {
                t.GetComponent<Renderer>().material.color = value;
                return true;
            });
        }
    
        private Narrator m_narrator;
        [SerializeField] private List<GameObject> m_hidingSpots;
        
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_tutorialDialogues;
        
        [SerializeField] private PlayerInteract m_player;
        [FormerlySerializedAs("m_enemy")] [SerializeField] private GameObject m_enemyDefault;

        private GameObject m_enemy;
        [SerializeField] private List<Door> m_doors;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_deathDialogues;

        private Vector3 m_defaultPosition;
        private Quaternion m_defaultRotation;

        [TextArea(4, 6)] 
        public List<string> m_noticedDialogues;

        private GameObject m_hidingSpot;

        private void Start()
        {
            m_defaultRotation = m_player.transform.rotation;
            m_defaultPosition = m_player.transform.position;
            m_narrator = GetComponent<Narrator>();
            // RunTutorial();
            m_enemy = GameObject.Instantiate(m_enemyDefault, transform, true);
            m_player.m_enemies.Add(m_enemy.GetComponent<EnemyController>());
        }

        public void RunTutorial()
        {
            StartCoroutine(eTutorial());
        }
        
        private IEnumerator eTutorial(bool _retry = false)
        {
            wallcolor = Color.white;
            m_narrator = GetComponent<Narrator>();
            // m_narrator.enabled = false;
            int index = 0;
            m_tutorialOver = false;

            IEnumerator SkipCheck()
            {

                yield return new WaitUntil(() => m_doors.Any((t) => t.isDoorOpen));

                m_tutorialSkipped = true;
                m_doors.First((t) => t.isDoorOpen).doorOpen = false;
                m_player.transform.position = m_defaultPosition;
                m_player.m_followCam.transform.rotation = m_defaultRotation;
                m_player.m_followCam.ResetMouse();
                m_player.m_followCam.m_mouseInput = true;
                
                m_player.m_enemies.Remove(m_enemy.GetComponent<EnemyController>());
                Destroy(m_enemy);
                
                m_hidingSpot?.SetActive(false);
                
                m_player.Unhide();
                m_narrator.Play("Suit Yourself");
                m_tutorialOver = true;
                // m_narrator.enabled = true;
                wallcolor = Color.black;
                StopAllCoroutines();
                yield return new WaitWhile(() => m_narrator.settingText);

                
                yield return null;
            }

            IEnumerator PleaseGetNoticed()
            {
                float timer = 20f;
                while (m_player.playerDanger == EnemyController.eDanger.white)
                {
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        if(m_player.playerDanger != EnemyController.eDanger.white)
                            yield break;
                    }
                    else
                    {
                        
                        m_narrator.settingText = false;
                        m_narrator.Play(m_noticedDialogues[0]);

                        if(m_noticedDialogues.Count > 1)
                            m_noticedDialogues.RemoveAt(0);
                        timer = 20f;

                        yield return new WaitWhile(() => m_narrator.settingText);
                    }
                    
                    yield return null;
                }
            }

            
                StartCoroutine(SkipCheck());
                IEnumerator PlayNextDialogue(int _index = 0)
                {
                    index = _index != 0 ? _index : index;
                    m_narrator.settingText = false;
                    m_narrator.Play(m_tutorialDialogues[index]);

                    index++;

                    yield return new WaitWhile(() => m_narrator.settingText);
                }


                if (!_retry)
                {

                    yield return new WaitForFixedUpdate();
                    yield return PlayNextDialogue();
                    m_hidingSpot = m_hidingSpots.Random();

                    while (m_hidingSpot.transform.parent.GetComponent<BoxCollider>().bounds
                        .Intersects(m_player.transform.GetChild(0).GetComponent<Collider>().bounds))
                    {
                        m_hidingSpot = m_hidingSpots.Random();
                        yield return null;
                    }

                    m_hidingSpot.transform.parent.GetComponent<BoxCollider>().enabled = false;
                    m_hidingSpot.SetActive(true);

                    Debug.Log("Why don't ya hide under that table");

                    yield return new WaitUntil(() => m_hidingSpot.GetComponent<HidingSpot>().isUsed);

                    yield return PlayNextDialogue();

                    yield return PlayNextDialogue();


                    yield return new WaitWhile(() => m_hidingSpot.GetComponent<HidingSpot>().isUsed);
                }

                m_enemy.SetActive(true);

                Vector3 dir = m_enemy.transform.position - m_player.transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                GameObject.FindObjectOfType<PlayerFollow>().m_mouseInput = false;

                while (Mathf.Abs(GameObject.FindObjectOfType<PlayerFollow>().m_mouseX - rot.eulerAngles.y) > 5f)
                {
                    dir = m_enemy.transform.position - m_player.transform.position;
                    dir.y = 0; // keep the direction strictly horizontal
                    rot = Quaternion.LookRotation(dir);
                    // slerp to the desired rotation over time
                    GameObject.FindObjectOfType<PlayerFollow>().m_mouseX =
                        Mathf.Lerp(GameObject.FindObjectOfType<PlayerFollow>().m_mouseX, rot.eulerAngles.y,
                            Time.deltaTime * 2.75f);

                    GameObject.FindObjectOfType<PlayerFollow>().m_mouseY =
                        Mathf.Lerp(GameObject.FindObjectOfType<PlayerFollow>().m_mouseY, rot.eulerAngles.x,
                            Time.deltaTime * 2.75f);

                    yield return null;
                }

                GameObject.FindObjectOfType<PlayerFollow>().m_mouseInput = true;



                yield return PlayNextDialogue(3);

                m_enemy.GetComponent<StateMachine>().enabled = true;

                yield return PlayNextDialogue();
                

                yield return new WaitForSeconds(2f);


                yield return PleaseGetNoticed();

                yield return new WaitUntil(() =>
                    m_player.playerDanger == EnemyController.eDanger.red ||
                    m_player.playerDanger == EnemyController.eDanger.yellow);

                yield return PlayNextDialogue();

                yield return new WaitUntil(() => m_player.playerDanger == EnemyController.eDanger.white);

                yield return PlayNextDialogue();

                
                m_player.transform.position = m_defaultPosition;
                m_player.m_followCam.transform.rotation = m_defaultRotation;
                m_player.m_followCam.ResetMouse();
                m_player.m_followCam.m_mouseInput = true;
                
                m_player.m_enemies.Remove(m_enemy.GetComponent<EnemyController>());
                Destroy(m_enemy);
                
                
                m_player.Unhide();
                m_hidingSpot.SetActive(false);

                yield return null;
            

            m_tutorialOver = true; 
            StopAllCoroutines();
            wallcolor = Color.black;
        }


        public void Retry()
        {
            StopAllCoroutines();
            
            
            IEnumerator PlayNextDialogue()
            {
                m_narrator.settingText = false;
                if(m_deathDialogues.Count > 0)
                {
                    m_narrator.Play(m_deathDialogues[0]);
                    m_deathDialogues.RemoveAt(0);
                    m_deathCounter++;
                }
                else
                {
                    m_narrator.Play($"No, I still believe in you. We’re only on attempt {++m_deathCounter}.");
                }

                yield return new WaitWhile(() => m_narrator.settingText);
            }
            IEnumerator DeathDialogues()
            {
                
                yield return PlayNextDialogue();
                yield return StartCoroutine(eTutorial(true));
            }
            
            
            m_player.transform.position = m_defaultPosition;
            m_player.m_followCam.transform.rotation = m_defaultRotation;
            m_player.m_followCam.ResetMouse();
            m_player.m_followCam.m_mouseInput = true;
                
            m_player.m_enemies.Remove(m_enemy.GetComponent<EnemyController>());
            Destroy(m_enemy);
                
                
            m_player.Unhide();
            
            m_enemy = GameObject.Instantiate(m_enemyDefault, transform, true);
            m_player.m_enemies.Add(m_enemy.GetComponent<EnemyController>());
            
            StartCoroutine(DeathDialogues());
        }
    }
}