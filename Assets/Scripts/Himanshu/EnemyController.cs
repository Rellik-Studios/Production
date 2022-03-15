using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt;
using Cinemachine;
using rachael.qte;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Himanshu
{
    
    /// <summary>
    /// EnemyController : Works alongside the Visual State Machine to provide functionality and store variables
    /// </summary>
    public class EnemyController : MonoBehaviour, IEnemy
    {

        public enum eDanger
        {
            white,
            yellow,
            red,
        }
        [SerializeField] private GameObject m_distortion;

        [SerializeField] private MaskChange m_mask;
        
        PlayerMovement m_player;

        public eDanger dangerLevel {
            get => m_dangerLevel;
            set
            {
                if (value != eDanger.white)
                    aDanger = true;
                else
                    aDanger = false;
                m_dangerLevel = value;
                m_mask.dangerLevel = value;
            }
        }
        public Distraction currentDistraction { get; set; }
        public bool qteHideResult => m_QTEHide.GetComponent<QteRing>().m_result;
        
        public bool qteResult => m_QTE.GetComponent<QTE>().m_result;

        [SerializeField] private float m_hearingRadius = 5f;
        // private float distortionValue
        // {
        //     get => m_distortion.GetComponent<Renderer>().material.GetFloat("DistortionSpeed");
        //     set => m_distortion.GetComponent<Renderer>().material.SetFloat("DistortionSpeed", value);
        //
        // }
        private bool m_toPatrol;

        private float aSpeed
        {
            get => m_animator.GetFloat("speed");
            set => m_animator.SetFloat("speed", value);
        }

        private bool aKill
        {
            get => m_animator.GetBool("kill");
            set => m_animator.SetBool("kill", value);
        }

        private bool aDanger
        {
            get => m_animator.GetBool("danger");
            set
            {
                if(value)
                    m_animator.SetTrigger("dangerT");
                m_animator.SetBool("danger", value);
            }
        }

        [SerializeField] private EnemyHeadTurn m_enemyHead;
        [SerializeField] private Transform m_headBone;
        [SerializeField] private Transform m_neck1Bone;
        [SerializeField] private Transform m_neck2Bone;
        [SerializeField] private List<Distraction> m_distractions;
        
        
        public float lookAngle
        {
            get => m_lookAngle;
            set
            {
                m_lookAngle = value;
                //m_headBone.gameObject.SetActive(false);
                m_headBone.transform.localRotation = Quaternion.Euler(value/ 2f, m_headBone.transform.localRotation.eulerAngles.y, m_headBone.transform.localRotation.eulerAngles.z);
                m_neck1Bone.transform.localRotation = Quaternion.Euler(value/ 4f, m_neck1Bone.transform.localRotation.eulerAngles.y,m_neck1Bone.transform.localRotation.eulerAngles.z);
                m_neck2Bone.transform.localRotation = Quaternion.Euler(value/ 4f, m_neck2Bone.transform.localRotation.eulerAngles.y,m_neck2Bone.transform.localRotation.eulerAngles.z);
            }
        }
        
        public bool toPatrol
        {
            get => m_toPatrol;
            set
            {
                if(GetComponent<StateMachine>().enabled)
                    m_toPatrol = value;
                else
                    m_toPatrol = false;
                
            }
        }

        public bool m_spotted;
        
        [Header("Attack")]
        [SerializeField] private float m_attackTimer;
        private float m_defaultAttackTimer;

        [Header("Patrol")] 
        [SerializeField] private float m_defaultPatrolWaitTime;

        [Header("Infect")] 
        [SerializeField] private List<HidingSpot> m_hidingSpotsToInfect;

        private HidingSpot m_hidingSpotToInfect;
        
        public UnityEvent m_command;
        [FormerlySerializedAs("frozen")] public bool m_frozen = false;
        public bool m_commandFinished { get; set; }

        [SerializeField] private bool m_noCommand;

        private List<Transform> m_patrolPoints = new List<Transform>();
        private int m_index;
        private NavMeshAgent m_agent;

        
        
        private RaycastHit[] m_hits = new RaycastHit[3];
        private float m_lookAngle;
        private Animator m_animator;
        public bool m_isRandomPatrol;
        public GameObject m_QTE;
        [SerializeField] private GameObject m_QTEHide;
        private bool m_coroutinePlaying;
        private bool m_canBeRed = false;
        private bool m_canChase = true;
        private eDanger m_dangerLevel = eDanger.white;


        private void Awake()
        {
            // m_QTE = FindObjectOfType<QTE>(true).gameObject;
            //
            // m_QTEHide = FindObjectOfType<QteRing>(true).gameObject;
        }

        private int index
        {
            get => m_index;
            set => m_index = value > m_patrolPoints.Count - 1 ? 0 : value < 0 ? m_patrolPoints.Count - 1 : value;
        }
        
        //Called through the Visual Script
        public void RunCommand()
        {
            m_command?.Invoke();

            if (m_noCommand)
                m_commandFinished = true;
        }
        private void Start()
        {
            m_player = FindObjectOfType<PlayerMovement>();
            m_animator = transform.Find("GFX").GetComponent<Animator>();
            m_agent = GetComponent<NavMeshAgent>();
            m_defaultAttackTimer = m_attackTimer;
            
            if (m_patrolPoints.Count == 0)
            {
                var patrolPointsParent = transform.Find("PatrolPoints");

                if (patrolPointsParent == null)
                {
                    return;
                    //throw new Exception($"Cannot Find Patrol Points in Enemy: {name}");
                }

                for (int i = 0; i < patrolPointsParent.childCount; i++)
                {
                    if(patrolPointsParent.GetChild(i).gameObject.activeInHierarchy)
                        m_patrolPoints.Add(patrolPointsParent.GetChild(i));
                }

                patrolPointsParent.SetParent(null);
            }
        }


        
        private IEnumerator UnFreeze()
        {
            yield return new WaitForSeconds(6f);
            m_frozen = false;
        }


        public bool AttackToChase()
        {
            var playerInteract = m_player.GetComponent<PlayerInteract>();
            if (playerInteract.m_invincible || playerInteract.m_debugInvincible)
                return true;

            return false;
        }

        private bool m_killing = false;
        //Called through the Visual Script
        public void Attack()
        {

            
            IEnumerator KillRoutine()
            {
                m_killing = true;
                var playerInteract = m_player.GetComponent<PlayerInteract>();
                if (playerInteract.m_invincible || playerInteract.m_debugInvincible)
                    yield break;
            
                if (dangerLevel == eDanger.yellow)
                {
                    if (m_canBeRed)
                        dangerLevel = eDanger.red;
                    else
                        yield break;
                }
            
            
                if(m_frozen || m_coroutinePlaying) yield break;
                var player = FindObjectOfType<PlayerInteract>();

                if (player.m_isDying) yield break;
            
                Time.timeScale = 0.1f;

                player.m_followCam.m_mouseInput = false;
                // player.m_followCam.transform.LookAt(transform);
                //player.m_followCam.transform.rotation = Quaternion.Euler(player.m_followCam.transform.rotation.eulerAngles.x, 0f, player.m_followCam.transform.rotation.eulerAngles.z);
            
                
                player.m_followCam.transform.LookAt(transform.position + new Vector3(0f, 4f, 0f));
                player.m_followCam.ResetMouse();

         
                aKill = true;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.position -= transform.forward * 3f;
                transform.LookAt(player.transform);

                player.m_isDying = true;

                yield return new WaitForSecondsRealtime(1.2f);
                
                player.Death();
                Time.timeScale = 1f;

            
            
                m_spotted = true;
                yield return null;
            }

            if (!m_killing)
                StartCoroutine(KillRoutine());


        }

        private void Update()
        {
            aSpeed = m_agent.velocity.magnitude;
        }


        public bool PatrolToDistraction()
        {
            if (m_distractions.Any(t => t.m_playing))
            {
                currentDistraction = m_distractions.First(t => t.m_playing);
                return true;
            }

            return false;
        }

        public void DistractionStart()
        {
            
            m_agent.stoppingDistance = 4f;
            m_agent.SetDestination(currentDistraction.transform.position);

            if ((m_agent.transform.position - currentDistraction.transform.position).magnitude < m_agent.stoppingDistance)
            {
                currentDistraction.playing = false;
                m_agent.stoppingDistance = 0f;
                currentDistraction = null;
            }
            
        }

        private IEnumerator eDistractionUpdate()
        {
            if (currentDistraction != null)
            {
               
                // yield return new WaitForSeconds(2f);
                //yield return new WaitUntil(() => m_agent.hasPath);
            }
            //
            // while (true)
            // {
            //     
            //     if (m_agent.remainingDistance < 3f)
            //     {
            //         currentDistraction.playing = false;
            //         m_agent.stoppingDistance = 0f;
            //         currentDistraction = null;
            //         break;
            //     }
            //     
            //     yield return null;
            // }

            yield return null;
        }

        //Called through the Visual Script
        public void ResetAttack()
        {
            m_attackTimer = m_defaultAttackTimer;
        }

        public void ChaseUpdate()
        {
            if ((m_player.transform.position - transform.position).magnitude < gameManager.Instance.m_triggerDistance && m_canBeRed)
            {
                dangerLevel = eDanger.red;
            }
            else
            {
                dangerLevel = eDanger.yellow;
            }
            // Physics.Raycast(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward, out m_hits[0], 20f);
            // Physics.Raycast(transform.position, transform.forward, out m_hits[1], 20f);
            // Physics.Raycast(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward, out m_hits[2], 20f);
            //
            // for (int i = 0; i <= 2; i++)
            // {
            //     if (m_hits[i].collider != null && m_hits[i].collider.gameObject.CompareTag("Player") && m_hits[i].collider.GetComponentInParent<CharacterController>().enabled)
            //     {
            //         return;
            //     }
            // }
            // m_spotted = false;
        }
        
        public void PatrolStart()
        {
            m_spotted = false;
            dangerLevel = eDanger.white;

            
            GetComponent<AudioSource>().Stop();
           
            if(m_patrolPoints.Count > 0)
                m_agent.SetDestination(m_patrolPoints[index].position);

        }

        public void PatrolUpdate()
        {
            if (m_agent.remainingDistance < 0.1f)
            {
                StartCoroutine(SetDestination());
            }
            
            Physics.Raycast(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward, out m_hits[0], 20f);
            Physics.Raycast(transform.position, transform.forward, out m_hits[1], 20f);
            Physics.Raycast(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward, out m_hits[2], 20f);
            
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward * 18f);
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(0f, transform.up) * transform.forward * 20f);
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward * 18f);

        }

        IEnumerator SetDestination()
        {

            yield return new WaitForSeconds(m_defaultPatrolWaitTime);
            
            if(m_patrolPoints.Count > 0 && !m_isRandomPatrol)
            {
                if (m_agent.remainingDistance < 0.1f)
                    m_agent.SetDestination(m_patrolPoints[index++].position);
            }
        
        
            else if(m_patrolPoints.Count >= 0)
                    if (m_agent.remainingDistance < 0.1f)
                        m_agent.SetDestination(m_patrolPoints[Random.Range(0, m_patrolPoints.Count - 1)].position);
            
            //Debug.Log(index);
        }

        public bool PatrolToChaseTransition()
        {
            if (!canChase) return false;

            var playerInteract = m_player.GetComponent<PlayerInteract>();
            if (playerInteract.m_invincible || playerInteract.m_debugInvincible)
                return false;    
            
            
            for (int i = 0; i <= 2; i++)
            {
                if (m_hits[i].collider != null && m_hits[i].collider.gameObject.CompareTag("Player") && m_hits[i].collider.GetComponentInParent<CharacterController>().enabled && !m_hits[i].collider.GetComponentInParent<PlayerInteract>().m_hiding)
                {
                    return true;
                }
            }

            var colliders = Physics.OverlapSphere(transform.position, m_hearingRadius * (m_player.crouching ? 1f : 3f));
            if (colliders.Any(t => t.CompareTag("Player") && !t.transform.parent.GetComponent<PlayerInteract>().m_hiding))
            {
                return true;
            }
            return false;
        }

        public bool canChase
        {
            get => m_canChase;
            set
            {
                m_canChase = value;
                if(!value)
                    this.Invoke(()=>canChase = true, 2f);
            }
            
        }

        public bool ChaseToPatrol()
        {
            
            var playerInteract = m_player.GetComponent<PlayerInteract>();
            if (playerInteract.m_invincible || playerInteract.m_debugInvincible)
                return true;    
            Physics.Raycast(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward, out m_hits[0], 20f);
            Physics.Raycast(transform.position, transform.forward, out m_hits[1], 20f);
            Physics.Raycast(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward, out m_hits[2], 20f);

            if (m_player.GetComponent<PlayerInteract>().m_hiding && dangerLevel == eDanger.yellow)
            {
                return true;
            }
            
            for (int i = 0; i <= 2; i++)
            {
                if (m_hits[i].collider != null && m_hits[i].collider.gameObject.CompareTag("Player"))
                {
                    return false;
                }
                if (m_hits[i].collider != null && m_hits[i].collider.gameObject.CompareTag("EnemyBlocker"))
                {
                    return true;
                }
            }

            return false;
        }
        public bool PatrolToInfectTransition()
        {

            for (int i = 0; i <= 2; i++)
            {
                
                if (m_hits[i].collider != null &&
                    m_hidingSpotsToInfect.Any(t => t.gameObject.transform == m_hits[i].collider.transform))
                {
                    m_hidingSpotToInfect = m_hits[i].collider.GetComponent<HidingSpot>();
                    if (m_hidingSpotToInfect.isActive)
                        if (Random.Range(0, 10) <= 4)
                            return true;
                        else
                            return false;
                    else
                        return false;
                }
            }
            
            return false;
        }

        public bool InfectToPatrolTransition()
        {
            return !m_hidingSpotToInfect.isActive;
        }

        public void ChaseEnter()
        {

            m_canBeRed = false;
            this.Invoke(()=>m_canBeRed = true, 3f);
            //StartCoroutine(eChaseEnter());
            dangerLevel = eDanger.yellow;
            m_spotted = true;
            GetComponent<AudioSource>().Play();
            m_enemyHead.m_look = true;
            m_enemyHead.m_lookTarget = FindObjectOfType<PlayerFollow>().transform;

            var speed = m_agent.speed;
            var angularSpeed = m_agent.angularSpeed;
            m_agent.speed = 0f;
            m_agent.angularSpeed = 0f;
            
            this.Invoke(() => m_enemyHead.m_look = false, 3f);

            
            this.Invoke(()=>
            {
                m_agent.speed = speed;
                m_agent.angularSpeed = angularSpeed;
                
            },
                1f);
            //this.Invoke(()=>lookAngle = 0, 2f);
        }

        IEnumerator eChaseEnter()
        {

            var angle = Vector3.SignedAngle( transform.position, FindObjectOfType<PlayerInteract>().transform.position, Vector3.up);

            //angle -= 180f;
            // while ( Mathf.Abs(lookAngle - angle) > 0.1f)
            // {
            //     //m_agent.SetDestination(transform.position);    
            //     // dir = transform.position - FindObjectOfType<PlayerMovement>().transform.position;
            //     // angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //     Debug.Log(Mathf.Abs(lookAngle - angle));
            //     lookAngle = Mathf.Lerp(lookAngle, angle, Time.deltaTime * 3);
            //     yield return null;
            // }

            yield return null;
            //m_agent.enabled = true;


        }
        public void InfectUpdate()
        {
            // m_agent.stoppingDistance = 4;
            // m_agent.SetDestination(m_hidingSpotToInfect.gameObject.transform.position);
            //
            // if (m_agent.remainingDistance <= m_agent.stoppingDistance && !m_hidingSpotToInfect.infectStared)
            // {
            //     //m_hidingSpotToInfect.Infect();
            // }
        }

        public void QTE()
        {
            StartCoroutine(eQTE());
        }
        private IEnumerator eQTE()
        {
            m_coroutinePlaying = true;
            yield return new WaitForSeconds(1f);
            if (qteResult)
            {
                //pushback Here
                m_frozen = true;
                toPatrol = true;
                this.Invoke(() => m_frozen = false, 3f);
                //FindObjectOfType<PlayerInteract>().m_hasAmulet = false;
            }
            else
                FindObjectOfType<PlayerInteract>().Death();
            m_coroutinePlaying = false;
        }
        
        public void QTEHide()
        {
            StartCoroutine(eQTEHide());
        }
        private IEnumerator eQTEHide()
        {
            m_coroutinePlaying = true;
            yield return new WaitForSeconds(1f);
            if (qteHideResult)
            {
                m_frozen = true;
                
                this.Invoke(() => m_frozen = false, 3f);
                // FindObjectOfType<PlayerInteract>().m_canQTEHide = false;
            }
            else
                FindObjectOfType<PlayerInteract>().Death();

            m_coroutinePlaying = false;
            yield return new WaitForSeconds(6f);
                        
            FindObjectOfType<PlayerInteract>().m_canQTEHide = true;
        }
        
    }

}