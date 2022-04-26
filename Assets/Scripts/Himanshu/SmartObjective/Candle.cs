using rachael;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class Candle : MonoBehaviour, IInteract, IPickup
    {
        private Vector3 m_defaultPosition;
        private Quaternion m_defaultRotation;
        private Animator m_animator;
        private bool m_isLit;
        public bool isLit {
            get => m_isLit;
            set
            {
                m_isLit = value;
                m_animator.SetBool("isLit", m_isLit);
            }
        }

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_defaultPosition = transform.position;
            m_defaultRotation = transform.rotation;
        }



        public void Execute(PlayerInteract _player)
        {
            ItemHold.Instance.HoldItem(this.gameObject);
            _player.GetComponent<PlayerSmartObjectives>().m_hasCandle = true;
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
        }
        public void SetTransform()
        {
            transform.localPosition = new Vector3(0f, 0f, 1.39f);
            transform.localRotation = Quaternion.Euler(16.723f, 0f, 0f);
        }
        public void SetTransform(Transform _transform)
        {
            transform.position = _transform.position;
            transform.rotation = _transform.rotation;
            transform.localScale = _transform.localScale;
        }
    }


}
