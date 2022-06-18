using System;
using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEngine;
using UnityEngine.Events;

public class CommandTrigger : MonoBehaviour
{
    public UnityEvent m_command;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>() != null)
            m_command?.Invoke();
    }
}
