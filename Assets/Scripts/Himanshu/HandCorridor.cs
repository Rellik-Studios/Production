using System;
using UnityEngine;

namespace Himanshu
{
    public class HandCorridor : MonoBehaviour
    {
        private Narrator m_narrator;

        private void Start()
        {
            m_narrator = FindObjectOfType<Narrator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            m_narrator.corridor = true;
            this.Invoke(() => Destroy(this), 2f);
        }
    }
}
