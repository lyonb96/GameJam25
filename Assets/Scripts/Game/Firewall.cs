using UnityEngine;

public class Firewall : MonoBehaviour
{
    public GameManager gameManager;
    public Ability ability;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Die();
            Destroy(gameObject);
            gameManager.OnFirewallDead();
            ability.StartCooldown();
        }
    }
}
