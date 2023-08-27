namespace AlifTask.BusinessLogic;

public sealed class SmsService : ISmsService
{
    public string SendSms(string message, string phoneNumber)
        => $"SMS отправлена на номер {phoneNumber}:\n{message}";
}