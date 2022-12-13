using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotationChair : MonoBehaviour
{
    [SerializeField] float minAngle = 10;
    [SerializeField] float maxAngle = 40;

    void Awake()
    {
        Debug.Log(transform.rotation.eulerAngles.x);
    }
    IEnumerator Start()
    {
        float endTime;
        float rotTime;
        Quaternion curRot = transform.rotation;
        Vector3 targetVec;
        Quaternion targetRot;

        while (true)
        {
            targetVec = curRot.eulerAngles;
            targetVec.x += Random.Range(minAngle, maxAngle);
            targetRot = Quaternion.Euler(targetVec);

            endTime = 0f;
            rotTime = Random.Range(1.5f, 2f);
            while (endTime < rotTime)
            {
                
                transform.rotation = Quaternion.Slerp(curRot, targetRot, endTime / rotTime);
                endTime += Time.deltaTime;
                yield return null;
            }

            endTime = 0f;
            rotTime = Random.Range(1.5f, 2f);
            while (endTime < rotTime)
            {
                transform.rotation = Quaternion.Slerp(targetRot, curRot, endTime / rotTime);
                endTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
