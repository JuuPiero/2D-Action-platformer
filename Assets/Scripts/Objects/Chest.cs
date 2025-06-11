using UnityEngine;

public class Chest : BaseInteractable
{
    [SerializeField] public int itemCount = 6;
    public Animator Anim { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponent<Animator>();
    }


    public override void Interact()
    {
        Open();
    }

    public void Open()
    {
        _canInteract = false;
        Anim.SetBool("Open", true);
        AudioManager.Instance?.PlaySoundOneShot("OpenChest", transform.position);
        for (int i = 0; i < itemCount; i++)
        {
            ItemManager.Instance.SpawnItem(transform.position + Vector3.up);
        }
        Destroy(gameObject, 3.5f);
    }
   
}