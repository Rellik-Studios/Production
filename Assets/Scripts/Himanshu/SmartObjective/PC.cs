using System;
using System.Collections;
using rachael;
using rachael.FavorSystem;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class PC : MonoBehaviour, IInteract
    {

        private Animator m_animator;
        private FavorSystem m_favorSystem;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_favorSystem = FindObjectOfType<FavorSystem>();
        }
        public void Execute(PlayerInteract _player)
        {
            IEnumerator PCRoutine()
            {
                yield return m_favorSystem.SetProcess("Installing Command");
            }

            StartCoroutine(PCRoutine());

        }
        public void CanExecute(Raycast _raycast)
        {
            
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (_collider.TryGetComponent(out PlayerSmartObjectives _player)) 
            {
                //m_animator.SetBool("TurnOn", true);
            }
        }
    }
}
