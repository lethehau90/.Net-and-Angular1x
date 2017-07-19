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
    public interface IFeedbackService
    {
        Feedback Add(Feedback feedback);

        void Update(Feedback feedback);

        void Delete(int id);

        IEnumerable<Feedback> GetAll();

        Feedback GetById(int id);

        void Save();
    }
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public Feedback Add(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public void Delete(int id)
        {
            _feedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll();
        }

        public Feedback GetById(int id)
        {
            return _feedbackRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
        }
    }
}
