using rachael;
using UnityEngine;
namespace Himanshu
{
    public class JukeBox : MonoBehaviour, IInteract
    {
        
        public AudioClip[] m_clips;
        public AudioSource m_audioSource;
        private int m_index = 0;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }
        
        public void Execute(PlayerInteract _player)
        {
            m_audioSource.clip = m_clips[m_index];
            m_audioSource.Play();
            m_index++;
            if (m_index >= m_clips.Length)
            {
                m_index = 0;
            }
        }
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }
    }
}
