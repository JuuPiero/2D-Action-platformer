using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] protected LayerMask _playerLayer;
    [field: SerializeField] public string InteractionPrompt { get; set; }
    [field: SerializeField] public float InteractRange { get; set; }
    [field: SerializeField] public bool IsPlayerInRange { get; set; }

    protected InteractionUI _interactionUI;

    protected bool _canInteract = true;

    protected virtual void Awake()
    {
        _interactionUI = FindFirstObjectByType<InteractionUI>();
    }


    protected virtual void Update()
    {
        IsPlayerInRange = Physics2D.OverlapCircle(transform.position, InteractRange, _playerLayer);
        if (IsPlayerInRange)
        {
            Collider2D playerCol = Physics2D.OverlapCircle(transform.position, InteractRange, _playerLayer);
            var player = playerCol.GetComponent<Player>();
            if (player.InputHandler.InteractPressed && _canInteract)
            {
                Interact();
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            _interactionUI?.SetText(InteractionPrompt);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _interactionUI?.Hide();
        }
    }

    public virtual void Interact()
    {
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, InteractRange);
    }
}