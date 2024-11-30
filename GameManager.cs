using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("game contrl")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public bool isLive;

    [Header("Game object")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelup;
    public Result uiResult;
    public Transform uiJoy;
    public GameObject enemyClener;
    public BoxOpen boxOpen;

    [Header("player info")]
    public int playerId;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {10,20,30,50,70,90,130};
    public int coin;

     void Awake()
     {
        Instance = this;
        Application.targetFrameRate = 60;
     }

    private void Start()
    {
        coin = PlayerPrefs.GetInt("CoinCount", 0); // 기본값은 0
        Debug.Log("Loaded Coin Count: " + coin);
    }

    public void GameStart(int id)
    {
        playerId = id;
        GameManager.Instance.player.curr_health = GameManager.Instance.player.max_health;

        player.gameObject.SetActive(true);

        //기본무기 지급
        uiLevelup.Select(playerId%2);

        Resume();

        AudioManager.Instance.PlayBgm(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyClener.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive)
            return;


        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if(!isLive)
            return;

        exp++;

        if(exp >= nextExp[Mathf.Min(level,nextExp.Length-1)])//최고경험치를 그대로 사용
        {
            level++;
            exp = 0;
            uiLevelup.Show();

        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;//시간정지
        uiJoy.localScale = Vector3.zero;
    }
    
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;//시간 이동, 기본이1
        uiJoy.localScale = Vector3.one;
    }
}
