using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : InstanceManager<NPCController>
{

    #region Variables

    [Header("Objects")]
    [Space]
    [SerializeField] GameObject[] cleanObject;
    [SerializeField] ParticleSystem spawnParticle;
    public ParticleSystem vacumParticle;
    public Animator AI_Anim;

    [Header("Variables")]
    [Space]
    int useNumber = 0;
    int xNum;
    float AISpeed = 2;
    bool colide = false;
    #endregion
    #region STARTER
   
    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (GameManager.Instance.gameStarted)
            transform.Translate(transform.forward * AISpeed * Time.deltaTime, Space.World);
    }
    #endregion

    #region  CHANGECLEANER
    private void CheckClean(int x)
    {
        useNumber = x;
        switch (useNumber)
        {
            case 1:
                cleanObject[0].SetActive(true);
                cleanObject[1].SetActive(false);
                cleanObject[2].SetActive(false);
                break;
            case 2:
                cleanObject[0].SetActive(false);
                cleanObject[1].SetActive(true);
                cleanObject[2].SetActive(false);
                break;
            case 3:
                cleanObject[0].SetActive(false);
                cleanObject[1].SetActive(false);
                cleanObject[2].SetActive(true);
                break;

            default:
                break;
        }
        spawnParticle.Play();
    }

    public void RandomChange()
    {
        int xNums = Random.Range(1, 3);
        CheckClean(xNums);
    }
    #endregion
# region TRIGGER EVENT
    private void OnTriggerStay(Collider other)
    {

        // GATE & AI
        if (other.gameObject.CompareTag("Respawn"))
        {
            int gateNumber = other.gameObject.GetComponent<BarrierController>().gateNum;
            xNum = gateNumber;
            if (useNumber == gateNumber)
            {
                AISpeed = 2.3f;
                AI_Anim.speed = 1.2f;

            }
            else
            {
                AISpeed = 1.6f;
                AI_Anim.speed = 0.8f;

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Respawn"))
        {
            if (!colide)
            {
                colide = true;
                RandomChange();
            }
        }

        // FINISH && AI
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.GameOver();
            AI_Anim.Play("Win");

            for (int i = 0; i < cleanObject.Length; i++)
            {
                cleanObject[i].SetActive(false);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // GATE EXIT && AI
        if (other.gameObject.CompareTag("Respawn"))
        {
            AISpeed = 2f;
            AI_Anim.speed = 1f;

            if (colide)
            {
                colide = false;
            }
        }

    }
    #endregion
  
}
