using System;
using System.Collections.Generic;
using rachael;
using UnityEngine;
using UnityEngine.Events;
namespace Himanshu.SmartObjective
{

    public class PickupObj : MonoBehaviour, IInteract, IPickup
    {
        protected Vector3 m_defaultPosition;
        protected Quaternion m_defaultRotation;
        protected Animator m_animator;

        public enum eObjName
        {
            Candle, PaintBrush, MusicNotes, VRHeadset,NewsPaper,
        }
        private static Dictionary<eObjName, Action> m_actions;

        public eObjName m_objName;
        private PlayerSmartObjectives m_player;

        public Vector3 m_position = new Vector3(0f, 0f, 1.39f);
        public Vector3 m_rotation =new Vector3(16.723f, 0f, 0f);
        public UnityEvent m_onPickup;

        private void Start()
        {
            m_player = FindObjectOfType<PlayerSmartObjectives>();
            m_actions = new Dictionary<eObjName, Action>();
            m_actions.Add(eObjName.MusicNotes, MusicNotes);
            m_actions.Add(eObjName.Candle, Candle);
            m_actions.Add(eObjName.PaintBrush, PaintBrush);
            m_actions.Add(eObjName.VRHeadset, VRHeadset);
            m_actions.Add(eObjName.NewsPaper, NewsPaper);
            
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

        void VRHeadset()
        {
            m_player.hasVRHeadset = true;
        }

        void NewsPaper()
        {
            m_player.m_hasNewsPaper = true;
        }

        public void Execute(PlayerInteract _player)
        {
            bool wait = false;
            if(!OneTimeText.alreadyUsed.Contains("Press RMB to drop the object"))
            {
                OneTimeText.SetText("Press RMB to drop the object", ()=>false);
                wait = true;
            }
            // this.Invoke(() => {
            //     switch (m_objName) {
            //         case eObjName.Candle:
            //             OneTimeText.SetText("Find the misplaced Fire and put it back in the right place", () => false);
            //             break;
            //         case eObjName.PaintBrush:
            //             OneTimeText.SetText("Find the painting with the Anomaly", () => false);
            //             break;
            //         case eObjName.MusicNotes:
            //             break;
            //         case eObjName.NewsPaper:
            //             OneTimeText.SetText("Return the NewsPaper, back to the Man", () => false);
            //             break;
            //     }
            // }, wait ? 3f : 0);

            switch (m_objName) {
                case eObjName.VRHeadset:
                    GetComponent<MeshRenderer>().enabled = false;
                    FindObjectOfType<Narrator>().Play("Ah, the wonders of augmented reality.#" +
                                                      " I’m rather curious, let’s look around!");
                    break;
                case eObjName.PaintBrush:
                    FindObjectOfType<Narrator>().Play("Watch out Monet, Van Gogh, Klimt!");
                break;
            }
            ItemHold.Instance.HoldItem(this.gameObject);
            m_actions[m_objName]();
            m_onPickup?.Invoke();
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
        }
        public virtual void SetTransform()
        {
            transform.localPosition = m_position;
            transform.localRotation = Quaternion.Euler(m_rotation);
        }
        public virtual void SetTransform(Transform _transform)
        {
            transform.position = _transform.position;
            transform.rotation = _transform.rotation;
            transform.localScale = _transform.localScale;
        }


    }
}
