using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceUI : MonoBehaviour
{
    [SerializeField] private Image _hpFill;
    [SerializeField] private Image _manaFill;

    [SerializeField] private Player _player;

    void Start()
    {
        _player.Resource.OnPlayerResourceChanged += ResetResource;
        ResetResource();
    }

    public void ResetResource()
    {
        Debug.Log("Call");
        _hpFill.fillAmount = _player.Resource.CurrentHealth / _player.Data.maxHealthPoint;
        _manaFill.fillAmount = _player.Resource.CurrentMana / _player.Data.maxMana;
    }
}