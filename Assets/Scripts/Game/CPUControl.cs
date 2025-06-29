using System.Linq;
using TMPro;
using UnityEngine;

public class CPUControl : MonoBehaviour
{
    public int Health { get; set; } = 3;

    public TextMeshProUGUI HealthText { get; private set; }

    private GameManager gameManager;

    private Animator animator;

    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthText = GetComponentInChildren<TextMeshProUGUI>();
        gameManager = GetComponentInParent<GameManager>();
        animator = GetComponent<Animator>();
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.SetText(Health.ToString());
    }

    public void Damage()
    {
        Health -= 1;
        gameManager.OnCPUDamaged();
        audioSource.Play();
        UpdateSprite();
        if (Health <= 0)
        {
            gameManager.OnCPUDeath();
        }
    }

    private void UpdateSprite()
    {
        animator.SetInteger("Health", Health);
    }
}
