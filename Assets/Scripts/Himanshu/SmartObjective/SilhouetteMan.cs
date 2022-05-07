using rachael;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class SilhouetteMan : MonoBehaviour, IInteract
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
                
            }
        }
        public void Execute(PlayerInteract _player)
        {
            m_player = _player.GetComponent<PlayerSmartObjectives>();
            if(!m_player.m_hasNewsPaper) return;
            m_animator.SetBool("NewsPaper", true);
            PlayerInteract.PlaySound(Resources.Load<AudioClip>("SFX/NewsPaperDelivery"));
            ItemHold.Instance.DropItem();
            m_objective.Execute(_player);
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }
    }
}
