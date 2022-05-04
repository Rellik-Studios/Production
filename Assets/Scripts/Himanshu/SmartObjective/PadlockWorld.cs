using rachael;
using UnityEngine;

namespace Himanshu.SmartObjective
{
    public class PadlockWorld : MonoBehaviour, IInteract
    {
        public GameObject padlock;


        public void Execute(PlayerInteract _player)
        {
            padlock.SetActive(!padlock.activeSelf);
        }

        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }
    }
}