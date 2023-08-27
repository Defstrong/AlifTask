namespace AlifTask.BusinessLogic;

public interface ISmsService
{
    string  SendSms(string message, string phoneNumber);
}