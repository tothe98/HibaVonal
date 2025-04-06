using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.DataContext.Entities;
using HibaVonal.Services.Exceptions;
using HibaVonal.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services
{

    public interface IAuthService
    {
        Task<AccessTokenDto> Login(LoginDto login);
        Task<UserDataDto> Registration(RegisterDto regiser);
        bool ValidateToken(string token);

    }

    public class AuthService : IAuthService
    {
        private SQL _context;
        private TokenHandlerService _tokenHandler;
        private readonly IMapper _mapper;
        public AuthService(SQL context, TokenHandlerService tokenHandlerService, IMapper mapper)
        {
            _context = context;
            _tokenHandler = tokenHandlerService;
            _mapper = mapper;
        }

        public async Task<AccessTokenDto> Login(LoginDto loginData)
        {
            ObjectValidatorService<LoginDto> v = new ObjectValidatorService<LoginDto>(loginData);
            v.IsValid();
            if (_context.User.Any(u => u.Email == loginData.Email))
            {
                User user = await _context.User.FirstAsync(u => u.Email == loginData.Email);

                if(user.IsDeleted == true)
                {
                    throw new UnauthorizedAccessException();
                }

                Encryption enc = Encryption.Initialize(user.Password);
                if (enc.Validate(loginData.Password))
                {
                    AccessTokenDto token = new AccessTokenDto();
                    token.Token = _tokenHandler.GenerateToken(user).Token;
                    token.ExpireAt = DateTime.Now.AddHours(24);
                    token.UserData = _mapper.Map<UserDataDto>(user); 
                    return token;
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<UserDataDto> Registration(RegisterDto regiser)
        {
            ObjectValidatorService<RegisterDto> v = new ObjectValidatorService<RegisterDto>(regiser);
            v.IsValid();
            if (_context.User.Any(u => u.Email == regiser.Email))
            {
                throw new UserAlreadyExistsException();
            }
            else
            {
                if (regiser.Password != regiser.PasswordConfirm)
                {
                    throw new PasswordsNotMatchException();
                }
                else
                {

                    Encryption enc = Encryption.Initialize();

                    User user = new User()
                    {
                        Email = regiser.Email,
                        Name = regiser.Name,
                        Password = enc.EncyptPassword(regiser.Password),
                        PhoneNumber = regiser.PhoneNumber,
                    };

                    _context.User.Add(user);
                    await _context.SaveChangesAsync();
                    //Ezt lehetne szebben is megoldani
                    int userId = _context.User.First(u => u.Email == user.Email).Id;
                    UserRole userRole = new UserRole() { RoleId = 4, UserId = userId };
                    _context.UserRole.Add(userRole);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<UserDataDto>(user);
                }
            }

        }

        public bool ValidateToken(string token)
        {
            return _tokenHandler.ValidateToken(token);
        }
    }
}
