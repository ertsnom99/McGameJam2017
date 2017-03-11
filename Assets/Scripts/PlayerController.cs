using System;
using System.Collections;
using System.Collections.Generic;
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
	}

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
