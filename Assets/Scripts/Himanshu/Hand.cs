using rachael;
using UnityEngine;
namespace Himanshu
{
    public class Hand : MonoBehaviour, IInteract
    {
        public bool HandOpen;
        private Animator m_animator;

        [SerializeField] private Objective m_objective;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
            aIsHandOpen = HandOpen;
            m_objective.m_locked = aIsHandOpen;
            
        }

        private bool aIsHandOpen {
            get => m_animator.GetBool("IsOpening");
            set => m_animator.SetBool("IsOpening", value);
        }
        
        public void Execute(PlayerInteract _player)
        {
            aIsHandOpen = !aIsHandOpen;
            m_objective.m_locked = aIsHandOpen;
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }
    }
}
