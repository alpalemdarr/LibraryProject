using LibarySystem.Data;
using LibarySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibarySystem.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly DataContext _context;
        public BorrowingRepository(DataContext context)
        {
            _context = context;
        }
        public void AddBorrowing(BorrowingsDTO _borrow)
        {
            try
            {
                Borrowing borrowing = new Borrowing
                {
                    BorrowingsId = _borrow.BorrowingsId,
                    BorrowingDate = _borrow.BorrowingDate,
                    ReturnTime = _borrow.ReturnTime,
                    BookId = _borrow.BookId,
                    UserId = _borrow.UserId,
                    Book = _context.Books.Find(_borrow.BookId),
                    User = _context.Users.Find(_borrow.UserId)
                };

                _context.Borrowings.Add(borrowing);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception("An error occurred while adding the borrowing record.", ex);
            }
        }

        public void DeleteBorrowing(BorrowingsDTO _borrow)
        {
            var borrow = _context.Borrowings.Find(_borrow.BorrowingsId);
            if (borrow != null)
            {
                _context.Borrowings.Remove(borrow);
                _context.SaveChanges();
            }
        }

        public List<BorrowingsDTO> GetAllBorrowings()
        {
            return _context.Borrowings.Select(borrow => new BorrowingsDTO
            {
                BorrowingsId = borrow.BorrowingsId,
                BookId = borrow.BookId,
                UserId = borrow.UserId,
                BorrowingDate = borrow.BorrowingDate,
                ReturnTime = borrow.ReturnTime,
                Book = new BookDTO
                {
                    BookId = borrow.Book.BookId,
                    BookName = borrow.Book.BookName,
                    BookTitle = borrow.Book.BookTitle,
                    Author = borrow.Book.Author,
                    FilePath = borrow.Book.FilePath,
                    PublishedDate = borrow.Book.PublishedDate
                },
                User = new UserDTO
                {
                    UserId = borrow.User.UserId,
                    Name = borrow.User.Name,
                    Email = borrow.User.Email,
                    MembershipDate = borrow.User.MembershipDate
                }
            }).ToList();
        }

        public BorrowingsDTO GetBorrowById(int borrowsId)
        {
            var borrow = _context.Borrowings
                                 .Include(b => b.Book)
                                 .Include(b => b.User)
                                 .FirstOrDefault(b => b.BorrowingsId == borrowsId);

            if (borrow == null)
            {
                return null; // Ödünç alma kaydı bulunamazsa null döndür
            }

            return new BorrowingsDTO
            {
                BorrowingsId = borrow.BorrowingsId,
                BookId = borrow.BookId,
                UserId = borrow.UserId,
                BorrowingDate = borrow.BorrowingDate,
                ReturnTime = borrow.ReturnTime,
                Book = new BookDTO
                {
                    BookId = borrow.Book.BookId,
                    BookName = borrow.Book.BookName,
                    BookTitle = borrow.Book.BookTitle,
                    Author = borrow.Book.Author,
                    FilePath = borrow.Book.FilePath,
                    PublishedDate = borrow.Book.PublishedDate
                },
                User = new UserDTO
                {
                    UserId = borrow.User.UserId,
                    Name = borrow.User.Name,
                    Email = borrow.User.Email,
                    MembershipDate = borrow.User.MembershipDate
                }
            };
        }

        public void UpdateBorrowing(BorrowingsDTO borrow)
        {
            var existingBorrow = _context.Borrowings
                                         .Include(b => b.Book)
                                         .Include(b => b.User)
                                         .FirstOrDefault(b => b.BorrowingsId == borrow.BorrowingsId);

            if (existingBorrow != null)
            {
                existingBorrow.BorrowingDate = borrow.BorrowingDate;
                existingBorrow.ReturnTime = borrow.ReturnTime;
                existingBorrow.BookId = borrow.BookId;
                existingBorrow.UserId = borrow.UserId;

                // Book ve User entity'lerini yeniden yükle
                existingBorrow.Book = _context.Books.Find(borrow.BookId);
                existingBorrow.User = _context.Users.Find(borrow.UserId);

                _context.SaveChanges();
            }
        }
    }
}