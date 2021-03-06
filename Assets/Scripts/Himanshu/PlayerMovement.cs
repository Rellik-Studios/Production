using System;
using System.Collections;
using rachael.FavorSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Himanshu
{

    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController m_characterController;
        private Rigidbody m_rigidbody;
        private PlayerInput m_playerInput;

        [SerializeField] private float m_speed = 5.0f;
        [SerializeField] private float m_gravity = 10.0f;
        private Vector3 m_playerVelocity = Vector3.zero;
        [SerializeField] private float m_jumpHeight;
        [SerializeField] private float m_groundDistance = 0.1f;
        private bool m_isGrounded;
        public bool canMoveUnscaled {
            get {
                if (FindObjectOfType<FavorSystem>() != null)
                    return FindObjectOfType<FavorSystem>().m_timeStop && FindObjectOfType<FavorSystem>().m_continueCounting;
                return false;
            }
        }
        public bool crouching {
            get {
                if (m_playerInput.m_crouching) {
                    m_characterController.height = 2f;
                    m_characterController.center = new Vector3(0, -1f, 0);
                } 
                else {
                    m_characterController.height = 4.5f;
                    m_characterController.center = new Vector3(0, 0.25f, 0);
                }
                
                return m_playerInput.m_crouching;
            }
            set {
                //GetComponent<CharacterController>().height = value ? 2f : 4f;
                if((value && !Physics.Raycast(m_eye.position, transform.up, 3f, Physics.AllLayers, QueryTriggerInteraction.Ignore)) || !value)
                    m_playerInput.m_crouching = value;
                    
            }
        }

        public float m_currentSpeed;  
        [SerializeField] private AudioClip m_breathingClip;
        [SerializeField] private float m_maxSprintTimer;
        private float m_sprintTimer;
        [SerializeField] private Image m_sprintImage;
        private Transform m_eye;
        private float m_sprintNarratorTimer = 40f;
        private AudioSource m_audioSource;
        [SerializeField] private LayerMask m_groundMask;
        public bool m_stopWhileStarting;
        private float calculatedDeltaTime => (canMoveUnscaled ? Time.unscaledDeltaTime : Time.deltaTime);

        private float sprintTimer
        {
            get => m_sprintTimer;
            set
            {
                
                if (value < m_maxSprintTimer / 10.0f && value > 0.1f && m_sprintTimer > value)
                {
                    if (m_sprintNarratorTimer < 0f)
                    {
                        m_sprintNarratorTimer = 40f;
                        if(FindObjectOfType<Narrator>()!=null)
                            FindObjectOfType<Narrator>().breathing = true;
                    }

                    //m_audioSource.volume = 0.5f;
                    if(!m_audioSource.isPlaying)
                        m_audioSource.PlayOneShot(m_breathingClip, 0.2f);
                    
                }
                m_sprintTimer = value;
                if (m_sprintImage != null)
                    m_sprintImage.fillAmount = m_sprintTimer / m_maxSprintTimer;
            }
        }
        
        public Vector3 calculatedPosition {
            get => m_eye.transform.position;
        }

        public Vector3 crouchPosition {
            get => m_eye.transform.GetChild(0).position;
        }

        private IEnumerator Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            sprintTimer = m_maxSprintTimer;
            m_playerInput = GetComponent<PlayerInput>();
            m_rigidbody = transform.Find("GFX").gameObject.GetComponent<Rigidbody>();
            m_characterController = GetComponent<CharacterController>();

            m_eye = transform.Find("Eye");
            if (m_stopWhileStarting)
            {
                m_characterController.enabled = false;
                yield return new WaitForSeconds(1.1f);
                m_characterController.enabled = true;
                m_stopWhileStarting = false;
            }
        }


        private void Update()
        {
            m_isGrounded = Physics.Raycast(transform.position, -Vector3.up, m_groundDistance, m_groundMask, QueryTriggerInteraction.Ignore);
            Movement();
            Jump();
            m_sprintNarratorTimer -= calculatedDeltaTime;
        }

        private void Jump()
        {
            m_playerVelocity.y -= m_gravity;
            if (m_isGrounded) m_playerVelocity.y = 0f;
            //if (m_playerInput.jump && m_isGrounded) 
            //    m_playerVelocity.y += Mathf.Sqrt(m_jumpHeight * 3.0f * m_gravity);
            m_characterController.Move(m_playerVelocity * calculatedDeltaTime);
        }

        private bool m_isSliding;
        private bool m_isSCorouting = false;
        [SerializeField] private AudioSource m_footstepSound;
        private void Movement()
        {
            IEnumerator Slide()
            {
                if (m_playerInput.sprint && sprintTimer > 0f) {
                    
                    if (!m_playerInput.m_crouching && !m_isSCorouting) {
                        m_isSCorouting = true;
                        yield return new WaitUntil(() => m_playerInput.m_crouching);

                        if (!m_playerInput.sprint || sprintTimer <= 0.01f) {
                            m_isSCorouting = false;
                            yield break;
                        }

                        // // Debug.Log("Slide BABYY");
                        m_isSliding = true;
                        int counter = 0;
                        while (counter < 25) {
                            m_characterController.Move(transform.forward * 0.1f);
                            yield return null;
                            counter++;
                        }
                        m_isSliding = false;
                        yield return new WaitUntil(() => !m_playerInput.sprint || sprintTimer <= 0.01f);
                        
                        m_isSCorouting = false;
                    }
                    
                    else if (!m_isSCorouting) {
                        //m_isSCorouting = true;
                        crouching = false;
                    }
                    
                }
            }
            
            //StartCoroutine(Slide());

            
            
            var movement = m_playerInput.movement.x * transform.right + m_playerInput.movement.z * transform.forward;
            if (m_characterController.enabled)
                m_characterController.Move(movement * (m_speed * (crouching ? 0.5f : 1f) * ((m_playerInput.sprint && sprintTimer > 0f && !crouching) ? 1.75f : 1.0f)  * calculatedDeltaTime));

            if (m_footstepSound != null && m_footstepSound.pitch > 1.3f)
                m_footstepSound.pitch = 1.2f;
            if (movement.magnitude > 0.01f && m_characterController.velocity.magnitude > 0.5f && !crouching && !m_footstepSound.isPlaying) {
                m_footstepSound?.Play();
                
            }
            
            if(movement.magnitude < 0.01f && m_footstepSound.isPlaying)
                m_footstepSound?.Stop();
           

            
            if (m_playerInput.sprint && sprintTimer > 0f && m_playerInput.movement.magnitude > 0f && !m_isSliding) {
                crouching = false;
                sprintTimer -= calculatedDeltaTime / 2f;
                if (m_footstepSound != null && m_footstepSound.pitch < 1.4f)
                    m_footstepSound.pitch = 1.5f;
            }
            else if (sprintTimer < m_maxSprintTimer)
            {
                sprintTimer += calculatedDeltaTime / 4f;
            }
            m_currentSpeed = m_characterController.velocity.magnitude;
             //// // Debug.Log(m_characterController.velocity.magnitude);
        }
    }

}
