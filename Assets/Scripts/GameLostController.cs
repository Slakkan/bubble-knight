using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLostController : MonoBehaviour
{
    [SerializeField]
    private Button _retryButton;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField] private ScoreController _scoreController;

    [SerializeField]
    private GameObject _gameOverScreen;

    private void Start()
    {
        _retryButton.onClick.AddListener(Retry);
        _playerController.OnHealthChanged += HealthChanged;
    }

    private void HealthChanged(int newHealth)
    {
        if (newHealth <= 0)
        {
            _scoreText.text = _scoreController.Score.ToString();
            _gameOverScreen.SetActive(true);
        }
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
