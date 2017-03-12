using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CharacterInteraction))]

public class PlayerController : MonoBehaviour {

    public const string VERTICAL_INPUT = "VInput";
    public const string HORIZONTAL_INPUT = "HInput";
    public const string ACTION_INPUT = "AInput";
    public const string KILL_INPUT = "KInput";
    public const string INFECT_INPUT = "IInput";

    public int joystickNumber;

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
        movementScript.moveCharacter(inputs);
        if ((bool)inputs[ACTION_INPUT]) // interact with object
        {
            interactionScript.Interact();
        }
        if ((bool)inputs[INFECT_INPUT])
        {
            interactionScript.Infect();
        }
        if ((bool)inputs[KILL_INPUT])
        {
            interactionScript.Kill();
        }
    }

    private Hashtable fetchInputs()
    {
        Hashtable ht = new Hashtable();

        ht.Add(VERTICAL_INPUT, Input.GetAxis("Joy" + joystickNumber + "Vertical"));
        ht.Add(HORIZONTAL_INPUT, Input.GetAxis("Joy" + joystickNumber + "Horizontal"));
        ht.Add(ACTION_INPUT, Input.GetButtonDown("Joy" + joystickNumber + "Fire1"));
        ht.Add(KILL_INPUT, Input.GetButtonDown("Joy" + joystickNumber + "Fire2"));
        ht.Add(INFECT_INPUT, Input.GetButtonDown("Joy" + joystickNumber + "Fire3"));    
        
        return ht;
    }
}
	