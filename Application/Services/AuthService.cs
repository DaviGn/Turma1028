﻿using Domain.Exceptions;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Repositories;

namespace Application.Services;

public interface IAuthService
{
    AuthResponse SignIn(AuthRequest request);
}

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IHashingService _hashingService;
    private readonly IUserRepository _userRepository;

    private const string InvalidLoginMessage = "Login is invalid!";

    public AuthService(IUserRepository userRepository, IHashingService hashingService,
        IJwtService jwtService)
    {
        _jwtService = jwtService;
        _hashingService = hashingService;
        _userRepository = userRepository;
    }

    public AuthResponse SignIn(AuthRequest request)
    {
        var user = _userRepository.FindByEmail(request.Email!);

        if (user is null)
            throw new UnathorizedException(InvalidLoginMessage);

        var isPasswordValid = _hashingService.Verify(request.Password!, user.Password!);

        if (!isPasswordValid)
            throw new UnathorizedException(InvalidLoginMessage);

        var jwt = _jwtService.CreateToken(user);
        return new AuthResponse
        {
            Token = jwt,
        };
    }
}
