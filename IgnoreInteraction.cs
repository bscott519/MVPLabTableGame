using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IgnoreInteraction"))
        {

        }
    }
}
