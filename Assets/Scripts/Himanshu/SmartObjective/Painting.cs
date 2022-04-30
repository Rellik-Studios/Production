using System;
using rachael;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class Painting : MonoBehaviour, IInteract
    {

        private PlayerSmartObjectives m_player;
        [SerializeField] private Objective m_objective;
        public bool m_hasAnomaly;
        private Animator m_animator;
        [SerializeField] private GameObject m_painting;
        private bool m_isInspecting;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_player = FindObjectOfType<PlayerSmartObjectives>();
        }
        public void Execute(PlayerInteract _player)
        {
            if (true) {
               m_painting.SetActive(true);
               m_painting.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
               if (!m_hasAnomaly) {
                   m_painting.transform.GetChild(0).gameObject.SetActive(false);
                   this.Invoke(()=>m_painting.SetActive(false), 2f);
               } 
               else if(m_player.m_hasPaintBrush) {
                   m_painting.GetComponent<Animator>().SetTrigger("Painted");
                   m_animator.SetTrigger("Painted");
                   m_objective.Execute(m_player.GetComponent<PlayerInteract>());
                   this.Invoke(()=>m_painting.SetActive(false), 2f);
                   ItemHold.Instance.DropItem();
               }
            }
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerInteract>() != null) {
                m_painting.SetActive(false);
                m_isInspecting = false;
                FindObjectOfType<PlayerFollow>().m_mouseInput = true;

            }
        }

    }
}
