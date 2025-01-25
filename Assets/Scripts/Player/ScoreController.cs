using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score;

    public int Score => _score;

    public void AddScore()
    {
        _score++;
        _scoreText.text = "Score: " + _score;
    }


}
