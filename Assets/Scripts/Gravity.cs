using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public LayerMask GravMask;
    private float pullRadius = 8;
    private float totalForce = 2000;
    public float pullForce;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        pullForce = totalForce; //* 1 / Mathf.Pow(dist, 2);
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius, GravMask))
        {

            Vector3 forceDirection = transform.position - collider.transform.position;
            dist = (collider.transform.position - transform.position).magnitude;
            collider.attachedRigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);

        }

    }
}
