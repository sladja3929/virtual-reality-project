using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("key"))
        {
            Debug.Log("¿­¼è È¹µæ");
            gameObject.GetComponent<GameManager>().increaseKeyValue();
            Destroy(collider.gameObject);
        }
    }
}
