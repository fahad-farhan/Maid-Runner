using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grime : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.CompareTag("Supurge"))
        {
            Destroy(gameObject);
            PlayerManager.Instance.dirtyParcticle.Play();
        }

    }

}
