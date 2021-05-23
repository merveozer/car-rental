using Domain.Constants;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    internal class AuthenticationService : IAuthenticationService
    {
        private IUserService UserService { get; }
        public IHashService HashService { get; }
        internal IUserOperationClaimService UserOperationClaimService { get; }
        public IAuthorizationService AuthorizationService { get; }

        public AuthenticationService(IUserService userService, IHashService hashService,
            IUserOperationClaimService userOperationClaimService,
            IAuthorizationService authorizationService)
        {
            UserService = userService;
            HashService = hashService;
            UserOperationClaimService = userOperationClaimService;
            AuthorizationService = authorizationService;
        }
        public Response<ClaimsPrincipal> Register(UserRegisterDTO user)
        {
            byte[] passwordHash, passwordSalt;
            HashService.Create(user.Password, out passwordHash, out passwordSalt);

            User userToCreate = new User
            {
                Email = user.EMail,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserOperationClaim = new Collection<UserOperationClaim>()
                {
                    new UserOperationClaim
                    {
                        OperationClaimId = AuthenticationConstants.OperationClaims.User
                    }
                }
            };

         
            var response = UserService.Add(userToCreate);

            if (response.IsSuccess == false)
            {
                return Response<ClaimsPrincipal>.Fail(response.Message);
            }


            var claimsPrincipal = GetClaimsPrincipal(response.Data);
            return Response<ClaimsPrincipal>.Success("Kayıt başarılı", claimsPrincipal);

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var algorithm = new HMACSHA512())
            {
                passwordSalt = algorithm.Key;
                passwordHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));


            }
        }

        public Response<ClaimsPrincipal> Login(UserLoginDTO user)
        {
            var userToCheck = UserService.GetByEmail(user.EMail);
            if (userToCheck == null)
                return Response<ClaimsPrincipal>.Fail("Kullanıcı bulunamadı.");
            if (VerifyHash(user.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt) == false)
                return Response<ClaimsPrincipal>.Fail("Giriş bilgilerinizi kontrol edin.");

            var claimsPrincipal = GetClaimsPrincipal(userToCheck);

            return Response<ClaimsPrincipal>.Success("Giriş başarılı", claimsPrincipal);
        }

        private bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var algorithm = new HMACSHA512(passwordSalt))
            {
                var computedPassword = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i =0; i< computedPassword.Length; i++)
                {
                    if (computedPassword[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        private ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            var operationClaims = UserOperationClaimService.GetClaims(user.Id);
            var claimsPrincipal = AuthorizationService.GetClaimsPrincipal(user, operationClaims);
            return (ClaimsPrincipal)claimsPrincipal;


        }
    }
}
