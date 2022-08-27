using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyBased : MonoBehaviour
{
    public float Speed;
    public float HP;
    public float Damage;
    public int Rarity;
    public List<PlayerBased> L_Player;
    public ECharacterType enemyType;
    private PlayerBased playerBased;

    private bool isTurnPlayer = false;
    private Vector2 StartPos;

    StateEnemyBased _StateEnemy;
    private void Start()
    {
        StartPos = transform.position;
        playerBased = FindObjectOfType<PlayerBased>();
        EnemyStat stat = Controller.Instance.GetStatEnemy(enemyType);
        HP = stat.HP;
        Damage = stat.Damage;
        Rarity = stat.Rarity;
        
    }
    private void Update()
    {
        switch (_StateEnemy)
        {
            case StateEnemyBased.IDLE:
                if (playerBased != null)
                {
                    if (playerBased.GetTurnEnemyBased())
                    {
                        _StateEnemy = StateEnemyBased.ATTACK;
                    }
                }
                break;
            case StateEnemyBased.ATTACK:
                Attack();
                break;
        }
    }
    
    private void Attack()
    {
        playerBased.SetTurnEnemuBased(false);
        if (playerBased != null)
        {
            playerBased = GetNearTarget(L_Player);
            transform.DOMove(playerBased.transform.position, Speed).SetSpeedBased(true).OnComplete(() =>
            {
                transform.DOMove(StartPos, Speed).SetSpeedBased(true).OnComplete(() =>
                {
                    isTurnPlayer = true;
                    playerBased.PurChaseBtn.enabled = true;
                });
            });
        }
        _StateEnemy = StateEnemyBased.IDLE;
    }
    public void SetTurnPlayerBased(bool isTurnPlayer)
    {
        this.isTurnPlayer = isTurnPlayer;
    }
    public bool GetTurnPlayerBased()
    {
        return isTurnPlayer;
    }
    private PlayerBased GetNearTarget(List<PlayerBased> L_Player)
    {
        int flag = 0;
        if (L_Player.Count > 1)
        {
            float MinDis = Vector2.Distance(transform.position, L_Player[0].transform.position);
            for (int i = 0; i < L_Player.Count; i++)
            {
                float TempDis = Vector2.Distance(transform.position, L_Player[0].transform.position);
                if (MinDis > TempDis)
                {
                    MinDis = TempDis;
                    flag = i;
                }
            }
            return L_Player[flag];
        }
        else if (L_Player.Count == 1)
        {
            return L_Player[0];
        }
        return null;
    }
}
public enum StateEnemyBased
{
    IDLE,
    ATTACK
}
