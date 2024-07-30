using CarMechanic.Models;

namespace CarMechanic.Repositories
{
    public interface IMalfunctionRepository
    {
        void AddMalfunction(MalfunctionDTO malfunctionDTO);
        void UpdateMalfunction(MalfunctionDTO malfunctionDTO);
        void DeleteMalfunction(MalfunctionDTO malfunctionDTO);
        List<MalfunctionDTO> GetAllMalfunctions();
        MalfunctionDTO GetMalfunctionById(int id);
    }
}
