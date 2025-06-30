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
        NarrativeScript.Instance.OnGameLost();
        if (NarrativeScript.Instance.Day < 4)
        {
            OSManager.Instance.AddError(
                "Your performance has been deemed inadequate. You have been terminated from your position at Axion Technologies.",
                true,
                () =>
                {
                    OSManager.Instance.ShutDown();
                });
        }
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
            1 => 30.0F,
            2 => 60.0F,
            3 => 90.0F,
            _ => 10000000.0F,
        };
        var mult = day switch
        {
            1 => 1.25F,
            2 => 1.1F,
            3 => 1.0F,
            _ => 1.0F,
        };
        while (startTime + duration > Time.time)
        {
            var timeSinceStart = Time.time - startTime;
            if (day >= 4 && timeSinceStart > 60.0F)
            {
                var after = timeSinceStart - 60.0F;
                mult = Mathf.Clamp(1.0F - after / 180.0F, 0.0F, 1.0F);
            }
            var enemySpawnChance = Random.Range(0.0F, 1.0F);
            var (enemyToSpawn, delay) = enemySpawnChance switch
            {
                <= 0.9F => (EnemyPrefab, 1.0F),
                _ => (BigEnemyPrefab, 7.0F),
                // _ => (LightningEnemyPrefab, 7.0F),
            };
            delay *= mult;
            var dir = new Vector3
            {
                x = Random.Range(-1.0F, 1.0F),
                y = Random.Range(-1.0F, 1.0F),
                z = 0.0F,
            }.normalized;
            Vector3 pos = dir * 10.0F;
            var enemy = Instantiate(enemyToSpawn, transform.position + pos, Quaternion.identity, Background.transform);
            enemies.Add(enemy);
            yield return new WaitForSeconds(delay);
        }
        Debug.Log("Done killing enemies, waiting for enemy count now");
        yield return new WaitUntil(() => enemies.Count == 0);
        Debug.Log("Enemy count is done");
        NarrativeScript.Instance.OnGameWon();
        Debug.Log("On game won called");
    }

    public bool ActivateAbility(string name, Ability script)
    {
        if (name == "Firewall")
        {
            ActivateFirewallAbility(script);
            return false;
        }
        else if (name == "Ice")
        {
            StartCoroutine(ActivateIceAbility());
            return true;
        }
        else if (name == "KILL")
        {
            OSManager.Instance.OpenCommandPrompt(
                "killall",
                null,
                () =>
                {
                    foreach (var enemy in enemies)
                    {
                        enemy.GetComponent<Enemy>().Die();
                    }
                });
            return true;
        }
        return true;
    }

    IEnumerator ActivateIceAbility()
    {
        SpeedMult = 0.5F;
        yield return new WaitForSeconds(5.0F);
        SpeedMult = 1.0F;
    }

    public void ActivateFirewallAbility(Ability script)
    {
        var firewall = Instantiate(FirewallPrefab, transform);
        var firewallComp = firewall.GetComponent<Firewall>();
        firewallComp.gameManager = this;
        firewallComp.ability = script;
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
