using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
    {
        Home, 
        Gameplay,
        Pause,
        Gameover
    }
public class GameManagerr : MonoBehaviour
{
    private static GameManagerr m_Instance;
    public static GameManagerr Instance
    {
        get{
            if(m_Instance ==null)
             {
            m_Instance =FindObjectOfType<GameManagerr>();
            
        }
        return m_Instance;
        }   
       
    }
    [SerializeField] private HomePanel m_HomePanel;
    [SerializeField] private GameplayPanel m_GameplayPanel;
    [SerializeField] private PausePanel m_PausePanel;
    [SerializeField] private GameoverPanel m_GameoverPanel;
    // private SpawnManager m_SpawnManager;
    private GameState m_GameState;
    // private AudioManager m_AudioManger;
    private bool m_Win;
    private int m_Score;
    private void Awake() {
        if(m_Instance==null)
            m_Instance=this;
        else if(m_Instance!=this)
        Destroy(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        m_HomePanel.gameObject.SetActive(false);
        m_GameplayPanel.gameObject.SetActive(false);
        m_PausePanel.gameObject.SetActive(false);
        m_GameoverPanel.gameObject.SetActive(false);
        // m_SpawnManager=FindObjectOfType<SpawnManager>();
        // m_AudioManger=FindObjectOfType<AudioManager>();
        SetState(GameState.Home);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetState(GameState state)
    {
        m_GameState= state;
        m_HomePanel.gameObject.SetActive(m_GameState==GameState.Home);
        m_GameplayPanel.gameObject.SetActive(m_GameState==GameState.Gameplay);
        m_PausePanel.gameObject.SetActive(m_GameState==GameState.Pause);
        m_GameoverPanel.gameObject.SetActive(m_GameState==GameState.Gameover);
        if(m_GameState==GameState.Pause)
        {
            Time.timeScale=0;
        }
        else
        {
            Time.timeScale= 1;
        }

        if(m_GameState==GameState.Home)
        {
            AudioManager.Instance.PlayHomeMusic();
        }
        else
        {
            AudioManager.Instance.PlayBattleMusic();
        }
        
    }

    public void Play()
    {
        SetState(GameState.Gameplay);
        m_Score=0;
        m_GameplayPanel.DisplayScore(m_Score);
        SpawnManager.Instance.StartBattle();
    }
    public void Pause()
    {
        SetState(GameState.Pause);
    }
    public void Home()
    {   Debug.Log("Scence home");
        SetState(GameState.Home);
        SpawnManager.Instance.Clear();
    }
    public void Continue()
    {
        SetState(GameState.Gameplay);
    }
    public void GameOver(bool win)
    {
        m_Win= win;
        SetState(GameState.Gameover);
        m_GameoverPanel.DisplayResult(win);
        m_GameoverPanel.DisplayHighScore(m_Score);
    }
    public void AddScore(int value)
    {   
        GameOver(false);
        m_Score+= value;
        m_GameplayPanel.DisplayScore(m_Score);
        if(SpawnManager.Instance.isClear())
        {
            GameOver(true);
        }
    }
    public bool IsActivate()
    {
        return m_GameState ==GameState.Gameover;
    }
}
