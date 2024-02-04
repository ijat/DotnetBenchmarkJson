using System.Collections.Generic;

namespace BenchmarkJson.Views;

public class MessageView
{
    public int message_id { get; set; }
    public FromView? from { get; set; }
    public string? author_signature { get; set; }
    public SenderChatView? sender_chat { get; set; }
    public ChatView? chat { get; set; }
    public int date { get; set; }
    public string? text { get; set; }
    public List<EntityView>? entities { get; set; }
}