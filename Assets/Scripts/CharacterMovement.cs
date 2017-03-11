using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    private Rigidbody2D rb2d;
    private float speed;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 5;
    }

    public void moveCharacter(Hashtable inputs)
    {
        Vector2 movement = new Vector2((float)inputs[PlayerController.HORIZONTAL_INPUT], (float)inputs[PlayerController.VERTICAL_INPUT]);

        movement = movement.normalized * speed * Time.deltaTime;

        rb2d.position += movement;

    }
}
