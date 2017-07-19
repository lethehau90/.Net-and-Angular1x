using HauShop.Data.Infrastructure;
using HauShop.Data.Repositories;
using HauShop.Model.Models;
using System;
using System.Collections.Generic;

namespace HauShop.Service
{
    public interface IFooterService
    {
        Footer Add(Footer footer);

        void Update(Footer footer);

        void Delete(string id);

        IEnumerable<Footer> GetAll();

        Footer GetById(string id);

        void Save();
    }

    public class FooterService : IFooterService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;

        public FooterService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Footer Add(Footer footer)
        {
            return _footerRepository.Add(footer);
        }

        public void Delete(string id)
        {
            _footerRepository.DeleteMulti(x => x.ID == id);
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll();
        }

        public Footer GetById(string id)
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Footer footer)
        {
            _footerRepository.Update(footer);
        }
    }
}