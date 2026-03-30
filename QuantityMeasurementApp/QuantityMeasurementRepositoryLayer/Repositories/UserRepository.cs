using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementModelLayer.Entities;
using Microsoft.EntityFrameworkCore;
public class UserRepository : IUserRepository
{
    private readonly QuantityMeasurementDbContext _context;

    public UserRepository(QuantityMeasurementDbContext context) => _context = context;

    public async Task<UserEntity?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<UserEntity> AddUserAsync(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}