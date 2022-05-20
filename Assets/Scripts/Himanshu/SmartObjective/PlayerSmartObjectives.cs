using UnityEngine;
using UnityEngine.Serialization;
namespace Himanshu.SmartObjective
{
    public class PlayerSmartObjectives : MonoBehaviour
    {
        public bool m_hasFire = false;
        public bool m_hasCandle = false;
        public bool m_hasNotes;
        public bool m_hasPaintBrush;
        public bool hasVRHeadset {
            get => m_hasVRHeadset;
            set {
                m_hasVRHeadset = value;
                GameObject.FindGameObjectWithTag("VRCam").GetComponent<Camera>().enabled = value;
            }
        }
        public bool m_hasNewsPaper;
        private bool m_hasVRHeadset;
        public void DropAll()
        {
            m_hasFire = false;
            m_hasCandle = false;
            m_hasNotes = false;
            m_hasPaintBrush = false;
            hasVRHeadset = false;
            m_hasNewsPaper = false;
        }
    }
}
