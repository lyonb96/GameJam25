using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class NarrativeScript : MonoBehaviour
{
    public static NarrativeScript Instance;

    public int Day { get; private set; } = 1;

    public bool CanLaunchGame { get; private set; } = false;

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
        StopAllCoroutines();
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
                        Message = "Welcome to your first day at Axion! I wanted to reach out to you about the abilities and threats you'll need to know about.",
                    },
                    new()
                    {
                        Sender = "Manager",
                        Message = "Your job is to defend our network from viruses. The boys in R&D cooked up an antivirus training software that you'll be granted access to shortly. As you play this game, the viruses will become stronger!",
                    },
                    new()
                    {
                        Sender = "Manager",
                        Message = "Err, weaker. Not stronger. Our antivirus will be the thing getting stronger.",
                    },
                    new()
                    {
                        Sender = "Manager",
                        Message = "To defeat the viruses, simply click on them as they approach the center. You will also be equipped with a firewall, which you can deploy via the button on the bottom of the training software.",
                    },
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
        CanLaunchGame = true;
        OSManager.Instance.ShowIcon("Antivirus Trainer 2.0");
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameWon);
        gameWon = false;
        CanLaunchGame = false;
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
                Message = "Glad to see yesterday wasn't too hard on you. Some new information has come up about what to expect today on top of yesterday's additions. The PC can now utilize a Virus Freeze - it will slow down the virus for a moment, allowing you to target threats more carefully.",
            },
            // new()
            // {
            //     Sender = "Manager",
            //     Message = "Cloaked Viruses are attacking the systems! Use your Spacebar to see them but be careful, the charge is limited. Use it too often you might get snuck up on!",
            // },
            new()
            {
                Sender = "Manager",
                Message = "Be warned, though. The viruses seem to be getting stronger. Some have tapped into the operating system's process host. R&D has updated the training application to display a 'PID' above them.",
            },
            new()
            {
                Sender = "Manager",
                Message = "When you click these viruses, a command window will open. Simply type 'kill' followed by the PID above the virus to terminate them.",
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
        yield return new WaitForSeconds(2.0F);
        OSManager.Instance.ShowChatNotification();
        yield return new WaitForSeconds(20.0F);
        OSManager.Instance.AddWarning("Virus incursion detected! Head to the Antivirus Trainer 2.0 application to repel the attack.");
        CanLaunchGame = true;
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameWon);
        gameWon = false;
        CanLaunchGame = false;
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
            new()
            {
                Sender = "Manager",
                Message = "Heads up, by the way. R&D tells me the viruses have learned to elevate their permissions. If you see a red virus, you will need to prefix the 'kill' command with the word 'sudo'.",
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
        yield return new WaitForSeconds(2.0F);
        OSManager.Instance.ShowChatNotification();
        yield return new WaitForSeconds(20.0F);
        OSManager.Instance.AddWarning("Virus incursion detected! Head to the Antivirus Trainer 2.0 application to repel the attack.");
        CanLaunchGame = true;
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameWon);
        gameWon = false;
        CanLaunchGame = false;
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
        chats.Add(new()
        {
            Person = "EXTERNAL: [[Name Hidden]]",
            Messages = new()
            {
                new()
                {
                    Sender = "[[Name Hidden]]",
                    Message = "I'm not sure if this message will get through the Axion firewalls. I sent you a message two days ago, and they fired me for it.",
                },
                new()
                {
                    Sender = "[[Name Hidden]]",
                    Message = "Something fishy is going on at Axion, I know it. Have you seen anything?",
                },
            },
        });
        yield return new WaitForSeconds(2.0F);
        OSManager.Instance.ShowChatNotification();
        yield return new WaitForSeconds(20.0F);
        OSManager.Instance.AddWarning("Virus incursion detected! Head to the Antivirus Trainer 2.0 application to repel the attack.");
        CanLaunchGame = true;
        yield return new WaitUntil(() => gameStarted);
        gameStarted = false;
        yield return new WaitUntil(() => gameLost);
        gameLost = false;
        CanLaunchGame = false;
        OSManager.Instance.AddInfo("Virus offensive capabilities verified. Deploying worldwide virus. Thank you for participating in the Axion Virus Training Initiative. Happy New Millenium.");
        OSManager.Instance.FadeToCredits();
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
