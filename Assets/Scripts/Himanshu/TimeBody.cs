using System.Collections.Generic;
using UnityEngine;

namespace Himanshu
{
    public class TimeBody : MonoBehaviour
    {
        private bool m_isRewinding;

        private List<Vector3> m_positions;

        private List<Quaternion> m_rotations;
        
        // Start is called before the first frame update
        void Start()
        {
            m_positions = new List<Vector3>();
            m_rotations = new List<Quaternion>();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
                m_isRewinding = true;
            if (Input.GetKeyUp(KeyCode.R))
                m_isRewinding = false;
        }

        private void FixedUpdate()
        {
            if (m_isRewinding)
                Rewind();
            else
                Record();
        }

        private void Rewind()
        {
            transform.position = m_positions[0];
            transform.rotation = m_rotations[0];
            m_positions.RemoveAt(0);
            m_rotations.RemoveAt(0);

            if (m_positions.Count == 0 || m_rotations.Count == 0)
                m_isRewinding = false;
        }

        private void Record()
        {
            m_positions.Insert(0, transform.position);
            m_rotations.Insert(0, transform.rotation);

            if (m_positions.Count > 250)
            {
                m_positions.RemoveAt(250);
                m_rotations.RemoveAt(250);
            }
        }
    }
}