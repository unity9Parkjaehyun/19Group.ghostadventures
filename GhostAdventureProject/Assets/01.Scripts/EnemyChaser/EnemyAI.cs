using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("기본 설정")]
    public Transform Player;
        //=> GameManager.Instance.Player.transform;
    public float detectionRange = 5f;
    public float moveSpeed = 2f;

    [Header("순찰 설정")]
    public float patrolDistance = 3f;
    public float patrolWaitTime = 1f;

    [Header("수색 설정")]
    public float searchWaitTime = 2f;
    public float searchEndWaitTime = 2f;
    public float searchDistance = 1.5f;
    public int searchLaps = 2;

    [Header("추적 종료 설정")]
    public float lostTargetWaitTime = 2f;
    public float returnedWaitTime = 1f;

    private Vector3 startPos;
    private Vector3[] patrolPoints;
    private int currentPatrolIndex = 0;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float stateTimer = 0f;
    private bool isPatrolWaiting = false;

    private Vector3 searchCenter;
    private int currentSearchLap = 0;
    private bool searchingRight = true;
    private bool isSearchWaiting = false;

    private PlayerHide playerHide;
    private Transform currentHideArea;

    private enum AIState
    {
        Patrolling,
        Chasing,
        SearchWaiting,
        Searching,
        SearchComplete,
        LostTarget,
        Returning,
        Waiting
    }

    private AIState currentState = AIState.Patrolling;

    void Start()
    {
        startPos = transform.position;
        if (Player != null)
        {
            playerHide = Player.GetComponent<PlayerHide>();
        }
        SetupPatrolPoints();
        ChangeState(AIState.Patrolling);
    }

    void Update()
    {
        stateTimer += Time.deltaTime;
        UpdateCurrentState();
        CheckStateTransitions();
    }

    void ChangeState(AIState newState)
    {
        currentState = newState;
        stateTimer = 0f;

        switch (newState)
        {
            case AIState.Patrolling:
                SetNextPatrolTarget();
                isPatrolWaiting = false;
                break;
            case AIState.Searching:
                SetupSearchPattern();
                isSearchWaiting = false;
                break;
            case AIState.Returning:
                SetTarget(startPos);
                break;
            default:
                StopMoving();
                break;
        }
    }

    void UpdateCurrentState()
    {
        switch (currentState)
        {
            case AIState.Patrolling:
                UpdatePatrolling();
                break;
            case AIState.Chasing:
                UpdateChasing();
                break;
            case AIState.SearchWaiting:
                StopMoving();
                break;
            case AIState.Searching:
                UpdateSearching();
                break;
            case AIState.SearchComplete:
                UpdateSearchComplete();
                break;
            case AIState.LostTarget:
                StopMoving();
                break;
            case AIState.Returning:
                MoveToTarget(moveSpeed * 0.9f);
                break;
            case AIState.Waiting:
                StopMoving();
                break;
        }
    }

    void CheckStateTransitions()
    {
        if (Player == null)
        {
            if (currentState != AIState.Patrolling)
                ChangeState(AIState.Patrolling);
            return;
        }

        bool hiding = playerHide != null && playerHide.IsHiding;
        float dist = Vector3.Distance(transform.position, Player.position);
        bool inRange = dist < detectionRange;

        switch (currentState)
        {
            case AIState.Patrolling:
                if (hiding) { FindCurrentHideArea(); ChangeState(AIState.SearchWaiting); }
                else if (inRange) ChangeState(AIState.Chasing);
                break;
            case AIState.Chasing:
                if (hiding) { FindCurrentHideArea(); ChangeState(AIState.SearchWaiting); }
                else if (!inRange) ChangeState(AIState.LostTarget);
                break;
            case AIState.SearchWaiting:
                if (!hiding && inRange) ChangeState(AIState.Chasing);
                else if (stateTimer >= searchWaitTime) ChangeState(AIState.Searching);
                break;
            case AIState.Searching:
                if (!hiding && inRange) ChangeState(AIState.Chasing);
                break;
            case AIState.SearchComplete:
                if (!hiding && inRange) ChangeState(AIState.Chasing);
                else if (stateTimer >= searchEndWaitTime) ChangeState(AIState.Returning);
                break;
            case AIState.LostTarget:
                if (!hiding && inRange) ChangeState(AIState.Chasing);
                else if (stateTimer >= lostTargetWaitTime) ChangeState(AIState.Returning);
                break;
            case AIState.Returning:
                if (!hiding && inRange) ChangeState(AIState.Chasing);
                else if (Vector3.Distance(transform.position, startPos) <= 0.5f) ChangeState(AIState.Waiting);
                break;
            case AIState.Waiting:
                if (!hiding && inRange) ChangeState(AIState.Chasing);
                else if (stateTimer >= returnedWaitTime) ChangeState(AIState.Patrolling);
                break;
        }
    }

    void UpdatePatrolling()
    {
        if (isPatrolWaiting)
        {
            if (stateTimer >= patrolWaitTime)
            {
                isPatrolWaiting = false;
                SetNextPatrolTarget();
                stateTimer = 0f;
            }
            return;
        }

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
        {
            StopMoving();
            isPatrolWaiting = true;
            stateTimer = 0f;
        }
        else
        {
            MoveToTarget(moveSpeed * 0.7f);
        }
    }

    void UpdateChasing()
    {
        if (Player != null)
        {
            SetTarget(Player.position);
            MoveToTarget(moveSpeed);
        }
    }

    void UpdateSearching()
    {
        if (isSearchWaiting)
        {
            if (stateTimer >= 1f)
            {
                isSearchWaiting = false;
                stateTimer = 0f;
                SetNextSearchTarget();
            }
            return;
        }

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
        {
            isSearchWaiting = true;
            stateTimer = 0f;
        }
        else
        {
            MoveToTarget(moveSpeed * 0.6f);
        }
    }

    void UpdateSearchComplete()
    {
        if (Vector3.Distance(transform.position, searchCenter) > 0.3f)
        {
            MoveToTarget(moveSpeed * 0.6f);
        }
    }

    void SetTarget(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    void MoveToTarget(float speed)
    {
        if (!isMoving) return;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void StopMoving()
    {
        isMoving = false;
    }

    void SetNextPatrolTarget()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        SetTarget(patrolPoints[currentPatrolIndex]);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void SetupPatrolPoints()
    {
        Vector3 leftPoint = startPos + Vector3.left * patrolDistance;
        Vector3 rightPoint = startPos + Vector3.right * patrolDistance;

        List<Vector3> validPoints = new List<Vector3>();
        if (!IsBlocked(leftPoint)) validPoints.Add(leftPoint);
        if (!IsBlocked(rightPoint)) validPoints.Add(rightPoint);

        if (validPoints.Count == 2)
            patrolPoints = new Vector3[] { validPoints[0], validPoints[1] };
        else if (validPoints.Count == 1)
            patrolPoints = new Vector3[] { startPos, validPoints[0] };
        else
            patrolPoints = new Vector3[] {
                startPos + Vector3.left * 0.5f,
                startPos + Vector3.right * 0.5f
            };
    }

    bool IsBlocked(Vector3 position)
    {
        Collider2D obstacle = Physics2D.OverlapCircle(position, 0.2f);
        return obstacle != null && obstacle.CompareTag("Ground");
    }

    void SetupSearchPattern()
    {
        searchCenter = transform.position;
        currentSearchLap = 0;
        searchingRight = true;
        SetNextSearchTarget();
    }

    void SetNextSearchTarget()
    {
        if (currentSearchLap >= searchLaps)
        {
            SetTarget(searchCenter);
            ChangeState(AIState.SearchComplete);
            return;
        }

        Vector3 nextTarget = searchingRight
            ? searchCenter + Vector3.right * searchDistance
            : searchCenter + Vector3.left * searchDistance;

        searchingRight = !searchingRight;
        if (!searchingRight) currentSearchLap++;

        SetTarget(nextTarget);
    }

    void FindCurrentHideArea()
    {
        GameObject[] hideAreas = GameObject.FindGameObjectsWithTag("HideArea");
        float closest = float.MaxValue;
        Transform closestArea = null;

        foreach (GameObject obj in hideAreas)
        {
            float dist = Vector3.Distance(Player.position, obj.transform.position);
            if (dist < closest)
            {
                closest = dist;
                closestArea = obj.transform;
            }
        }

        currentHideArea = closestArea;
    }
}
