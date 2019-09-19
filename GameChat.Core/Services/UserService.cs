using AutoMapper;
using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using GameChat.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameChat.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _settings;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, AppSettings settings, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CreateNewAccountAsync(UserLoginRegisterDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
                return new ServiceResult(false, "Password is required");

            if (await _unitOfWork.UserRepository.FindByUsernameAsync(user.Username) != null)
                return new ServiceResult(false, "That username is already taken");

            var hashes = GeneratePasswordHash(user.Password);

            var newUser = new User()
            {
                PasswordHash = hashes["PasswordHash"],
                PasswordSalt = hashes["PasswordSalt"],
                Username = user.Username
            };

            await _unitOfWork.UserRepository.AddUserAsync(newUser);
            await _unitOfWork.CompleteTransactionAsync();

            return new ServiceResult(true, "Successfully created new account");
        }

        public async Task<ServiceResult<string>> AuthenticateAsync(UserLoginRegisterDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Username))
                return new ServiceResult<string>(false, "Invalid credentials");

            var userEntity = await _unitOfWork.UserRepository.FindByUsernameAsync(user.Username);
            if (userEntity == null)
                return new ServiceResult<string>(false, "Invalid credentials");


            var result = ValidatePassword(user.Password, userEntity.PasswordHash, userEntity.PasswordSalt);

            if (!result)
                return new ServiceResult<string>(false, "Invalid credentials");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var jwtToken = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString())
                },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new ServiceResult<string>(true, "Authentication successful", tokenAsString);
        }

        public async Task<ServiceResult<IEnumerable<UserDto>>> GetUsers(string filter)
        {
            var users = await _unitOfWork.UserRepository.GetUsers(filter);
            var dto = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
            return new ServiceResult<IEnumerable<UserDto>>(true, "Users retrieved", dto);
        }

        #region Helpers for hashing and validating password

        private Dictionary<string, byte[]> GeneratePasswordHash(string password)
        {
            var salt = new byte[64];
            byte[] passwordHash;

            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            using (var crypto = new Rfc2898DeriveBytes(password, salt))
                passwordHash = crypto.GetBytes(128);

            return new Dictionary<string, byte[]>()
            {
                {"PasswordHash", passwordHash },
                {"PasswordSalt", salt}
            };
        }

        private bool ValidatePassword(string providedPassword, byte[] passwordHashFromDb, byte[] salt)
        {
            byte[] providedPasswordhash;

            using (var crypto = new Rfc2898DeriveBytes(providedPassword, salt))
                providedPasswordhash = crypto.GetBytes(128);

            if (passwordHashFromDb.Length != providedPasswordhash.Length)
                return false;

            for (int i = 0; i < passwordHashFromDb.Length; i++)
            {
                if (providedPasswordhash[i] != passwordHashFromDb[i])
                    return false;
            }
            return true;
        }

        #endregion
    }
}
