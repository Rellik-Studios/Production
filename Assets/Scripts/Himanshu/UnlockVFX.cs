using System;
using System.Collections;
using System.Linq;
using UnityEngine;
namespace Himanshu
{
    public class UnlockVFX : MonoBehaviour
    {
        private void OnEnable()
        {
        }

        private void Update()
        { 
            var col = Physics.OverlapSphere(transform.position, 8f, ~0, QueryTriggerInteraction.Ignore);
            if (col.Any(t => t.GetComponent<PlayerInteract>() != null))
                gameObject.SetActive(false);
        }
    }
}
