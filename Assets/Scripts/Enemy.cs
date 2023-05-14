using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //give us the ability to access the navmesh components and navmesh clauses
using System.Linq;  //able to work with lists and arrays

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int CurrentHP;
    public int MaxHP;
    public int ScoreToGive;

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    private List<Vector3> path;

    private Weapon weapon;
    private GameObject target;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<Player>().gameObject;

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);  //we call update path every 0.5 second
    }

    public void Update()
    {
        
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= attackRange)
        {
            ChasePlayer();
        }
      





        Vector3 dir = (target.transform.position - transform.position).normalized;

        float angel = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angel;
    }

    void ChasePlayer()
    {
        if (path.Count == 0)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

        if (transform.position == path[0]+ new Vector3(0, yPathOffset, 0))
        {
            path.RemoveAt(0);
        }
    }
    void UpdatePath()
    {
        //calculate the path to the target
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        path = navMeshPath.corners.ToList();

    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            Die();
        }

        void Die()
        {
            GameManager.instance.AddScore(ScoreToGive);
            Destroy(gameObject);
        }
    }
}
