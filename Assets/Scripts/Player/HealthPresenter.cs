using System;
using DG.Tweening;
using UnityEngine;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField]
    private HealthDisplay _firstLife;
    [SerializeField]
    private HealthDisplay _secondLife;
    [SerializeField]
    private HealthDisplay _thirdLife;
    [SerializeField]
    private HealthDisplay _fourthLife;
    [SerializeField]
    private HealthDisplay _fifthLife;
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private Material _bubbleMat;
    
    private Color origColor = new Color(0.0417357f, 0.8679164f, 1f, 0.2980392f);

    private void Start()
    {
        _bubbleMat.color = origColor;
        _playerController.OnHealthChanged += UpdateDisplays;
    }

    private void OnDestroy()
    {
        _bubbleMat.color = origColor;
    }

    private void UpdateDisplays(int currentHealth)
    {
        if (currentHealth < 5)
        {
            _fifthLife.Health.CrossFadeAlpha(0, 0.5f, true);
        }
        if (currentHealth < 4)
        {
            _fourthLife.Health.CrossFadeAlpha(0, 0.5f, true);
        }

        if (currentHealth < 3)
        {
            _thirdLife.Health.CrossFadeAlpha(0, 0.5f, true);
        }
        if (currentHealth < 2)
        {
            _secondLife.Health.CrossFadeAlpha(0, 0.5f, true);
        }
        if (currentHealth < 1)
        {
            _firstLife.Health.CrossFadeAlpha(0, 0.5f, true);
        }

        _bubbleMat.color = new Color(1f, 0f, 0f, 0.4f);
        _bubbleMat.DOColor(origColor, 1f);
    }
}
