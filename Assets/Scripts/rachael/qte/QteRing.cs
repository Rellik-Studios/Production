using System.Collections;
using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace rachael.qte
{
    public class QteRing : MonoBehaviour
    {
        [FormerlySerializedAs("playerRing")] public GameObject m_playerRing;
        [FormerlySerializedAs("Canvas")] public GameObject m_canvas;
        [FormerlySerializedAs("DisplayBox")] public GameObject m_displayBox;

        bool m_isSpawn = true;

        bool m_isDecided = false;

        int m_numOfPass = 0;
        int m_numOfFail = 0;
        public bool m_result;
        private GameObject m_ring;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 0;
            m_numOfPass = 0;
            m_numOfFail = 0;
            m_isSpawn = true;
            m_isDecided = false;
            SetOrigin();
        }
    

        private void Update()
        {
            if (m_ring == null)
                return;

            if (m_ring.GetComponent<GrowingRing>().IsRingInisde()) 
            {
                m_displayBox.GetComponent<Text>().text = "Its in a ring";
                if (Input.GetKeyDown(KeyCode.P))
                {
                    m_numOfPass++;
                    //Give the player a chance
                    Destroy(m_ring.gameObject);
                    m_isDecided = true;
                    Pass();
                }

            }
            else
            {
                m_displayBox.GetComponent<Text>().text = "Its not in a ring";
                if (Input.GetKeyDown(KeyCode.P))
                {
                    m_numOfFail++;
                    //don't give them a chance
                    Destroy(m_ring);

                    m_isDecided = true;
                    Fail();
                }
            }
        }

        // Update is called once per frame
   
        void LateUpdate()
        {
            if(m_isDecided)
            {
                if(m_numOfPass >= 1)
                {
                    Debug.Log("You get second chance");
                    Time.timeScale = 1f;
                    m_result = true;
                    m_canvas.SetActive(false);
                }
                else if(m_numOfFail >= 1)
                {
                    m_result = false;
                    Debug.Log("You dont get second chance");
                    Time.timeScale = 1f;
                    m_canvas.SetActive(false);
                }
                else
                {
                    SetNewPosition();
                    m_isSpawn = true;
                }
                m_isDecided = false;
            }

            if(m_isSpawn)
                StartCoroutine(SpawnRing());
            //playerRing.rectTransform.sizeDelta = new Vector2(playerRing.rectTransform.sizeDelta.x  + (speed), playerRing.rectTransform.sizeDelta.y + (speed));
            //if(OuterRing() && InnerRing())
            //{
            //    Debug.Log("yes");
            //}
        }

        IEnumerator SpawnRing()
        {
            m_isSpawn = false;
            float counter = 0f;
            yield return (StartCoroutine(Utility.WaitForRealSeconds(3f)));
            m_ring = Instantiate(m_playerRing, m_canvas.transform);

            m_ring.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(GetComponent<Image>().rectTransform.anchoredPosition.x, GetComponent<Image>().rectTransform.anchoredPosition.y);
        }
    


        private void Pass()
        {
        
        }

        private void Fail()
        {
        
        }

        void SetOrigin()
        {
            GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(0,0);
            GetComponent<Image>().rectTransform.sizeDelta = new Vector2(1550.0f , 1550.0f);
        }
        void SetNewPosition()
        {

            float posx = Random.Range(-200, 200);
            float posy = Random.Range(-300, 300);
            float size = Random.Range(1000, 1551);

            GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(posx,posy);
            GetComponent<Image>().rectTransform.sizeDelta = new Vector2(size, size);


        }
        public void LateRing()
        {
            m_isDecided = true;
            m_numOfFail++;
        }

        //public bool OuterRing()
        //{
        //    return (playerRing.rectTransform.sizeDelta.x >= 1350.0f && playerRing.rectTransform.sizeDelta.y >= 1350.0f);

        //}
        //public bool InnerRing()
        //{
        //    return (playerRing.rectTransform.sizeDelta.x <= 1550.0f && playerRing.rectTransform.sizeDelta.y <= 1550.0f);

        //}
    }
}
