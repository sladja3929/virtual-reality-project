using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool m_isOpen = false;
    private bool isInteract = true;
    private IEnumerator doorCoroutine;

    // 플레이어가 열고 닫는 문
    public void InteractDoor(float rotTime = 0.8f)
    {
        if (!isInteract) return;
        if (m_isOpen) doorCoroutine = DoorCoroutine(false, rotTime);
        else doorCoroutine = DoorCoroutine(true, rotTime);
        if (doorCoroutine is not null) StopCoroutine(doorCoroutine);
        StartCoroutine(doorCoroutine);
        m_isOpen = !m_isOpen;
    }

    // 트리거로 인한 문은 플레이어가 풀 수 없음
    public void InteractDoor(bool isOpen, float rotTime = 0.8f)
    {
        isInteract = false;
        doorCoroutine = DoorCoroutine(isOpen, rotTime);
        if (doorCoroutine is not null) StopCoroutine(doorCoroutine);
        StartCoroutine(doorCoroutine);
        m_isOpen = !m_isOpen;
    }

    // 특정 조건이 지나고 플레이어 조작 가능 문이 됨
    public void SetInteractable() => isInteract = true;

    private IEnumerator DoorCoroutine(bool isOpen, float rotTime)
    {
        float endTime;
        Quaternion curRot = transform.rotation;
        Vector3 targetVec;
        Quaternion targetRot;

        targetVec = transform.rotation.eulerAngles;
        if (isOpen) targetVec.y += 105f;
        else targetVec.y -= 105f;
        targetRot = Quaternion.Euler(targetVec);

        endTime = 0f;
        while (endTime < rotTime)
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(curRot.eulerAngles, targetRot.eulerAngles, endTime / rotTime));
            endTime += Time.deltaTime;
            yield return null;
        }
    }
}
