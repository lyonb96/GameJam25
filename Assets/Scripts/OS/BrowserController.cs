using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;

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
        NavigateToAddress(directory[0].name);
    }

    void NavigateToAddress(string address)
    {
        if (string.IsNullOrEmpty(address)) return;

        GameObject pageToLoad = directory.FirstOrDefault(p => p.name.Equals(address, StringComparison.OrdinalIgnoreCase));
        if (pageToLoad != null)
        {
            GoToPage(pageToLoad);
        }
        else if (currentPage != null && addressBar != null)
        {
            addressBar.text = currentPage.name;
        }

    }
    
    public void GoToPage(GameObject page)
    {
        if (page == null || page == currentPage) return;

        if (currentPage != null)
        {
            currentPage.SetActive(false);
            backStack.Push(currentPage);
        }
        forwardStack.Clear();

        currentPage = page;
        currentPage.SetActive(true);
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
