﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent navComponent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float minDistForDestReached;
    private float maxWaitTime;

    private bool waitingForTarget;
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
            waitingForTarget = true;
            StartCoroutine(SelectNewTarget());
        }

        Vector3 currentMovement = navComponent.velocity;        
        Animate(currentMovement);
    }

    private IEnumerator SelectNewTarget()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, maxWaitTime));
        navComponent.SetDestination(areaManager.GeneratePosition());
        waitingForTarget = false;
    }

    private void Animate(Vector3 movement)
    {
        Debug.Log("current Movement : " + movement);
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
