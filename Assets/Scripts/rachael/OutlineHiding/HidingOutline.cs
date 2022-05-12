using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingOutline : MonoBehaviour
{
    GameObject initialOutline;
    // Start is called before the first frame update
    void Start()
    {
        initialOutline = new GameObject("Outline");
        GameObject myNewOutline = Instantiate(initialOutline, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation, gameObject.transform);
        Destroy(initialOutline);

        myNewOutline.name = "Outline";
        myNewOutline.AddComponent<MeshFilter>();
        myNewOutline.GetComponent<MeshFilter>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        myNewOutline.AddComponent<MeshRenderer>();
        myNewOutline.GetComponent<MeshRenderer>().materials = new Material[0];
        myNewOutline.AddComponent<Outline>();
        myNewOutline.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
        myNewOutline.GetComponent<Outline>().OutlineColor = new Color(0, 0.7070804f, 1.0f, 1.0f);
        myNewOutline.layer = LayerMask.NameToLayer("NoPost");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
