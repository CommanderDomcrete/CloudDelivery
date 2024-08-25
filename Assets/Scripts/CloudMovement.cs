using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float cloudSpeed;
    private float xBound = 6000f;
    // Start is called before the first frame update
    void Start()
    {
        cloudSpeed = Random.Range(50f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= xBound)
        {
            Destroy(gameObject);
        }
        transform.Translate(new Vector3(cloudSpeed, 0, 0) * Time.deltaTime, Space.World);
    }
    
}
