using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public string AbilityName;

    public int DayAvailable = 1;

    public GameManager GameManager;

    public AbilityState State = AbilityState.Ready;

    public float Cooldown;

    private Image SelfImage;
    private Image CooldownImage;

    void Start()
    {
        SelfImage = GetComponent<Image>();
        var cdObject = transform.Find("CooldownBar");
        CooldownImage = cdObject.GetComponent<Image>();
        CooldownImage.gameObject.SetActive(false);
        if(NarrativeScript.Instance != null)
        {
            if (NarrativeScript.Instance.Day < DayAvailable)
            {
                // Hide this if not available yet
                gameObject.SetActive(false);
            }
        }
        else
        {
            // If NarrativeScript is not available, assume it's always available
            gameObject.SetActive(true);
        }
    }

    public void OnClick()
    {
        if (State != AbilityState.Ready)
        {
            return;
        }
        var startCd = GameManager.ActivateAbility(AbilityName, this);
        if (startCd)
        {
            StartCooldown();
        }
    }

    public void StartCooldown()
    {
        SelfImage.color = new Color(1.0F, 1.0F, 1.0F, 0.4F);
        State = AbilityState.OnCooldown;
        CooldownImage.gameObject.SetActive(true);
        CooldownImage.rectTransform
            .DOSizeDelta(new Vector2(64, 16), Cooldown)
            .From(new Vector2(0, 16))
            .OnComplete(() =>
            {
                CooldownImage.gameObject.SetActive(false);
                State = AbilityState.Ready;
                SelfImage.color = Color.white;
            });
    }
}
