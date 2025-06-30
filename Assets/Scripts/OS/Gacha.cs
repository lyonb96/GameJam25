using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Gacha : MonoBehaviour
{
    public List<GameObject> rewards = new List<GameObject>();
    public Sprite icon;

    public void OpenRandomRewardWindow()
    {

        if (NarrativeScript.Instance.pp > 0)
        {
            NarrativeScript.Instance.pp--;
            int randomIndex = Random.Range(0, rewards.Count);
            GameObject rewardPrefab = rewards[randomIndex];
            rewards.RemoveAt(randomIndex);

            OSManager.Instance.SpawnWindow(new()
            {
                Title = "Reward!",
                Icon = icon,
                Content = rewardPrefab,
                Size = WindowSize.Medium,
            });
        }

    }
}
