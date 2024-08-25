using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{
    public bool inVehicle = false;
    SpaceshipController spaceshipController;
    private GameObject player;
    public Transform exitPoint;
    public GameObject seat;
    public GameObject cameraCradle;
    private Camera playerCam;
    private Transform camHolder;

    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        
        inputManager = GameObject.Find("Input_Manager").GetComponent<InputManager>();
        spaceshipController = GetComponent<SpaceshipController>();
        player = GameObject.Find("Player");

    }

    public void BoardVehicle()
    {
        Debug.Log("Boarding Vehicle");

        spaceshipController.enabled = true;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        
        player.GetComponent<Rigidbody>().isKinematic = true;
        
        player.transform.position = seat.transform.position;
        player.transform.rotation = seat.transform.rotation;
        player.transform.SetParent(seat.transform);


        playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerCam.transform.SetParent(cameraCradle.transform);
        playerCam.transform.position = cameraCradle.transform.position;
        playerCam.transform.rotation = cameraCradle.transform.rotation;
        playerCam.transform.localScale = Vector3.one;
        inputManager.EnableShipControls();
        Debug.Log("Ship Controls Enabled");

        player.SetActive(false);

    }

    public void AlightVehicle()
    {

        camHolder = player.transform.Find("CameraHolder");
        Debug.Log("Alighting Vehicle");

        spaceshipController.enabled = false;
        player.GetComponent<PlayerController>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Collider>().enabled = true;
        
        player.transform.parent = null;
        player.transform.position = exitPoint.transform.position;
        player.transform.rotation = Quaternion.identity;


        
        
        player.GetComponent<Rigidbody>().isKinematic = false;        

        
            
        playerCam.transform.SetParent(camHolder);
        playerCam.transform.position = camHolder.transform.position;
        playerCam.transform.rotation = camHolder.transform.rotation;
        playerCam.transform.localScale = Vector3.one;
        inputManager.EnablePlayerControls();

        player.transform.localScale = Vector3.one;

        player.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
