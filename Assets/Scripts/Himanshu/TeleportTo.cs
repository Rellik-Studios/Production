using System;
using UnityEngine;
namespace Himanshu
{
    public class TeleportTo : MonoBehaviour
    {
        public Transform m_target;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInteract _) && m_target != null) {
                other.transform.position = m_target.position;
                Destroy(this);
            }
        }

    }
}
