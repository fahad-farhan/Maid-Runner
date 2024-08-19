using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : InstanceManager<GameManager>
{

    #region VARIABLES
    [Header("GameObjects")]
    [Space]
    [SerializeField] AudioSource[] sound;
    [SerializeField] GameObject perfect;
    [SerializeField] GameObject bad;
    [SerializeField] Sprite[] goodSmiley;
    [SerializeField] Sprite[] badSmiley;

    [Header("UI Panel")]
    [Space]
    public GameObject finishPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] Text levelText;
    [SerializeField] Text coinText;

    [Header(("Variables"))]
    [Space]
    [HideInInspector] public bool gameStarted;
    private int smileyNum = 0;
    int level;
    int coin;
    #endregion
    #region START

    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        GetDatas();
    }

    private void Start()
    {
        LevelGenerate();
    }

    #endregion
# region UI PANEL CONTROL
    public void FinishLevel()
    {
        gameStarted = false;
        sound[0].Play();
        NPCController.Instance.AI_Anim.Play("Fail");
        StartCoroutine(FinishPanel());
        LevelUp();
    }

    public void GameOver()
    {
        gameStarted = false;
        sound[1].Play();
        PlayerManager.Instance.playerAnim.Play("Fail");
        PlayerManager.Instance.DisableObjects();
        StartCoroutine(OverPanel());
    }

    IEnumerator FinishPanel()
    {
        yield return new WaitForSeconds(5f);
        gamePanel.SetActive(false);
        finishPanel.SetActive(true);
        GetReward.instance.callFillBox();
        AddCoin(50);

    }

    IEnumerator OverPanel()
    {
        yield return new WaitForSeconds(3f);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

    }
    #endregion
    #region SAVE AND GAME SETUP
    public void SceneLoad()
    {
        SceneManager.LoadScene(0);
    }

    public void AddCoin(int newCoin)
    {
        sound[2].Play();
        int prevCoin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", prevCoin + newCoin);
        coin = newCoin;
        coinText.text = coin.ToString();
    }

    private void LevelGenerate()
    {
        int i = level - 1;
        // Generate level
        StageBuilder.Instance.SpawnLevel(i);
        coinText.text = coin.ToString();
        levelText.text = "LEVEL " + level.ToString();
    }

    private void LevelUp()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
    }

    private void GetDatas()
    {
      
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
            level = 1;
        }

       
        if (PlayerPrefs.HasKey("coin"))
        {
            coin = PlayerPrefs.GetInt("coin");
        }
        else
        {
            PlayerPrefs.SetInt("coin", coin);
        }

   
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }
    }

    private void GetPlayerControl()
    {
        PlayerManager.Instance.playerAnim.Play("Run");
        PlayerManager.Instance.confeti = GameObject.FindGameObjectWithTag("Confeti").GetComponent<ParticleSystem>();
        PlayerManager.Instance._finishLine = GameObject.FindGameObjectWithTag("Finish").transform;
        PlayerManager.Instance._distance = Vector3.Distance(transform.position, PlayerManager.Instance._finishLine.position);
        NPCController.Instance.AI_Anim.Play("Run");
        NPCController.Instance.RandomChange();
    }
    #endregion
    #region UI SETUP

    public void StartButton()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameStarted = true;
        GetPlayerControl();
    }

    public void RestartButton()
    {
        FindObjectOfType<AdvertisementManager>().ShowAdmobInterstitial();
        SceneLoad();
    }
    #endregion
    #region SMILEY SPAWNER
    public void PerfectSmiley()
    {
        
        perfect.GetComponent<Image>().sprite = goodSmiley[smileyNum];       
        perfect.transform.DOScale(1f, 1f).OnComplete(() =>
        {
            perfect.transform.DOScale(0f, 0.5f);
            smileyNum = Random.Range(0, 3);
        });

    }

    public void BadSmiley()
    {

        bad.GetComponent<Image>().sprite = badSmiley[smileyNum];
        bad.transform.DOScale(1f, 1f).OnComplete(() =>
        {
            bad.transform.DOScale(0f, 0.5f);
            smileyNum = Random.Range(0, 3);
           
        });
    }
    #endregion


}