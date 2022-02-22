using Himanshu;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class Gauge : MonoBehaviour
    {
        [FormerlySerializedAs("playerinteract")] public PlayerInteract m_playerinteract;
        [FormerlySerializedAs("gauge")] public GameObject m_gauge;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //if(playerinteract.dangerBarVal ==0)
            //{
            //    gauge.SetActive(false);
            //}
            //else
            //{
            //    gauge.SetActive(true);
            //}
        }
    }
}
