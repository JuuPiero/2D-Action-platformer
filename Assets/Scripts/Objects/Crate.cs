using System.Collections;
using UnityEngine;

public class Crate : MonoBehaviour, IPushable {

    [SerializeField] private LayerMask _whoCanPush;
    [SerializeField] private Vector2 _detectedSize;


    private Rigidbody2D _rb;
    private bool _isBeingPushed = false;
    private float _moveSpeed = 0f; // Sẽ nhận tốc độ từ Player

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX; 
    }

    void Update()
    {
        bool canPush = Physics2D.OverlapBox(transform.position, _detectedSize, _whoCanPush);
        if(canPush) {
            // var playerDetect =  Physics2D.OverlapBoxAll(transform.position, _detectedSize, _whoCanPush);
            // foreach (var player in playerDetect)
            // {
            //     player.GetComponent<Player>().CanPush = true;
            // }
        } 
    }

    void FixedUpdate()
    {
        if (_isBeingPushed)
        {
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }
    }

    public void Pushed(Vector2 direction, float force)
    {
        _moveSpeed = force * Mathf.Sign(direction.x); // Đảm bảo hướng đúng
        _isBeingPushed = true;
    }

   public void StopPushing()
    {
        _isBeingPushed = false;
        _rb.velocity = Vector2.zero; // Dừng lại ngay khi player dừng
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _detectedSize);
    }
}