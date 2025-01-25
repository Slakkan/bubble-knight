using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _abilityNameText;
    [SerializeField]
    private Image _currentAbilityCDImage;
    [SerializeField]
    private TextMeshProUGUI _hotkeyText;

    public TextMeshProUGUI AbilityNameText => _abilityNameText;
    public Image CurrentAbilityCDImage => _currentAbilityCDImage;
    public TextMeshProUGUI HotkeyText => _hotkeyText;
}
