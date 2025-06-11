using UnityEngine;

public class WinningCheckpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.winTrigger = true; 

        }
    }
}