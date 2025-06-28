using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CPUControl CPU { get; private set; }

    public GameObject EnemyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CPU = GetComponentInChildren<CPUControl>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0F);
            var dir = new Vector3
            {
                x = Random.Range(-1.0F, 1.0F),
                y = Random.Range(-1.0F, 1.0F),
                z = 0.0F,
            }.normalized;
            Vector3 pos = dir * 1000.0F;
            Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);
        }
    }
}
