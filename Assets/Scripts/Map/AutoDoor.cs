using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    [SerializeField] float minAngle = 10;
    [SerializeField] float maxAngle = 40;

    IEnumerator Start()
    {
        float endTime;
        float rotTime;
        Quaternion curRot = transform.rotation;
        Vector3 targetVec;
        Quaternion targetRot;

        while (true)
        {
            targetVec = transform.rotation.eulerAngles;
            targetVec.y += Random.Range(minAngle, maxAngle);
            targetRot = Quaternion.Euler(targetVec);

            endTime = 0f;
            rotTime = Random.Range(1.5f, 2f);
            while (endTime < rotTime)
            {
                transform.rotation = Quaternion.Euler(Vector3.Lerp(curRot.eulerAngles, targetRot.eulerAngles, endTime / rotTime));
                endTime += Time.deltaTime;
                yield return null;
            }

            endTime = 0f;
            rotTime = Random.Range(1.5f, 2f);
            while (endTime < rotTime)
            {
                transform.rotation = Quaternion.Euler(Vector3.Lerp(targetRot.eulerAngles, curRot.eulerAngles, endTime / rotTime));
                endTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
