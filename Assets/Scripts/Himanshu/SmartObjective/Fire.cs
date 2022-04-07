using UnityEngine;
namespace Himanshu.SmartObjective
{

    public class Fire : MonoBehaviour
    {
        private void OnTriggerEnter(Collider _collider)
        {
            if(_collider.TryGetComponent(out PlayerSmartObjectives _player) && !_player.m_hasFire && _player.m_hasCandle)
            {
                _player.m_hasFire = true;
                if (ItemHold.Instance.m_heldItemPlaceHolder.GetComponent<Candle>() != null) 
                {
                    ItemHold.Instance.m_heldItemPlaceHolder.GetComponent<Candle>().isLit = true;
                    ItemHold.Instance.m_heldItem.GetComponent<Candle>().isLit = true;
                }    
                
            }
        }
    }

}
