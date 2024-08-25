using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject holdPosition;
    private Rigidbody objRig;
    private float interactRange = 3f;

    public bool holding = false;


    public LayerMask pickUps;
    public LayerMask vehicles;
    

    // Update is called once per frame
    void Start()
    {

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (objRig != null)
            {
                Drop();
            }
            else
            {
                InteractCheck();
            }
        }



    }
    public void InteractCheck()
    {
        Ray ray = new Ray();
        ray.direction = holdPosition.transform.forward;
        ray.origin = holdPosition.transform.position;

        RaycastHit hit;
            

        if (Physics.Raycast(ray, out hit, interactRange, pickUps))
        {

            objRig = hit.collider.gameObject.GetComponent<Rigidbody>();


            if (!holding)
            {
                PickUp();            
                Debug.Log("Picking Up Object");
            }

        }
        else if (Physics.Raycast(ray, out hit, interactRange, vehicles))
        {
            hit.collider.gameObject.GetComponent<EnterVehicle>().BoardVehicle();

            Debug.Log("Trying to get in");
            //inputManager.EnableShipControls();
        }
    }

    public void PickUp()
    {
        //Make Object a child of the HoldPosition and move it to default position
        objRig.transform.SetParent(holdPosition.transform);
        objRig.transform.localPosition = Vector3.zero;
        objRig.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //objRig.transform.localScale = Vector3.one;

        objRig.isKinematic = true;
        objRig.GetComponent<BoxCollider>().enabled = false;

        holding = true;

    }

    private void Drop()
    {
        Debug.Log("Dropping Object");
        //Set Parent to null
        objRig.transform.SetParent(null);

        objRig.isKinematic = false;
        objRig.GetComponent<BoxCollider>().enabled = true;
        objRig = null;

        holding = false;

    }
}
