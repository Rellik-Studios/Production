using System;
using rachael;
using UnityEngine;
namespace Himanshu
{
    public class Stall : MonoBehaviour, IInteract
    {
        private Animator m_animator;
        public enum eStallState
        {
            Open,
            Close,
        }
        public eStallState m_stallState;
        public eStallState stallState {
            get => m_stallState;
            set {
                if (value != m_stallState) {
                    m_stallState = m_stallState == eStallState.Close ? eStallState.Open : eStallState.Close;
                    m_animator.SetBool("open", m_stallState == eStallState.Open);        
                }
            }
        }
        private void Awake()
        {
            m_stallState = eStallState.Close;
            m_animator = GetComponentInParent<Animator>();
        }
        public void Execute(PlayerInteract _player)
        {
            stallState = stallState == eStallState.Close ? eStallState.Open : eStallState.Close;
        }
        public void CanExecute(Raycast _raycast)
        {
            if(!_raycast.m_doOnce)
            {
                _raycast.CrosshairChange(true);
            }
            //temp until UI for the opening/closing door
            _raycast.m_indication.enabled = false;
            //--------------------------
            _raycast.m_doOnce = true;
        }
    }

}
