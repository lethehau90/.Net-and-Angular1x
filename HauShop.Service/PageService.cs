using HauShop.Data.Infrastructure;
using HauShop.Data.Repositories;
using HauShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HauShop.Service
{
    public interface IPageService
    {
        Page GetByAlias(string alias);

        IEnumerable<Page> GetAll();

        Page GetByID(int id);

        Page Delete(int id);

        Page Add(Page page);

        void Update(Page page);

        void Save();
    }

    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public Page Add(Page page)
        {
            return _pageRepository.Add(page);
        }

        public Page Delete(int id)
        {
            return _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll().OrderBy(x => x.Ord);
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }

        public Page GetByID(int id)
        {
            return _pageRepository.GetSingleById(id);
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}