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
    public interface ISlideService
    {
        IEnumerable<Slide> GetAll();
        Slide GetByID(int Id);
        Slide Create(Slide slide);
        void Update(Slide slide);
        Slide Delete(int Id);
        void Save();
    }
    public class SlideService : ISlideService
    {
        UnitOfWork _unitOfWork;
        ISlideRepository _slideRepository;
        public SlideService(UnitOfWork unitOfWork, ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public Slide Create(Slide slide)
        {
            return _slideRepository.Add(slide);
        }

        public Slide Delete(int Id)
        {
            return _slideRepository.Delete(Id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public Slide GetByID(int Id)
        {
            return _slideRepository.GetSingleById(Id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}
