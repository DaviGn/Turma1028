using Domain.Entities;
using Domain.Requests;

namespace Domain.Mappers;

public static class UserMapper
{
    public static UserResponse ToResponse(User user) => new UserResponse
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email
    };

    public static User ToEntity(BaseUserRequest user) => new User
    {
        Name = user.Name,
        Email = user.Email,
        Password = user.Password,
    };

    public static User ToEntity(UpdateUserRequest user) => new User
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Password = user.Password,
    };
}
