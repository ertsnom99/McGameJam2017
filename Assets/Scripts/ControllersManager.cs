using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ControllersManager : MonoBehaviour
{
    public int[] controllersNumber { private set; get; }
    //public int[] xInputDotNetControllersNymbers { private set; get; }

    private float remainingTime;
    
    public void SearchForControllers()
    {
        string[] controllers = Input.GetJoystickNames();

        List<int> connectedControllers = new List<int>();

        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i] != "")
            {
                connectedControllers.Add(i + 1);
            }
        }

        controllersNumber = connectedControllers.ToArray();





        /*List<int> connectedControllers = new List<int>();

        for (int j = 0; j < 4; ++j)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)j;
            if (GamePad.GetState(testPlayerIndex).IsConnected)
            {
                connectedControllers.Add(j);
                Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
            }
        }

        controllersNumber = connectedControllers.ToArray();*/
    }

    public void VibrateController(int controllerNumber, float duration, float intensity)
    {
        remainingTime = duration;
print("joys " + (controllerNumber));
        StartCoroutine(vibrate(controllerNumber, intensity));
    }

    private IEnumerator vibrate(int controllerNumber, float intensity)
    {
        while (remainingTime > 0)
        {
            yield return 0;

            remainingTime -= Time.deltaTime;
            GamePad.SetVibration((PlayerIndex)controllerNumber, intensity, intensity);
        }

        GamePad.SetVibration((PlayerIndex)controllerNumber, 0, 0);
    }
}
