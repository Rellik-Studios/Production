using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class FirePlace : MonoBehaviour
    {
        private   Animator m_animator;
        public Objective m_objective;
        
        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (_collider.TryGetComponent(out PlayerSmartObjectives _player) && _player.m_hasFire)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                m_objective.Execute(_player.GetComponent<PlayerInteract>());
                _player.m_hasFire = false;
                _player.m_hasCandle = false;
                ItemHold.Instance.DropItem();
            }
        }
    }
}
