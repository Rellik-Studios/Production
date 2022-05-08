using rachael;
using UnityEngine;
namespace Himanshu
{
    public class Hand : MonoBehaviour, IInteract
    {

        private Animator m_animator;

        [SerializeField] private Objective m_objective;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        private bool aIsHandOpen {
            get => m_animator.GetBool("IsOpen");
            set => m_animator.SetBool("IsOpen", value);
        }
        
        public void Execute(PlayerInteract _player)
        {
            aIsHandOpen = !aIsHandOpen;
            m_objective.m_locked = !aIsHandOpen;
        }
        public void CanExecute(Raycast _raycast)
        {
            throw new System.NotImplementedException();
        }
    }
}
