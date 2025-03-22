using System;
using UnityEngine;

public class PlayerHitboxManager : MonoBehaviour
{
    public enum DetectDirection 
    {
        Front,
        Back,
        FrontAndBack,
        Top
    }

    public Transform[] hitboxes;


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitboxes[0].position, 0.6f); 
        //Gizmos.DrawWireCube(hitboxes[1].position, new Vector2(1f, 1.4f));
        Gizmos.DrawWireCube(hitboxes[3].position, new Vector2(3f, 1f));
    }

    
}
