using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class RespawnManager : MonoBehaviour
    {
        [FormerlySerializedAs("PlayerObject")] public GameObject m_playerObject;
        [SerializeField] Transform m_playerTransform;
        Transform m_currentTransform;
        [FormerlySerializedAs("cam")] [SerializeField] GameObject m_cam;
        private Vector3 m_position;
        private Quaternion m_rotation;
        // Start is called before the first frame update
        void Start()
        {
            if (m_playerTransform != null)
            {
                m_currentTransform = m_playerTransform;
                m_position = m_currentTransform.position;
                m_rotation = m_currentTransform.rotation;
            }

        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    GetComponent<CharacterController>().enabled = false;
            //    gameObject.transform.position = position;
            //    if (cam != null)
            //    {
            //        cam.transform.rotation = rotation;
            //        Debug.Log("Here");
            //        cam.GetComponent<PlayerFollow>()?.ResetMouse(rotation.eulerAngles.y, rotation.eulerAngles.x);

            //    }
            //    GetComponent<CharacterController>().enabled = true;
            //}
        }

        public void Respawn()
        {
            GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = m_position;
            if (m_cam != null)
            {
                m_cam.transform.rotation = m_rotation;
                Debug.Log("Here");
                m_cam.GetComponent<PlayerFollow>()?.ResetMouse(m_rotation.eulerAngles.y, m_rotation.eulerAngles.x);

            }
            GetComponent<CharacterController>().enabled = true;
        }

        public void Teleport(Transform _location)
        {
        
            GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = _location.position;
            if (m_cam != null)
            {
                m_cam.transform.rotation = m_rotation;
                Debug.Log("Here");
                m_cam.GetComponent<PlayerFollow>()?.ResetMouse(m_rotation.eulerAngles.y, m_rotation.eulerAngles.x);

            }
            GetComponent<CharacterController>().enabled = true;
        }
        public void SetPosition(Transform _checkpoint)
        {
            m_position = _checkpoint.position;
            m_rotation = _checkpoint.rotation;
        }

    }
}
