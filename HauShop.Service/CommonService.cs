using HauShop.Common;
using HauShop.Data.Infrastructure;
using HauShop.Data.Repositories;
using HauShop.Model.Models;
using System.Collections.Generic;

namespace HauShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();

        IEnumerable<Slide> GetSlide();

        SystemConfig GetSystemConfig(string code);
    }

    public class CommonService : ICommonService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;
        private ISlideRepository _slideRepository;
        private ISystemConfigRepository _systemConfigRepository;

        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository, ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            this._footerRepository = footerRepository;
            this._slideRepository = slideRepository;
            this._systemConfigRepository = systemConfigRepository;
            this._unitOfWork = unitOfWork;
        }

        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }

        public IEnumerable<Slide> GetSlide()
        {
            return _slideRepository.GetMulti(x => x.Status == true);
        }

        public SystemConfig GetSystemConfig(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code);
        }
    }
}