using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MovePlayer()
    {
        float h=Input.GetAxis("Horizontal");
        float v=Input.GetAxis("Vertical");
        Vector3 direction=new Vector3(h,v,0);
        transform.position+=direction*Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }
}
