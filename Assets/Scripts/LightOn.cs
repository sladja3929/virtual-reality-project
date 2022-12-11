using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour
{
    public void Light(int idx)
    {
        transform.GetChild(idx).gameObject.SetActive(true);
    }
}
