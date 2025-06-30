using UnityEngine;

public class ZFighter : MonoBehaviour {

    private void Update() {
        ResetPanelZPositions();
    }

    public static void ResetPanelZPositions()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Panel")
            {
                if (obj.transform.childCount > 0)
                {
                    Transform firstChild = obj.transform.GetChild(0);
                    RectTransform rectTransform = firstChild.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        Vector3 pos = rectTransform.localPosition;
                        pos.z = 0f;
                        rectTransform.localPosition = pos;
                    }
                }
            }
        }
    }
}
