using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    // private GameManagerr m_GameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        // m_GameManager=FindObjectOfType<GameManagerr>();
    }

    // Update is called once per frame
   public void BtnPlay_Pressed()
   {
    GameManagerr.Instance.Play();
   }
}
