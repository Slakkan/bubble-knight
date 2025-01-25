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
    private PlayerController _playerController;

    [SerializeField]
    private Material _bubbleMat;

    private void Start()
    {
        _playerController.OnHealthChanged += UpdateDisplays;
    }

    private void UpdateDisplays(int currentHealth)
    {
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

        Color orig = _bubbleMat.color;
        _bubbleMat.color = new Color(1f, 0f, 0f, 0.4f);
        _bubbleMat.DOColor(orig, 1f);
    }
}
