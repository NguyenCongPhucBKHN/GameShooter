using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]public class EnemyPool : MonoBehaviour
{
    [SerializeField] EnemyController prefab;
    [SerializeField] List<EnemyController> inactivateObjs;
    [SerializeField] public List<EnemyController> activateObjs;
    public EnemyController Spawn(Vector3 position, Transform parent)
    {
        if(inactivateObjs.Count==0){
            EnemyController newEnemy= Instantiate(prefab, parent );
            activateObjs.Add(newEnemy);
            return newEnemy;
        }
        else
        {
            EnemyController oldEnemy = inactivateObjs[0];
            oldEnemy.gameObject.SetActive(true);
            oldEnemy.transform.SetParent(parent);
            oldEnemy.transform.position= position;
            activateObjs.Add(oldEnemy);
            inactivateObjs.Remove(oldEnemy);
            return oldEnemy;

        }
    }
    public void Release (EnemyController obj)
    {
        if(activateObjs.Contains(obj))
        {
            activateObjs.Remove(obj);
            inactivateObjs.Add(obj);
            obj.gameObject.SetActive(false);
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
