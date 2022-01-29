using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Repository.License;
using ToDoApp_backend.Repository.User;
using ToDoApp_backend.ViewModel.Login;

namespace ToDoApp_backend.UseCase
{
    public class ResetPasswordUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILicenseRepository _licenseRepository;

        public ResetPasswordUseCase(IUserRepository userRepository, ILicenseRepository licenseRepository)
        {
            _userRepository = userRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordViewModel viewModel)
        {
            // User情報を取得しつつ更新日時を更新
            var user = await _userRepository.UpdateUserAsync(viewModel.UserId);

            // ライセンス情報のパスワード更新
            var result = await _licenseRepository.UpdateResetPasswordAsync(user, viewModel.Password);

            return result;
        }
    }
}
