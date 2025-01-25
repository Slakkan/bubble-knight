using System;
using TMPro;
using UnityEngine;

public class WaveTimePresenter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _time;
    [SerializeField]
    private EnemySpawner _enemySpawner;

    private void Update()
    {
        _time.text = _enemySpawner.CurrentTimeTillNextSpawn.ToString("F0");
    }
}
