using UnityEngine;

public class AbilityPresenter : MonoBehaviour
{
    [SerializeField]
    private AbilityDisplay _display;
    [SerializeField]
    private Ability _ability;

    public void Start()
    {
        _display.AbilityNameText.text = _ability.AbilityData.Name;
        _display.CurrentAbilityCDImage.fillAmount = 1f -_ability.CurrentCooldown / ( _ability.AbilityData.Cooldown + _ability.AbilityData.AnimationLength / _ability.AbilityData.AnimationSpeed);
        //_display.HotkeyText.text = TODO
    }

    private void Update()
    {
        _display.CurrentAbilityCDImage.fillAmount = 1f -_ability.CurrentCooldown / ( _ability.AbilityData.Cooldown + _ability.AbilityData.AnimationLength / _ability.AbilityData.AnimationSpeed);
    }
}
