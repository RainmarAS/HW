using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 4.5f;
    public Rigidbody rb;
    public Transform orientation; 
    Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        moveDirection = orientation.forward;

        rb.AddForce(moveDirection.normalized*moveSpeed*10f, ForceMode.Force);
    }
}
