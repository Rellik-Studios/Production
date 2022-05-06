using rachael;
using rachael.FavorSystem;
using UnityEngine;

namespace Himanshu
{
    public class Objective : MonoBehaviour, IInteract
    {
        [SerializeField] private GameObject m_dissapearingDoor;
        [SerializeField] private GameObject m_unlockVFX;

        private AudioSource m_audioSource;
        public bool m_locked = false;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        public void Execute(PlayerInteract _player)
        {
            if(m_locked)    return;
            FavorSystem.startTimer = false;
            m_unlockVFX.SetActive(true);
            m_audioSource?.Play();
            m_dissapearingDoor.SetActive(false);
            gameObject.SetActive(false);
        }

        public void CanExecute(Raycast _raycast)
        {
            if (m_locked)
            {
                _raycast.m_indication.enabled = false;
                return;
            }
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
        }
    }
}
