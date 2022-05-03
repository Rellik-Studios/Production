using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
namespace Himanshu.SmartObjective
{
    public enum eKitchenTile
    {
        Diamond, Star,
    }


    public class KitchenTile : MonoBehaviour
    {
        public eKitchenTile m_tileType;
        private KitchenTileManager m_manager;
        public Material m_starMat;
        public Material m_diamondMat;

        private void Start()
        {
            m_manager = GetComponentInParent<KitchenTileManager>();
            var type = Random.Range(0, 2);
            if (type == 0)
            {
                m_tileType = eKitchenTile.Star;
                GetComponent<MeshRenderer>().material = m_starMat;
            }
            else
            {
                m_tileType = eKitchenTile.Diamond;
                GetComponent<MeshRenderer>().material = m_diamondMat;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerInteract>() == null)    return;
            m_tileType = m_tileType == eKitchenTile.Diamond ? eKitchenTile.Star : eKitchenTile.Diamond;
            GetComponent<MeshRenderer>().material = m_tileType == eKitchenTile.Diamond ? m_diamondMat : m_starMat;
            m_manager.CheckUpdate();
        }

    }
}
