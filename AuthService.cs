using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IJwtService _jwtService;

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            if (await _unitOfWork.Users.ExistsAsync(x => x.UserName == dto.UserName))
                throw new Exception("Username already exists.");

            User user = new User
            {
                UserName = dto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                IsActive = true
            };

            await _unitOfWork.Users.AddAsync(user);

            await _unitOfWork.SaveAsync();
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var users = await _unitOfWork.Users.FindAsync(x =>
                x.UserName == dto.UserName);

            var user = users.FirstOrDefault();

            if (user == null)
                throw new Exception("Invalid Username");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid Password");

            return _jwtService.GenerateToken(user);
        }
    }
}
