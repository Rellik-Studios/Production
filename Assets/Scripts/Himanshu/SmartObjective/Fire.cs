using System;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class Fire : MonoBehaviour
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
            if(_collider.TryGetComponent(out PlayerSmartObjectives _player) && !_player.m_hasFire && _player.m_hasCandle) {
                m_player = _player;
                _player.m_hasFire = true;
                if (ItemHold.Instance.m_heldItemPlaceHolder.GetComponent<Candle>() != null) 
                {
                    PlayerInteract.PlaySound(Resources.Load<AudioClip>("SFX/FireToCandle"));
                    ItemHold.Instance.m_heldItemPlaceHolder.GetComponent<Candle>().isLit = true;
                    ItemHold.Instance.m_heldItem.GetComponent<Candle>().isLit = true;
                    m_animator.SetBool("DisableFire", true);
                }
            }
        }

        private void Update()
        {
            if(m_objective.gameObject.activeSelf && m_player != null && !m_player.m_hasFire) {
                m_animator.SetBool("DisableFire", false);
            }
        }
    }


}
