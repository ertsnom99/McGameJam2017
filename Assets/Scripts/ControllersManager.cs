using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersManager : MonoBehaviour
{
    public int[] controllersNumber { private set; get; }

    public void SearchForControllers()
    {        
        string[] controllers = Input.GetJoystickNames();
        Debug.Log(controllers.Length);
        List<int> connectedControllers = new List<int>();

        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i] != "") connectedControllers.Add(i+1);
        }
        controllersNumber = connectedControllers.ToArray();
    }
}
