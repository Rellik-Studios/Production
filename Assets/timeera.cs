using System;
using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEngine;

public class timeera : MonoBehaviour
{
    [SerializeField] private string m_era;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInteract>() != null)
            gameManager.Instance.m_timeEra = m_era;
    }

}
