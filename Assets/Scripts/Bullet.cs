using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;
    public bool createEnemy = false;
    private GameObject player;
    private void Start()
    {
        Movement movement = Movement.instance;
        player = movement.gameObject;
        Destroy(gameObject, 1f);
    }
    private Vector3 direction;
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    private void Update()
    {
        if (createEnemy)
        {
            direction = player.transform.position;
            transform.position = Vector3.Lerp(transform.position, direction, 5f * Time.deltaTime);
        }         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Movement movementScript = collision.gameObject.GetComponent<Movement>();
            int localPowerUlt = movementScript.powerUlt;
            bool teleportate = movementScript.teleportate;
            if (!teleportate)
            {
                if (localPowerUlt >= 0)
                {
                    localPowerUlt -= damage;
                    movementScript.UpdateTextUlt();
                    Destroy(gameObject);
                }
                else
                {
                    localPowerUlt = 0;
                    Destroy(gameObject);
                }
                movementScript.powerUlt = localPowerUlt;
            }
            Destroy(gameObject);
        }
    }
}
