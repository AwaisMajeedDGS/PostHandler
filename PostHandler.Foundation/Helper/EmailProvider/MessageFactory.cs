namespace PostHandler.Foundation.Helper
{
    public static class MessageFactory
    {
        public static IMessageService Get(MessageServiceType messageServiceType)
        {
            switch (messageServiceType)
            {
                case MessageServiceType.Email:
                    return new EmailService();
                case MessageServiceType.Sms:
                default:
                    return new EmailService();
            }
        }
    }
    public enum MessageServiceType
    {
        Sms = 1,
        Email = 2
    }
}