using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float startPostion;
    public Camera mainCamera;
    public float speed;
    public float backgroundWidth;

    private void Start()
    {
        startPostion = transform.position.x;
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        mainCamera = Camera.main;

    }

    private void FixedUpdate()
    {
        float movement = mainCamera.transform.position.x * (1 - speed);

        float distance = mainCamera.transform.position.x * speed;

        transform.position = new Vector3(startPostion + distance, transform.position.y , transform.position.z);
        
        if(movement >= startPostion + backgroundWidth)
        {
            startPostion += backgroundWidth;
        }  
        else if(movement < startPostion - backgroundWidth)
        {
            startPostion -= backgroundWidth;
        }      
    }
}