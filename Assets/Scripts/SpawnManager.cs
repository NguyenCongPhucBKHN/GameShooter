using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ProjecttilePool 
{
    [SerializeField] ProjecttileController m_Prefab_Projecttile;
    [SerializeField] List<ProjecttileController> inactivateObjs;
    [SerializeField] List<ProjecttileController> activateObjs;

    public ProjecttileController Spawn(Vector3 position, Transform parent)
    {
        if(inactivateObjs.Count==0)
        {
            ProjecttileController newObject= GameObject.Instantiate(m_Prefab_Projecttile, parent);
            newObject.transform.position= position;
            activateObjs.Add(newObject);
            return newObject;
        }
        else
        {
            ProjecttileController oldObj = inactivateObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position= position;
            activateObjs.Add(oldObj);
            inactivateObjs.Remove(oldObj);
            return oldObj;
        }
    }

    public void Release(ProjecttileController obj)
    {
        if(activateObjs.Contains(obj))
        {
            obj.gameObject.SetActive(false);
            inactivateObjs.Add(obj);
            activateObjs.Remove(obj);
        }
        
    }
    public void Clear()
   {
    while(activateObjs.Count>0)
    {
        Release(activateObjs[0]);
    }
   }
}

[System.Serializable] 
public class ParticleFXPool
{
    [SerializeField] ParticleFX m_Prefab_ParticleFX;
    [SerializeField] List<ParticleFX> inactivateObjs;
    [SerializeField] List<ParticleFX> activateObjs;
    

    public ParticleFX Spawn(Vector3 position, Transform parent)
    {
        if(inactivateObjs.Count==0)
        {
            ParticleFX newObject= GameObject.Instantiate(m_Prefab_ParticleFX, parent);
            newObject.transform.position= position;
            activateObjs.Add(newObject);
            return newObject;
        }
        else
        {
            ParticleFX oldObj = inactivateObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position= position;
            activateObjs.Add(oldObj);
            inactivateObjs.Remove(oldObj);
            return oldObj;
        }
    }

    public void Release(ParticleFX obj)
    {
        if(activateObjs.Contains(obj))
        {
            obj.gameObject.SetActive(false);
            inactivateObjs.Add(obj);
            activateObjs.Remove(obj);
        }
        
    }
    public void Clear()
   {
    while(activateObjs.Count>0)
    {
        Release(activateObjs[0]);
    }
   }
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] bool m_Activate;
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] ProjecttilePool m_PlayerProjectilesPool;
    [SerializeField] ProjecttilePool m_EnemyProjectilesPool;
    // [SerializeField] private EnemyController m_EnemyPrefab;
    [SerializeField] int m_MinTotalEnemy;
    [SerializeField] int m_MaxTotalEnemy;
    
    [SerializeField] float m_EnemySpawnInterval;
    [SerializeField] private EnemyPath[] m_Paths;
    [SerializeField] private int m_TotalGroups;
    [SerializeField] private float delayGroup;
    [SerializeField] private ParticleFXPool m_HitFXPool;
    [SerializeField] private ParticleFXPool m_ShootingFXPool;
    [SerializeField] private PlayerController m_PlayerControllerPrefab;
    private bool m_IsSpawningEnemies;
    private EnemyPool m_EnemyPool;
    private PlayerController m_Player;
    // Start is called before the first frame update
    void Start()
    {
        
        m_EnemyPool=gameObject.GetComponent<EnemyPool>();
    }

    public void StartBattle()
    {
        if(m_Player==null)
            m_Player= Instantiate(m_PlayerControllerPrefab);
        m_Player.transform.position= new Vector3(0, 0, -0.25f);
        StartCoroutine(IESpawnGroups(m_TotalGroups));
    }
    public void Clear()
    {
        m_EnemyPool.Clear();
        m_EnemyProjectilesPool.Clear();
        m_HitFXPool.Clear();
        m_PlayerProjectilesPool.Clear();
        m_ShootingFXPool.Clear();
        StopAllCoroutines();
    }
    private IEnumerator IESpawnGroups(int pGroups)
    {   
        m_IsSpawningEnemies=true;
        for (int i =0; i<pGroups; i++)
        {
            int m_TotalEnemy = Random.Range(m_MinTotalEnemy, m_MaxTotalEnemy);
            int pathIndex= Random.Range(0, m_Paths.Length);
            EnemyPath path= m_Paths[pathIndex];
            yield return StartCoroutine(IESpawnEnenies(m_TotalEnemy, path));
            if(i< pGroups-1)
                yield return new WaitForSeconds(delayGroup);
        }
        m_IsSpawningEnemies=false;
    }

    private IEnumerator IESpawnEnenies(int m_TotalEnemy, EnemyPath path)
    {   
        
        for (int i =0; i<m_TotalEnemy; i++)
        {   
            yield return new WaitUntil(()=>m_Activate);
            yield return new WaitForSeconds(m_EnemySpawnInterval);
            EnemyController enemy = enemyPool.Spawn(path.WayPoints[0].position, path.WayPoints[0].parent);
            enemy.Init(path.WayPoints);
        }
    }

    public void ReleaseEnemy (EnemyController obj)
    {
        enemyPool.Release(obj);
    }

    public ProjecttileController SpawnEnemyProjectile(Vector3 position)
    {
        ProjecttileController obj = m_EnemyProjectilesPool.Spawn(position, transform);
        obj.SetFromPlayer(false);
        return obj;
    }
    public void ReleaseEnemyProjectile(ProjecttileController projectile)
    {
        m_EnemyProjectilesPool.Release(projectile);
    }

    public ProjecttileController SpawnPlayerProjectile(Vector3 position)
    {
        ProjecttileController obj = m_PlayerProjectilesPool.Spawn(position, transform);
        obj.SetFromPlayer(true);
        return obj;
    }
    public void ReleasePlayerProjectile(ProjecttileController projectile)
    {
        m_PlayerProjectilesPool.Release(projectile);
    }
    
    public ParticleFX SpawnHitFX(Vector3 postion)
    {
        ParticleFX fx= m_HitFXPool.Spawn(postion, transform);
        fx.SetPool(m_HitFXPool);
        return fx;
    }
    public void ReleaseHitFX(ParticleFX obj)
    {
        m_HitFXPool.Release(obj);
    }
    
    public ParticleFX SpawnShootingFX(Vector3 postion)
    {
        ParticleFX fx= m_ShootingFXPool.Spawn(postion, transform);
        fx.SetPool(m_ShootingFXPool);
        return fx;
    }
    public void ReleaseShootingFX(ParticleFX obj)
    {
        m_ShootingFXPool.Release(obj);
    }
    //Ham check da het Enemy chua
    public bool isClear()
    {
        if(m_IsSpawningEnemies || m_EnemyPool.activateObjs.Count>0)
        {
            return false;
        }
        return true;
    }
}
