using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Transform[] m_WayPoints;
    private int m_CurrentWayPointIndex;
    private bool m_Activate;
    private GameManagerr m_GameManager;

    [SerializeField] private ProjecttileController m_Projecttile;
    [SerializeField] private Transform m_FiringPoint;
    //Thoi Gian xuat hien dan
    [SerializeField] private float m_MinFiringCooldown;
    [SerializeField] private float m_MaxFiringCooldown;
    [SerializeField] private int m_Hp;
    // [SerializeField] private ProjecttilePool m_ProjectPoll;
    private int m_Current_Hp;
    private float m_TempCooldown;
    private SpawnManager m_SpawnManager;
    private AudioManager m_AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";
        //Duyet tat ca object tren Scence va lay ra SpawnManager
        m_SpawnManager= FindObjectOfType<SpawnManager>();
        m_GameManager =FindObjectOfType<GameManagerr>();
        m_AudioManager= FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!m_Activate)
            return;
        int nextWayPoint = m_CurrentWayPointIndex + 1;
        if(nextWayPoint > m_WayPoints.Length-1)
        {
            nextWayPoint= 0;
        }

        transform.position= Vector3.MoveTowards( transform.position, m_WayPoints[nextWayPoint].position, m_MoveSpeed*Time.deltaTime);
        
        if(transform.position == m_WayPoints[nextWayPoint].position)
        {
            m_CurrentWayPointIndex = nextWayPoint;
        }
        Vector3 direction = m_WayPoints[nextWayPoint].position - transform.position;
        float angle= Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation= Quaternion.AngleAxis(angle+90, Vector3.forward);
        if(m_TempCooldown<=0)
        {
            Fire();
            m_TempCooldown = Random.Range(m_MinFiringCooldown, m_MaxFiringCooldown);
        }
        m_TempCooldown-= Time.deltaTime;
    }
    public void Fire()
    {   
        // ProjecttileController projecttile = Instantiate(m_Projecttile, m_FiringPoint.position, Quaternion.identity, null);
        ProjecttileController projecttile = m_SpawnManager.SpawnEnemyProjectile(m_FiringPoint.position);
        projecttile.DestroyFire();
        m_AudioManager.PlayPlasmaSFX();
    }

    public void Init(Transform[] wayPoints)
    {
        m_WayPoints= wayPoints;
        m_Activate= true;
        transform.position= wayPoints[0].position;
        m_TempCooldown = Random.Range(m_MinFiringCooldown, m_MaxFiringCooldown);
        m_Current_Hp=m_Hp;
    }
    public void hit( int damage)
    {
        m_Current_Hp -= damage;
        if(m_Current_Hp<=0)
        {
            m_SpawnManager.ReleaseEnemy(this);
            //Phuc
            //m_SpawnManager.SpawnExlosionFX(transform.position);
            m_GameManager.AddScore(1);
            m_AudioManager.PlayExplosionSFX();
        }
        m_AudioManager.PlayHitSFX(); 
    }
    
}
