using System.Collections.Generic;

public class Chat
{
    public string Person { get; set; }

    public List<AxiChatMessage> Messages { get; set; }
}

public class AxiChatMessage
{
    public string Sender { get; set; }

    public string Message { get; set; }
}
