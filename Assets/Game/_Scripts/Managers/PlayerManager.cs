using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class PlayerManager : InstanceManager<PlayerManager>
{

    #region VARIABLES
    [Header("Objects")]
    [Space]
    [SerializeField] GameObject[] cleanObject;
    [SerializeField] Image _progressBar;
    [SerializeField] CinemachineVirtualCamera cm;
    public Animator playerAnim;

    [Header("Particles")]
    [Space]
    [SerializeField] ParticleSystem spawnParticle;
    public ParticleSystem vacumParticle;
    public ParticleSystem dirtyParcticle;
    public ParticleSystem confeti;

    [Header("Variables")]
    [Space]
    [HideInInspector] public float _distance;
    [HideInInspector] public Transform _finishLine;
    private Vector3 _firstPlayerPosition;
    private int useNumber = 0;
    private float playerSpeed = 2;
    private int xNum;
    #endregion
    #region  METHODS
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _firstPlayerPosition = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            transform.Translate(transform.forward * playerSpeed * Time.deltaTime, Space.World);
            FillProgressBar();
        }

    }
    #endregion
    #region  GAME EVENT   
    public void CheckClean(int x)
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

    private void FillProgressBar()
    {
        float currentDistance = Vector3.Distance(transform.position, _firstPlayerPosition);
        _progressBar.fillAmount = currentDistance / _distance;
    }

    private void SmileyCheck()
    {

        if (xNum == useNumber)
        {
            GameManager.Instance.PerfectSmiley();
        }
        else if (xNum != useNumber)
        {
            GameManager.Instance.BadSmiley();
        }

    }

    public void DisableObjects()
    {
        for (int i = 0; i < cleanObject.Length; i++)
        {
            cleanObject[i].SetActive(false);
        }
    }
    #endregion
# region TRIGGER EVENT
    private void OnTriggerStay(Collider other)
    {

        // GATE & PLAYER
        if (other.gameObject.CompareTag("Respawn"))
        {
            int gateNumber = other.gameObject.GetComponent<BarrierController>().gateNum;
            xNum = gateNumber;
            if (useNumber == gateNumber)
            {
                playerSpeed = 2.3f;
                playerAnim.speed = 1.2f;

            }
            else
            {
                playerSpeed = 1.5f;
                playerAnim.speed = 0.8f;

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // GATE & PLAYER
        if (other.gameObject.CompareTag("Respawn"))
        {
            InvokeRepeating("SmileyCheck", 1f, 3f);

        }

        // FINISH & PLAYER
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.FinishLevel();
            playerAnim.Play("Win");
            confeti.Play();
            DisableObjects();

        }
    }

    private void OnTriggerExit(Collider other)
    {

        // GATE & PLAYER
        if (other.gameObject.CompareTag("Respawn"))
        {
            CancelInvoke();
            playerSpeed = 2f;
            playerAnim.speed = 1f;

        }

    }
    #endregion

  
}