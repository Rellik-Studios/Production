using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLooking : MonoBehaviour
{
    public Transform targetObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(targetObject.position.x, gameObject.transform.position.y, targetObject.position.z));
    }
}
