using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 5;
    public int chanceRicochet = 100;
    private GameObject player;
    private bool hasRicocheted = false;
    private void Start()
    {
        Movement movement = Movement.instance;
        player = movement.gameObject;
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyRed"))
        {
            Ricochet(collision);
            takesdamage(collision);
            
            RedEnemyController redEnemyController = collision.gameObject.GetComponent<RedEnemyController>();
            if (redEnemyController.health <= 0)
            {
                if (KillRicochet())
                {
                    RewardPlayerAfterRicochetKill();
                }
                else
                {
                    player.GetComponent<Movement>().powerUlt += 15;
                    player.GetComponent<Movement>().UpdateTextUlt();
                }
            }
        }
        if (collision.gameObject.CompareTag("EnemyBlue"))
        {
            Ricochet(collision);
            takesdamage(collision);
            
            BlueEnemyController blueEnemyController = collision.gameObject.GetComponent<BlueEnemyController>();
            if (blueEnemyController.health <= 0)
            {
                if (KillRicochet())
                {
                    RewardPlayerAfterRicochetKill();
                }
                else 
                { 
                player.GetComponent<Movement>().powerUlt += 50;
                player.GetComponent<Movement>().UpdateTextUlt();
                }
            }    
        }
    }

    private void takesdamage(Collision collision)
    {
        HealthController healthController = collision.gameObject.GetComponent<HealthController>();
        healthController.TakeDamage(damage);
    }

    private void Ricochet(Collision collision)
    {
        if (ShouldRicochet())
        {
            hasRicocheted = true;
            GameObject currentEnemy = collision.gameObject;
            GameObject closestEnemy = FindClosestEnemy(currentEnemy.transform.position, 2.5f, currentEnemy);

            if (closestEnemy != null)
            {
                Vector3 directionToEnemy = (closestEnemy.transform.position - transform.position).normalized;
                GetComponent<Rigidbody>().velocity = directionToEnemy * 4f;
            }
            else
            {
               Destroy(gameObject);
            }
        }
        else Destroy(gameObject);
    }
    private bool ShouldRicochet()
    {
        return Random.Range(0f, chanceRicochet) < 10;
    }

    private GameObject FindClosestEnemy(Vector3 position, float radius, GameObject ignoreEnemy)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != ignoreEnemy && (hitCollider.gameObject.CompareTag("EnemyRed") || hitCollider.gameObject.CompareTag("EnemyBlue")))
            {
                float distance = (hitCollider.transform.position - position).sqrMagnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.gameObject;
                }
            }
        }

        return closestEnemy;
    }
    private bool KillRicochet()
    {
        return hasRicocheted;
    }
    private void RewardPlayerAfterRicochetKill()
    {
        float chance = Random.Range(0f, 1f);
        if (chance < 0.7f)
        {
            player.GetComponent<Movement>().powerUlt += 10;
        }
        else
        {  
            player.GetComponent<Movement>().RestoreHalfHealth();
        }
        player.GetComponent<Movement>().UpdateTextUlt();
    }
}
