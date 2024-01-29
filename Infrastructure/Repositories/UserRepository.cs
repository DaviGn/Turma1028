using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    List<User> List();
    User? GetById(int id);
    User? FindByEmail(string email);
    User Create(User newCar);
    User Update(User updatedCar);
    void Delete(int id);
}

public class UserRepository : IUserRepository
{
    private List<User> _users = new List<User>()
    {
        new User
        {
            Id = 1,
            Name = "Davi",
            Email = "davign20@gmail.com",
            Password = "123456",
        }
    };

    public List<User> List()
    {
        return _users;
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    public User? FindByEmail(string email)
    {
        return _users.FirstOrDefault(x => x.Email == email);
    }

    public User Create(User newUser)
    {
        newUser.Id = _users.Count + 1;
        _users.Add(newUser);
        return newUser;
    }

    public User Update(User updatedUser)
    {
        var user = GetById(updatedUser.Id);

        if (user is null)
            throw new Exception("User not found!");

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        return user;
    }

    public void Delete(int id)
    {
        var user = GetById(id);

        if (user is null)
            throw new Exception("Car not found!");

        _users.Remove(user);
    }
}
