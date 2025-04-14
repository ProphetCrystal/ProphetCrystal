using ProphetCrystal.Models;

namespace ProphetCrystal.Repositories.Interfaces;

public interface INoteRepository
{
    public List<T> Get<T>(string uuid) where T : Note;
}