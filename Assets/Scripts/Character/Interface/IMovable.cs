using UnityEngine;

public interface IMovable
{
    public Rigidbody2D RB { get; set; }
    public Vector2 Direction { get; set; }
    public bool IsFacingRight { get; set; }

    // void Move();
    void CheckFlip();
}