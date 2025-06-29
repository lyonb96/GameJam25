using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CPUControl CPU { get; private set; }

    public GameObject EnemyPrefab;

    public GameObject BigEnemyPrefab;

    public GameObject LightningEnemyPrefab;

    public Image Background;

    public int Score { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CPU = GetComponentInChildren<CPUControl>();
        StartCoroutine(SpawnEnemies());
    }

    public void OnCPUDamaged()
    {
        Background.DOColor(Color.pink, 0.25F).OnComplete(() => Background.DOColor(Color.white, 0.25F));
        DOTween.Shake(
            () => CPU.transform.position,
            v => CPU.transform.position = v,
            0.5F,
            Vector3.one * 10.0F);
    }

    public void OnCPUDeath()
    {
        // Game loss stuff here
    }

    public void OnEnemyKilled()
    {
        Score += 1;
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var enemySpawnChance = Random.Range(0.0F, 1.0F);
            var (enemyToSpawn, delay) = enemySpawnChance switch
            {
                <= 0.85F => (EnemyPrefab, 1.0F),
                <= 0.95F => (BigEnemyPrefab, 6.0F),
                _ => (LightningEnemyPrefab, 2.0F),
            };
            var dir = new Vector3
            {
                x = Random.Range(-1.0F, 1.0F),
                y = Random.Range(-1.0F, 1.0F),
                z = 0.0F,
            }.normalized;
            Vector3 pos = dir * 600.0F;
            Instantiate(enemyToSpawn, transform.position + pos, Quaternion.identity, transform);
            yield return new WaitForSeconds(delay);
        }
    }
}
