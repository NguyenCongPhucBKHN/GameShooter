using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] float m_MoveSpeed;
    [SerializeField] ProjecttileController m_projectTile;
    [SerializeField] Transform m_FiringPoint;
    [SerializeField] float m_FiringCooldown;
    [SerializeField] float m_TempCooldown;
    [SerializeField] int m_HpPlayer;
    // private SpawnManager m_SpawnManager;
    
    // private AudioManager m_AudioManager;
    // [SerializeField] ProjecttilePool projecttilePool;
    private int m_CurrentHp;
    // private GameManagerr m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag="Player";
        transform.position=transform.position+ new Vector3(0, 0, -0.23f);
        m_CurrentHp= m_HpPlayer;
        // SpawnManager.Instance= FindObjectOfType<SpawnManager>();
        // m_GameManager= FindObjectOfType<GameManagerr>();
        // m_AudioManager =FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManagerr.Instance.IsActivate())
            return;
        float horizontal= Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2 (horizontal, vertical);
        transform.Translate(direction*Time.deltaTime*m_MoveSpeed);
        if(Input.GetKey(KeyCode.Space))
            {
                if(m_TempCooldown<=0)
                    {
                        Fire();
                        m_TempCooldown= m_FiringCooldown;
                    }
                m_TempCooldown-=Time.deltaTime;
            }
    }

    void Fire()
    {
        // ProjecttileController projectTile = Instantiate(m_projectTile, m_FiringPoint.position, Quaternion.identity );
        ProjecttileController projectTile= SpawnManager.Instance.SpawnPlayerProjectile(m_FiringPoint.position);
        projectTile.DestroyFire();
        SpawnManager.Instance.SpawnShootingFX(m_FiringPoint.position+ new Vector3 (0, 1.5f, 0));
        AudioManager.Instance.PlayLazerSFX();
    }

    public void Hit(int damage)
    {   
        m_CurrentHp-=damage;
        if(m_CurrentHp<=0)
        {
            Destroy(gameObject);
            //Phuc
            // m_SpawnManager.SpawnExlosionFX(transform.position);
            GameManagerr.Instance.GameOver(false);
            AudioManager.Instance.PlayExplosionSFX();

        }
         AudioManager.Instance.PlayHitSFX();   
    }
    
}
