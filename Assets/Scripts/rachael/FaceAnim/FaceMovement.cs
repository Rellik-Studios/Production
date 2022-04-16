using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMovement : MonoBehaviour
{
    public float m_seconds = 0;
    private Animator FaceAnim;
    // Start is called before the first frame update
    void Start()
    {
        FaceAnim = GetComponent<Animator>();
        StartCoroutine(playAnimation(m_seconds));
    }

    IEnumerator playAnimation(float second) 
    {
        yield return new WaitForSeconds(second);
        FaceAnim.enabled = true;
    }

}
