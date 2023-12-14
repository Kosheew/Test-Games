using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "DataState/State")]
public class Stats : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    public int health => _health;
    public int damage => _damage;
}
