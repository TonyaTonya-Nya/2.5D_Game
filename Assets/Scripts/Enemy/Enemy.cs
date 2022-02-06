using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private PlayerManager player;


    public float chaseRange;
    public float attackRange;
    public float attackValue;
    public float health;
    public float maxHealth;
    public Transform model;
    public Animator animator;
    public GameObject ui;
    public Slider healthBar;

    public Action OnDie;
    private bool isDiying = false;

    EnemyState state = EnemyState.idle;

    enum EnemyState
    {
        trace, idle, attack, die
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ui.transform.rotation = new Quaternion(0, 0, 0, 1);


        healthBar.value = Mathf.Max(0, health / maxHealth * 100);

        float distance = Vector3.Distance(transform.position, target.position);

        if (state == EnemyState.idle)
        {
            if (distance <= chaseRange)
            {
                state = EnemyState.trace;
                return;
            }

            if (distance <= attackRange)
            {
                state = EnemyState.attack;
                return;
            }

            animator.SetBool("Running", false);
            if (agent != null)
                agent.SetDestination(transform.position);
        }
        else if (state == EnemyState.trace)
        {
            if (distance > chaseRange)
            {
                state = EnemyState.idle;
                return;
            }

            if (distance <= attackRange)
            {
                state = EnemyState.attack;
                return;
            }


            animator.SetBool("Running", true);

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
            {
                agent.SetDestination(target.transform.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
        else if (state == EnemyState.attack)
        {
            if (distance > chaseRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
            {
                state = EnemyState.idle;
                return;
            }

            if (distance > attackRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
            {
                state = EnemyState.trace;
                return;
            }

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
            {
                animator.SetTrigger("Kicking");
            }

            animator.SetBool("Running", false);
            agent.SetDestination(transform.position);
        }
        else if (state == EnemyState.die)
        {
            agent.SetDestination(transform.position);
        }


        /*
        if (PlayerManager.gameOver)
        {
            animator.enabled = false;
            this.enabled = false;
        }*/

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            chaseRange = 60;
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && model.GetComponent<AnimEvents>().attackCheck)
        {
            player.Damage(attackValue);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDiying)
            return;
        health -= damage;
        if (health < 0)
        {
            state = EnemyState.die;
            OnDie?.Invoke();
            isDiying = true;
            Die();
        }
    }

    private void Die()
    {
        if (!animator.GetBool("Death"))
        {
            animator.SetBool("Death", true);
        }

        Destroy(gameObject, 3);
        //this.enabled = false;
    }
}
