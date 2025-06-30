using UnityEngine;

public class Ability : MonoBehaviour
{
    public string AbilityName;

    public int DayAvailable = 1;

    public GameManager GameManager;

    public AbilityState State = AbilityState.Ready;

    public float Cooldown;

    void Start()
    {
        if (NarrativeScript.Instance.Day < DayAvailable)
        {
            // Hide this if not available yet
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        var startCd = GameManager.ActivateAbility(AbilityName, this);
        if (startCd)
        {
            StartCooldown();
        }
    }

    public void StartCooldown()
    {

    }
}
