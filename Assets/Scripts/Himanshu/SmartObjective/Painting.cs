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
        private void Start()
        {
            m_player = FindObjectOfType<PlayerSmartObjectives>();
        }
        public void Execute(PlayerInteract _player)
        {
            if (m_player.m_hasPaintBrush && m_hasAnomaly) {
                m_objective.Execute(m_player.GetComponent<PlayerInteract>());
            }
        }
        public void CanExecute(Raycast _raycast)
        {
        }
    }
}
