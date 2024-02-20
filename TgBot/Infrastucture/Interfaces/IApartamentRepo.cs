using TgBot.Domain;
using TgBot.Domain.Entity;

namespace TgBot.Repositories;

public interface IApartmentRepo
{
    public Task Add(Apartment apartment);
    public Task<Apartment[]> GetAll(long chatId);
}