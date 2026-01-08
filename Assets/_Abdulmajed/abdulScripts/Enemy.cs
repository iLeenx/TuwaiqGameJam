using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State { IdleAtShop, Chasing, ReturnToDoor }

    [Header("Points")]
    public Transform chaseSpawnPoint;
    public Transform returnDoorPoint;

    [Header("Chase")]
    public float chaseSpeed = 8f;
    public float chaseLoseRange = 15f;
    public float stopDistance = 0.8f;

    [Header("Return")]
    public float returnSpeed = 5f;
    public float doorStopDistance = 0.6f;

    [Header("Re-Chase While Returning")]
    public float reacquireRange = 10f;

    [Header("Animator Params (must match exactly)")]
    public string isChasingParam = "IsChasing";
    public string isReturningParam = "IsReturning";

    Transform player;
    NavMeshAgent agent;
    Animator animator;

    State state = State.IdleAtShop;

    Vector3 homePosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (agent != null)
            agent.updateRotation = false;

        homePosition = transform.position;
        GoIdleAtHome();
    }

    void Update()
    {
        if (agent == null) return;

        if (Input.GetKeyDown(KeyCode.T))
            OnTheft();

        if (state == State.IdleAtShop)
        {
            agent.isStopped = true;
            agent.ResetPath();

            if (player != null)
                LookAtFlat(player.position);

            return;
        }

        if (state == State.Chasing)
        {
            DoChase();
            return;
        }

        if (state == State.ReturnToDoor)
        {
            DoReturnToDoor();
            return;
        }
    }

    public void OnTheft()
    {
        StartChaseFromSpawn();
    }

    void StartChaseFromSpawn()
    {
        if (agent == null) return;

        agent.isStopped = true;
        agent.ResetPath();

        if (chaseSpawnPoint != null)
        {
            Vector3 p = chaseSpawnPoint.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(p, out hit, 2f, NavMesh.AllAreas))
                p = hit.position;

            if (!agent.Warp(p))
                transform.position = p;
        }

        SetStateChasing();
    }

    void DoChase()
    {
        if (player == null)
        {
            StartReturnToDoor();
            return;
        }

        agent.speed = chaseSpeed;
        agent.stoppingDistance = stopDistance;
        agent.isStopped = false;

        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        agent.SetDestination(targetPos);

        float distToPlayer = Vector3.Distance(transform.position, player.position);
        if (distToPlayer > chaseLoseRange)
        {
            StartReturnToDoor();
            return;
        }

        LookAtFlat(player.position);
    }

    void StartReturnToDoor()
    {
        if (returnDoorPoint == null)
        {
            GoIdleAtHome();
            return;
        }

        state = State.ReturnToDoor;

        agent.isStopped = false;
        agent.speed = returnSpeed;
        agent.stoppingDistance = doorStopDistance;
        agent.SetDestination(returnDoorPoint.position);

        SetAnim(false, true);
    }

    void DoReturnToDoor()
    {
        if (player != null)
        {
            float d = Vector3.Distance(transform.position, player.position);
            if (d <= reacquireRange)
            {
                SetStateChasing();
                return;
            }
        }

        if (returnDoorPoint == null)
        {
            GoIdleAtHome();
            return;
        }

        LookAtFlat(returnDoorPoint.position);

        float distToDoor = Vector3.Distance(transform.position, returnDoorPoint.position);
        if (distToDoor <= doorStopDistance + 0.25f)
        {
            GoIdleAtHome();
        }
    }

    void GoIdleAtHome()
    {
        state = State.IdleAtShop;

        if (agent == null) return;

        agent.isStopped = true;
        agent.ResetPath();

        Vector3 snapPos = homePosition;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(homePosition, out hit, 2f, NavMesh.AllAreas))
            snapPos = hit.position;

        if (!agent.Warp(snapPos))
            transform.position = snapPos;

        SetAnim(false, false);
    }

    void SetStateChasing()
    {
        state = State.Chasing;

        agent.speed = chaseSpeed;
        agent.stoppingDistance = stopDistance;
        agent.isStopped = false;

        SetAnim(true, false);
    }

    void SetAnim(bool isChasing, bool isReturning)
    {
        if (animator == null) return;

        animator.SetBool(isChasingParam, isChasing);
        animator.SetBool(isReturningParam, isReturning);
    }

    void LookAtFlat(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 8f * Time.deltaTime);
    }
}
