using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        
    }


    void Start()
    {

        //m_Controls.Player.Interact.started += ctx => 


    }
    // Start is called before the first frame update
    public void EnableShipControls()
    {
        playerInput.SwitchCurrentActionMap("Spaceship");
        Debug.Log("YOU'RE A SHIP!!!");
        
    }

    public void EnablePlayerControls()
    {
        playerInput.SwitchCurrentActionMap("Player");
        Debug.Log("YOU'RE A OOMAN!!!");

    }






}
