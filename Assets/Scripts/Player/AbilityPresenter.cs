using UnityEngine;

public class AbilityPresenter : MonoBehaviour
{
    [SerializeField]
    private AbilityDisplay _display;
    [SerializeField]
    private Ability _ability;

    public void Start()
    {
        _display.AbilityNameText.text = _ability.AbilityData.name;
        _display.CurrentAbilityCDText.text = _ability.CurrentCooldown.ToString("F2");
        //_display.HotkeyText.text = TODO
    }

    private void Update()
    {
        _display.CurrentAbilityCDText.text = _ability.CurrentCooldown.ToString("F2");
        _display.CurrentAbilityCDText.enabled = _ability.CurrentCooldown > 0;
    }
}
