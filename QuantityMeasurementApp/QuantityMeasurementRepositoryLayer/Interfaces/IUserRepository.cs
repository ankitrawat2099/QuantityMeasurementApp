using QuantityMeasurementModelLayer.Entities;
namespace QuantityMeasurementRepositoryLayer.Interfaces;
public interface IUserRepository
{
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity> AddUserAsync(UserEntity user);
}