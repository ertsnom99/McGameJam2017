using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void moveCharacter(Hashtable inputs)
    {
        Vector3 movement = new Vector3((float)inputs[PlayerController.HORIZONTAL_INPUT], 0, (float)inputs[PlayerController.VERTICAL_INPUT]);
        
        movement = movement.normalized * GameManager.CHARACTER_SPEED * Time.deltaTime;

        characterController.Move(movement);
    }
}
