

    private PlayerMovement movementScript;
    private CharacterInteraction interactionScript;
  
    private void Awake()
    {
        InitializeVariables();
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

    private Hashtable fetchInputs()
    {
        Hashtable ht = new Hashtable();

        ht.Add(VERTICAL_INPUT, Input.GetAxis("Vertical"));
        ht.Add(HORIZONTAL_INPUT, Input.GetAxis("Horizontal"));
        ht.Add(ACTION_INPUT, Input.GetButtonDown("Fire1"));
        ht.Add(INFECT_INPUT, Input.GetButtonDown("Fire3"));    
        
        return ht;
    }
    private PlayerMovement movementScript;
    private CharacterInteraction interactionScript;
  
    private void Awake()
    {
        InitializeVariables();
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

    private Hashtable fetchInputs()
    {
        Hashtable ht = new Hashtable();

        ht.Add(VERTICAL_INPUT, Input.GetAxis("Vertical"));
        ht.Add(HORIZONTAL_INPUT, Input.GetAxis("Horizontal"));
        ht.Add(ACTION_INPUT, Input.GetButtonDown("Fire1"));
        ht.Add(INFECT_INPUT, Input.GetButtonDown("Fire3"));    
        
        return ht;
    }