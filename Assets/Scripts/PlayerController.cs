using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject camHolder;
    public float speed, sensitivity, maxForce;
    private Vector2 move, look;
    private float lookRotation;


    private GameManager gameManager;
    private InputManager inputManager;

    void Start()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        inputManager = GameObject.Find("Input_Manager").GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;

    }


    public void OnMove (InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Find Target
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        //Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3 (velocityChange.x, 0, velocityChange.z);

        //Limit Force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
        Debug.Log("I AM MOVING!");
    }
    // Start is called before the first frame update



    // Update is called once per frame
    void LateUpdate()
    {
        //Turn on 1 axis
        transform.Rotate(Vector3.up * look.x * sensitivity);
        //look
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
        
    }
}
