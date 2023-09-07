using AutoMapper;
using E3Starter.Contracts.Persistence;
using E3Starter.Contracts.Services;
using E3Starter.Dtos;
using E3Starter.Models;

namespace E3Starter.Services;

public class UserService : IUserService
{
    private readonly ICryptoService _cryptoService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(ICryptoService cryptoService, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _cryptoService = cryptoService;
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return null;
        var isPasswordValid = _cryptoService.VerifyPassword(password, user.HashedPassword, user.PasswordSalt);
        if (!isPasswordValid) return null;

        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetAsync<User>(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> CreateAsync(NewUserDto dto)
    {
        _unitOfWork.Begin();
        var newUser = new User();
        var salt = _cryptoService.GenerateSalt();
        newUser.Username = dto.Username;
        newUser.Email = dto.Email;
        newUser.PasswordSalt = salt;
        newUser.HashedPassword = _cryptoService.HashPassword(dto.Password, salt);
        newUser.CreatedAt = DateTime.UtcNow;
        newUser.Roles = new List<Role>();

        foreach (var role in dto.Roles)
        {
            var roleModel = await _userRepository.LoadAsync<Role>(role.Id);
            newUser.Roles.Add(roleModel);
            roleModel.Users.Add(newUser);
            await _userRepository.SaveAsync(roleModel);
        }

        await _userRepository.SaveAsync(newUser);
        _unitOfWork.Commit();

        return _mapper.Map<UserDto?>(newUser);
    }
}
