namespace CityInfo.API.Services
{
    public interface ILocalMailService
    {
        void send(string subject, string message);
    }
}