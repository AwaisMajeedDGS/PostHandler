namespace PostHandler.Foundation.Helper
{
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task SendAsync(Message message);
        Task SendAsync(Message message, string imageAttachmentPath,string logAttachmentPath);     
    }
}
