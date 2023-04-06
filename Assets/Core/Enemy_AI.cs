using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum EnemyState { CloseRange, LongRange, TouchEnemy, Null }
public enum EnemyName { Solider, Flower, Ghost, Light, Null }

public class Enemy_AI : MonoBehaviour
{

    [Header("敌人类型")]
    public EnemyState enemyState;
    public EnemyName enemyName;
    [Header("敌人识别范围")]
    public Vector2 sightRadius;//Overlap检测范围   

    [Header("敌人巡逻范围")]
    public float patrolRange;
    [Tooltip("巡逻点更新CD")]
    public float cooldown = 0;//巡逻点获取时间

    [Header("敌人攻击参数")]
    [Tooltip("攻击CD")]
    public float attackTime = 2;
    [Tooltip("攻击范围")]
    public float attackRange;

    [Header("绘制攻击范围参数")]
    public Vector2 vector2;

    //动画Bool值判断
    private bool isWalk;
    private bool isAttack;
    private bool isIdle;

    //组件获取
    private Vector2 wayPoint;
    private Rigidbody2D rb;
    private float randomX;//存放巡逻X值

    [HideInInspector]
    public GameObject attackTarget;//攻击目标
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Collider2D[] collider2ds;//overlap碰撞体存放

    void Awake()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {

    }

    void Update()
    {

        cooldown += Time.deltaTime;
        SwitchStates();
        SwitchAnimations();

        if (cooldown >= 3)
        {
            GetNewWayPoint();
            cooldown = 0;
        }

    }

    void SwitchAnimations()
    {

        anim.SetBool("Walk", isWalk);
        anim.SetBool("Attack", isAttack);
        anim.SetBool("Idle", isIdle);

    }

    bool FoundPlayer()
    {
        collider2ds = Physics2D.OverlapBoxAll(transform.position, sightRadius, 0);

        foreach (var target in collider2ds)
        {
            if (target.CompareTag("Player"))
            {

                attackTarget = target.gameObject;
                return true;

            }

        }
        attackTarget = null;
        return false;
    }

    void SwitchStates()
    {

        switch (enemyState)
        {

            case EnemyState.CloseRange:

                CloseRangeWay();

                break;

            case EnemyState.LongRange:

                LongRangeWay();

                break;

            case EnemyState.TouchEnemy:

                TouchRangeWay();

                break;

            case EnemyState.Null:
                break;

        }

    }

    void CloseRangeWay()
    {

        attackTime -= Time.deltaTime;

        //查找到玩家并向玩家移动
        if (FoundPlayer())
        {

            //在攻击范围内
            if (TargetInAttackRange() == true)
            {
                //  rb.velocity = new Vector2(0, 0);             
                if (attackTime <= 0)
                {

                    if (attackTarget.transform.position.x < rb.transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    isAttack = true;
                    attackTime = 2;

                }
                else
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                    {
                        isAttack = false;
                    }
                    isWalk = false;
                }

            }

            //不在攻击范围内
            else
            {
                isWalk = true;
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    isAttack = false;

                }

                if (rb.transform.position.x > attackTarget.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                    {
                        rb.velocity = new Vector2(-3, 0);
                    }
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);

                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                    {
                        rb.velocity = new Vector2(3, 0);
                    }
                }

            }

        }

        //随机巡逻点
        else
        {

            PartrolMove();

        }

        //攻击范围获取
        bool TargetInAttackRange()
        {
            if (attackTarget != null)
                // { if (transform.localScale.x == attackTarget.pos)
                return Vector3.Distance(attackTarget.transform.position, transform.position) <= attackRange;
            //     else return false;
            // }
            else
                return false;

        }
    }

    void LongRangeWay()
    {

        attackTime -= Time.deltaTime;

        //查找到玩家并向玩家移动
        if (FoundPlayer())
        {

            //在攻击范围内
            if (TargetInAttackRange() == true)
            {
                if (rb.transform.position.x > attackTarget.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1); ;
                }
                rb.velocity = new Vector2(0, 0);
                if (attackTime <= 0)
                {

                    isAttack = true;
                    attackTime = 5;

                }
                else
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                    {
                        isAttack = false;
                    }
                    isWalk = false;
                }

            }

            //不在攻击范围内
            else
            {
                isWalk = true;
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    isAttack = false;
                }

                if (rb.transform.position.x > attackTarget.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    rb.velocity = new Vector2(-3, 0);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    rb.velocity = new Vector2(3, 0);
                }

            }

        }

        //随机巡逻点
        else
        {

            PartrolMove();

        }

        //攻击范围获取
        bool TargetInAttackRange()
        {

            if (attackTarget != null)
                return Vector3.Distance(attackTarget.transform.position, transform.position) <= attackRange;

            else
                return false;

        }

    }


    void TouchRangeWay()
    {

        //查找到玩家并向玩家移动
        if (FoundPlayer())
        {

            //在攻击范围内
            if (TargetInAttackRange() == true)
            {
                rb.velocity = new Vector2(0, 0);
                isWalk = true;
            }

            //不在攻击范围内
            else
            {
                isIdle = true;
                isWalk = false;

                transform.DOMove(attackTarget.transform.position, 5f, false);
                if (rb.transform.position.x > attackTarget.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

            }
        }

        //随机巡逻点
        else
        {

            PartrolMove();

        }

        //攻击范围获取
        bool TargetInAttackRange()
        {

            if (attackTarget != null)
                return Vector3.Distance(attackTarget.transform.position, transform.position) <= attackRange;

            else
                return false;

        }


    }

    //随机巡逻点的获取
    void GetNewWayPoint()
    {

        randomX = Random.Range(-patrolRange, patrolRange);
        Vector2 randomPoint = new Vector2(randomX, 0);
        wayPoint = randomPoint;

    }

    void PartrolMove()
    {

        transform.Translate(wayPoint * Time.deltaTime);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
        {
            isAttack = false;
        }
        isWalk = true;
        if (randomX > 0)
        {
            //transform.Translate(wayPoint * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, vector2);
    }

}
