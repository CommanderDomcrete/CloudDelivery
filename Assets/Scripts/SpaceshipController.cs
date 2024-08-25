using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    private Rigidbody shipRb;

    private EnterVehicle enterVehicle;
    
    public float timePassed;

    public AudioSource[] sounds;
    public AudioSource shipEngineAudio_01;
    public AudioSource shipEngineAudio_02;

    private float sensitivity = 3f;
    private float rollSens = 40f;
    public Vector2 look;
    public float roll;

    private float verticalThrust = 1;

    public float boost = 0f;
    public float maxThrust;
    private float acceleration = 50f;
    public float currentThrust = 0f;
    private Vector3 angularVel;

    public bool inFlight;
    public bool storageLock;
    
    RaycastHit hit;
    private void Awake()
    {
        enterVehicle = GetComponent<EnterVehicle>();
    }

    private void Start()
    {
        storageLock = false;
        inFlight = false;
        shipRb = GetComponent<Rigidbody>();

        sounds = GetComponents<AudioSource>();
        shipEngineAudio_01 = sounds[0];
        shipEngineAudio_02 = sounds[1];

        shipEngineAudio_02.Play();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        if (inFlight)
        {
            boost = 100 * context.ReadValue<float>();
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll = context.ReadValue<float>();
    }

    public void OnTakeOffLand(InputAction.CallbackContext context)
    {
        shipEngineAudio_01.Play();

        if (context.performed && !inFlight)
        {
            StartCoroutine(TakeOff());
        }
        else if (context.performed && !IsGrounded())
        {
            StartCoroutine(Land());
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnExitVehicle (InputAction.CallbackContext context)
    {
        if (context.performed && !inFlight)
        {
            enterVehicle.AlightVehicle();
        }
    }

    IEnumerator TakeOff()
    {
        timePassed = 0;
        while (timePassed < 2)
            {
                shipRb.velocity = transform.up * verticalThrust;
                timePassed += Time.deltaTime;

                yield return null;
            }
        inFlight = true;
        shipRb.useGravity = false;
    }

    IEnumerator Land()
    { 
        inFlight = false;
        while (!IsGrounded())
        {
            shipRb.velocity = transform.up * -verticalThrust;
            yield return null;
        }
        shipRb.useGravity = true;
        shipEngineAudio_01.Stop();
    }

    private void Thrust()
    {
        if (inFlight)
        {
            maxThrust = 20 + boost;

            shipRb.velocity = transform.forward * currentThrust;

            currentThrust += acceleration * Time.deltaTime;
            if (currentThrust > maxThrust)
                currentThrust = maxThrust;
        }
        else
        {
            currentThrust = 0;
        }
    }

    void FixedUpdate()
    {
        Thrust();

        if (inFlight)
        {
            Rotate();
        }    
    }

    private bool IsGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2.5f))
            {
            }
            return hit.collider != null;
    }

    private void Rotate()
    {
        angularVel = new Vector3(look.y * sensitivity, look.x * sensitivity, roll * rollSens);
        shipRb.AddRelativeTorque(angularVel);
        Quaternion deltaRotation = Quaternion.Euler(angularVel * Time.deltaTime);
        shipRb.MoveRotation(shipRb.rotation * deltaRotation);
    }
}
