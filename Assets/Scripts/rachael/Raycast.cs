using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace rachael
{
    public class Raycast : MonoBehaviour
    {
        [FormerlySerializedAs("raydist")] [SerializeField] private float m_raydist = 10.0f;

        public GameObject objectInFront
        { 
            get; 
            private set;
        }
        
        [FormerlySerializedAs("player")] public GameObject m_player;
        public Image m_indication;
        [FormerlySerializedAs("crosshair")] public Image m_crosshair;
        public bool m_doOnce;

        /// <summary>
        /// Start is a Unity Event function which is called at begining of each play,
        /// In this class it sets the objectInFront to null
        /// </summary>
        void Start()
        {
            objectInFront = null;
        }

        /// <summary>
        /// Update is a Unity function which is called every frame,
        /// In this class it generates a raycast and updates UI based on the object that it hits.
        /// </summary>
        void Update()
        {
            //detecting all hits from raycast
            Debug.DrawRay(transform.position, transform.forward * m_raydist, Color.green, 0.1f);
            int layer = 3;
            int layerMask = 2 << layer;
            layerMask = ~layerMask;
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, m_raydist, layerMask, QueryTriggerInteraction.Ignore);
            
            if (hits.Length == 0)
            {
                objectInFront = null;
                CrosshairChange(false);
                m_doOnce = false;
                if (m_indication != null)
                    m_indication.enabled = false;
                return;
            }

            //if there is a hit take the first one as a default
            RaycastHit closestHit = hits[0];
            if (m_indication != null)
                m_indication.enabled = true;

            //find the closest hit
            foreach (var hit in hits)
            {
                if (closestHit.distance > hit.distance)

                    closestHit = hit;
            }
            objectInFront = closestHit.collider.gameObject;
            ObjectIdentify();

        }
        
        /// <summary>
        /// ObjectIdentify: Identifies the object in front and applies the desired UI change
        /// </summary>
        void ObjectIdentify()
        {
            if (objectInFront.GetComponent<IInteract>() != null)
            {
                objectInFront.GetComponent<IInteract>()?.CanExecute(this);
            }
            
            else
            {
                if (m_indication != null)
                {
                    m_indication.enabled = false;
                
                }
                CrosshairChange(false);
                m_doOnce = false;
                return;
                //show no UI
            }
        }

        /// <summary>
        /// CrosshairChange: Turns the crosshair on or off
        /// </summary>
        /// <param name="_on">The state</param>
        public void CrosshairChange(bool _on)
        {
            if(_on && !m_doOnce)
            {
                m_crosshair.color = Color.red;
            }
            else
            {
                m_crosshair.color = Color.white;
            }
        }
        
    
    }
}
