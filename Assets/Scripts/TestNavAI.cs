using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavAI : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    void Start()
    {
        // 해당 개체의 NavMeshAgent 를 참조합니다.
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 매프레임마다 목표지점으로 이동합니다.
        agent.SetDestination(target.position);

    }
}