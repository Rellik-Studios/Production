using System.Collections.Generic;
using Bolt;
using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class Door : MonoBehaviour, IInteract
    {
        private Animator m_doorAnim;

        //private RaycastingTesting m_raycastingTesting;
        
        private bool m_doorOpen = false;

        [FormerlySerializedAs("enemiesToEnable")] [SerializeField]
        private List<GameObject> m_enemiesToEnable;

        public bool isDoorOpen => m_doorOpen;
        
        public bool doorOpen
        {
            get => m_doorOpen;
            set
            {
                m_doorOpen = value;
                m_doorAnim.SetBool("IsOpening", value);
            }
        }


        private void Awake()
        {
            m_doorAnim = GetComponent<Animator>();
            //m_raycastingTesting = FindObjectOfType<RaycastingTesting>();
        }

        public void PlayAnimation()
        {
            if (!doorOpen)
            {
                OpenTheDoor();
            }
            else
            {
                CloseTheDoor();
            }
        }

        public void CloseTheDoor()
        {
            doorOpen = false;
        }

        public void OpenTheDoor()
        {
            doorOpen = true;

            if (!Tutorial.m_objTutorialPlayed ?? false) return;
            
            
            foreach (var enemy in m_enemiesToEnable)
            {
                enemy.GetComponent<StateMachine>().enabled = true;
                enemy.GetComponent<EnemyController>().enabled = true;
            }

        }

        public void Execute(PlayerInteract _player)
        {
            PlayAnimation();
        }

        public void CanExecute(Raycast _raycast)
        {
            if(!_raycast.m_doOnce)
            {
                _raycast.CrosshairChange(true);
            }
            //temp until UI for the opening/closing door
            _raycast.m_indication.enabled = false;
            //--------------------------
            _raycast.m_doOnce = true;
        }

       
    }
}
