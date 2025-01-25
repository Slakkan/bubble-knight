using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplay : MonoBehaviour
{
    [SerializeField]
    private Image _backgroundImage;
    [SerializeField]
    private TextMeshProUGUI _abilityNameText;
    [SerializeField]
    private TextMeshProUGUI _currentAbilityCDText;
    [SerializeField]
    private TextMeshProUGUI _hotkeyText;

    public Image BackgroundImage => _backgroundImage;
    public TextMeshProUGUI AbilityNameText => _abilityNameText;
    public TextMeshProUGUI CurrentAbilityCDText => _currentAbilityCDText;
    public TextMeshProUGUI HotkeyText => _hotkeyText;
}
