using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent navComponent;

    private float maxWaitTime;

    private bool waitingForTarget;

	// Use this for initialization
	void Awake ()
	{
	    InitializeVariables();
    }

    void InitializeVariables()
    {
        navComponent = GetComponent<NavMeshAgent>();
        maxWaitTime = 3.0f;
    }

    // Update is called once per frame
    void Update () {
        if (navComponent.remainingDistance <= 0.075f && !waitingForTarget)
        {
            waitingForTarget = true;
            StartCoroutine(SelectNewTarget());
        }
    }

    private IEnumerator SelectNewTarget()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, maxWaitTime));
        navComponent.SetDestination(GeneratePosition());
        waitingForTarget = false;
    }

    Vector3 GeneratePosition()
    {
        return new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
    }
}
