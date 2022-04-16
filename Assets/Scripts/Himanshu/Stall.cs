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
        private void Start()
        {
            m_stallState = eStallState.Close;
            m_animator = GetComponentInParent<Animator>();
        }
        public void Execute(PlayerInteract _player)
        {
            m_stallState = m_stallState == eStallState.Close ? eStallState.Open : eStallState.Close;
            m_animator.SetBool("open", m_stallState == eStallState.Open);
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
