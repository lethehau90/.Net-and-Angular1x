using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HauShop.Data.Repositories;
using HauShop.Data.Infrastructure;
using HauShop.Model.Models;

namespace HauShop.Service
{
    public interface ILogoService
    {
        IEnumerable<Logo> GetAll();
        Logo GetId(int Id);
        IEnumerable<Logo> GetCode(string code);
        Logo Create(Logo logo);
        void Update(Logo logo);
        Logo Delete(int Id);
        void Save();
    }
    public class LogoService : ILogoService
    {
        private IUnitOfWork _unitOfWork;
        private ILogoRepository _logoRepository;
        public LogoService(IUnitOfWork unitOfWork, ILogoRepository logoRepository)
        {
            _unitOfWork = unitOfWork;
            _logoRepository = logoRepository;
        }

        public Logo Create(Logo logo)
        {
            return _logoRepository.Add(logo);
        }

        public Logo Delete(int Id)
        {
            return _logoRepository.Delete(Id);
        }

        public IEnumerable<Logo> GetAll()
        {
            return _logoRepository.GetAll();
        }

        public IEnumerable<Logo> GetCode(string code)
        {
            return _logoRepository.GetMulti(x => x.Code.Equals(code));
        }

        public Logo GetId(int Id)
        {
            return _logoRepository.GetSingleById(Id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Logo logo)
        {
            _logoRepository.Update(logo);
        }
    }
}
