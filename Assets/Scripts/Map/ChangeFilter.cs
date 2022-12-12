using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChangeFilter : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private float dangerRange;
    [SerializeField] private PostProcessVolume volume;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        ray = new(transform.position, Vector3.up);
    }

    private void FixedUpdate()
    {
        ray.origin = transform.position;
        if (Physics.SphereCast(transform.position, dangerRange, transform.forward, out RaycastHit hit, 5f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!volume.enabled) volume.enabled = true;
            }
        }
        else
        {
            if (volume.enabled) volume.enabled = false;
        }
    }
}
