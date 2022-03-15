using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    public class PlayerFollow : MonoBehaviour
    {

        [SerializeField] private Transform m_playerTransform;
        private PlayerMovement m_playerMovement;

        public float m_mouseX;
        public float m_mouseY;

        private bool m_xLimiter;
        private bool m_yLimiter = true;
        public bool m_mouseInput = true;

        private Vector2 m_xRange;
        private Vector2 m_yRange = new Vector2(-45f, 45f);
        void Start()
        {
            m_playerMovement = m_playerTransform.GetComponent<PlayerMovement>();
            Cursor.lockState = CursorLockMode.Locked;
            ResetMouse(m_playerTransform.rotation.eulerAngles.y, m_playerTransform.rotation.eulerAngles.x);
        }


        // public IEnumerator eLookInDirection(Transform _target, bool _lookAway = false)
        // {
        //     var dir = _target.position - transform.position;
        //     
        //     dir.y = 0; // keep the direction strictly horizontal
        //     Quaternion rot = Quaternion.LookRotation(dir);
        //     GameObject.FindObjectOfType<PlayerFollow>().m_mouseInput = false;
        //
        //     rot.eulerAngles += _lookAway ? new Vector3(0f, 180f, 0f) : Vector3.zero;
        //     while (Mathf.Abs(GameObject.FindObjectOfType<PlayerFollow>().m_mouseX - rot.eulerAngles.y) > 5f)
        //     {
        //         dir = _target.position - transform.position;
        //         dir.y = 0; // keep the direction strictly horizontal
        //         rot = Quaternion.LookRotation(dir);
        //         // slerp to the desired rotation over time
        //         GameObject.FindObjectOfType<PlayerFollow>().m_mouseX =
        //             Mathf.Lerp(GameObject.FindObjectOfType<PlayerFollow>().m_mouseX, rot.eulerAngles.y,
        //                 Time.deltaTime * 2.75f);
        //
        //         GameObject.FindObjectOfType<PlayerFollow>().m_mouseY =
        //             Mathf.Lerp(GameObject.FindObjectOfType<PlayerFollow>().m_mouseY, rot.eulerAngles.x,
        //                 Time.deltaTime * 2.75f);
        //
        //         yield return null;
        //     }
        // }
        void Update()
        {
            if(Time.timeScale == 0 && !m_playerMovement.canMoveUnscaled) return;
            transform.position = m_playerMovement.calculatedPosition;

            if (m_mouseInput)
            {
                if (!m_playerMovement.canMoveUnscaled)
                {
                    m_mouseX += Input.GetAxis("Mouse X");
                    m_mouseY -= Input.GetAxis("Mouse Y");
                }
                else
                {
                    m_mouseX += Input.GetAxisRaw("Mouse X");
                    m_mouseY -= Input.GetAxisRaw("Mouse Y");
                }

                m_mouseY = m_yLimiter ? Mathf.Clamp(m_mouseY, m_yRange.x, m_yRange.y) : m_mouseY;
                m_mouseX = m_xLimiter ? Mathf.Clamp(m_mouseX, m_xRange.x, m_xRange.y) : m_mouseX;


                transform.rotation = Quaternion.Euler(m_mouseY, m_mouseX, 0f);
            }

            m_playerTransform.forward = new Vector3(transform.forward.x, 0f, transform.forward.z);

        }

        public void ResetMouse(float _mouseX = 0f, float _mouseY = 0f) 
        {
            var rotation = transform.rotation;
            m_mouseX = rotation.eulerAngles.y;
            m_mouseY = rotation.eulerAngles.x;
        }
        
        public void SetRotation(Quaternion _rotation)
        {
            ResetMouse();
            transform.rotation = _rotation;
        }

        public void SetRotation(Transform _transform, Vector2 _xRange)
        {
            m_xLimiter = true;
            m_xRange = new Vector2(_transform.rotation.eulerAngles.y - 30, _transform.rotation.eulerAngles.y + 30);
            ResetMouse(_transform.rotation.eulerAngles.y, _transform.rotation.eulerAngles.x);
            transform.rotation = _transform.rotation;
        }
        
       

        public void ResetRotationLock()
        {
            m_xLimiter = false;
        }
    }
}
