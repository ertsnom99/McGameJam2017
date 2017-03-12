using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterInteraction))]

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent navComponent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CharacterInteraction interactionScript;

    private float minDistForDestReached;
    private float maxWaitTime;

    private bool waitingForTarget;
    private string lastDestinationType;
    private Vector3 previousPosition;
    
    public AreaManager areaManager;

    void Awake()
    {
        InitializeVariables();
        InitializeNavMeshAgent();
    }

    void InitializeVariables()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        interactionScript = GetComponent<CharacterInteraction>();

        lastDestinationType = AreaManager.WALKABLE_AREA;
        minDistForDestReached = 0.075f;
        maxWaitTime = 3.0f;
    }

    void Start()
    {
        previousPosition = transform.position;
    }

    void InitializeNavMeshAgent()
    {
        navComponent = GetComponent<NavMeshAgent>();

        navComponent.speed = GameManager.CHARACTER_SPEED;
    }

    void Update()
    {
        if (navComponent.remainingDistance <= minDistForDestReached && !waitingForTarget)
        {
            if (lastDestinationType == AreaManager.WALKABLE_AREA)
            {
                waitingForTarget = true;
                StartCoroutine(WaitBeforeSelectingTarget());
            }
            else if (lastDestinationType == AreaManager.INTERACTIF_AREA)
            {
                interactionScript.interact();
                lastDestinationType = "";
            }
            else
            {
                SelectNewTarget();
            }
        }

        Vector3 currentMovement = navComponent.velocity;
        Animate(currentMovement);
    }

    private IEnumerator WaitBeforeSelectingTarget()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, maxWaitTime));

        SelectNewTarget();
    }

    private void SelectNewTarget()
    {
        ArrayList destinationInfo = areaManager.GenerateDestination();

        navComponent.SetDestination((Vector3)destinationInfo[0]);
        lastDestinationType = (string)destinationInfo[1];

        waitingForTarget = false;
    }

    private void Animate(Vector3 movement)
    {
        float pi = Mathf.PI;

        if (movement.magnitude < 0.1f)
        {
            animator.SetTrigger("idle");
        }
        else if (movement.x < Mathf.Cos(pi / 4) && movement.x > Mathf.Cos(3 * pi / 4) && movement.z > Mathf.Sin(pi / 4))
        {
            animator.SetTrigger("moveVerticalUp");
        }
        else if (movement.x > Mathf.Cos(-3 * pi / 4) && movement.x < Mathf.Cos(-pi / 4) && movement.z < Mathf.Sin(-pi / 4))
        {
            animator.SetTrigger("moveVerticalDown");
        }
        else if ((movement.x > Mathf.Cos(pi / 4) && movement.z < Mathf.Sin(pi / 4) && movement.z > Mathf.Sin(-pi / 4)) ||
            (movement.x < Mathf.Cos(3 * pi / 4) && movement.z < Mathf.Sin(pi / 4) && movement.z > Mathf.Sin(-pi / 4)))
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
