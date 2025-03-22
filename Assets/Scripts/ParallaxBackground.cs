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
        mainCamera = Camera.main;

        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate()
    {
        float distance = mainCamera.transform.position.x * speed;
        float movement = mainCamera.transform.position.x * (1 - speed);

        transform.position = new Vector3(startPostion + distance, transform.position.y , transform.position.z);

        if(movement > startPostion + backgroundWidth)
        {
            startPostion += backgroundWidth;
        }  
        else if(movement < startPostion - backgroundWidth)
        {
            startPostion -= backgroundWidth;
        }      
    }
}