using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class onClick_MainMenu : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

    }
}