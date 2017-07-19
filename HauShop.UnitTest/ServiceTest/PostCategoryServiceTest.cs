using HauShop.Data.Infrastructure;
using HauShop.Data.Repositories;
using HauShop.Model.Models;
using HauShop.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace HauShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mocUnitOfWork;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCategory;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mocUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object, _mocUnitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory() { ID =1, Name = "DM1", Status = true },
                new PostCategory() { ID =2, Name = "DM2", Status = true },
                new PostCategory() { ID =3, Name = "DM3", Status = true }
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //set method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);

            //call action
            var result = _categoryService.GetAll() as List<PostCategory>;

            //comepare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory category = new PostCategory();
            category.Name = "Test";
            category.Alias = "test";
            category.Status = true;

            _mockRepository.Setup(m => m.Add(category)).Returns((PostCategory p) =>
              {
                  p.ID = 1;
                  return p;
              });

            var result = _categoryService.Add(category);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
