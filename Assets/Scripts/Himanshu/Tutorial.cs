using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

namespace Himanshu
{
    public class Tutorial : MonoBehaviour
    {
        public bool m_tutorialOver => m_tutorialDialogues.Count == 0;
        
        private Narrator m_narrator;
        [SerializeField] private List<GameObject> m_hidingSpots;
        
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_tutorialDialogues;
        
        [SerializeField] private PlayerInteract m_player;
        [SerializeField] private GameObject m_enemy;

        private void Start()
        {
            m_narrator = GetComponent<Narrator>();
            RunTutorial();
        }

        public void RunTutorial()
        {
            StartCoroutine(eTutorial());
        }
        
        private IEnumerator eTutorial()
        {

            IEnumerator PlayNextDialogue()
            {
                m_narrator.settingText = false;
                m_narrator.Play(m_tutorialDialogues[0]);
            
                m_tutorialDialogues.RemoveAt(0);

                yield return new WaitWhile(() => m_narrator.settingText);
            }

            yield return PlayNextDialogue();
            var hidingSpot = m_hidingSpots.Random();

            while (hidingSpot.transform.parent.GetComponent<BoxCollider>().bounds.Intersects(m_player.transform.GetChild(0).GetComponent<Collider>().bounds))
            {
                hidingSpot = m_hidingSpots.Random();
                yield return null;
            }

            hidingSpot.transform.parent.GetComponent<BoxCollider>().enabled = false;
            hidingSpot.SetActive(true);
            
            Debug.Log("Why don't ya hide under that table");

            yield return new WaitUntil(()=>hidingSpot.GetComponent<HidingSpot>().isUsed);
            
            yield return PlayNextDialogue();

            m_enemy.SetActive(true);
            
            Vector3 dir = m_enemy.transform.position - m_player.transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);

            yield return new WaitWhile(()=>hidingSpot.GetComponent<HidingSpot>().isUsed);


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

            
            
            yield return PlayNextDialogue();
            
            m_enemy.GetComponent<StateMachine>().enabled = true;
            
            yield return PlayNextDialogue();
            
            yield return null;
        }
        
        
    }
}