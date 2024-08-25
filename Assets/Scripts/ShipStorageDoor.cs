using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStorageDoor : MonoBehaviour
{
    public GameObject spaceship;
    private SpaceshipController spaceshipController;
    private float doorClosed = -100;
    private float doorOpen = 100;
    private float lerpDuration = 1.0f;
    public bool closed;

    void Start()
    {
        closed = false;
        spaceshipController = spaceship.GetComponent<SpaceshipController>();
    }

    void Update()
    {
        if (spaceshipController.inFlight && !closed)
        {
            StartCoroutine(DoorOpen(doorClosed));
            closed = true;
        }
        else if (!spaceshipController.inFlight && closed)
        {
            StartCoroutine(DoorOpen(doorOpen));
            closed = false;
        }
    }

    IEnumerator DoorOpen(float angle)
    {      
        float timeElapsed = 0;
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = transform.localRotation * Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, angle);
        while (timeElapsed < lerpDuration)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed/lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = targetRotation;  
    }
}

