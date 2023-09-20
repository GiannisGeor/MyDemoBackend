using Models.Entities;


namespace Data.Interfaces
{
    public interface ITainiaRepository
    {
        Task<Tainia> AddNewTainia(Tainia candidate);
        Task<Tainia> MetaboliTainias(Tainia candidate);
        Task<Tainia> GetTainiaTrackedById(int id);
        Task<Tainia> GetTainiaUnTrackedById(int id);
        Task<Tainia> AddNewTainiaKaiSintelestes(Tainia candidate);
        Task<Tainia> AddSintelestesSeTainies(Tainia candidate);
    }
}
