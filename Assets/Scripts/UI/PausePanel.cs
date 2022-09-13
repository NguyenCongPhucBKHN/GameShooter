using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{   
    private GameManagerr m_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        m_GameManager =FindObjectOfType<GameManagerr>();
    }

    public void BtnHome_Press()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        m_GameManager.setScore(0);
        m_GameManager.Home();
    }
    public void BtnContine_Press()
    {
        m_GameManager.Continue();
    }

}
