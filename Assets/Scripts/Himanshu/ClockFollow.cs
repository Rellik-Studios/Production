using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Himanshu
{

    public class ClockFollow : MonoBehaviour
    {
        private GameObject m_secondsHand;
        private GameObject m_hourHand;
        private GameObject m_minuteHand;
        private GameObject m_centre;
        private Camera m_camera;

        private float hourRotation => m_hourHand.transform.rotation.eulerAngles.z;
        private float minuteRotation => ((hourRotation/6f) % 5) * 72f; 
        
        void Start()
        {
            m_camera = Camera.main;
            m_centre = transform.Find("Center").gameObject;
            m_secondsHand = transform.Find("S").gameObject;
            m_hourHand = transform.Find("H").gameObject;
            m_minuteHand = transform.Find("M").gameObject;
        }

        void Update()
        {
            m_secondsHand.transform.rotation = Quaternion.Euler(0f, 0f,
                m_secondsHand.transform.rotation.eulerAngles.z - Time.deltaTime * 6f);
            
            Vector2 positionOnScreen = m_camera.WorldToViewportPoint (m_hourHand.transform.position);
         
            Vector2 mouseOnScreen = (Vector2)m_camera.ScreenToViewportPoint(Input.mousePosition);
         
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
 
            m_hourHand.transform.rotation =  Quaternion.Euler(0f,0f,angle + 90f);
            m_minuteHand.transform.rotation = Quaternion.Euler(0f, 0f, minuteRotation);
//            Debug.Log(minuteRotation);
            
        }
 
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}
