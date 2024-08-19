﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);

        }

    }
}
