using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float ricochetChance = 0.1f; // ���� �������� ��� ��������
    public float lowHealthRicochetChance = 1.0f; // ���� �������� ��� ������ ��������
    private GameObject player;
    private void Start()
    {
        Movement movement = Movement.instance;
        player = movement.gameObject;
    }
    void Update()
    {

        // ������: �������� ������� ������ ����� (������ ��� �����������)
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

    // ��������� ������ ��������
    public void SetRicochetChances(float normalChance, float lowHealthChance)
    {
        normalRicochetChance = normalChance;
        lowHealthRicochetChance = lowHealthChance;
    }

    void OnTriggerEnter(Collider other)
    {
        // ������: ��������� �������� �����
        if (other.CompareTag("Enemy"))
        {
            bool shouldRicochet = Random.Range(0f, 1f) <= normalRicochetChance;

            // �������� ������� �������� ������
            // ���� ������ ����������� ������� �������� �����...

            if (shouldRicochet)
            {
                Ricochet();
            }
            else
            {
                // ��������� ��������
                Destroy(gameObject);
            }
        }
    }

    void Ricochet()
    {
        // ������: ������ �������� � ���������� �����
        // ���� ������ �������� �����...
    }
}
