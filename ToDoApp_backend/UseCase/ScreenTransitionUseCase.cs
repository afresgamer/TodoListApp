using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDoApp_backend.Auth;
using ToDoApp_backend.Repository.SysMenu;
using ToDoApp_backend.Utility;

namespace ToDoApp_backend.UseCase
{
    public class ScreenTransitionUseCase
    {
        private readonly ISysMenuRepository _sysMenuRepository;
        private readonly IConfiguration _configuration;

        public ScreenTransitionUseCase(ISysMenuRepository sysMenuRepository, IConfiguration configuration)
        {
            _sysMenuRepository = sysMenuRepository;
            _configuration = configuration;
        }

        public async Task<bool> SiteAuthorizationCheck(string toPath)
        {
            //ログイン後か確認
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);
            if (userInfo == null) return false;


            //パス確認
            if (toPath == "error" || toPath == "login" || toPath == "signup")
            {
                return true;
            }

            //遷移画面一覧にデータがあるか確認
            var result = await _sysMenuRepository.FindSysMenuAsync(userInfo.UserId, toPath);
            
            return result != null;
        }
    }
}
