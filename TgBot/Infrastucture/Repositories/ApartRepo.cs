using Microsoft.EntityFrameworkCore;
using TgBot.Domain.Entity;
using TgBot.Infrastucture.DataBase;
using TgBot.Repositories;

namespace TgBot;

public class ApartRepo : IApartmentRepo
{
    private readonly DbObjective _db;

    public ApartRepo(DbObjective db)
    {
        _db = db;
    }

    public async Task Add(Apartment apartment)
    {
        await _db.Apartments.AddAsync(apartment);
        await _db.SaveChangesAsync();
    }

    public async Task<Apartment[]> GetAll(long chatId)
    {
        return await _db.Apartments.Where(x => x.CharId == chatId).ToArrayAsync();
    }
}