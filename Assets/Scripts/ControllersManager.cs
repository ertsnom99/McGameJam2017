//using XInputDotNetPure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersManager : MonoBehaviour
{
    public int[] controllersNumber { private set; get; }

    private float remainingTime;


    public void SearchForControllers()
    {
        string[] controllers = Input.GetJoystickNames();

        List<int> connectedControllers = new List<int>();

        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i] != "") connectedControllers.Add(i + 1);
        }
        controllersNumber = connectedControllers.ToArray();
    }

    //public void VibrateController(int controllerNumber, float duration, float intensity)
    //{
    //    remainingTime = duration;
    //    StartCoroutine(vibrate(controllerNumber, intensity));
    //}

    //private IEnumerator vibrate(int controllerNumber, float intensity)
    //{
    //    print(remainingTime);
    //    while (remainingTime > 0)
    //    {
    //        yield return 0;

    //        remainingTime -= Time.deltaTime;
    //        GamePad.SetVibration((PlayerIndex)controllerNumber, intensity, intensity);
    //    }

    //    GamePad.SetVibration((PlayerIndex)controllerNumber, 0, 0);
    //}
}
