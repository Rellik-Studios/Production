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
        GameObject myNewSmoke = Instantiate(initialOutline, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation, gameObject.transform);
        Destroy(initialOutline);

        myNewSmoke.name = "Outline";
        myNewSmoke.AddComponent<MeshFilter>();
        myNewSmoke.GetComponent<MeshFilter>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        myNewSmoke.AddComponent<MeshRenderer>();
        myNewSmoke.GetComponent<MeshRenderer>().materials = new Material[0];
        myNewSmoke.AddComponent<Outline>();
        myNewSmoke.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
        myNewSmoke.GetComponent<Outline>().OutlineColor = new Color(0, 0.7070804f, 1.0f, 1.0f);
        myNewSmoke.layer = LayerMask.NameToLayer("NoPost");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
