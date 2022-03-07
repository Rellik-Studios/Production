using rachael;
using UnityEngine;

namespace Himanshu
{
    public class Objective : MonoBehaviour, IInteract
    {
        [SerializeField] private GameObject m_dissapearingDoor;
        
        public void Execute(PlayerInteract _player)
        {
            m_dissapearingDoor.SetActive(false);
            gameObject.SetActive(false);
        }

        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
        }
    }
}
