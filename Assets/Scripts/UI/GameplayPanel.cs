using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameplayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TxtScore;
    private GameManagerr m_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        m_GameManager=FindObjectOfType<GameManagerr>();
    }
    public void BtnPausePress()
    {
        Debug.Log("Screen Pause");
        m_GameManager.Pause();
    }
    public void DisplayScore(int score)
    {
        m_TxtScore.text= "Score: "+ score;

    }

    
}
