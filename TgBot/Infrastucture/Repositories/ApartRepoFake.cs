using TgBot.Domain.Entity;
using TgBot.Repositories;

namespace TgBot;

public class ApartRepoFake : IApartmentRepo
{
    private readonly List<Apartment> _apartments = new List<Apartment>();
    public async Task Add(Apartment apartment)
    {
        _apartments.Add(apartment);
    }

    public Task<List<Apartment>> GetAll(Mail mail)
    {
        throw new NotImplementedException();
    }

    public async Task<Apartment[]> GetAll(long chatId)
    {
        return _apartments.Where(x => x.CharId == chatId).ToArray();
    }
}