using System;
using System.Collections;
using UnityEngine;

public class NarrativeScript : MonoBehaviour
{
    public static NarrativeScript Instance;

    public int Day { get; private set; } = 1;

    private bool axiChatOpened;
    private bool gameStarted;
    private bool gameWon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        Func<IEnumerator> routine = Day switch
        {
            1 => RunDayOne,
            2 => RunDayTwo,
            3 => RunDayThree,
            _ => RunDayFour,
        };
        StartCoroutine(routine());
    }

    public IEnumerator RunDayOne()
    {
        AxiChatController.Chats = new()
        {
            new()
            {
                Person = "Manager",
                Messages = new()
                {
                    new()
                    {
                        Sender = "Manager",
                        Message = "Your 1st day is starting soon! I wanted to reach out to you about the abilities and threats you’ll need to know about. You’ll have access to an ability called Firewall- Drag a firewall application from the desktop to spawn a fiery shield to defend the CPU for 2 hits.",
                    },
                    new()
                    {
                        Sender = "Manager",
                        Message = "Viruses are attacking the CPU, you’ll have to click them with your cursor to destroy them and prevent them from damaging the CPU.",
                    },
                    new()
                    {
                        Sender = "Manager",
                        Message = "Be careful however there is a known Voltage Surge affecting company computers. Lightning bolts are quickly attacking the CPU, they don’t do damage but will mess up the PC’s graphics temporarily if not destroyed.",
                    },
                    new()
                    {
                        Sender = "Manager",
                        Message = "If you have any additional questions or forget about the information above you can come back to this chat or check the ‘howTo’ folder in your first day folder.",
                    },
                },
            },
            new()
            {
                Person = "SYSTEM TEST",
                Messages = new()
                {
                    new()
                    {
                        Sender = "SYSTEM TEST",
                        Message = "This is a test of the message system to ensure compatibility.",
                    },
                },
            },
        };
        yield return new WaitForSeconds(3.0F);
        OSManager.Instance.ShowChatNotification();
        yield return new WaitUntil(() => axiChatOpened);
        axiChatOpened = false;
        yield return new WaitForSeconds(20.0F);
        OSManager.Instance.AddWarning("Virus incursion detected! Head to the Antivirus Trainer 1.0 application to repel the attack.");
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameWon);
        gameWon = false;
        OSManager.Instance.AddInfo("Congratulations, you successfully repelled the virus incursion.");
    }

    public IEnumerator RunDayTwo()
    {
        yield return new WaitForSeconds(1.0F);
    }

    public IEnumerator RunDayThree()
    {
        yield return new WaitForSeconds(1.0F);
    }

    public IEnumerator RunDayFour()
    {
        yield return new WaitForSeconds(1.0F);
    }

    public void OnAxiChatOpened()
    {
        axiChatOpened = true;
    }

    public void OnGameStarted()
    {

    }

    public void OnGameWon()
    {

    }
}
