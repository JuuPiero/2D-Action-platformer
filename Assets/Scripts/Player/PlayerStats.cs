
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Player _player;

    public int damage;
    public int defense;
    public float moveSpeed;
    public float jumpForce;

    void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    void Start()
    {
        damage = _player.Data.basicAttackDamage;
    }
}