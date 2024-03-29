﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;
using ToDoApp_backend.ViewModel.CategoryMaster;
using ToDoApp_backend.Utility;

namespace ToDoApp_backend.Repository.CategoryMaster
{
    public interface ICategoryMasterRepository
    {
        Task<DB.CategoryMaster> FindCategoryMaster(long categoryMasterId);
        Task<List<DB.CategoryMaster>> FetchCategoryMaster(long userId);
        Task<List<CategoryMasterViewModel>> FetchCategoryMasterListAsync(long userId);
        Task<bool> InsertCategoryMaster(CategoryMasterViewModel viewModel, long userId);
        Task<bool> SortCategoryMasterList(List<CategoryMasterViewModel> categoryMasterList);
        Task DeleteCategoryMaster(CategoryMasterViewModel viewModel);

    }

    public class CategoryMasterRepository : RepositoryBase<DB.CategoryMaster>, ICategoryMasterRepository
    {
        private readonly IMapper _mapper;

        public CategoryMasterRepository(MainContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<DB.CategoryMaster> FindCategoryMaster(long categoryMasterId)
        {
            var result = await FindAsync(categoryMasterId);
            return result;
        }

        public async Task<List<DB.CategoryMaster>> FetchCategoryMaster(long userId)
        {
            var result = await db.CategoryMasters
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Id).ThenBy(x => x.SortNo)
                .ToListAsync();

            return result;
        }

        public async Task<List<CategoryMasterViewModel>> FetchCategoryMasterListAsync(long userId)
        {
            var dataList = await db.CategoryMasters
                .AsNoTracking()
                .Where(x => x.SortNo > 0 && x.UserId == userId)
                .OrderBy(x => x.Id).ThenBy(x => x.SortNo)
                .ToListAsync();

            var result = _mapper.Map<List<CategoryMasterViewModel>>(dataList);

            return result;
        }

        public async Task<bool> InsertCategoryMaster(CategoryMasterViewModel viewModel, long userId)
        {
            if (viewModel == null || string.IsNullOrEmpty(viewModel.CategoryName)) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var data = _mapper.Map<DB.CategoryMaster>(viewModel);
                    data.CreateDate = DateTime.Now;
                    data.UserId = userId;

                    await Add(data);
                    await db.SaveChangesAsync();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> SortCategoryMasterList(List<CategoryMasterViewModel> categoryMasterList)
        {
            if (categoryMasterList == null || categoryMasterList.Count <= 0) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<DB.CategoryMaster> resultList = new List<DB.CategoryMaster>(); 

                    foreach (var data in categoryMasterList)
                    {
                        var res = await db.CategoryMasters
                            .Where(x => x.CategoryName == data.CategoryName && x.SortNo == data.SortNo)
                            .FirstOrDefaultAsync();
                        if (res != null)
                        {
                            res.UpdateDate = DateTime.Now;
                            resultList.Add(res);
                        }   
                    }

                    if (resultList.Count <= 0) return false;

                    resultList = SortUtility.Renumbering(resultList) as List<DB.CategoryMaster>;
                    Update(resultList);
                    await db.SaveChangesAsync();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task DeleteCategoryMaster(CategoryMasterViewModel viewModel)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var res = await db.CategoryMasters
                            .Where(x => x.CategoryName == viewModel.CategoryName 
                                    && x.Summary == viewModel.Summary
                                    && x.SortNo == viewModel.SortNo)
                            .FirstOrDefaultAsync();

                    Remove(res);
                    db.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
