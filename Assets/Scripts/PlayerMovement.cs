﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour 
{	
	private CharacterController characterController;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
	
	private void Awake()
	{
		InitializeVariables();
	}
	
	private void InitializeVariables()
	{
		characterController = GetComponent<CharacterController>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void moveCharacter(Hashtable inputs)
    {
        Vector3 movement = new Vector3((float)inputs[PlayerController.HORIZONTAL_INPUT], -1.0f, (float)inputs[PlayerController.VERTICAL_INPUT]);
		
		Vector3 movementNormalized = new Vector3(movement.x, 0, movement.z).normalized;
        movement = movement * GameManager.CHARACTER_SPEED * Time.deltaTime;

        characterController.Move(movement);

        Animate(movementNormalized);
    }

    private void Animate(Vector3 movement)
    {
        float pi = Mathf.PI;

        if (movement.magnitude < 0.1f)
        {
            animator.SetTrigger("idle");
        } else if (movement.x < Mathf.Cos(pi/4) && movement.x > Mathf.Cos(3*pi/4) && movement.z > Mathf.Sin(pi/4)) {
            animator.SetTrigger("moveVerticalUp");
        } else if (movement.x > Mathf.Cos(-3*pi/4) && movement.x < Mathf.Cos(-pi/4) && movement.z < Mathf.Sin(-pi/4))
        {
            animator.SetTrigger("moveVerticalDown");
        }
        else if ((movement.x > Mathf.Cos(pi/4) && movement.z < Mathf.Sin(pi / 4) && movement.z > Mathf.Sin(-pi/4)) ||
            (movement.x < Mathf.Cos(3*pi / 4) && movement.z < Mathf.Sin(pi / 4) && movement.z > Mathf.Sin(-pi/4)))
        {
            animator.SetTrigger("moveHorizontal");
            bool flipSprite = (spriteRenderer.flipX ? (movement.x > 0.1f) : (movement.x < 0.1f));
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }
}