using System;
using System.Collections.Generic;
using rachael;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class PickupObj : MonoBehaviour, IInteract, IPickup
    {
        protected Vector3 m_defaultPosition;
        protected Quaternion m_defaultRotation;
        protected Animator m_animator;

        public enum eObjName
        {
            Candle, PaintBrush, MusicNotes
        }
        private static Dictionary<eObjName, Action> m_actions;

        public eObjName m_objName;
        private PlayerSmartObjectives m_player;

        private void Start()
        {
            m_player = FindObjectOfType<PlayerSmartObjectives>();
            m_actions = new Dictionary<eObjName, Action>();
            m_actions.Add(eObjName.MusicNotes, MusicNotes);
            m_actions.Add(eObjName.Candle, Candle);
            m_actions.Add(eObjName.PaintBrush, PaintBrush);
            
            m_animator = GetComponent<Animator>();
            m_defaultPosition = transform.position;
            m_defaultRotation = transform.rotation;
        }

        void Candle()
        {
            m_player.m_hasCandle = true;
        }
        
        void PaintBrush()
        {
            m_player.m_hasPaintBrush = true;
        }
        
        void MusicNotes()
        {
            m_player.m_hasNotes = true;
        }



        public void Execute(PlayerInteract _player)
        {
            ItemHold.Instance.HoldItem(this.gameObject);
            m_actions[m_objName]();
            _player.GetComponent<PlayerSmartObjectives>().m_hasNotes = true;
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
        }
        public virtual void SetTransform()
        {
            transform.localPosition = new Vector3(0f, 0f, 1.39f);
            transform.localRotation = Quaternion.Euler(16.723f, 0f, 0f);
        }
        public virtual void SetTransform(Transform _transform)
        {
            transform.position = _transform.position;
            transform.rotation = _transform.rotation;
            transform.localScale = _transform.localScale;
        }
    }
}
