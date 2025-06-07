using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public int maxHp = 100;
    private int curHp;

    public Transform target;

    private Animator enemyAnim;
    private Rigidbody rigid;

    public float range = 10f;

    private bool isDead = false;

    public int damageAmount = 20;

    void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        curHp = maxHp;
    }
    void Update()
    {
        if (isDead)
            return;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= range)
        {
            enemyAnim.SetBool("isWalk", true);
            transform.LookAt(target);

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                enemyAnim.SetTrigger("doAttack");
                player.TakeDamage(damageAmount);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        enemyAnim.SetTrigger("getHit");
        curHp -= damage;

        if (curHp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;

        enemyAnim.SetTrigger("isDead");

        Collider enemyCollider = GetComponent<Collider>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }
        Destroy(gameObject, 2f);
    }
}