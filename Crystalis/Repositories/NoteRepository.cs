using Crystalis.Contexts;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Sieve.Services;

namespace Crystalis.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly DataContext _dataContext;
    private readonly ISieveProcessor _sieveProcessor;

    public NoteRepository(DataContext dataContext, ISieveProcessor sieveProcessor)
    {
        _dataContext = dataContext;
        _sieveProcessor = sieveProcessor;
    }

    public List<T> Get<T>(string uuid) where T : Note
    {
        throw new NotImplementedException();
    }
}