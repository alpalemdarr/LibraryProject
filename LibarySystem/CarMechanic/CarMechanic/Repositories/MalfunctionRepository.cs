using CarMechanic.Data;
using CarMechanic.Models;

namespace CarMechanic.Repositories
{
    public class MalfunctionRepository : IMalfunctionRepository
    {
        private readonly DataContext _context;
        public MalfunctionRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void AddMalfunction(MalfunctionDTO malfunctionDTO)
        {
            Malfunction malfunction = new Malfunction()
            {
                MalfunctionName = malfunctionDTO.MalfunctionName
            };
            _context.malfunctions.Add(malfunction);
            _context.SaveChanges();

        }

        public void DeleteMalfunction(MalfunctionDTO malfunctionDTO)
        {
            var malfunction = _context.malfunctions.FirstOrDefault(m => m.Id == malfunctionDTO.Id);
            if (malfunction != null)
            {
                _context.malfunctions.Remove(malfunction);
                _context.SaveChanges();
            }
        }

        public List<MalfunctionDTO> GetAllMalfunctions()
        {
            var malfunction = _context.malfunctions.ToList();
            var malfunctionDTO = malfunction.Select(malfunction => new MalfunctionDTO()
            {
                Id = malfunction.Id,
                MalfunctionName = malfunction.MalfunctionName
            }).ToList();

            return malfunctionDTO;
        }

        public MalfunctionDTO GetMalfunctionById(int id)
        {
            var malfunction = _context.malfunctions.FirstOrDefault(m =>m.Id == id);
            if (malfunction == null)
            {
                return null;
            }
            return new MalfunctionDTO
            {
                Id = malfunction.Id,
                MalfunctionName = malfunction.MalfunctionName
            };
        }

        public void UpdateMalfunction(MalfunctionDTO malfunctionDTO)
        {
            var malfunction = _context.malfunctions.FirstOrDefault(m => m.Id == malfunctionDTO.Id);
            if(malfunction != null)
            {
                malfunction.MalfunctionName = malfunctionDTO.MalfunctionName;

                _context.malfunctions.Update(malfunction);
                _context.SaveChanges();
            }
        }
    }
}
