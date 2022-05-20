using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    
    public class KitchenTileManager : MonoBehaviour
    {
        private List<KitchenTile> m_KitchenTiles;
        [SerializeField] private Objective m_objective;
        private List<eKitchenTile> currentTiles => m_KitchenTiles.Select(t=>t.m_tileType).ToList();
        private List<eKitchenTile> m_answerTiles;

        private void Start()
        {
            m_answerTiles = new List<eKitchenTile>();
            m_KitchenTiles = new List<KitchenTile>();
            m_answerTiles.Add(eKitchenTile.Diamond);
            m_answerTiles.Add(eKitchenTile.Diamond);
            m_answerTiles.Add(eKitchenTile.Star);
            m_answerTiles.Add(eKitchenTile.Star);
            m_answerTiles.Add(eKitchenTile.Star);
            m_answerTiles.Add(eKitchenTile.Diamond);
            m_answerTiles.Add(eKitchenTile.Diamond);
            m_answerTiles.Add(eKitchenTile.Diamond);
            
            
            for (int i = 0; i < 8; i++) {
                m_KitchenTiles.Add(transform.GetChild(2 + i).GetComponent<KitchenTile>());
            }
        }

        private void Update()
        {

        }

        public void CheckUpdate()
        {
            if (m_answerTiles.IsEqual(currentTiles)) {
                m_objective.Execute(FindObjectOfType<PlayerInteract>());
                GetComponent<Animator>().SetTrigger("Shrink");
            }
        }
    }
}
