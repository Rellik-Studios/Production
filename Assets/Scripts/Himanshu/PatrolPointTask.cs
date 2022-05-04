using UnityEngine;
using UnityEngine.Events;

namespace Himanshu
{
    public class PatrolPointTask : MonoBehaviour
    {
        public int m_numOfTimesToSkip = 0;
        public UnityEvent m_onExecute;
        public GameObject m_target;
        public int m_numOfTimesSkipped;

        public void TV()
        {
            m_target.SetActive(true);
        }

        public void OnComplete()
        {
            Destroy(gameObject);
        }
    }
}