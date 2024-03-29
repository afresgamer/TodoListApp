﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Utility;
using ToDoApp_backend.ViewModel.Signup;
using ToDoApp_backend.Repository.License;
using ToDoApp_backend.Repository.User;
using ToDoApp_backend.Repository.SysMenu;

namespace ToDoApp_backend.UseCase
{
    public class SignupUseCase
    {
        private readonly IUserRepository _repository;
        private readonly ILicenseRepository _licenseRepository;
        private readonly ISysMenuRepository _sysMenuRepository;
        private readonly IConfiguration _configuration;

        public SignupUseCase(
            IUserRepository repository, 
            ILicenseRepository licenseRepository,
            ISysMenuRepository sysMenuRepository,
            IConfiguration configuration)
        {
            _repository = repository;
            _licenseRepository = licenseRepository;
            _sysMenuRepository = sysMenuRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// ライセンス情報を作成後にユーザーを新規登録して、登録されたメールアドレスにユーザー作成完了のメール送信を行う
        /// </summary>
        /// <returns></returns>
        public async Task<long> CreateNewUser(SignupViewModel viewModel)
        {
            var license = await _licenseRepository.InsertInitLicense(viewModel.Password);

            var result = await _repository.InsertSignupUser(viewModel, license);

            var fromAddress = _configuration.GetValue<string>("fromAddress");
            MailApi.SendMail(result.Id, fromAddress, viewModel.MailAddress, viewModel.UserName);

            //画面一覧に初期データを挿入
            await _sysMenuRepository.InsertSysMenu(result.Id);
            
            return result.Id;
        }
    }
}
