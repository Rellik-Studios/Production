using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEngine;

namespace rachael
{
    public class OneShotAnim : MonoBehaviour, IInteract
    {
        // Start is called before the first frame update
        private Animator m_objectAnim;
        private bool isPlaying = false;


        public void isIDLE()
        {
            m_objectAnim.SetBool("OneShot", false);
            //Debug.Log("stop playing");
            isPlaying = false;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Awake()
        {
            m_objectAnim = GetComponent<Animator>();
            //m_raycastingTesting = FindObjectOfType<RaycastingTesting>();
        }

        public void PlayAnimation()
        {
            if (!isPlaying)
            {
                m_objectAnim.SetBool("OneShot", true);
                isPlaying = true;
                //Debug.Log("start playing");
            }
        }


        public void Execute(PlayerInteract _player)
        {
            PlayAnimation();
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
