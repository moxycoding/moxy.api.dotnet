using Microsoft.EntityFrameworkCore;
using Moxy.Core;
using Moxy.Data;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Moxy.Services.Config
{
    public class ConfigService : IConfigService
    {
        /// <summary>
        /// ConfigService
        /// </summary>
        private readonly MoxyDbContext _dbContext;
        private readonly IUnitOfWork<MoxyDbContext> _unitOfWork;
        public ConfigService(MoxyDbContext dbContext
            , IUnitOfWork<MoxyDbContext> unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        public Dictionary<EnumAppConfig, string> GetAll()
        {
            var dirs = new Dictionary<EnumAppConfig, string>();
            var configList = _unitOfWork.GetRepository<SysConfig>().Table.ToListAsync().Result;
            foreach (EnumAppConfig item in typeof(EnumAppConfig).GetEnumValues())
            {
                dirs.Add(item, configList.FirstOrDefault(s => s.Code == item.ToString())?.Value ?? string.Empty);
            }
            return dirs;
        }
        public T Get<T>(EnumAppConfig key) where T : class
        {
            if (GetAll().TryGetValue(key, out string value))
            {
                return value as T;
            }
            return default(T);
        }
        public OperateResult Save(Dictionary<EnumAppConfig, string> keyValues)
        {
            var existCodes = _unitOfWork.GetRepository<SysConfig>().Table.Select(s => s.Code).ToList();
            var newItems = keyValues
                .Where(s => !existCodes.Contains(s.Key.ToString()))
                .Select(s => new SysConfig() { Code = s.Key.ToString(), Value = s.Value })
                .ToList();

            if (newItems.Count > 0)
            {
                _unitOfWork.GetRepository<SysConfig>().Insert(newItems);
            }
            var existItems = keyValues
                .Where(s => existCodes.Contains(s.Key.ToString()))
                .Select(s => new SysConfig() { Code = s.Key.ToString(), Value = s.Value })
                .ToList();
            if (existItems.Count > 0)
            {
                _unitOfWork.GetRepository<SysConfig>().Update(existItems);
            }
            _unitOfWork.SaveChanges();
            return OperateResult.Succeed("保存成功");
        }
    }
}
