using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hazard : MonoBehaviour
{
    [SerializeField] bool enemy;
    private void OnTriggerEnter(Collider other)
    {

        // OBSTACLE VACUUM TAG == GameController
        if (other.gameObject.CompareTag("GameController"))
        {
            transform.DOMove(other.transform.position, 1.5f);
            if (enemy == false)
                PlayerManager.Instance.vacumParticle.Play();
            else
                NPCController.Instance.vacumParticle.Play();
        }
    }
}
