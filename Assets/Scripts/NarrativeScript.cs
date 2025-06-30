using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class NarrativeScript : MonoBehaviour
{
    public static NarrativeScript Instance;

    public int Day { get; private set; } = 1;

    public int pp = 0;

    private bool axiChatOpened;
    private bool gameStarted;
    private bool gameWon;
    private bool gameLost;
    private bool loggedOff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        Continue();
    }

    public void Continue()
    {
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
                        Message = "Your 1st day is starting soon! I wanted to reach out to you about the abilities and threats you'll need to know about. You'll have access to an ability called Firewall - it will spawn a fiery shield to defend the CPU from a single virus.",
                    },
                    new()
                    {
                        Sender = "Manager",
                        Message = "Viruses are attacking the CPU, you'll have to click them with your cursor to destroy them and prevent them from damaging the CPU.",
                    },
                    // new()
                    // {
                    //     Sender = "Manager",
                    //     Message = "Be careful however there is a known Voltage Surge affecting company computers. Lightning bolts are quickly attacking the CPU, they don�t do damage but will mess up the PC�s graphics temporarily if not destroyed.",
                    // },
                    new()
                    {
                        Sender = "Manager",
                        Message = "If you have any additional questions or forget about the information above you can come back to this chat or check the 'howTo' folder in your first day folder.",
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
        yield return new WaitForSeconds(10.0F);
        OSManager.Instance.ShowChatNotification();
        OSManager.Instance.ShowIcon("AxiChat");
        yield return new WaitUntil(() => axiChatOpened);
        axiChatOpened = false;
        yield return new WaitForSeconds(6.0F);
        OSManager.Instance.AddWarning("Virus incursion detected! Head to the Antivirus Trainer 2.0 application to repel the attack.");
        OSManager.Instance.ShowIcon("Antivirus Trainer 2.0");
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameWon);
        gameWon = false;
        OSManager.Instance.AddInfo("Congratulations, you successfully repelled the virus incursion. Feel free to log off when you are done for the day.");
        yield return new WaitUntil(() => loggedOff);
        Day += 1;
    }

    public IEnumerator RunDayTwo()
    {
        var chats = AxiChatController.Chats;
        var managerChat = chats.Single(x => x.Person == "Manager");
        managerChat.Messages = new()
        {
            new()
            {
                Sender = "Manager",
                Message = "Glad to see yesterday wasn't too hard on you, some new information has come up about what to expect today on top of yesterday's additions. The PC can now utilize a Virus Quarantine - it will slow down the virus for a moment, allowing you to target threats more carefully.",
            },
            // new()
            // {
            //     Sender = "Manager",
            //     Message = "Cloaked Viruses are attacking the systems! Use your Spacebar to see them but be careful, the charge is limited. Use it too often you might get snuck up on!",
            // },
            new()
            {
                Sender = "Manager",
                Message = "Some viruses have also tapped into the operating system's process engine. These will display a 'PID' that must be killed.",
            },
            new()
            {
                Sender = "Manager",
                Message = "When you click these viruses, a command window will arise. Simply type 'kill' followed by the PID above the virus to terminate them.",
            },
            new()
            {
                Sender = "Manager",
                Message = "Be careful though - some of these elevated viruses will be tapped into elevated processes, requiring you to prefix the 'kill' command with 'sudo'!",
            },
        };
        chats.Add(new()
        {
            Person = "AX-PLOX-1",
            Messages = new()
            {
                new()
                {
                    Sender = "AX-PLOX-1",
                    Message = "Hello, nice to see some fresh blood around here, hopefully we don't end up scaring you off. I know you've been here for a few days already but have you gotten the feeling that something is off? Something doesn't seem right about the mail and viruses being spread throughout the company this widely and quickly"
                },
                new()
                {
                    Sender = "AX-PLOX-1",
                    Message = "I'm not sure but I think the people working at this company are in great danger of something we cannot understand yet. Please keep your eyes weary, if not for us then for yourself.",
                },
            },
        });
        yield return new WaitForSeconds(10.0F);
        OSManager.Instance.ShowChatNotification();
        yield return new WaitForSeconds(20.0F);
        OSManager.Instance.AddWarning("Virus incursion detected! Head to the Antivirus Trainer 2.0 application to repel the attack.");
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameWon);
        gameWon = false;
        OSManager.Instance.AddInfo("Congratulations, you successfully repelled the virus incursion.");
        yield return new WaitUntil(() => loggedOff);
        Day += 1;
    }

    public IEnumerator RunDayThree()
    {
        var chats = AxiChatController.Chats;
        var managerChat = chats.Single(x => x.Person == "Manager");
        managerChat.Messages = new()
        {
            new()
            {
                Sender = "Manager",
                Message = "You're handling this better than most employees who have been here longer, incredible! The boys in R&D cooked up another antivirus tool for you, the KILLALL command. Click the KI button and type KILLALL to elimiate all viruses on screen.",
            },
            new()
            {
                Sender = "Manager",
                Message = "This command hammers your system, though, so in order to prevent burning out your CPU it can only be run sparingly.",
            },
        };
        var homieMessages = chats.Single(x => x.Person == "AX-PLOX-1");
        homieMessages.Messages = new()
        {
            new()
            {
                Sender = "Unknown",
                Message = "*Message deleted by system*",
            },
        };
        yield return new WaitForSeconds(10.0F);
        OSManager.Instance.ShowChatNotification();
        yield return new WaitForSeconds(20.0F);
        OSManager.Instance.AddWarning("Virus incursion detected! Head to the Antivirus Trainer 2.0 application to repel the attack.");
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameWon);
        gameWon = false;
        OSManager.Instance.AddInfo("Congratulations, you successfully repelled the virus incursion.");
        yield return new WaitUntil(() => loggedOff);
        Day += 1;
    }

    public IEnumerator RunDayFour()
    {
        var chats = AxiChatController.Chats;
        var managerChat = chats.Single(x => x.Person == "Manager");
        managerChat.Messages = new()
        {
            new()
            {
                Sender = "Manager",
                Message = "Great work! There doesn’t seem to be any additional viruses that the higher-ups have alerted me to but that doesn’t mean there won’t be any. Keep a keen eye out today for anything suspicious or out of the ordinary.",
            },
            new()
            {
                Sender = "Manager",
                Message = "Also I wanted to let you know that the company has allotted for you to be able to have tomorrow off thanks to your excellent work these past few days. Happy new year! Who knows what it will bring...",
            },
        };
        yield return new WaitForSeconds(10.0F);
        OSManager.Instance.ShowChatNotification();
    }

    public void OnAxiChatOpened()
    {
        axiChatOpened = true;
    }

    public void OnGameStarted()
    {
        gameStarted = true;
    }

    public void OnGameWon()
    {
        gameWon = true;
    }

    public void OnGameLost()
    {
        gameLost = true;
    }

    public void OnLoggedOff()
    {
        loggedOff = true;
    }
}
