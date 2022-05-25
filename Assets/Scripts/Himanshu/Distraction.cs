using System;
using Himanshu.SmartObjective;
using rachael;
using UnityEngine;
using UnityEngine.Events;

namespace Himanshu
{
    /// <summary>
    /// Used for Distracting the enemies
    /// Implements IInteract interface
    /// </summary>
    public class Distraction : MonoBehaviour, IInteract
    {
        private AudioSource m_audioSource;
        public bool m_playing;

        public bool m_DestroyAfterUse;

        public UnityEvent m_onExecute;

        public bool m_canDistract = true;

        /// <summary>
        /// playing : Stores if the distraction is used.
        /// Plays the audiocue when trigerred
        /// </summary>
        public bool playing
        {
            get => m_playing;
            set
            {
                m_playing = value;
                if(value)
                    m_audioSource.Play();
                else
                {
                    if(m_DestroyAfterUse) 
                        Destroy(this);
                    m_audioSource.Stop();
                }
                
            }
        }

        /// <summary>
        /// Unity Event Function
        /// </summary>
        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        
        /// <summary>
        /// Execute : Interface function, Plays the audioclip and distracts enemies
        /// </summary>
        /// <param name="_player">Reference to PlayerInteract instance</param>
        public void Execute(PlayerInteract _player)
        {
            
            
            if (!m_audioSource.isPlaying)
            {
                //m_audioSource.Play();
                m_onExecute?.Invoke();
                if(!m_canDistract) return;
                FindObjectOfType<Narrator>().madeSound = true;
                playing = true;
                this.Invoke(() => playing = false, m_audioSource.clip.length);
            }
        }

        /// <summary>
        /// Interface Function, Used to change the sprite of interaction
        /// </summary>
        /// <param name="_raycast">Reference to Raycast script from the MainCamera</param>
        public void CanExecute(Raycast _raycast)
        {
            if (!m_canDistract && !FindObjectOfType<PlayerSmartObjectives>().m_hasNotes) {
                if (_raycast.m_indication != null)
                    _raycast.m_indication.enabled = false;
                return;
            }
            
            
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");        
            
        }
        
        /// <summary>
        /// Unity Event Function,
        /// Plays the audiocue when player collides
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (!m_audioSource.isPlaying && other.GetComponent<PlayerInteract>() != null)
            {
                if(!m_canDistract)    
                    return;
                m_onExecute?.Invoke();
                FindObjectOfType<Narrator>().madeSound = true;

                //m_audioSource.Play();
                if(!m_canDistract) return; 
                playing = true;
                //this.Invoke(() => playing = false, m_audioSource.clip.length);
                
            }
        }
    }
}