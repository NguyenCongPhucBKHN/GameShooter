using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileController : MonoBehaviour
{   
    [SerializeField] float m_MoveSpeed;
    [SerializeField] Vector2 m_Direction;
    [SerializeField] float time_Life;
    [SerializeField] int m_Damage;
    private bool m_FromPlayer;
    private SpawnManager m_SpawnManager;
    // [SerializeField] ProjecttilePool m_ProjectPoll;
    
    // Start is called before the first frame update
    void Start()
    {
        m_SpawnManager= FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Direction*m_MoveSpeed*Time.deltaTime);

    }
    public void DestroyFire()
    {
        // m_ProjectPoll.Release(this);
        // Destroy(gameObject, time_Life);
        if(time_Life<=0)
        {
            Release();
        }
        time_Life-=Time.deltaTime;

    }
    
    private void OnTriggerEnter2D(Collider2D other) {
         //Destroy dan
        //Release();
        // m_ProjectPoll.Release(this);
        if(other.gameObject.CompareTag("Enemy"))
        {
            
            if(m_FromPlayer)
                m_SpawnManager.ReleasePlayerProjectile(this);
            else
                m_SpawnManager.ReleaseEnemyProjectile(this);
            Vector3 hitPos= other.ClosestPoint(transform.position);
            m_SpawnManager.SpawnHitFX(hitPos);

            //Distroy Enemy  
            EnemyController enemy;
            other.gameObject.TryGetComponent(out enemy);
            enemy.hit(m_Damage);


        }
        else  if(other.gameObject.CompareTag("Player"))
        {
            if(m_FromPlayer)
                m_SpawnManager.ReleasePlayerProjectile(this);
            else
                m_SpawnManager.ReleaseEnemyProjectile(this);
            Vector3 hitPos= other.ClosestPoint(transform.position);
            m_SpawnManager.SpawnHitFX(hitPos);
            
            PlayerController player;
            other.gameObject.TryGetComponent(out player);
            player.Hit(m_Damage);
        }
    }
    public void SetFromPlayer(bool value)
    {
        m_FromPlayer =value;
    }
    public void Release()
    {
        if(m_FromPlayer)
        {
            m_SpawnManager.ReleasePlayerProjectile(this);
        }
        else
        {
            m_SpawnManager.ReleaseEnemyProjectile(this);
        }
    }
}
