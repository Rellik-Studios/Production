using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bolt;
using rachael;
using rachael.FavorSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Himanshu
{
    public class PlayerInteract : MonoBehaviour
    {
        private bool m_spotted;
        public GameObject PauseScreen;
        public SceneChanger m_sceneManager;
        public Animator SaveProcess;

        public bool m_invincible = false;
        public bool m_debugInvincible = false;
        public PlayerFollow m_followCam;
        private Narrator m_narrator;
        public List<EnemyController> m_enemies;
        public  IEnumerator FillBar(Image _fillImage, float _time, int _dir = 1, float _waitTime = 0f)
        {
            yield return new WaitForSeconds(_waitTime);
            var time = 0f;

            while (time < _time)
            {
                time += Time.deltaTime;
                _fillImage.fillAmount += _dir * Time.deltaTime / _time;
                yield return null;
            }
        }

        private bool playingUp = false;
        private bool playingDown = false;
        private Coroutine m_fillRoutine;

        private int m_enemySpotNum;

        private int enemySpotNum
        {
            get => m_enemySpotNum;
            set
            {
                if (m_enemySpotNum != value)
                {
                    m_enemySpotNum = value;
                    spotted = m_enemySpotNum > 0;
                }
            }
        }
        
        public bool spotted
        {
            get => m_spotted;
            set
            {
                if (m_spotted != value)
                {
                    //if (value)
                    //{
                    //    StopCoroutine(m_fillRoutine);
                    //    m_fillRoutine = StartCoroutine(FillBar(m_danger, 8f / enemySpotNum));
                    //}
                    //else 
                    //{
                    //    StopCoroutine(m_fillRoutine);
                    //    m_fillRoutine = StartCoroutine(FillBar(m_danger, 3f, -1));
                    //}
                    m_spotted = value;
                    m_narrator.spottedLines = true;
                }
            }
        }

        [Header("Images")] 
        public Image m_timeRewind;
        public Image m_timeStop;
        //public Image m_amulet;
        
        
        public bool interactHold => m_playerInput.interactHold;

        private int m_bulletCount = 1;

        public int bulletCount
        {
            get => m_bulletCount;
            private set => m_bulletCount = value;
        }

        public bool timeReverse { get; set; }

        public bool cloudedVision
        {
            set
            {
                if (value)
                {
                    StopCoroutine(m_kickRoutine);
                    m_kickRoutine = StartCoroutine(eLensDistortion(2f));
                }
                else
                {
                    StopCoroutine(m_kickRoutine);
                    m_kickRoutine = StartCoroutine(eLensDistortion(1f, false));
                }
            } 
            
        }

        private float m_lensDistort;
        private float m_lensScale;
        private float lensDistort
        {
            get => m_lensDistort;
            set
            {
                m_lensDistort = value;
                if (GameObject.FindObjectOfType<Camera>().GetComponent<Volume>().profile
                    .TryGet(out LensDistortion _lensDistortion))
                {
                    _lensDistortion.intensity.value = m_lensDistort;
                    _lensDistortion.scale.value = m_lensScale;
                }
            }
        }

        private float lensScale
        {
            get => m_lensScale;
            set
            {
                m_lensScale = value;
            }
        }

        private IEnumerator eLensDistortion(float _time, bool _distort = true)
        {
            if (_distort)
            {
                var count = 0f;
                while (count < _time)
                {
                    count += Time.deltaTime;
                    lensScale = Mathf.Lerp(lensScale, 0.1f, Time.deltaTime * _time);
                    lensDistort = Mathf.Lerp(lensDistort,0.74f, Time.deltaTime * _time);
                    yield return null;
                }

                lensScale = 0.1f;
                lensDistort = 0.74f;
            }

            else
            {
                var count = 0f;
                while (count < _time || lensScale < 1f || lensDistort > 0f)
                {
                    count += Time.deltaTime;
                    lensScale = Mathf.Lerp(lensScale, 1.1f, Time.deltaTime * _time);
                    lensDistort = Mathf.Lerp(lensDistort,-0.1f, Time.deltaTime * _time);
                    yield return null;
                }

                lensScale = 1f;
                lensDistort = 0f;

            }
            
        }

        [Header("General")]
        public bool m_hiding;
        public int m_deathCount = 0;

        [Header("Audio")]
        [SerializeField] private AudioClip m_rewindAudio;
        [SerializeField] private AudioClip m_timeStopAudio;
        public PlayerInput m_playerInput;
        private Raycast m_raycast;
        private HidingSpot m_hidingSpot;
        private PlayerFollow m_playerFollow;
        private Coroutine m_kickRoutine;
        public bool m_canQTEHide = true;
        //public bool m_hasAmulet = true;

        public Dictionary<CollectableObject, Wrapper<int>> m_inventory;

        [SerializeField] private Image m_eye;
        private EnemyController.eDanger m_playerDanger = EnemyController.eDanger.white;
        [SerializeField] private Sprite m_whiteEye;
        [SerializeField] private Sprite m_yellowEye;
        [SerializeField] private Sprite m_redEye;
        
        public EnemyController.eDanger playerDanger
        {
            get => m_playerDanger;
            set
            {
                m_playerDanger = value;
                m_eye.sprite = value == EnemyController.eDanger.red ? m_redEye :
                    value == EnemyController.eDanger.yellow ? m_yellowEye : m_whiteEye;
            }
        }
        public List<CollectableObject> m_testInventory;
        public bool m_isDying = false;

        private void OnEnable()
        {
            m_enemies = GameObject.FindObjectsOfType<EnemyController>(true).ToList();
            m_inventory ??= new Dictionary<CollectableObject, Wrapper<int>>();
            Debug.Log(m_enemies.Count);
        }

        private void Start()
        {
            //m_testInventory = new List<CollectableObject>();
            //
            m_narrator = FindObjectOfType<Narrator>();
            m_kickRoutine = StartCoroutine(temp());
            m_fillRoutine = StartCoroutine(temp());
            m_playerFollow = GameObject.FindObjectOfType<PlayerFollow>();
            m_raycast = FindObjectOfType<Raycast>();
            m_playerInput = GetComponent<PlayerInput>();
            timeReverse = true;

        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    GetComponent<CharacterController>().enabled = false;
            //    StopCoroutine(m_fillRoutine);
            //    m_fillRoutine = StartCoroutine(m_timeRewind.FillBar(5));
            //}

            //if (Input.GetKeyUp(KeyCode.R))
            //{
            //    GetComponent<CharacterController>().enabled = true;
            //    StopCoroutine(m_fillRoutine);
            //    m_fillRoutine = StartCoroutine(m_timeRewind.FillBar(5, -1));
            //}

#if UNITY_EDITOR

            if(Input.GetKeyDown(KeyCode.Alpha0) && !m_isDying && (Math.Abs(Time.timeScale - 1) < 0.1f || FindObjectOfType<FavorSystem>().m_timeStop))
            {
                PauseScreen.SetActive(true);
                FindObjectOfType<FavorSystem>().m_continueCounting = false;
                //m_sceneManager.MainScene();
            }

#else
            if (Input.GetKeyDown(KeyCode.Escape)  && (Math.Abs(Time.timeScale - 1) < 0.1f || FindObjectOfType<FavorSystem>().m_timeStop))
            {
                PauseScreen.SetActive(true);
                FindObjectOfType<FavorSystem>().m_continueCounting = false;
                //m_sceneManager.MainScene();
            }

#endif




            playerDanger = m_enemies.Any((t) => t.dangerLevel == EnemyController.eDanger.red) 
                                                                                                                    ?  EnemyController.eDanger.red 
                         : m_enemies.Any((t) => t.dangerLevel == EnemyController.eDanger.yellow)
                                                                                                                    ? EnemyController.eDanger.yellow
                                                                                                                    : EnemyController.eDanger.white; 
            
            if (m_playerInput.interact && !m_hiding)
            {
                m_raycast.objectInFront?.GetComponent<IInteract>()?.Execute(this);
            }
            else if(m_playerInput.interact && Time.timeScale > 0)
            {
                Unhide();
            }

            if (m_playerInput.interact)
            {
                //m_raycastingTesting.ObjectInFront?.GetComponent<IEnemy>()?.Shoot(this);
            }

            

            //if (dangerBarVal == 1f && !LoseScreen.activeInHierarchy)
            //{
            //    LoseScreen.SetActive(true);
            //    m_enemies.All(t => t.toPatrol = true);
            //    dangerBarVal = 0;
            //    gameObject.SetActive(false);
            //    Cursor.lockState = CursorLockMode.None;
            //    //SceneManager.LoadScene(1);
            //}
            enemySpotNum = m_enemies.Count(_enemy => _enemy.m_spotted && _enemy.GetComponent<StateMachine>().enabled);
            //Debug.Log($"{enemySpotNum} enemies spotted you");
//            Debug.Log(m_enemySpotNum);

            
            
            
            //if (m_hiding && dangerBarVal < 0.1f)
            //    dangerBarVal = 0f;


        }

        public void Unhide()
        {
            if(!m_hiding) return;
            if (m_hidingSpot.m_cupboard)
            {
                StartCoroutine(eUnHide());
            }
            else
            {
                GetComponent<CharacterController>().enabled = true;
                GetComponent<PlayerMovement>().crouching = true;
                //GetComponent<CharacterController>().Move(m_playerFollow.transform.forward * 3f);
                m_playerFollow.ResetRotationLock();
                m_hidingSpot.Disable();
                m_hiding = false;
                m_hidingSpot = null;
            }
        }

        IEnumerator temp()
        {
            yield return null;
        }

        private IEnumerator eUnHide()
        {
            m_hidingSpot.aOpen = true;
            //m_hidingSpot.aClose = false;
            yield return new WaitForSeconds(1f);
            //transform.Translate(m_playerFollow.transform.forward * 3f);
            m_hidingSpot.aOpen = false;
            //m_hidingSpot.aClose = true;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<CharacterController>().Move(m_playerFollow.transform.forward * 3f);

            m_playerFollow.ResetRotationLock();
            m_hidingSpot.Disable();
            m_hiding = false;
            m_hidingSpot = null;            
        }

        private IEnumerator TimeHandler()
        {
            if (m_bulletCount > 0)
            {
                m_bulletCount--;
                Time.timeScale = 0.1f;

                yield return new WaitForSeconds(6f * Time.timeScale);

                Time.timeScale = 1f;
            }
        }

        public void Hide(HidingSpot _hidingSpot)
        {
            m_narrator.hidingLines = true;
            if (_hidingSpot.m_cupboard)
            {
                StartCoroutine(eHide(_hidingSpot));
            }
            else
            {
                m_hidingSpot = _hidingSpot;
                GetComponent<CharacterController>().enabled = false;
                Debug.Log("Hiding now");
                m_hiding = true;
            }
        }

        private IEnumerator eHide(HidingSpot _hidingSpot)
        {
            m_hiding = true;
            _hidingSpot.aOpen = true;
            //_hidingSpot.aClose = false;
            yield return new WaitForSeconds(2.5f);
            _hidingSpot.aOpen = false;
            //_hidingSpot.aClose = true;

            m_hidingSpot = _hidingSpot;
            GetComponent<CharacterController>().enabled = false;
            Debug.Log("Hiding now");
            
        }

        public void SetPositionAndRotation(Transform _transform, float _delay = 0)
        {
            StopAllCoroutines();
            StartCoroutine(eSetPositionAndRotation(_transform, _delay));
        }

        private IEnumerator eSetPositionAndRotation(Transform _transform,float _delay)
        {
            if(_delay > 0f)
                yield return new WaitForSeconds(_delay);
            GetComponent<CharacterController>().enabled = false;

            while (transform.rotation != _transform.rotation || transform.position != _transform.position)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _transform.rotation, Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, _transform.position, Time.deltaTime);
                m_playerFollow.SetRotation(_transform, new Vector2(-30, 30));
                yield return null;
            }
            yield break;
            //GetComponent<CharacterController>().enabled = true;
        }

        public void Collect(CollectableObject _collectableObject)
        {
            Debug.Log("Object Collected?");
            if (m_inventory.TryGetValue(_collectableObject, out Wrapper<int> _count))
            {
                _count.value++;
            }
            else
            {
                m_inventory.Add(_collectableObject, new Wrapper<int>(1));
                m_testInventory = m_inventory.Keys.ToList();
            }
        }

        public void Shoot()
        {
            GetComponent<AudioSource>().PlayOneShot(m_timeStopAudio);
            m_bulletCount -= 1;
                        
            this.Invoke(() => { bulletCount = 1; }, 6f);
            
            StartCoroutine(m_timeStop.FillBar(0.1f));
            StartCoroutine(m_timeStop.FillBar(6f, -1));
        }

        public void UnSpot()
        {
            //dangerBarVal = 0f;
            foreach (var enemy in m_enemies)
            {
                enemy.m_spotted = false;
            }
        }

        public void Kick()
        {
            //cloudedVision = true;
            this.Invoke(() => { cloudedVision = false; }, 2f);
            this.Invoke(() => { Unhide(); }, 3f);

        }

        public void PlayTimeRewind()
        {
            GetComponent<AudioSource>().PlayOneShot(m_rewindAudio);
        }
        public void Death()
        {
            m_deathCount++;
            PlayerPrefs.SetInt("Death", m_deathCount);

            
            if (!gameManager.Instance.isTutorialRunning)
            {
                FindObjectOfType<Fade>().color = Fade.eColor.black;
                
                this.Invoke(() => m_sceneManager.LoseScreen(), 2f);
                
            }
            else
            {
                
                FindObjectOfType<Tutorial>().Retry();
                m_isDying = false;
            }
            gameManager.Instance.m_isSafeRoom = true;
            
            //MUST REDO
            //if (!LoseScreen.activeInHierarchy)
            //{
            //    LoseScreen.SetActive(true);
            //    //m_hiding = true;
            //    GetComponent<RespawnManager>().Respawn();
            //    m_enemies.All(t => t.toPatrol = true);
            //    //dangerBarVal = 0;
            //    //gameObject.SetActive(false);
            //    GetComponent<RespawnManager>().Respawn();
            //    Cursor.lockState = CursorLockMode.None;
            //}
        }

        //Collision on the enemy results in death
        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.collider.GetComponent<EnemyController>() != null)
        //         Death();
        // }
        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.GetComponent<EnemyController>() != null)
        //         Death();
        // }
        public void Load(List<CollectableObjectWrapper> _inventory)
        {
            m_inventory = new Dictionary<CollectableObject, Wrapper<int>>();
            var collectables = GameObject.FindObjectsOfType<Collectable>();
            
            //collectables = collectables.Where(t => t.m_collectableObject.m_objectName.Contains("Clock_")).ToArray();
            
            foreach (var piece in _inventory)
            {
                    var objToAdd = collectables.FirstOrDefault(t => t.m_collectableObject.m_objectName.Equals(piece.m_objectName));
                    if (objToAdd == null) throw new Exception("object cannot be located");
                    
                    m_inventory.Add(objToAdd.m_collectableObject, new Wrapper<int>(1));
                    objToAdd.gameObject.SetActive(false);
                
            }
            
            m_testInventory = m_inventory.Keys.ToList();
        }
    }
}