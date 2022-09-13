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
    [SerializeField] private HomePanel m_HomePanel;
    [SerializeField] private GameplayPanel m_GameplayPanel;
    [SerializeField] private PausePanel m_PausePanel;
    [SerializeField] private GameoverPanel m_GameoverPanel;
    [SerializeField] private float m_MoveSpeed;
    private SpawnManager m_SpawnManager;
    private GameState m_GameState;
    private AudioManager m_AudioManger;
    private bool m_Win;
    private int m_Score;
    // Start is called before the first frame update
    void Start()
    {   
        PlayerPrefs.SetInt("HighScore", 0);
        m_HomePanel.gameObject.SetActive(false);
        m_GameplayPanel.gameObject.SetActive(false);
        m_PausePanel.gameObject.SetActive(false);
        m_GameoverPanel.gameObject.SetActive(false);
        m_SpawnManager=FindObjectOfType<SpawnManager>();
        m_AudioManger=FindObjectOfType<AudioManager>();
        SetState(GameState.Home);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(( m_SpawnManager.isClear()) || FindObjectOfType<PlayerController>()==null)
        {
            if(m_SpawnManager.isClear())
                return;
            PlayerController m_PlayerController= FindObjectOfType<PlayerController>();
        
        }
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
            m_AudioManger.PlayHomeMusic();
        }
        else
        {
            m_AudioManger.PlayBattleMusic();
        }
        
    }

    public void Play()
    {
        m_SpawnManager.StartBattle();
        SetState(GameState.Gameplay);
        // m_Score=0;
        m_GameplayPanel.DisplayScore(PlayerPrefs.GetInt("HighScore"));
        
    }
    public void Pause()
    {
        SetState(GameState.Pause);
    }
    public void Home()
    {   Debug.Log("Scence home");
        SetState(GameState.Home);
        m_SpawnManager.Clear();
    }
    public void Continue()
    {
        SetState(GameState.Gameplay);
    }
    public void GameOver(bool win)
    {   
        int curScore= PlayerPrefs.GetInt("HighScore");
        Debug.Log("curScore 1"+ curScore);
        if(curScore<m_Score)
        {
            PlayerPrefs.SetInt("HighScore", m_Score);
            Debug.Log("curScore 2"+ curScore);
            curScore= m_Score;
            Debug.Log("curScore 3"+ curScore);
        }
        Debug.Log("Screen Gameover");
        m_Win= win;
        SetState(GameState.Gameover);
        m_GameoverPanel.DisplayResult(win);
        m_GameoverPanel.DisplayHighScore(curScore);
        Debug.Log("curScore 4"+ curScore);
    }
    public void AddScore(int value)
    {   
        
        m_Score+= value;

        m_GameplayPanel.DisplayScore(m_Score);

        if(( m_SpawnManager.isClear()) || FindObjectOfType<PlayerController>()==null)
        {
            if(m_SpawnManager.isClear())
            {
                GameOver(false); //False la thua
                // PlayerPrefs.SetInt("HighScore", 0);
            }
            PlayerController m_PlayerController= FindObjectOfType<PlayerController>();
            // m_PlayerController.transform.Translate(new Vector3(0, 1)* m_MoveSpeed*Time.deltaTime);
            GameOver(true); //True la thang
        }

        
    }
    public bool IsActivate()
    {
        return m_GameState ==GameState.Gameover;
    }
    public int getScore(){
        return m_Score;
    }
    public void setScore(int score){
        m_Score=score;
    }
}
