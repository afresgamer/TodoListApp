using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Auth;
using ToDoApp_backend.Repository.Setting;
using ToDoApp_backend.Utility;
using ToDoApp_backend.ViewModel.Setting;

namespace ToDoApp_backend.UseCase
{
    public class SettingUseCase
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IConfiguration _configuration;

        public SettingUseCase(ISettingRepository settingRepository, IConfiguration configuration)
        {
            _settingRepository = settingRepository;
            _configuration = configuration;
        }

        public async Task<SettingViewModel> GetSetting()
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _settingRepository.FindSettingViewModel(userInfo.UserId);

            return result;
        }

        public async Task<bool> PostSetting(SettingViewModel viewModel)
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _settingRepository.UpsertSetting(viewModel, userInfo.UserId);

            return result;
        }
    }
}
