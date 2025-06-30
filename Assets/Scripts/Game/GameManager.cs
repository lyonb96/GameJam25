using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CPUControl CPU { get; private set; }

    public GameObject EnemyPrefab;

    public GameObject BigEnemyPrefab;

    public GameObject LightningEnemyPrefab;

    public GameObject FirewallPrefab;

    public Image Background;

    public float SpeedMult { get; private set; } = 1.0F;

    public int Score { get; set; }

    private List<GameObject> enemies = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        NarrativeScript.Instance.OnGameStarted();
        CPU = GetComponentInChildren<CPUControl>();
    }

    public void OnCPUDamaged(GameObject damager)
    {
        enemies.Remove(damager);
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

    public void OnEnemyKilled(GameObject enemy)
    {
        Score += 1;
        enemies.Remove(enemy);
    }

    IEnumerator SpawnEnemies()
    {
        var day = NarrativeScript.Instance.Day;
        var startTime = Time.time;
        var duration = day switch
        {
            1 => 10.0F,
            2 => 120.0F,
            3 => 150.0F,
            _ => 10000000.0F,
        };
        while (startTime + duration > Time.time)
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
            var enemy = Instantiate(enemyToSpawn, transform.position + pos, Quaternion.identity, transform);
            enemies.Add(enemy);
            yield return new WaitForSeconds(delay);
        }
        Debug.Log("Done killing enemies, waiting for enemy count now");
        yield return new WaitUntil(() => enemies.Count == 0);
        Debug.Log("Enemy count is done");
        NarrativeScript.Instance.OnGameWon();
        Debug.Log("On game won called");
    }

    IEnumerator ActivateIceAbility()
    {
        SpeedMult = 0.5F;
        yield return new WaitForSeconds(5.0F);
        SpeedMult = 1.0F;
    }

    public void ActivateFirewallAbility()
    {
        var firewall = Instantiate(FirewallPrefab, transform);
        var firewallComp = firewall.GetComponent<Firewall>();
        firewallComp.gameManager = this;
    }

    public void OnFirewallDead()
    {
        // Start the cooldown
    }
}

public enum AbilityState
{
    Ready,
    Active,
    OnCooldown,
}
