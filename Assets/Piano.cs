using System.Collections;
using System.Collections.Generic;
using Himanshu;
using Himanshu.SmartObjective;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public bool m_morphing = false;
    [SerializeField] private Objective m_objective;
    [SerializeField] private PickupObj m_musicNotes;

    private Distraction m_distraction;
    private PlayerSmartObjectives m_playerSmartObjectives;
    // Start is called before the first frame update
    void Start()
    {
        m_distraction = GetComponent<Distraction>();
        m_playerSmartObjectives = FindObjectOfType<PlayerSmartObjectives>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaceNote()
    {
        FindObjectOfType<Narrator>().madeSound = true;
        if (m_playerSmartObjectives.m_hasNotes) {
            m_morphing = false;
            this.Invoke(() => m_distraction.m_canDistract = true, .5f);
            m_objective.Execute(m_playerSmartObjectives.GetComponent<PlayerInteract>());
            FindObjectOfType<ItemHold>().DropItem();
            m_playerSmartObjectives.m_hasNotes = false;
        }
    }
}
