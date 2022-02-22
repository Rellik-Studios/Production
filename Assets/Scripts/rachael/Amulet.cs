using Himanshu;
using UnityEngine;

namespace rachael
{
    public class Amulet : MonoBehaviour, IInteract
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.right, Time.deltaTime * 50f); 
        }

        public void Execute(PlayerInteract _player)
        {

            _player.m_hasAmulet = true;
            _player.m_amulet.enabled = true;
            Destroy(gameObject);


        }

        public void CanExecute(Raycast _raycast)
        {
            if ( _raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
            //return;
        }
    }
}
