using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael.SaveSystem
{
    public class SaveCanvas : MonoBehaviour
    {
        [FormerlySerializedAs("saveCanvas")] [SerializeField] GameObject m_saveCanvas;
        [FormerlySerializedAs("playerObject")] [SerializeField] GameObject m_playerObject;
        [FormerlySerializedAs("camObject")] [SerializeField] GameObject m_camObject;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
            
                m_saveCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                DisbleMovement();
            }
        }
        public void DisbleMovement()
        {
            m_playerObject.GetComponent<CharacterController>().enabled = false;
            m_camObject.GetComponent<PlayerFollow>().enabled = false;
        }
        public void EnableMovement()
        {
            m_playerObject.GetComponent<CharacterController>().enabled = true;
            m_camObject.GetComponent<PlayerFollow>().enabled = true;

        }

    }
}