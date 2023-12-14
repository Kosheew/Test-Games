using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float ricochetChance = 0.1f; // Шанс рикошета при убийстве
    public float lowHealthRicochetChance = 1.0f; // Шанс рикошета при низком здоровье
    private GameObject player;
    private void Start()
    {
        Movement movement = Movement.instance;
        player = movement.gameObject;
    }
    void Update()
    {

        // Пример: проверка нажатия кнопки атаки (просто для иллюстрации)
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Get the camera position
        Vector3 cameraPosition = Camera.main.transform.position;
        GameObject projectile = Instantiate(projectilePrefab, cameraPosition, Quaternion.identity);

        // Calculate the direction from the camera position to the mouse position
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
        Vector3 projectileDirection = (worldMousePosition - cameraPosition).normalized;


        projectile.transform.LookAt(worldMousePosition);
        projectile.GetComponent<Bullet>().damage = player.GetComponent<Movement>().damage;
        // Adjust the projectile's velocity based on the calculated direction
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = projectileDirection * projectileSpeed;
    }




}

public class ProjectileKillHandler : MonoBehaviour
{
    private float normalRicochetChance;
    private float lowHealthRicochetChance;

    // Установка шансов рикошета
    public void SetRicochetChances(float normalChance, float lowHealthChance)
    {
        normalRicochetChance = normalChance;
        lowHealthRicochetChance = lowHealthChance;
    }

    void OnTriggerEnter(Collider other)
    {
        // Пример: обработка убийства врага
        if (other.CompareTag("Enemy"))
        {
            bool shouldRicochet = Random.Range(0f, 1f) <= normalRicochetChance;

            // Проверка низкого здоровья игрока
            // Ваша логика определения низкого здоровья здесь...

            if (shouldRicochet)
            {
                Ricochet();
            }
            else
            {
                // Обработка убийства
                Destroy(gameObject);
            }
        }
    }

    void Ricochet()
    {
        // Пример: логика рикошета в ближайшего врага
        // Ваша логика рикошета здесь...
    }
}
