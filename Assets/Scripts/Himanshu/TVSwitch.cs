using System;
using rachael;
using UnityEngine;
namespace Himanshu
{
    public class TVSwitch : MonoBehaviour, IInteract
    {
        [SerializeField] private Material m_offMat;
        [SerializeField] private Material m_onMat;
        [SerializeField] private Distraction m_distraction;
        private PlayerInteract m_playerInteract;
        private MeshRenderer m_meshRenderer;
        private bool m_state;
        [SerializeField] private Material m_tvOffMat;
        [SerializeField] private MeshRenderer m_tvMeshRenderer;


        public bool state 
        {
            get => m_state;
            set {
                if (value) 
                {
                    m_distraction.Execute(m_playerInteract);
                    m_meshRenderer.material = m_onMat;
                    m_tvMeshRenderer.enabled = true;
                }
                else
                {
                    m_meshRenderer.material = m_offMat; 
                    // m_tvMeshRenderer.material = m_tvOffMat;
                    m_tvMeshRenderer.enabled = false;
                    m_distraction.m_playing = false;
                }
                m_state = value;

            }
        }

        private void Start()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if(m_state && !m_distraction.m_canDistract)
            {
                // m_distraction.m_canDistract = true;
            }
        }
        public void Execute(PlayerInteract _player)
        {
            if(gameManager.Instance.isTutorialRunning)
                return;
            if(m_playerInteract == null)
                m_playerInteract = _player;
            state = !state;
        }
        public void CanExecute(Raycast _raycast)
        {
            if (gameManager.Instance.isTutorialRunning)
                _raycast.m_indication.enabled = false;
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Interact");
        }
    }
}
