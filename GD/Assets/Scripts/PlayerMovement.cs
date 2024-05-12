using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5.0f;

    public Transform orientation; 

    float horizontalInput;
    float verticalInput;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    public float groundDrag;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    Rigidbody rb;


    public InputAction playerControls;
    public InputActionAsset asset{get;}
    Vector3 moveDirection = Vector3.zero;
    private InputAction move;
    private InputAction fire;
    
    private void Awake(){
        //playerControls = new PlayerInputActions();
    }

    private void OnEnable(){
        //move = playerControls.Player.Move;
        //move.Enable();
        //fire = playerCon
    }

    private void OnDisable(){
        //move.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position,Vector3.down,playerHeight *0.5f + 0.2f,whatIsGround);
        MyInput();
        SpeedControl();
        //MovePlayer();
        //var moveDirection = moveAction.ReadValue<Vector2>();
        //transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if(grounded)
            rb.drag = groundDrag;
        else 
            rb.drag = 0;
    }

    void FixedUpdate(){
        MovePlayer();
    }

    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log(grounded);
        if(Input.GetKey(jumpKey) && readyToJump && grounded){
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump),jumpCooldown);
        }
    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
            rb.AddForce(moveDirection.normalized*moveSpeed*10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized*moveSpeed*10f*airMultiplier, ForceMode.Force);
    }

    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up*jumpForce, ForceMode.Impulse);
    }

    private void ResetJump(){
        readyToJump = true;
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        if(flatVel.magnitude>moveSpeed){
            Vector3 limitedVel = flatVel.normalized*moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y,limitedVel.z);
        }
    }

}
