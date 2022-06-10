using System.Collections.Generic;
using UnityEngine;

namespace Himanshu
{
    public class TimeBody : MonoBehaviour
    {
        private bool m_isRewinding;

        private List<Vector3> m_positions;

        private List<Quaternion> m_rotations;

        public bool isRewinding
        {
            get => m_isRewinding;
            set
            {
                m_isRewinding = value;
                if (value)
                {
                    if(TryGetComponent(out EnemyController enemyController))
                        enemyController.m_frozen = true;
                    this.Invoke(()=> isRewinding = false, 5f);
                }
                else
                {
                    if(TryGetComponent(out EnemyController enemyController))
                    {
                        enemyController.m_frozen = true;
                        this.Invoke(()=>GetComponent<EnemyController>().m_frozen = false, 0.5f);
                    }
                    else
                    {
                        // Debug.Log($"{gameObject.name} does not have a EnemyController");
                    }
                   
                }
            }

        
        }

        // Start is called before the first frame update
        void Start()
        {
            m_positions = new List<Vector3>();
            m_rotations = new List<Quaternion>();
        }

        
        void Update()
        {
            
            //if(Input.GetKeyDown(KeyCode.R))
            //    m_isRewinding = true;
            //if (Input.GetKeyUp(KeyCode.R))
            //    m_isRewinding = false;
        }

        private void FixedUpdate()
        {
            if (isRewinding)
                Rewind();
            else
                Record();
        }

        private void Rewind()
        {
            transform.position = m_positions[0];
            transform.rotation = m_rotations[0];
            if (m_positions.Count > 1 && m_rotations.Count > 1)
            {
                m_positions.RemoveAt(0);
                m_rotations.RemoveAt(0);
            }
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