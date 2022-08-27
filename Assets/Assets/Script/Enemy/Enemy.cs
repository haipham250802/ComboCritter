using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Enemy : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 PatrolPos;
    StateEnemy stateEnemy;

    public float SpeedPatrol;
    public float SpeedChasing;
    public float RangePatrol = 2.0f;
    public float RangeChasing;
    
    public GameObject UIBattle;

    player m_player;
    Tweener tweener;

    public GameObject GroupEnemyBased;
    public List<GameObject> L_EnemyBased;
    void Start()
    {
        startPos = transform.position;
        PatrolPos = startPos;
        stateEnemy = StateEnemy.PATROL;
        m_player = FindObjectOfType<player>();
    }
    bool isPatrol = false;
    private void Update()
    {
        switch (stateEnemy)
        {
            case StateEnemy.PATROL:
                if (Vector2.Distance(transform.position, m_player.transform.position) < RangePatrol)
                {
                    if (tweener != null)
                    {
                        KillTwen();
                    }
                    stateEnemy = StateEnemy.CHASING;
                }
                if (!isPatrol)
                {
                    if (tweener != null)
                    {
                        KillTwen();
                    }
                    isPatrol = true;
                    Patrol();
                }
                break;
            case StateEnemy.CHASING:
                if (Vector2.Distance(startPos, m_player.transform.position) > RangeChasing)
                {
                    stateEnemy = StateEnemy.PATROL;
                    isPatrol = false;
                }
                else
                {
                    if (tweener != null)
                    {
                        KillTwen();
                    }
                    transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, SpeedChasing * Time.deltaTime);
                }
                break;
        }
    }
    private void KillTwen()
    {
        tweener.Kill();

    }
    private void Patrol()
    {
        Vector2 RangePatrol = GetPatrolPos();
        tweener = transform.DOMove(RangePatrol, SpeedPatrol).SetSpeedBased(true).OnComplete(() =>
        {
            tweener.Kill();
            isPatrol = false;
        });
    }
    private Vector2 GetPatrolPos()
    {
        float Rand = Random.Range(0.0f, RangePatrol);
        PatrolPos = Random.insideUnitCircle * Rand + startPos;
        return PatrolPos;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangePatrol);
        Gizmos.color = Color.green;
        if (startPos == Vector2.zero)
        {
            Gizmos.DrawWireSphere(transform.position, RangeChasing);
        }
        else
        {
            Gizmos.DrawWireSphere(startPos, RangeChasing);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player m_player = collision.GetComponent<player>();
        if (m_player != null)
        {
            Debug.Log("da cham");
            UIBattle.SetActive(true);
        }
    }
#endif
}
public enum StateEnemy
{
    PATROL,
    CHASING
}