using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class AIController : MonoBehaviour
{

    public const string VERTICAL_INPUT = "VInput";
    public const string HORIZONTAL_INPUT = "HInput";
    public const string ACTION_INPUT = "AInput";
    public const string INFECT_INPUT = "IInput";
    private CharacterMovement movementScript;
    private CharacterInteraction interactionScript;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        movementScript = GetComponent<CharacterMovement>();
        interactionScript = GetComponent<CharacterInteraction>();
    }

    void Update()
    {
        movementScript.moveCharacter();
    }

}
