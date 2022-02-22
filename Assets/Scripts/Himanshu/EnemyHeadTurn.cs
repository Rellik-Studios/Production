using System;
using UnityEngine;

namespace Himanshu
{
    public class EnemyHeadTurn : MonoBehaviour
    {
        public bool m_look;
        public Transform m_lookTarget;

        private Animator m_animator;

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
    }
}
