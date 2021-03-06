using System.Collections.Generic;
using Bolt;
using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class 
        Door : MonoBehaviour, IInteract
    {
        public enum eTutToPlay
        {
            None, Obj1, Obj2, SObj1, SObj2
        }
        private Animator m_doorAnim;

        [SerializeField] private eTutToPlay m_tutToPlay;
        public bool m_locked = false;

        //private RaycastingTesting m_raycastingTesting;
        
        private bool m_doorOpen = false;

        [FormerlySerializedAs("enemiesToEnable")] [SerializeField]
        private List<GameObject> m_enemiesToEnable;

        public List<GameObject> enemiesToEnable => m_enemiesToEnable;
        public bool isDoorOpen => m_doorOpen;

        private AudioSource m_audioSource;
        
        [SerializeField] private AudioClip m_doorClip;
        
        public bool doorOpen
        {
            get => m_doorOpen;
            set
            {
                // m_audioSource?.Play();
                m_doorOpen = value;

                m_doorAnim.SetBool("IsOpening", value);
            }
        }


        public void LockDoor()
        {
            m_locked = true;
        }
        
        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
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

        public void CloseTheDoor(bool _silent = false)
        {
            doorOpen = false;
            if (!_silent)
                m_audioSource.Play();
        }

        public void OpenTheDoor(bool _silent = false)
        {
            doorOpen = true;

            if (!_silent)
                m_audioSource.Play();
            
            if ((!gameManager.Instance.m_objTutorialPlayed ?? true) && enemiesToEnable.Count > 0 && m_tutToPlay != eTutToPlay.None) 
                Tutorial.RunObjTutorial(this, m_tutToPlay);
            else
            {
                foreach (var enemy in m_enemiesToEnable)
                {
                    enemy.GetComponent<StateMachine>().enabled = true;
                    enemy.GetComponent<EnemyController>().enabled = true;
                }
            }

        }

        public void Execute(PlayerInteract _player)
        {
            if (m_locked)
                return;
            
            PlayAnimation();
        }

        public void CanExecute(Raycast _raycast)
        {
            if (m_locked)
            {
                _raycast.m_indication.enabled = false;
                return;
            }
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
