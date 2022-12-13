using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AutoMovementChair : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float endTime;

    private bool didWork = false;
    private GameObject player;
    private Vector3 originPos;

    private void Start()
    {
        originPos = transform.position;
        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (didWork) return;
        if (other.transform.CompareTag("Player"))
        {
            didWork = true;
            player = other.gameObject;
            StartCoroutine(AutoMovementCoroutine());
        }
    }

    private IEnumerator AutoMovementCoroutine()
    {
        float time = 0f;
        while (endTime - time > 0.01f)
        {
            transform.position = Vector3.Slerp(transform.position, destination.position, time / endTime);
            time += Time.deltaTime;
            yield return null;
        }

        // 카메라가 해당 이벤트를 벗어나게 고개를 돌리면 원위치로
        Vector3 curRot;
        while (true)
        {
            curRot = transform.position - player.transform.position;
            Debug.Log(Vector3.Angle(curRot, player.transform.forward));
            if (Vector3.Angle(curRot, player.transform.forward) > 120)
            {
                didWork = false;
                transform.position = originPos;
                break;
            }
            yield return null;
        }

        // 이벤트 쿨타임
        yield return new WaitForSeconds(20f);
    }
}
