namespace Caterpillar.Core.Mailing
{
    public interface IMailingService
    {
        void SendEmail(EmailModelBase model);

        void SendEmail(EmailModelBase model, string content);
    }
}
