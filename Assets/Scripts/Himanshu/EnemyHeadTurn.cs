using System;
using UnityEngine;

namespace Himanshu
{
    public class EnemyHeadTurn : MonoBehaviour
    {
        public bool m_look;
        public Transform m_lookTarget;

        public Transform m_neck1;
        public Transform m_neck2;
        public Transform m_head;
        private Animator m_animator;

        [SerializeField] private Transform m_lookAtHelper;
        private float m_lookAngle;
        [SerializeField] private float m_lAngle;

        public float lookAngle
        {
            get => m_lookAngle;
            set
            {
                m_lookAngle = value > 180f ? value - 360f : value < -180f ? value + 360f : value;
                if (m_lookAngle < 0)
                {
                    m_neck1.transform.localRotation = Quaternion.Euler(27.5f * m_lookAngle / 100f, m_neck1.transform.localRotation.eulerAngles.y, m_neck1.transform.localRotation.eulerAngles.z);
                    m_neck2.transform.localRotation = Quaternion.Euler(27.5f * m_lookAngle / 100f, m_neck2.transform.localRotation.eulerAngles.y, m_neck2.transform.localRotation.eulerAngles.z);
                    m_head.transform.localRotation = Quaternion.Euler(45f * m_lookAngle / 100f, m_head.transform.localRotation.eulerAngles.y, m_head.transform.localRotation.eulerAngles.z);                    
                }

                if (m_lookAngle >= 0f)
                {
                    m_neck1.transform.localRotation = Quaternion.Euler(27.5f * m_lookAngle / 100f, m_neck1.transform.localRotation.eulerAngles.y, m_neck1.transform.localRotation.eulerAngles.z);
                    m_neck2.transform.localRotation = Quaternion.Euler(27.5f * m_lookAngle / 100f, m_neck2.transform.localRotation.eulerAngles.y, m_neck2.transform.localRotation.eulerAngles.z);
                    m_head.transform.localRotation = Quaternion.Euler(45f * m_lookAngle / 100f, m_head.transform.localRotation.eulerAngles.y, m_head.transform.localRotation.eulerAngles.z);
                    
                }
            }
        }

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
           // Debug.Log("Here");
            if (m_look)
            {
                m_animator.SetLookAtWeight(1f, 0f, 1f, .5f, 0.1f);
                m_animator.SetLookAtPosition(m_lookTarget.position);
            }
        }

        private void Update()
        {
            lookAngle = m_lAngle;
//            Debug.Log(lookAngle);
        }

        private void LateUpdate()
        {
            if (m_look)
            {
                
                var dir = m_lookTarget.transform.position - transform.parent.position;
                dir.y = 0; // keep the direction strictly horizontal
                m_lookAtHelper.LookAt(m_lookTarget);
                m_lookAtHelper.RotateAround(transform.position, transform.forward, -90f);
                m_lookAtHelper.RotateAround(transform.position, transform.right, 90f);
                var rot = Quaternion.LookRotation(dir);

                lookAngle = -rot.eulerAngles.y + transform.parent.rotation.eulerAngles.y;
                //Debug.Log(lookAngle +" " + rot.eulerAngles.y);
            }
        }
    }
}
