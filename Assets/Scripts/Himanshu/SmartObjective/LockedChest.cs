using rachael;
using TMPro;
using UnityEngine;

namespace Himanshu.SmartObjective
{
    public class LockedChest : MonoBehaviour, IInteract
    {

        [SerializeField] private TMP_Text m_text;
        //public int id;
        public bool objectOpen
        {
            get => m_objectOpen;
            set
            {
                m_objectOpen = value;
                m_animator.SetBool("IsOpening", value);
            }
        }

        public int m_passcode;
        public bool m_locked;
        private bool m_objectOpen;
        private Animator m_animator;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_passcode = Random.Range(1000, 9999);
            m_text.text += m_passcode.ToString();
        }

        public void Execute(PlayerInteract _player)
        {
            if (!m_locked)
            {
                objectOpen = !objectOpen;
            }
            
        }

        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>(m_locked ? "locked" : "Interact");
        }
    }
}