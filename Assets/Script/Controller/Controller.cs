using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private static Controller instance;
    public static Controller Instance
    {
        get { 
            if (instance == null) 
            { 
                instance = new Controller();
                
            } 
            return instance; 
        }
    }
    
    public EnemyData enemyData;
    public MergeElementData mergeElementData;

    private void Awake()
    {
        instance = this;
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
    public EnemyStat GetStatEnemy(ECharacterType enemyType)
    {
        EnemyStat tmp = enemyData.EnemyStatIndex(enemyType);
        return tmp;
    }

    public ECharacterType GetTypeIndex(int index)
    {
        return enemyData.enemies[index].Type;
    }
}

