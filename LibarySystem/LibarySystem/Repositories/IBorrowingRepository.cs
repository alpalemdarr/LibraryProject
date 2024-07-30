using LibarySystem.Models;

namespace LibarySystem.Repositories
{
    public interface IBorrowingRepository
    {
        void AddBorrowing(BorrowingsDTO borrow);
        List<BorrowingsDTO> GetAllBorrowings();
        BorrowingsDTO GetBorrowById(int borrowsId);
        void UpdateBorrowing(BorrowingsDTO borrow);
        void DeleteBorrowing(BorrowingsDTO borrow);
    }
}
