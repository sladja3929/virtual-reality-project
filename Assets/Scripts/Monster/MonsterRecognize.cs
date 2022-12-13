using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRecognize : MonoBehaviour
{
    public int area_level;
    private MonsterAI AIScript;

    private void Start()
    {
        AIScript = transform.GetComponentInParent<MonsterAI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //AIScript.ChangeAI(area_level);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //AIScript.ChangeAI(area_level - 1);
        }
    }
}
