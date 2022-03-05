using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cinemachine;
using Himanshu;
using rachael;
using UnityEngine;

public class HidingLocation : MonoBehaviour, IInteract
{
    private HidingSpot m_hidingSpot;
    [SerializeField] private bool m_futuristic;

    public Vector3 actualForward => GetComponent<CinemachineVirtualCamera>() != null ? transform.forward : transform.GetChild(0).forward;

    void Start()
    {
        m_hidingSpot = transform.GetComponentInParent<HidingSpot>();
    }

    void Update()
    {
        
    }

    public void Execute(PlayerInteract _player)
    {
        m_hidingSpot.BeginHide(this, _player);
        Debug.Log("Hiding is begining");

    }

    public void CanExecute(Raycast _raycast)
    {
        if (_raycast.m_indication != null)
            _raycast.m_indication.sprite = Resources.Load<Sprite>("Hide");
    }


    public void TurnOn()
    { 
        var dir = FindObjectOfType<PlayerFollow>().transform.position - transform.position;
        dir.y = 0; // keep the direction strictly horizontal
        var rot = Quaternion.LookRotation(dir);


        if(GetComponent<CinemachineVirtualCamera>() != null)
            GetComponent<CinemachineVirtualCamera>().enabled = true;
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<CinemachineVirtualCamera>() != null)
            transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().enabled = true;
        
        this.Invoke(()=> FindObjectOfType<PlayerFollow>().transform.rotation = transform.rotation, 2f);

    }

    public void TurnOff()
    {
        if(GetComponent<CinemachineVirtualCamera>() != null)
            GetComponent<CinemachineVirtualCamera>().enabled = false;
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<CinemachineVirtualCamera>() != null)
            transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().enabled = false;

    }
}
