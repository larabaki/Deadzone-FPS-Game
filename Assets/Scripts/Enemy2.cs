using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    public Player player;
    public float attackDistance;
    public float chaseDistance;
    public int damage;
    public int health;

    private bool isAttacking;
    private bool isDead;
 

    public NavMeshAgent agent;
    public Animator anim;
    private AudioSource audioSource; //hurt sound
    public AudioClip ZombieHurtSFX;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isDead) return;
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            agent.isStopped = true;
            //attack code

            if (!isAttacking)
            {
                Attack();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
               // anim.SetBool("Idle", false);
                anim.SetBool("Running", true);
            }
            else
            {
                agent.isStopped = true;
             //anim.SetBool("Idle", true);
                anim.SetBool("Running", false);
            }
        }
    }
    void Attack()
    {  
        isAttacking = true;
        anim.SetBool("Running", false);
        anim.SetTrigger("Attack");

        //Invoke here gives us this ability to delay a function is specified time frame that you gave it
        Invoke("TryDamage", 1.2f);
        Invoke("DisableIsAttacking", 2.5f);
    }

    void TryDamage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            player.TakeDamage(damage);
        }
    }

    void DisableIsAttacking()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damageToTake)
    {
        health -= damageToTake;
        audioSource.PlayOneShot(ZombieHurtSFX);
        if (health <= 0)
        {
            isDead = true;
            agent.isStopped = true;
            //disable animations
            anim.SetTrigger("Die");
        }

    }
}

