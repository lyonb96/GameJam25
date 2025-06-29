using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 200.0F;

    public bool RequiresCommand;

    public GameManager Manager { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var tf = transform as RectTransform;
        var currentPos = tf.anchoredPosition3D;
        var dirToZero = (Vector3.zero - currentPos).normalized;
        var newPos = currentPos + (dirToZero * MoveSpeed * Time.deltaTime);
        tf.anchoredPosition3D = newPos;
        if (newPos.sqrMagnitude < 10000)
        {
            Manager.CPU.Damage();
            Destroy(gameObject);
        }
    }

    public void OnClicked()
    {
        if (RequiresCommand)
        {
            OSManager.Instance.OpenCommandPrompt(() =>
            {
                Die();
            });
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        // TODO: spawn a cool effect
        Manager.OnEnemyKilled();
        Destroy(gameObject);
    }
}
