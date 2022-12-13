using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLight : MonoBehaviour
{
    public void OffLight()
    {
        GetComponent<Light>().enabled = false;
    }
}
