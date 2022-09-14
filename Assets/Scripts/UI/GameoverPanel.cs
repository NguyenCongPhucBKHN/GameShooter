using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameoverPanel : MonoBehaviour
{
    private GameManagerr m_GameManager;
    [SerializeField] private TextMeshProUGUI m_TxtResult;
    [SerializeField] private TextMeshProUGUI m_TxtHighScore;
    [SerializeField] private TextMeshProUGUI m_TxtButton;
    // Start is called before the first frame update
    void Start()
    {
        m_GameManager=FindObjectOfType<GameManagerr>();
    }
    public void DisplayHighScore(int score)
    {
        m_TxtHighScore.text= "High Score: "+ score;
    }
    
    public void BtnHome_Pressed()
    {
        m_GameManager.Home();
    }
    public void DisplayResult(bool isWin)
    {   
        if (isWin)
        {
            m_TxtResult.text= "YOU WIN";
            m_TxtButton.text= "NEXT";
        }
        else
        {
            m_TxtResult.text="YOU LOSE";
            m_TxtButton.text= "REPLAY";
        }
    }
}
