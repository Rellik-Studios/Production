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

    
    private Vector2 m_xRange = new Vector2(-30f, 30f);
    private Vector2 m_yRange = new Vector2(-30f, 30f);
    
    public float m_mouseX;
    public float m_mouseY;


    public float m_defMouseX;
    public float m_defMouseY;
    private bool isActive => GetComponent<CinemachineVirtualCamera>()?.enabled ?? false;
    void Start()
    {
        m_hidingSpot = transform.GetComponentInParent<HidingSpot>();
    }

    void Update()
    {
        if (isActive && !m_hidingSpot.m_cupboard)
        {
            m_mouseX += Input.GetAxis("Mouse X");
            m_mouseY -= Input.GetAxis("Mouse Y");


            m_mouseY = Mathf.Clamp(m_mouseY, m_yRange.x, m_yRange.y);
            m_mouseX = Mathf.Clamp(m_mouseX, m_xRange.x, m_xRange.y);
            
            transform.rotation = Quaternion.Euler(  m_defMouseY + m_mouseY,m_defMouseX + m_mouseX, 0f);
        }
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
        m_defMouseX = transform.localRotation.eulerAngles.y;
        m_defMouseY = transform.localRotation.eulerAngles.x;
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
        transform.rotation = Quaternion.Euler(m_defMouseY, m_defMouseX, 0f);
        if(GetComponent<CinemachineVirtualCamera>() != null)
            GetComponent<CinemachineVirtualCamera>().enabled = false;
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<CinemachineVirtualCamera>() != null)
            transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().enabled = false;

    }
}
