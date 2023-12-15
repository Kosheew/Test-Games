using Pathfinding;
using UnityEngine;

public class RedEnemyController : HealthController
{
    [Header("Objects Move")]
    [SerializeField] private float flyingHeight = 2f;
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private float speed = 1f;
    
    private bool isFrozen = false; 
    private Transform player;
    private AIDestinationSetter destinationSetter;
    private AIPath AIPath;

    void Start()
    {
        Movement movement = Movement.instance;
        player = movement.transform;
        Initial();
        destinationSetter = GetComponent<AIDestinationSetter>();
        AIPath = GetComponent<AIPath>();
        destinationSetter.enabled = false;
    }

    void Update()
    {
        if (transform.position.y <= flyingHeight && !isFrozen) transform.Translate(Vector3.up * speed * Time.deltaTime);
        else Invoke("StartAttack", freezeTime);
    }

    void StartAttack()
    {
        isFrozen = true;
        destinationSetter.enabled = true;

        destinationSetter.target = player;
        AIPath.gravity = new Vector3(0f, -0.05f, 0f);
        if (AIPath.reachedEndOfPath)
        {
            // Deal damage to the player
            player.GetComponent<HealthController>().TakeDamage(damage);
            Destroy(gameObject);

        }
    }
}