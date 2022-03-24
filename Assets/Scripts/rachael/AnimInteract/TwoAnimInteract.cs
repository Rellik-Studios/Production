using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEngine;
namespace rachael
{
    public class TwoAnimInteract : MonoBehaviour, IInteract
    {
        // Start is called before the first frame update
        private Animator m_objectAnim;

        private AudioSource m_audioSource;
        //private RaycastingTesting m_raycastingTesting;

        private bool m_objectOpen = false;

        public bool isObjectOpen => m_objectOpen;

        public bool objectOpen
        {
            get => m_objectOpen;
            set
            {
                m_objectOpen = value;
                m_objectAnim.SetBool("IsOpening", value);
            }
        }


        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        private void Awake()
        {
            m_objectAnim = GetComponent<Animator>();
            //m_raycastingTesting = FindObjectOfType<RaycastingTesting>();
        }

        public void PlayAnimation()
        {
            if (!objectOpen)
            {
                OpenObject();
            }
            else
            {
                CloseObject();
            }
        }

        public void CloseObject()
        {
            objectOpen = false;
        }

        public void OpenObject()
        {
            objectOpen = true;
        }

        public void Execute(PlayerInteract _player)
        {
            PlayAnimation();
            m_audioSource.Play();
        }

        public void CanExecute(Raycast _raycast)
        {
            if (!_raycast.m_doOnce)
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
