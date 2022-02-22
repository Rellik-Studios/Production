using UnityEngine;

namespace rachael
{
    public class Goal : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider _other)
        {
            Debug.Log("This is the end of the game!!");
        }
    }
}
