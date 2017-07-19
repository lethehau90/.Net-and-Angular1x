using HauShop.Data.Infrastructure;
using HauShop.Data.Repositories;
using HauShop.Model.Models;
using System;
using System.Collections.Generic;

namespace HauShop.Service
{
    public interface IContactDetailService
    {
        ContactDetail Add(ContactDetail footer);

        void Update(ContactDetail footer);

        void Delete(int id);

        IEnumerable<ContactDetail> GetAll();

        ContactDetail GetById(int id);
        ContactDetail GetDefaultContact();

        void Save();
    }
    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ContactDetail Add(ContactDetail footer)
        {
            return _contactDetailRepository.Add(footer);
        }

        public void Delete(int id)
        {
            _contactDetailRepository.Delete(id);
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            return _contactDetailRepository.GetAll();
        }

        public ContactDetail GetById(int id)
        {
            return _contactDetailRepository.GetSingleById(id);
        }

        public ContactDetail GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x=>x.Status);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetail footer)
        {
            _contactDetailRepository.Update(footer);
        }
    }
}
