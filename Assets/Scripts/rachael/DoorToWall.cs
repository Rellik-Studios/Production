using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class DoorToWall : MonoBehaviour
    {
        [FormerlySerializedAs("door")] [SerializeField] GameObject m_door;
        [FormerlySerializedAs("wall")] [SerializeField] GameObject m_wall;

        //private bool fadeOut = false;
        //public float fadeSpeed = 1.0f;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //if(Input.GetKeyDown(KeyCode.A))
            //{
            //    FadeOutObject();
            //}
            //if(fadeOut)
            //{
            //    Color outDoorColor = this.GetComponent<Renderer>().material.color;
            //    //Color inDoorColor = GetComponentInChildren<Renderer>().material.color;
            //    float fadeAmount = outDoorColor.a - (fadeSpeed * Time.deltaTime);

            //    outDoorColor = new Color(outDoorColor.r, outDoorColor.g, outDoorColor.b, fadeAmount);
            //   // inDoorColor = new Color(inDoorColor.r, inDoorColor.g, inDoorColor.b, fadeAmount);
            

            //    this.GetComponent<Renderer>().material.color = outDoorColor;
            //    //GetComponentInChildren<Renderer>().material.color = inDoorColor;

            //    if(outDoorColor.a <=0)
            //    {
            //        fadeOut = false;
            //    }
            //}
        }

        //public void FadeOutObject()
        //{
        //    fadeOut = true;
        //}
        public void FadeHubWall()
        {
            m_wall.SetActive(true);
        }
        public void TransformDoorToWall()
        {
            m_door.SetActive(false);
            m_wall.SetActive(true);
        }
        public void TransformWallToDoor()
        {
            m_door.SetActive(true);
            m_wall.SetActive(false);
        }
        private void OnTriggerEnter(Collider _other)
        {
            if (_other.GetComponent<PlayerInteract>() != null)
            {
                m_door.SetActive(false);
                m_wall.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
