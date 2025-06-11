
using UnityEngine;

public interface IPushable {

    void Pushed(Vector2 direction, float force);

    public void StopPushing();
   
}