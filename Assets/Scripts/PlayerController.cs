using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]

public class PlayerController : MonoBehaviour {

    public const string VERTICAL_INPUT = "VInput";
    public const string HORIZONTAL_INPUT = "HInput";
    public const string ACTION_INPUT = "AInput";
    public const string INFECT_INPUT = "IInput";

    private CharacterMovement movementScript;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        movementScript = GetComponent<CharacterMovement>();
    }
	
	void Update () {
        Hashtable inputs = fetchInputs();
        movementScript.moveCharacter(inputs);
        if ((bool)inputs[ACTION_INPUT]) // interact with object
        {
            interactionScript.interact();
        }
        if ((bool)inputs[INFECT_INPUT])
        {
            interactionScript.infect();
        }
    }

﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CharacterInteraction))]

public class PlayerController : MonoBehaviour {

    public const string VERTICAL_INPUT = "VInput";
    public const string HORIZONTAL_INPUT = "HInput";
    public const string ACTION_INPUT = "AInput";
    public const string INFECT_INPUT = "IInput";

    private PlayerMovement movementScript;
    private CharacterInteraction interactionScript;
  
    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        movementScript = GetComponent<PlayerMovement>();
        interactionScript = GetComponent<CharacterInteraction>();
    }
	
	void Update () {
        Hashtable inputs = fetchInputs();
    private Hashtable fetchInputs()
    {
        Hashtable ht = new Hashtable();

        ht.Add(VERTICAL_INPUT, Input.GetAxis("Vertical"));
        ht.Add(HORIZONTAL_INPUT, Input.GetAxis("Horizontal"));
        ht.Add(ACTION_INPUT, Input.GetButtonDown("Fire1"));
        ht.Add(INFECT_INPUT, Input.GetButtonDown("Fire3"));    
        
        return ht;
    }
}
	