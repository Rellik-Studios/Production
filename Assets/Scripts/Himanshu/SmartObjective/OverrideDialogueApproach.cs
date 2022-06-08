using System;
using System.Linq;
using UnityEngine;
namespace Himanshu.SmartObjective
{

    public class OverrideDialogueApproach : MonoBehaviour
    {
        private Narrator m_narrator;
        [SerializeField] private float m_radius;
        [SerializeField] private string m_dialogue;
        private void Start()
        {
            m_narrator = FindObjectOfType<Narrator>();
        }

        private void Update()
        {
            if(gameManager.Instance.isTutorialRunning)
                return;
            //Check sphere collision for all layers
            var colliders = Physics.OverlapSphere(transform.position, m_radius, ~0);
            if (colliders.Any(t => t.TryGetComponent<PlayerInteract>(out PlayerInteract _playerInteract))) {
                m_narrator.Play(m_dialogue);
                Destroy(this);
            }
        }
    }
}
