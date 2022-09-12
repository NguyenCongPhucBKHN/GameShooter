using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{   
    // private GameManagerr m_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        // m_GameManager =FindObjectOfType<GameManagerr>();
    }

    public void BtnHome_Press()
    {
        GameManagerr.Instance.Home();
    }
    public void BtnContine_Press()
    {
        GameManagerr.Instance.Continue();
    }

}
