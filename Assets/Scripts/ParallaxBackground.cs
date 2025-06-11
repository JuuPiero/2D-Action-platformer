using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public float startPostion;
    public Camera mainCamera;
    public float speed;
    public float backgroundWidth;

    public int index;
    void Awake()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        transform.position = new Vector3((backgroundWidth * index), transform.position.y, 0f);
        startPostion = transform.position.x;
        mainCamera = Camera.main;
    }

    void Start()
    {
        
    }
    // void OnValidate()
    // {
    //     backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    //     // transform.position = new Vector3((backgroundWidth * index), transform.position.y, 0f);
    // }

    private void FixedUpdate()
    {
        float movement = mainCamera.transform.position.x * (1 - speed);

        float distance = mainCamera.transform.position.x * speed;

        transform.position = new Vector3(startPostion + distance, transform.position.y, transform.position.z);
        // transform.position = Vector3.Lerp(transform.position, new Vector3(startPostion + distance, transform.position.y, transform.position.z), Time.deltaTime * 10f);
        if (movement >= startPostion + backgroundWidth)
        {
            gameObject.SetActive(false);
            startPostion += backgroundWidth * 2;
            gameObject.SetActive(true);
        }
        else if (movement < startPostion - backgroundWidth)
        {
            gameObject.SetActive(false);
            startPostion -= backgroundWidth * 2;
            gameObject.SetActive(true);
        }
    }
}