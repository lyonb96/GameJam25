using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 150.0F;

    public int Damage = 1;

    public bool RequiresCommand;

    public string ProcessId;

    public bool IsElevated;

    public GameManager Manager { get; private set; }

    public bool SpawnErrorOnHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GetComponentInParent<GameManager>();
        if (RequiresCommand)
        {
            IsElevated = NarrativeScript.Instance.Day > 2 && Random.value > 0.8F;
            if (IsElevated)
            {
                GetComponent<Image>().color = Color.red;
            }
        }
        ProcessId = ((int)(Random.value * 10000)).ToString("0000");
        var text = GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = "PID: " + ProcessId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var tf = transform as RectTransform;
        var currentPos = tf.anchoredPosition3D;
        var dirToZero = (Vector3.zero - currentPos).normalized;
        var newPos = currentPos + (MoveSpeed * Manager.SpeedMult * Time.deltaTime * dirToZero);
        tf.anchoredPosition3D = newPos;
        if (newPos.sqrMagnitude < 10000)
        {
            Manager.CPU.Damage(gameObject, Damage);
            if (SpawnErrorOnHit)
            {
                OSManager.Instance.AddError("Unexpected voltage surge; some system functions may have been impacted");
            }
            Destroy(gameObject);
        }
    }

    public void OnClicked()
    {
        if (RequiresCommand)
        {
            var command = "kill " + ProcessId;
            if (IsElevated)
            {
                command = "sudo " + command;
            }
            OSManager.Instance.OpenCommandPrompt(
                command,
                gameObject,
                () =>
                {
                    if (this != null)
                    {
                        Die();
                    }
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
        Manager.OnEnemyKilled(gameObject);
        Destroy(gameObject);
    }
}
