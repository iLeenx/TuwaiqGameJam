using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State { IdleAtShop, Chasing, ReturnToDoor }

    public Transform chaseSpawnPoint;
    public Transform returnDoorPoint;

    public float chaseSpeed = 8f;
    public float chaseLoseRange = 15f;
    public float stopDistance = 0.8f;

    public float returnSpeed = 5f;
    public float doorStopDistance = 0.5f;

    Transform player;
    NavMeshAgent agent;
    Animator animator;

    State state = State.IdleAtShop;

    Vector3 homePosition;
    float rehomeCooldown = 0f;

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

        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);

        if (rehomeCooldown > 0f)
            rehomeCooldown -= Time.deltaTime;

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
            if (player == null) return;

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
            bool warped = agent.Warp(chaseSpawnPoint.position);
            if (!warped)
                transform.position = chaseSpawnPoint.position;
        }

        state = State.Chasing;

        agent.speed = chaseSpeed;
        agent.stoppingDistance = stopDistance;
        agent.isStopped = false;
    }

    void GoIdleAtHome()
    {
        state = State.IdleAtShop;

        agent.isStopped = true;
        agent.ResetPath();

        Vector3 snapPos = homePosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(homePosition, out hit, 2f, NavMesh.AllAreas))
            snapPos = hit.position;

        bool warped = agent.Warp(snapPos);
        if (!warped)
            transform.position = snapPos;

        rehomeCooldown = 0.25f;
    }

    void StartReturnToDoor()
    {
        if (rehomeCooldown > 0f) return;

        state = State.ReturnToDoor;

        agent.isStopped = false;
        agent.speed = returnSpeed;
        agent.stoppingDistance = doorStopDistance;

        if (returnDoorPoint != null)
            agent.SetDestination(returnDoorPoint.position);
        else
            GoIdleAtHome();
    }

    void DoReturnToDoor()
    {
        if (returnDoorPoint == null)
        {
            GoIdleAtHome();
            return;
        }

        float distToDoor = Vector3.Distance(transform.position, returnDoorPoint.position);

        if (distToDoor <= doorStopDistance + 0.25f)
        {
            GoIdleAtHome();
            return;
        }

        LookAtFlat(returnDoorPoint.position);
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
