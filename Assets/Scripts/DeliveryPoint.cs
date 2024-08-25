using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    private GameManager gameManager;
    public float timePassed;
    private float descentSpeed = -0.2f;
    private Collision col;
    private int pointValue = 1;
    private bool delivered = false;
    
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if(gameObject.tag == other.gameObject.tag && !delivered)
        {   
            delivered = true;
            Debug.Log("Attempting to Deliver");
            col = other;
            StartCoroutine(Deliver());
            gameManager.UpdateScore(pointValue);           
        }
    }

    IEnumerator Deliver()
    {
        
        while (timePassed < 5)
            {
            
            
                Debug.Log("Delivering Package....");
                transform.Translate(Vector3.up * descentSpeed * Time.deltaTime);
                timePassed += Time.deltaTime;

                yield return null;
            
            }

        
        Destroy(col.gameObject);
        
        col = null;
        Debug.Log("Package Delivered");
    }

}
