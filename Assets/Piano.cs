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
    private Material m_1870Mat;
    private Material m_futureMat;
    // Start is called before the first frame update
    void Start()
    {
        m_distraction = GetComponent<Distraction>();
        m_playerSmartObjectives = FindObjectOfType<PlayerSmartObjectives>();
        m_1870Mat = GetComponent<Renderer>().material;
        m_futureMat = transform.GetChild(0).GetComponent<Renderer>().material;
    }

    private bool m_dir = false;
    // Update is called once per frame
    void Update()
    {
        if (m_morphing) {

            if (m_dir) {
                //Interpolate between the two materials
                m_futureMat.SetFloat("_FillPercent", Mathf.Lerp(m_futureMat.GetFloat("_FillPercent"), 1f, Time.deltaTime / 3f));
                m_1870Mat.SetFloat("_FillPercent", Mathf.Lerp(m_1870Mat.GetFloat("_FillPercent"), 1f, Time.deltaTime / 3f));
                if(m_futureMat.GetFloat("_FillPercent") >= 0.8f) {
                    m_dir = false;
                }
            } 
            else {
                m_futureMat.SetFloat("_FillPercent", Mathf.Lerp(m_futureMat.GetFloat("_FillPercent"), 0f, Time.deltaTime / 2f));
                m_1870Mat.SetFloat("_FillPercent", Mathf.Lerp(m_1870Mat.GetFloat("_FillPercent"), 0f, Time.deltaTime / 2f));
                if (m_futureMat.GetFloat("_FillPercent") <= 0.2f) {
                    m_dir = true;
                }
            }
        } 
        else {
            if(m_futureMat.GetFloat("_FillPercent") < 0.99f) {
                m_futureMat.SetFloat("_FillPercent", Mathf.Lerp(m_futureMat.GetFloat("_FillPercent"), 1f, Time.deltaTime / 2f));
                m_1870Mat.SetFloat("_FillPercent", Mathf.Lerp(m_1870Mat.GetFloat("_FillPercent"), 1f, Time.deltaTime / 2f));
            }    
        }
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
