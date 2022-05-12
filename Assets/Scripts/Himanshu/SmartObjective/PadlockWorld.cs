using System;
using rachael;
using UnityEngine;

namespace Himanshu.SmartObjective
{
    public class PadlockWorld : MonoBehaviour, IInteract
    {
        public GameObject padlock;
        private float m_timer = 0f;
        [SerializeField] private GameObject m_objective;
        private bool played;
        private bool m_playedInit;
        private PlayerSmartObjectives m_player;

        private void Start()
        {
            m_player = FindObjectOfType<PlayerSmartObjectives>();
        }
        private void Update()
        {
            if(m_objective.activeSelf) return;
            
            if (padlock.GetComponent<Padlock>().m_isLocked && m_playedInit) {
                m_timer += Time.deltaTime;
                if (m_timer > 10f && !played) {
                    FindObjectOfType<Narrator>().Play("Maybe this has something to do with the room we found the anomaly in… ");
                    played = true;
                }    
            }

            if (!m_playedInit && Vector3.Distance(transform.position, m_player.transform.position) < 9f) {
                m_playedInit = true;
                FindObjectOfType<Narrator>().Play("Oh bummer, we're locked in. Got any ideas? # Don't look at me, I've got nothing. ");
            }
        }
        public void Execute(PlayerInteract _player)
        {
            padlock.SetActive(!padlock.activeSelf);
        }

        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }
    }
}