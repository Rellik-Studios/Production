using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class SilhouetteMan : MonoBehaviour
    {
        private Animator m_animator;
        private PlayerSmartObjectives m_player;
        [SerializeField] private Objective m_objective;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }
        private void OnTriggerEnter(Collider _collider)
        {
            if(_collider.TryGetComponent(out PlayerSmartObjectives _player) && _player.m_hasNewsPaper) {
                m_player = _player;
                m_animator.SetBool("NewsPaper", true);
                ItemHold.Instance.DropItem();
            }
        }
    }
}
