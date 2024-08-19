using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

       
        if (other.gameObject.CompareTag("Coin"))
        {
            
            Destroy(gameObject);
        }

    }
}
