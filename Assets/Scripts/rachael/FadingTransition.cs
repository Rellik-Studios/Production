using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class FadingTransition : MonoBehaviour
    {
        [FormerlySerializedAs("doorframe")] [SerializeField] GameObject m_doorframe;
        [FormerlySerializedAs("door")] [SerializeField] GameObject m_door;
        [FormerlySerializedAs("wall")] [SerializeField] GameObject m_wall;
        [FormerlySerializedAs("materialDoor")] [SerializeField] Material m_materialDoor;
        [FormerlySerializedAs("materialWall")] [SerializeField] Material m_materialWall;
    
        private bool m_fadeOut = false;
        private bool m_fadeIn = false;
        [FormerlySerializedAs("fadeSpeed")] public float m_fadeSpeed = 1.0f;

        // Start is called before the first frame update
        void Start()
        {
            Color wallColor = this.m_wall.GetComponent<Renderer>().material.color;
            wallColor = new Color(wallColor.r, wallColor.g, wallColor.b, 0.0f);

            m_wall.GetComponent<Renderer>().material.color = wallColor;
        
        
            //wall.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {

            if(m_fadeOut)
            {
                Color outDoorColor = this.m_doorframe.GetComponent<Renderer>().material.color;
                Color inDoorColor = this.m_door.GetComponent<Renderer>().material.color;
                Color wallColor = this.m_wall.GetComponent<Renderer>().material.color;

                float fadeOutAmount = outDoorColor.a - (m_fadeSpeed * Time.deltaTime);
                float fadeInAmount = wallColor.a + (m_fadeSpeed * Time.deltaTime);

                outDoorColor = new Color(outDoorColor.r, outDoorColor.g, outDoorColor.b, fadeOutAmount);
                inDoorColor = new Color(inDoorColor.r, inDoorColor.g, inDoorColor.b, fadeOutAmount);
                wallColor = new Color(wallColor.r, wallColor.g, wallColor.b, fadeInAmount);


                m_doorframe.GetComponent<Renderer>().material.color = outDoorColor;
                m_door.GetComponent<Renderer>().material.color = inDoorColor;
                m_wall.GetComponent<Renderer>().material.color = wallColor;

                if (outDoorColor.a <=0)
                {
                    //transforming the transparent material to opaque material
                    this.m_wall.GetComponent<Renderer>().material = m_materialWall;
                    m_fadeOut = false;
                    m_door.SetActive(false);
                    Destroy(this);
                }
            }
        }
        public void FadeOutObject()
        {
            m_fadeOut = true;
        }

        private void OnTriggerEnter(Collider _other)
        {
            if (_other.CompareTag("Player"))
            {
                //transforming the opaque material to transparent material
                this.m_doorframe.GetComponent<Renderer>().material = m_materialDoor;
                
                FadeOutObject();
                m_wall.SetActive(true);
                m_wall.tag = "EnemyBlocker";
            }
        }
    }
}
