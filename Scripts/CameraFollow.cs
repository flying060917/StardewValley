using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offect;
    // Start is called before the first frame update
    void Start()
    {
        target=GameObject.FindGameObjectWithTag("Player").transform;
        offect=this.transform.position-target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        this.transform.position=Vector3.Lerp(this.transform.position,target.position+offect,Time.deltaTime);
    }
}
