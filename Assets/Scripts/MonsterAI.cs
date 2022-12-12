using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState { None = -1, Idle = 0, Prowl, Recognize, Race, }
public class MonsterAI : MonoBehaviour
{
    public SkinnedMeshRenderer body;
    public Transform head;
    public float walkSpeed = 3f;
    public float prowlDistance = 10;
    public float hearRange = 10;
    public float seeRange = 5;
    public int level = 1;

    GameObject player;
    NavMeshAgent agent;
    Animator anim;

    Vector3 velocity = Vector3.zero;
    private MonsterState monsterState = MonsterState.None;
    float distance;

    public void ChangeState(MonsterState newState)
    {
        if (monsterState == newState) return;

        StopCoroutine(monsterState.ToString());

        monsterState = newState;

        StartCoroutine(monsterState.ToString());
    }

    public void ChangeAILevel(int l)
    {
        level = l;
        walkSpeed = walkSpeed * 2f;
        if(level != 1)
        {
            anim.SetBool("isRunning", true);
        }
        if(level == 3)
        {
            hearRange = 100;
            seeRange = 50;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updatePosition = false;
        ChangeState(MonsterState.Idle);

        StartCoroutine("DebugLevelChange", 2);
    }
    void Update()
    {
        Vector2 forward = new Vector2(transform.position.z, transform.position.x);
        Vector2 steeringTarget = new Vector2(agent.steeringTarget.z, agent.steeringTarget.x);

        Vector2 dir = steeringTarget - forward;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angle;
    }

    private IEnumerator DebugLevelChange(int l)
    {
        yield return new WaitForSeconds(10);
        ChangeAILevel(l);
        Debug.Log("level " + l);
    }
    private void CalculateDistanceToPlayer()
    {
        if (player == null) return;

        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= seeRange)
        {
            ChangeState(MonsterState.Race);
        }
        else if (distance <= hearRange)
        {
            ChangeState(MonsterState.Recognize);
        }
        
        else if(distance >= hearRange)
        {
            ChangeState(MonsterState.Prowl);
        }
    }
    private IEnumerator Idle()
    {
        anim.SetBool("isMoving", false);
        StartCoroutine("IdletoProwl");
        agent.velocity = Vector3.zero;

        while (true)
        {
            CalculateDistanceToPlayer();
            yield return null;
        }
    }

    private IEnumerator IdletoProwl()
    {
        yield return new WaitForSeconds(4);
        ChangeState(MonsterState.Prowl);
    }

    private IEnumerator Prowl()
    {
        Debug.Log("prowl");
        anim.SetBool("isMoving", true);
        float currentTime = 0;
        float maxTime = 6;

        agent.speed = walkSpeed;
        Vector3 targetPosition = SetProwlPosition();
        agent.SetDestination(targetPosition);

        while (true)        {
            GoToDestination(targetPosition);

            currentTime += Time.deltaTime;

            Vector3 to = new Vector3(agent.destination.x, 0, agent.destination.z);
            Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);

            if ((to - from).sqrMagnitude < 0.01f || currentTime >= maxTime) 
            {
                ChangeState(MonsterState.Idle);
            }

            CalculateDistanceToPlayer();

            Debug.DrawLine(transform.position, targetPosition, Color.blue);
            yield return null;
        }
    }

    private IEnumerator Recognize()
    {
        Debug.Log("hear");
        anim.SetBool("isMoving", true);

        agent.speed = walkSpeed;
        agent.SetDestination(player.transform.position);

        while (true)
        {
            GoToDestination(player.transform.position);
            CalculateDistanceToPlayer();
            yield return null;
        }
    }

    private IEnumerator Race()
    {
        Debug.Log("see");
        anim.SetBool("isMoving", true);
        agent.speed = walkSpeed;

        while (true)
        {
            agent.SetDestination(player.transform.position);
            GoToDestination(player.transform.position);
            //RotationToTarget();

            CalculateDistanceToPlayer();
            yield return null;
        }
    }
    private void GoToDestination(Vector3 position)
    {
        if (agent.speed == 0)
        {
            agent.speed = walkSpeed;
        }

        transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f);
    }

    private Vector3 SetProwlPosition()
    {
        int jitter = 0;
        int jitterMin = 0;
        int jitterMax = 360;

        jitter = Random.Range(jitterMin, jitterMax);
        Vector3 targetPosition = transform.position + SetAngle(prowlDistance, jitter);
    
        return targetPosition;
    }

    private Vector3 SetAngle(float radius, int angle)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Cos(angle) * radius;
        position.z = Mathf.Sin(angle) * radius;

        return position;
    }

    private void RotationToTarget()
    {
        Vector3 to = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);

        transform.rotation = Quaternion.LookRotation(to - from);
    }

    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, seeRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, prowlDistance);
    }
}
