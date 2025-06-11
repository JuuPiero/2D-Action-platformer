using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Crate : MonoBehaviour, IPushable {

    [SerializeField] private LayerMask _whoCanPush;
  
    private Rigidbody2D _rb;
    private float _moveSpeed = 0f; // Sẽ nhận tốc độ từ Player
    [field: SerializeField] public string InteractionPrompt { get; set; } = "Push the crate";

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX; 
        // if(_playerTriggerCollier != null) {
        //     _detectPlayerColliderSize = _playerTriggerCollier.size;
        // }
    }

    public void Pushed(Vector2 direction, float force)
    {
        _rb.constraints = RigidbodyConstraints2D.None; 
        _moveSpeed = force * Mathf.Sign(direction.x); // Đảm bảo hướng đúng
        _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
    }

    public void StopPushing()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX; 
        _rb.velocity = Vector2.zero; // Dừng lại ngay khi player dừng
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            collision.GetComponent<Player>().CanPush = true;

            Interact(player.gameObject);

            var control = FindFirstObjectByType<MobileControl>();
            control?.interactButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().CanPush = false;
            StopPushing();

            var control = FindFirstObjectByType<MobileControl>();
            control?.interactButton.gameObject.SetActive(false);
        }
    }

    public void Interact(GameObject interactor)
    {

    }
}