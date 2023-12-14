using Pathfinding;
using System;
using UnityEngine;

public class BlueEnemyController : HealthController
{
    [Header("Objects Stats")]
    [SerializeField] private float attackInterval = 3f;
    [SerializeField] private GameObject projectilePrefab;
    private AIDestinationSetter destinationSetter;
    private AIPath AIPath;
    private Transform player;
    private float attackTimer;
    private void Start()
    {
        Movement movement = Movement.instance;
        player = movement.transform;
        attackTimer = attackInterval;
        Initial();
        destinationSetter = GetComponent<AIDestinationSetter>();
        AIPath = GetComponent<AIPath>();
        destinationSetter.enabled = false;
       
    }

    private void Update()
    {
        StartAttack();
    }
    private void StartAttack()
    {
        destinationSetter.enabled = true;

        destinationSetter.target = player;
       // transform.LookAt(player);

        attackTimer -= Time.deltaTime;
        if (AIPath.reachedEndOfPath && attackTimer <= 0f)
        {
            
            Vector3 spawnPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 0.22f);
            GameObject projectible = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Vector3 direction = player.position;
            Debug.Log(direction);
            /*projectible.GetComponent<Bullet>().damage = damage;
            projectible.GetComponent<Bullet>().SetDirection(direction);*/
            Bullet bullet = projectible.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.damage = damage;
                bullet.createEnemy = true;
                bullet.SetDirection(direction);
            }

            attackTimer = attackInterval;
        }
    }
}
