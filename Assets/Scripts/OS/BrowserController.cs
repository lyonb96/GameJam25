using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;
using System.Collections;


public class BrowserController : MonoBehaviour
{
    public List<GameObject> directory = new List<GameObject>();

    private Stack<GameObject> backStack = new Stack<GameObject>();
    private Stack<GameObject> forwardStack = new Stack<GameObject>();

    public TMP_InputField addressBar;

    private GameObject currentPage;

    void Start()
    {
        Transform contentPanel = transform.Find("Content");
        addressBar.onEndEdit.AddListener(NavigateToAddress);

        directory.Clear();
        foreach (Transform pageTransform in contentPanel)
        {
            GameObject page = pageTransform.gameObject;
            directory.Add(page);
            page.SetActive(false);
        }

        StartCoroutine(GoToPage(directory[0], true));
    }

    void NavigateToAddress(string address)
    {
        if (string.IsNullOrEmpty(address)) return;

        GameObject pageToLoad = directory.FirstOrDefault(p => p.name.Equals(address, StringComparison.OrdinalIgnoreCase));
        if (pageToLoad != null)
        {
            StartCoroutine(GoToPage(pageToLoad));
        }
        else if (currentPage != null && addressBar != null)
        {
            addressBar.text = currentPage.name;
        }

    }

    public void LoadPage(GameObject page)
    {
        StartCoroutine(GoToPage(page));
    }
    
    private IEnumerator GoToPage(GameObject page, bool fast = false)
    {
        if (!fast)
        {
            OSManager.Instance.SetLoading(true);
            var delay = UnityEngine.Random.Range(1.0f, 2.5f);
            if (UnityEngine.Random.Range(0.0F, 1.0F) < 0.05F)
            {
                delay *= 5.0F;
            }
            yield return new WaitForSeconds(delay);
            OSManager.Instance.SetLoading(false);
        }
        if (!(page == null || page == currentPage))
        {
            if (currentPage != null)
            {
                currentPage.SetActive(false);
                backStack.Push(currentPage);
            }
            forwardStack.Clear();

            currentPage = page;
            currentPage.SetActive(true);
        }

    }

    public void Back()
    {
        if (backStack.Count > 0)
        {
            forwardStack.Push(currentPage);
            currentPage.SetActive(false);
            currentPage = backStack.Pop();
            currentPage.SetActive(true);
        }
    }

    public void Forward()
    {
        if (forwardStack.Count > 0)
        {
            backStack.Push(currentPage);
            currentPage.SetActive(false);
            currentPage = forwardStack.Pop();
            currentPage.SetActive(true);
        }
    }
}
