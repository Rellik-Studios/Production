using System;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class Paintbrush : PickupObj
    {
        private bool m_followMouse;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_defaultPosition = transform.position;
            m_defaultRotation = transform.rotation;
        }


        private void Update()
        {
            
        }


        public override void SetTransform()
        {
            transform.localPosition = new Vector3(-0.48f, 0f, 1.39f);
            transform.localRotation = Quaternion.Euler(34.453f, 0f, 118.745f);
        }
        public override void SetTransform(Transform _transform)
        {
            transform.position = _transform.position;
            transform.rotation = _transform.rotation;
            transform.localScale = _transform.localScale;
        }
        public void Paint()
        {
            FindObjectOfType<PlayerFollow>().m_mouseInput = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            m_followMouse = true;
        }
    }
}
