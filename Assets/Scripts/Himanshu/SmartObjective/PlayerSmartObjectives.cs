using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class PlayerSmartObjectives : MonoBehaviour
    {
        public bool m_hasFire = false;
        public bool m_hasCandle = false;
        public bool m_hasNotes;
        public bool m_hasPaintBrush;
        public bool m_hasVRHeadset;
        public bool m_hasNewsPaper;
        public void DropAll()
        {
            m_hasFire = false;
            m_hasCandle = false;
            m_hasNotes = false;
            m_hasPaintBrush = false;
            m_hasVRHeadset = false;
            m_hasNewsPaper = false;
        }
    }
}
