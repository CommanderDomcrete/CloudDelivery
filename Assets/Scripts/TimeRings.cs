using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRings : MonoBehaviour
{
    public int timeValue;
    private float rotationSpeed;
    private GameManager gameManager;
    private Vector3 randomRotation;
    private AudioManager audioManager;
    private bool hasRun;
    
    // Start is called before the first frame update
    void Start()
    {
        hasRun = false;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        randomRotation.z = Random.Range(-180f, 180f);
        randomRotation.y = transform.localEulerAngles.y;
        randomRotation.x = transform.localEulerAngles.x;    
        transform.eulerAngles = randomRotation;
        rotationSpeed = 80f;
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!hasRun)
        {
            gameManager.AddTime(timeValue);
            Debug.Log("Add Time!!!");
            audioManager.TimeRingChime();
            Destroy(gameObject);
        }
        hasRun = true;

        
    }
}
