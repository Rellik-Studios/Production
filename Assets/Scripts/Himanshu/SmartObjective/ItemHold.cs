using System;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class ItemHold : MonoBehaviour
    {
        public bool m_isHolding = false;
        public GameObject m_heldItem;
        public GameObject m_heldItemPlaceHolder;
        public Transform m_heldItemPosition;
        
        public static ItemHold Instance;
        private PlayerSmartObjectives m_player;

        private void Start()
        {
            Instance = this;
            m_player = FindObjectOfType<PlayerSmartObjectives>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
                DropItem();
        }

        public void HoldItem(GameObject _item)
        {
            DropItem();
            m_isHolding = true;
            m_heldItem = _item;
            m_heldItemPlaceHolder = Instantiate(_item, m_heldItemPosition.position, m_heldItemPosition.rotation, this.transform);
            m_heldItemPlaceHolder.GetComponent<IPickup>().SetTransform();
            m_heldItem.SetActive(false);
            m_heldItemPlaceHolder.layer = LayerMask.NameToLayer("HeldItem");
        }

        public void DropItem()
        {
            m_isHolding = false;
            if (m_heldItem ==null) return;
            m_heldItem.SetActive(true);
            m_heldItem = null;
            if(m_heldItemPlaceHolder != null)
                Destroy(m_heldItemPlaceHolder);
            m_player.DropAll();
        }


        public void Paint()
        {
            m_heldItemPlaceHolder.GetComponent<Paintbrush>().Paint();
        }
    }
}
