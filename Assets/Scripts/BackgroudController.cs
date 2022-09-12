using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudController : MonoBehaviour
{
    [SerializeField] private Material m_BigStar;
    [SerializeField] private Material m_MediumStar;
    [SerializeField] private float m_BigStartScollSpeed;
    [SerializeField] private float m_MediumStartScollSpeed;
    private int m_MainTextId;

    // Start is called before the first frame update
    void Start()
    {
        m_MainTextId=Shader.PropertyToID("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {   Vector2 offset= m_BigStar.GetTextureOffset(m_MainTextId);
        offset+= new Vector2(0, m_BigStartScollSpeed*Time.deltaTime);
        m_BigStar.SetTextureOffset(m_MainTextId, offset);

        Vector2 offset2= m_MediumStar.GetTextureOffset(m_MainTextId);
        offset2= new Vector2(0, m_MediumStartScollSpeed*Time.deltaTime);
        m_MediumStar.SetTextureOffset(m_MainTextId, offset2);
    }
}
