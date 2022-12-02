using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    public void MonsterState(int level)
    {
        switch (level)
        {
            case 0: Prowl();
                break;
            case 1: NearPlayer();
                break;
            case 2: SeePlayer();
                break;
            default:
                break;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Prowl()
    {
        Debug.Log("prowl");
    }

    private void NearPlayer()
    {
        Debug.Log("near");
    }

    private void SeePlayer()
    {
        Debug.Log("see");
        agent.SetDestination(player.transform.position);
    }
}
