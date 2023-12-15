using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float ricochetChance = 0.1f; 
    public float lowHealthRicochetChance = 1.0f; 
    private GameObject player;

    private void Start()
    {
        Movement movement = Movement.instance;
        player = movement.gameObject;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Attack();
        
    }

    void Attack()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        GameObject projectile = Instantiate(projectilePrefab, cameraPosition, Quaternion.identity);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
        Vector3 projectileDirection = (worldMousePosition - cameraPosition).normalized;


        projectile.transform.LookAt(worldMousePosition);
        projectile.GetComponent<PlayerBullet>().damage = player.GetComponent<Movement>().damage;
        projectile.GetComponent<PlayerBullet>().chanceRicochet = player.GetComponent<Movement>().health;

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = projectileDirection * projectileSpeed;
    }
}
