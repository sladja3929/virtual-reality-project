using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMovement : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [Tooltip("음수면 횟수제한 없음")]
    [SerializeField] private int loopCnt = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (loopCnt != 0)
            {
                other.gameObject.transform.SetPositionAndRotation(destination.position, destination.rotation);
                --loopCnt;
            }
        }
    }
}
