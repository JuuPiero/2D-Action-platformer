using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState 
{
    public SpriteRenderer _sr;
    public int afterimagePoolSize = 10;
    private CooldownTimer dashStopTimer;
    private List<GameObject> afterimagePool = new List<GameObject>();


    public PlayerDashState(string animationName, Player player) : base(animationName, player) {
        for (int i = 0; i < afterimagePoolSize; i++)
        {
            GameObject obj = GameObject.Instantiate(_player.afterimagePrefab);
            obj.SetActive(false);
            afterimagePool.Add(obj);
        }
        _sr = _player.GetComponentInChildren<SpriteRenderer>();
    }

    public override bool IsMatchingConditions()
    {
        return _player.InputHandler.IsDashPressed && _player.dashTimer.IsReady;
    }

    public override void Enter()
    {
        CanExit = false;
        base.Enter();
        StartDash();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.RB.velocity = new Vector2( _player.transform.localScale.x *  _player.Data.dashSpeed, 0f);

        dashStopTimer.Update(Time.fixedDeltaTime);
        if (dashStopTimer.IsReady) StopDash();
    }

    void StartDash() {
        dashStopTimer.Start(0.2f);
        _player.StartCoroutine(CreateAfterImages());
    }
    void StopDash() {
        _player.RB.velocity = Vector2.zero;
        CanExit = true;
        _player.dashTimer.Start(0.5f);
    }

    IEnumerator CreateAfterImages() {
        while (!CanExit) {
            GameObject afterimage = GetAfterimageFromPool();
            if (afterimage != null) {
                afterimage.GetComponent<AfterImage>().Setup(
                    _sr.sprite,
                    _player.transform.position,
                    _player.transform.localScale,
                    new Color(1f, 1f, 1f, 0.5f) // Màu trắng, Alpha = 0.5
                );
            }
            yield return new WaitForSeconds(0.005f); // Tạo 1 afterimage mỗi 0.025s
        }
    }

    GameObject GetAfterimageFromPool() {
        foreach (var obj in afterimagePool) {
            if (!obj.activeInHierarchy) {
                return obj;
            }
        }
        return null;
    }
}