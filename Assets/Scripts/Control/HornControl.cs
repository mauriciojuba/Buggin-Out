using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornControl : MonoBehaviour {
    Vector3 mov;
    Rigidbody rdb;
    public GameObject cameragame;
    public Animator anim;
	// Use this for initialization
	void Start () {
        rdb=GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(cameragame!=null)
        mov = cameragame.transform.TransformVector(mov);

        if (rdb.velocity.magnitude > 0.1f)
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rdb.velocity), Time.deltaTime*5);
    }

    void FixedUpdate()
    {
        Vector3 nvel = new Vector3(mov.x*5, rdb.velocity.y, mov.z*5);
        rdb.velocity = nvel ;
        anim.SetFloat("Mov", rdb.velocity.magnitude);

    }
}
