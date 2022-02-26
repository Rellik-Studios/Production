using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caret : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject m_caret;
    void Start()
    {
        m_caret = transform.parent.Find("Caret").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<RectTransform>().position = m_caret.GetComponent<RectTransform>().position;
        this.GetComponent<RectTransform>().sizeDelta = m_caret.GetComponent<RectTransform>().sizeDelta;
        this.GetComponent<RectTransform>().anchoredPosition = m_caret.GetComponent<RectTransform>().anchoredPosition;
        this.GetComponent<RectTransform>().pivot = m_caret.GetComponent<RectTransform>().pivot;
    }
}
