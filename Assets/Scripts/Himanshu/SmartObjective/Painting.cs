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
            if (m_hasAnomaly && m_player.m_hasPaintBrush)
            {
               m_painting.SetActive(true);
               m_painting.GetComponent<Renderer>().material = GetComponent<Renderer>().material;

               {
                   FindObjectOfType<Narrator>().Play("You’re an up and coming talent, a star on the rise.#" +
                                                     " Leonardo Da Vinci, Raphael, Caravaggio, @userName …");
                   m_painting.GetComponent<Animator>().SetTrigger("Painted");
                   m_animator.SetTrigger("Painted");
                   m_objective.Execute(m_player.GetComponent<PlayerInteract>());
                   this.Invoke(() => m_painting.SetActive(false), 2f);
                   ItemHold.Instance.DropItem();
               }
            }
            else if (m_player.m_hasPaintBrush) {
                FindObjectOfType<Narrator>().Play("There's nothing abnormal here.");
            }
        }
        public void CanExecute(Raycast _raycast)
        {
            if (!m_player.m_hasPaintBrush)
            {
                if (_raycast.m_indication != null)
                    _raycast.m_indication.enabled = false;
                return;
            }
            
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
