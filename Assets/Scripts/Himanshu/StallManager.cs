using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Himanshu
{
    public class StallManager : MonoBehaviour
    {
        public List<Stall> m_stalls;

        public List<bool> m_inspectorStallStates;
        
        public StallManager m_lookupStallManager;
        public StallManager m_otherStallManager;
        public int m_stallID = 0;
        public bool m_stallCompleted = false;
        [SerializeField] private Objective m_objective;
        
        public List<bool> stallStates {
            get {
                m_inspectorStallStates = m_stalls.Select(t => t.m_stallState == Stall.eStallState.Open).ToList();
                return m_inspectorStallStates;
            }
            set {
                m_inspectorStallStates = value;
                m_stalls.ForEach(t => t.stallState = value[m_stalls.IndexOf(t)] ? Stall.eStallState.Open : Stall.eStallState.Close);
            }
        }

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++) {
                m_stalls.Add(transform.GetChild(i).GetChild(0).GetComponent<Stall>());
            }

            if (m_lookupStallManager == null) {
                stallStates = new List<bool>() {
                    Random.Range(0f, 1f) > 0.5f, Random.Range(0f, 1f) > 0.5f, Random.Range(0f, 1f) > 0.5f, Random.Range(0f, 1f) > 0.5f
                };
            }
        }
        
        public void OpenStall(int _index)
        {
            m_stalls[_index].Execute(null);
        }

        private void Update()
        {
            if(m_lookupStallManager == null) return;
            if (m_lookupStallManager != null && stallStates.IsEqual(m_lookupStallManager.stallStates)) {
                m_stallCompleted = true;
                if (m_stallID == 1) {
                    if (m_otherStallManager.m_stallCompleted && m_objective.gameObject.activeSelf) {
                        m_objective.Execute(FindObjectOfType<PlayerInteract>());
                    }
                }
            }
            else {
                m_stallCompleted = false;
            }
        }
    }
}
