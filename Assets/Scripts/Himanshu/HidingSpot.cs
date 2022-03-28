using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    
    /// <summary>
    /// Hiding Spot : 
    /// </summary>
    public class HidingSpot : MonoBehaviour
    {
        private List<HidingLocation> m_hidingSpots;
        private HidingLocation m_hidingIndex;
        private PlayerInteract m_player;
        public bool m_cupboard;

        [SerializeField] private Shader m_shader;
        [SerializeField] public Animator m_animator;

        [SerializeField] private bool m_loop;
        private Renderer m_cubeRenderer;

        #region AnimatorProperties

        // private bool aInfect
        // {
        //     get => m_animator.GetBool("infect");
        //     set => m_animator.SetBool("infect", value);
        // }
        // private bool aDisInfect
        // {
        //     get => m_animator.GetBool("disinfect");
        //     set => m_animator.SetBool("disinfect", value);
        // }
        //
        private float aSpeed
        {
            get => m_animator.GetFloat("speed");
            set => m_animator.SetFloat("speed", value);
        }

        public bool aOpen
        {
            get
            {
                if(m_animator != null)
                    return m_animator.GetBool("open");
                else
                    Debug.Log($"{name} does not have an animator attached");
                return false;
            }
            set
            {
                if(m_animator != null)
                    m_animator.SetBool("open", value);
                else
                    Debug.Log($"{name} does not have an animator attached");
            }
        }

        #endregion

        public bool isUsed => m_player != null;

        [SerializeField] private float m_distortionValue;
        public float distortionValue
        {
            get => m_distortionValue;
            set
            {
                m_distortionValue = value;
                if(m_cupboard)
                    transform.Find("Cube").GetComponent<Renderer>().material.SetFloat("DistortionLevel", value);
                else
                    transform.Find("GFX").Find("Cube").GetComponent<Renderer>().material.SetFloat("DistortionLevel", value);
            }
        }

        public bool isActive
        {
            get;
            set;
        }
        
        public int hidingIndex
        {
            get
            {
                var index = 0;
                foreach (var hidingSpot in m_hidingSpots)
                {
                    if (hidingSpot == m_hidingIndex)
                        break;
                    index++;
                }

                return index;
            }
            set
            {
                m_hidingIndex.TurnOff();
               
                if(m_loop)
                    m_hidingIndex = m_hidingSpots[value < 0 ? 0 : value > m_hidingSpots.Count - 1 ? m_hidingSpots.Count - 1 : value];
                else
                    m_hidingIndex = m_hidingSpots[value < 0 ? m_hidingSpots.Count - 1 : value > m_hidingSpots.Count - 1 ? 0 : value];

                

                if (m_player != null)
                {
                    Debug.Log("Hide");
                    //m_player.SetPositionAndRotation(m_hidingSpots[m_hidingIndex]);
                    m_player.transform.position = new Vector3((m_hidingIndex.transform.position + m_hidingIndex.actualForward * 3f).x, m_player.transform.position.y, (m_hidingIndex.transform.position + m_hidingIndex.actualForward * 3f).z);
                    //m_player.transform.rotation = m_hidingIndex.transform.rotation;
                    m_player.GetComponent<CharacterController>().enabled = false;
                }
                m_hidingIndex.TurnOn();
            }
        }

        public bool infectStared { get; set; }

        private void Start()
        {
            //m_animator = GetComponent<Animator>();
            isActive = true;
            m_hidingSpots = new List<HidingLocation>();

            if (transform.Find("Cube") != null)
            {
                transform.Find("Cube").GetComponent<Renderer>().material = new Material(m_shader);
                m_cubeRenderer = transform.Find("Cube").GetComponent<Renderer>();
            }
            else
            {
                // transform.Find("GFX").Find("Cube").GetComponent<Renderer>().material = new Material(m_shader);
                // m_cubeRenderer = transform.Find("GFX").Find("Cube").GetComponent<Renderer>();
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).gameObject.activeInHierarchy && transform.GetChild(i).GetComponent<HidingLocation>() != null)
                    m_hidingSpots.Add(transform.GetChild(i).GetComponent<HidingLocation>());
            }
        }

        

        // private void DisInfect(float _time)
        // {
        //     infectStared = false;
        //     
        //     aDisInfect = true;
        //     aInfect = false;
        //     aSpeed = 1f / _time;
        //     
        //     StartCoroutine(eInfect(true, _time));
        // }

        public void Disable()
        {
            
            m_player.m_followCam.ResetMouse();
            m_player.m_followCam.m_mouseInput = true;
            m_player = null;
            m_hidingIndex.TurnOff();
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.U))
            // {
            //     //m_cubeRenderer.SetFloat("DistorionLevel", 0.02f);
            //     //transform.Find("Cube").GetComponent<Renderer>().material.SetFloat("DistortionLevel", 0.02f);
            //     distortionValue = m_distortionValue;
            // }
            
            if (m_player !=  null)
            {
                if (!isActive)
                {
                    m_player.Kick();
                    m_player = null;
                    return;
                }
                
                StartCoroutine(IndexHandler());
            }
        }

        private IEnumerator IndexHandler()
        {
            var movement = m_player.m_playerInput.movement;

            if (Input.GetKeyDown(KeyCode.A))
            {
                hidingIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                hidingIndex++;
            }

            yield return null;
        }

        // public void Infect()
        // {
        //     
        //     // aDisInfect = false;
        //     // aInfect = true;
        //     aSpeed = 1f / 3f;
        //
        //     infectStared = true;
        //     StartCoroutine(eInfect(false, 3f));
        // }

        IEnumerator eInfect(bool _state, float _time)
        {
            //Gradually apply Distortion here
            var counter = 0f;
            
            while (_state ? distortionValue > 0f : distortionValue < 0.02f) 
            {
                distortionValue = Mathf.Lerp(distortionValue,_state ? -0.001f : 0.021f, Time.deltaTime * _time);
                counter += Time.deltaTime / _time;
                Debug.Log(distortionValue);
                yield return null;
            }
            isActive = _state;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent<Renderer>(out Renderer _renderer))
                    _renderer.material.color = _state? Color.white : Color.red;
            }
            
        }

        public void BeginHide(HidingLocation _hidingLocation, PlayerInteract _player)
        {
            if(m_hidingSpots.Count > 1)
                OneTimeText.SetText("press 'A' and 'D' to move between hiding points", () => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(0));
            // Time.timeScale = 0.1f;
            // this.Invoke(()=>Time.timeScale  = 1f, 2, true);
            m_player = _player;
            m_player.Hide(this);
            m_player.m_followCam.m_mouseInput = false;
                m_hidingIndex = _hidingLocation;
            var index = 0;
            foreach (var hidingSpot in m_hidingSpots)
            {
                if (hidingSpot == m_hidingIndex)
                    break;
                index++;
            }
            if(m_cupboard)
                this.Invoke(()=>hidingIndex = index, 1.1f);
            else
                hidingIndex = index;
        }
    }
}