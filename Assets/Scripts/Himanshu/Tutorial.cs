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
        [SerializeField] private List<string> m_deathDialogues;

        private void Start()
        {
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
                
                m_narrator.Play("Suit Yourself");
                m_tutorialOver = true;
                // m_narrator.enabled = true;
                wallcolor = Color.black;
                StopAllCoroutines();
                yield return new WaitWhile(() => m_narrator.settingText);

                
                yield return null;
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

                    yield return PlayNextDialogue();
                    var hidingSpot = m_hidingSpots.Random();

                    while (hidingSpot.transform.parent.GetComponent<BoxCollider>().bounds
                        .Intersects(m_player.transform.GetChild(0).GetComponent<Collider>().bounds))
                    {
                        hidingSpot = m_hidingSpots.Random();
                        yield return null;
                    }

                    hidingSpot.transform.parent.GetComponent<BoxCollider>().enabled = false;
                    hidingSpot.SetActive(true);

                    Debug.Log("Why don't ya hide under that table");

                    yield return new WaitUntil(() => hidingSpot.GetComponent<HidingSpot>().isUsed);

                    yield return PlayNextDialogue();

                    yield return PlayNextDialogue();


                    yield return new WaitWhile(() => hidingSpot.GetComponent<HidingSpot>().isUsed);
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

                yield return new WaitUntil(() =>
                    m_player.playerDanger == EnemyController.eDanger.red ||
                    m_player.playerDanger == EnemyController.eDanger.yellow);

                yield return PlayNextDialogue();

                yield return new WaitUntil(() => m_player.playerDanger == EnemyController.eDanger.white);

                yield return PlayNextDialogue();

                Destroy(m_enemy);
                yield return null;
            

            m_tutorialOver = true; 
            wallcolor = Color.black;
        }


        public void Retry()
        {
            
            IEnumerator PlayNextDialogue()
            {
                m_narrator.settingText = false;
                if(m_deathDialogues.Count > 0)
                {
                    m_narrator.Play(m_deathDialogues[0]);
                    m_deathDialogues.RemoveAt(0);
                }

                yield return new WaitWhile(() => m_narrator.settingText);
            }
            
            
            IEnumerator DeathDialogues()
            {
                StopAllCoroutines();
                yield return PlayNextDialogue();
                yield return StartCoroutine(eTutorial(true));
                yield return null;
            }
            m_player.m_enemies.Remove(m_enemy.GetComponent<EnemyController>());
            Destroy(m_enemy);
            
            m_enemy = GameObject.Instantiate(m_enemyDefault, transform, true);
            m_player.m_enemies.Add(m_enemy.GetComponent<EnemyController>());
            
            StartCoroutine(DeathDialogues());
        }
    }
}