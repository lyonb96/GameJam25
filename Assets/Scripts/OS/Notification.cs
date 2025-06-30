using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Notification : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Run());
    }

    public IEnumerator Run()
    {
        yield return new WaitForSeconds(10.0F);
        transform.DOMoveY(-160.0F, 0.75F).OnComplete(() => Destroy(gameObject));
    }
}
