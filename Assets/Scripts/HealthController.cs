using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Stats Objects")]
    [SerializeField] private Stats objectStats;
    [SerializeField] private Slider slidertHealth;
    [SerializeField] private Text HealthNow;

    public int health { get; set; }
    public int damage { get; set; }


    public void Initial()
    {
        damage = objectStats.damage;
        health = objectStats.health;
        slidertHealth.maxValue = health;
        slidertHealth.value = health;
        UpdateText();
    }

    private void UpdateText()
    {
        slidertHealth.value = health;
        HealthNow.text = $"{health} / {slidertHealth.maxValue}";
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateText();
        if (health <= 0) 
        {
            Destroy(gameObject);
        }
    }

}
