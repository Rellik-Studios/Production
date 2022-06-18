using rachael;
using UnityEngine;
using UnityEngine.Events;
namespace Himanshu
{
    public class Hand : MonoBehaviour, IInteract
    {
        public bool HandOpen;
        private Animator m_animator;
        [SerializeField] private UnityEvent m_onExecute;

        [SerializeField] private MeshCollider m_objective;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
            aIsHandOpen = HandOpen;
            if(m_objective != null )
                m_objective.enabled = aIsHandOpen;
            
        }

        private bool aIsHandOpen {
            get => m_animator.GetBool("IsOpening");
            set => m_animator.SetBool("IsOpening", value);
        }
        
        public void Execute(PlayerInteract _player)
        {
            aIsHandOpen = !aIsHandOpen;
            if (m_objective != null)
                m_objective.enabled = aIsHandOpen;
            m_onExecute?.Invoke();
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }
    }
}
