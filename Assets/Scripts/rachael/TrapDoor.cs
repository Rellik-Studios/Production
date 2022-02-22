using Himanshu;
using UnityEngine;

namespace rachael
{
    public class TrapDoor : MonoBehaviour
    {
        PlayerInteract m_player;
        public int m_assignedNum;
        // Start is called before the first frame update
        void Start()
        {
            m_player = GameObject.FindObjectOfType<PlayerInteract>();
        }

        // Update is called once per frame
        void Update()
        {
            //if(m_player.m_numOfPieces == m_assignedNum)
            {
                Destroy(gameObject);
            }
        }
    }
}
