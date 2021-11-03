using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using TodoApp.ViewModel.Login;
using ToDoApp_backend.Repository.License;
using ToDoApp_backend.Repository.User;
using ToDoApp_backend.Utility;
using ToDoApp_backend.Auth;

namespace ToDoApp_backend.UseCase
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILicenseRepository _licenseRepository;
        private readonly IConfiguration _configuration;

        public LoginUseCase(IUserRepository userRepository, ILicenseRepository licenseRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _licenseRepository = licenseRepository;
            _configuration = configuration;
        }

        public async Task<long> Login(LoginViewModel loginViewModel)
        {
            var user = await _userRepository.FindLoginUser(loginViewModel);

            var result = await _licenseRepository.UpdateLicenseAsync(user, true);

            //ログイン情報取得後にログイン成功時にログイン情報保持
            if (result)
            {
                var filePath = Path.Combine(
                    _configuration.GetValue<string>("LoginInfoFilePath"),
                    _configuration.GetValue<string>("LoginInfoFileName")
                );

                UserSession userSession = new UserSession()
                {
                    UserId = user.Id,
                    Name = user.Name
                };

                JsonUtility.JsonSerialize<UserSession>(userSession, filePath);
            }

            return result ? user.Id : 0;
        }

        public async Task<bool> Logout()
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var user = await _userRepository.FindUserInfo(userInfo.UserId);

            var result = await _licenseRepository.UpdateLicenseAsync(user, false);
            if (!result) return false;

            JsonUtility.DeletedJson(filePath);

            return result;
        }
    }
}
