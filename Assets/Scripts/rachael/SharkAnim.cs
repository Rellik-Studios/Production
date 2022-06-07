using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAnim : MonoBehaviour
{
    public Animator sharkAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            sharkAnim.SetTrigger("Attack");
        }
    }


}
