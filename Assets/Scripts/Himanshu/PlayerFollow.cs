using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    public class PlayerFollow : MonoBehaviour
    {

        [SerializeField] private Transform m_playerTransform;
        private PlayerMovement m_playerMovement;

        private float m_mouseX;
        private float m_mouseY;

        private bool m_xLimiter;
        private bool m_yLimiter = true;

        private Vector2 m_xRange;
        private Vector2 m_yRange = new Vector2(-30f, 30f);
        void Start()
        {
            m_playerMovement = m_playerTransform.GetComponent<PlayerMovement>();
            Cursor.lockState = CursorLockMode.Locked;
            ResetMouse(m_playerTransform.rotation.eulerAngles.y, m_playerTransform.rotation.eulerAngles.x);
        }

        void Update()
        {
            if(Time.timeScale == 0) return;
            transform.position = m_playerMovement.calculatedPosition;

            
            m_mouseX += Input.GetAxis("Mouse X");
            m_mouseY -= Input.GetAxis("Mouse Y");

            m_mouseY = m_yLimiter ? Mathf.Clamp(m_mouseY, m_yRange.x, m_yRange.y): m_mouseY;
            m_mouseX = m_xLimiter ? Mathf.Clamp(m_mouseX, m_xRange.x, m_xRange.y): m_mouseX;

            transform.rotation = Quaternion.Euler(m_mouseY, m_mouseX, 0f);
            m_playerTransform.forward = new Vector3(transform.forward.x, 0f, transform.forward.z);

        }

        public void ResetMouse(float _mouseX = 0f, float _mouseY = 0f) 
        {
            m_mouseX = _mouseX;
            m_mouseY = _mouseY;
        }

        public void SetRotation(Transform _transform, Vector2 _xRange)
        {
            m_xLimiter = true;
            m_xRange = new Vector2(_transform.rotation.eulerAngles.y - 30, _transform.rotation.eulerAngles.y + 30);
            ResetMouse(_transform.rotation.eulerAngles.y, _transform.rotation.eulerAngles.x);
            transform.rotation = _transform.rotation;
        }
        
        public void SetRotation(Transform _transform)
        {
            ResetMouse();
            transform.rotation = _transform.rotation;
        }

        public void ResetRotationLock()
        {
            m_xLimiter = false;
        }
    }
}
