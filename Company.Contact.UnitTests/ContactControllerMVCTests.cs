using Company.Contact.WebApp.Controllers;
using NUnit.Framework;
using Moq;
using Company.Contact.WebApp.HttpUtility;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Company.Contacts.DomainEntities;
using Company.Contact.WebApp.Models;

namespace Tests
{
    [TestFixture]
    public class ContactCustomerMVCTests
    {
        private Mock<IHttpUtilities<ContactsController>> _mockHttpUtilities;
        private Mock<IMapper> _mockMapper;
        private ContactsController _sut;

        [SetUp]
        public void Setup()
        {
            _mockHttpUtilities = new Mock<IHttpUtilities<ContactsController>>();
            _mockMapper = new Mock<IMapper>();
            _sut = new ContactsController(_mockHttpUtilities.Object, _mockMapper.Object);
        }

        [Test]
        public void Index_Action_Retuns_ContactResult()
        {
            // Arrange
            var contacts = new List<Contact>()
                        { new Contact { Id = 1,FirstName = "Vishal", LastName = "Yadav",
                                            Email = "vishalyadav21@gmail.com", IsDeleted = false,
                                            PhoneNumber="9028224682",Status=1 }
                        };
            _mockHttpUtilities.Setup(x => x.Get()).Returns(contacts);

            _mockMapper.Setup(m => m.Map<List<ContactModel>>(It.IsAny<List<Contact>>()))
                        .Returns(new List<ContactModel>(){ new ContactModel() {
                            ID = 1,
                            FirstName = "Vishal",
                            LastName = "Yadav",
                            Email = "vishalyadav21@gmail.com",
                            IsDeleted = false,
                            PhoneNumber = "9028224682",
                            Status = 1
                        } });

            // Act
            var result = _sut.Index();
            var actResult = _sut.Index() as ViewResult;
            var ContactModel = (List<ContactModel>)actResult.ViewData.Model;

            // Assert
            Assert.IsNotNull(actResult);
            Assert.IsNotNull(actResult.Model);
            Assert.AreEqual(ContactModel[0].Email, "vishalyadav21@gmail.com");
            Assert.AreEqual(ContactModel.Count, 1);
        }

        [Test]
        public void Create_Action_CreateViewReturned()
        {
            // Arrange
            string expectedView = "Create";

            // Act
            var actual = _sut.Create() as ViewResult;

            // Assert
            Assert.AreEqual(expectedView, actual.ViewName);            
        }

        [Test]
        public void Delete_Given_IdGetsDeleted_ReturnsTrue()
        {
            // Arrange
            int id = 1;
            _mockHttpUtilities.Setup(x => x.DeleteRequest(It.IsAny<string>(), It.IsAny<int>())).Returns(true);

            // Act            
            var actual = _sut.Delete(id) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actual.ActionName);            
        }

        [Test]
        public void Delete_Given_IdCouldNotDeleted_ReturnsFalse()
        {
            // Arrange
            int id = 1;
            _mockHttpUtilities.Setup(x => x.DeleteRequest(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

            // Act            
            var actual = _sut.Delete(id) as ViewResult;

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [Test]
        public void Delete_Action_InvalidID_RedirectsToIndex()
        {
            // Arrange
            int id = -1;
            _mockHttpUtilities.Setup(x => x.DeleteRequest(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

            // Act            
            var actual = _sut.Delete(id) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actual.ActionName);
        }

        [Test]
        public void Edit_Action_Returns_SingleContact()
        {
            // Arrange
            int id = 1;
            _mockMapper.Setup(m => m.Map<ContactModel>(It.IsAny<Contact>()))
                .Returns(new ContactModel() {
                            ID = 1,
                            FirstName = "Vishal",
                            LastName = "Yadav",
                            Email = "vishalyadav21@gmail.com",
                            IsDeleted = false,
                            PhoneNumber = "9028224682",
                            Status = 1
                        });

            var contacts = new Contact { Id = 1,FirstName = "Vishal", LastName = "Yadav",
                                            Email = "vishalyadav21@gmail.com", IsDeleted = false,
                                            PhoneNumber="9028224682",Status=1 
                        };
            _mockHttpUtilities.Setup(x => x.Get(It.IsAny<string>())).Returns(contacts);

            // Act            
            var actual = _sut.Edit(id) as ViewResult;
            var model = (ContactModel)actual.Model;
            // Assert
            Assert.AreEqual(model.ID, id);
        }

        [Test]
        public void EditPost_Action_Returns_True()
        {
            // Arrange
            _mockMapper.Setup(m => m.Map<ContactModel>(It.IsAny<Contact>()))
                .Returns(new ContactModel()
                {
                    ID = 1,
                    FirstName = "Vishal",
                    LastName = "Yadav",
                    Email = "vishalyadav21@gmail.com",
                    IsDeleted = false,
                    PhoneNumber = "9028224682",
                    Status = 1
                });

            var contact = new ContactModel
            {
                ID = 1,
                FirstName = "Vishal",
                LastName = "Yadav",
                Email = "vishalyadav21@gmail.com",
                IsDeleted = false,
                PhoneNumber = "9028224682",
                Status = 1
            };
            _mockHttpUtilities.Setup(x => x.PutRequest(It.IsAny<string>(),It.IsAny<Contact>())).Returns(true);

            // Act            
            var actual = _sut.Edit(contact) as RedirectToActionResult;
            
            // Assert
            Assert.AreEqual("Index",actual.ActionName);
        }

        [Test]
        public void EditPost_Action_Returns_FalseIfCouldNotUpdate()
        {
            // Arrange
            _mockMapper.Setup(m => m.Map<ContactModel>(It.IsAny<Contact>()))
                .Returns(new ContactModel()
                {
                    ID = 1,
                    FirstName = "Vishal",
                    LastName = "Yadav",
                    Email = "vishalyadav21@gmail.com",
                    IsDeleted = false,
                    PhoneNumber = "9028224682",
                    Status = 1
                });

            var contact = new ContactModel
            {
                ID = 1,
                FirstName = "Vishal",
                LastName = "Yadav",
                Email = "vishalyadav21@gmail.com",
                IsDeleted = false,
                PhoneNumber = "9028224682",
                Status = 1
            };
            _mockHttpUtilities.Setup(x => x.PutRequest(It.IsAny<string>(), It.IsAny<Contact>())).Returns(false);

            // Act            
            var actual = _sut.Edit(contact) as ViewResult;

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [Test]
        public void CreatePost_Action_Returns_True()
        {
            // Arrange
            _mockMapper.Setup(m => m.Map<ContactModel>(It.IsAny<Contact>()))
                .Returns(new ContactModel()
                {
                    ID = 1,
                    FirstName = "Vishal",
                    LastName = "Yadav",
                    Email = "vishalyadav21@gmail.com",
                    IsDeleted = false,
                    PhoneNumber = "9028224682",
                    Status = 1
                });

            var contact = new ContactModel
            {
                ID = 1,
                FirstName = "Vishal",
                LastName = "Yadav",
                Email = "vishalyadav21@gmail.com",
                IsDeleted = false,
                PhoneNumber = "9028224682",
                Status = 1
            };
            _mockHttpUtilities.Setup(x => x.PostRequest(It.IsAny<string>(), It.IsAny<Contact>())).Returns(true);

            // Act            
            var actual = _sut.Create(contact) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actual.ActionName);
        }

        [Test]
        public void CreatePost_Action_Returns_FalseIfCouldNotUpdate()
        {
            // Arrange
            _mockMapper.Setup(m => m.Map<ContactModel>(It.IsAny<Contact>()))
                .Returns(new ContactModel()
                {
                    ID = 1,
                    FirstName = "Vishal",
                    LastName = "Yadav",
                    Email = "vishalyadav21@gmail.com",
                    IsDeleted = false,
                    PhoneNumber = "9028224682",
                    Status = 1
                });

            var contact = new ContactModel
            {
                ID = 1,
                FirstName = "Vishal",
                LastName = "Yadav",
                Email = "vishalyadav21@gmail.com",
                IsDeleted = false,
                PhoneNumber = "9028224682",
                Status = 1
            };
            _mockHttpUtilities.Setup(x => x.PostRequest(It.IsAny<string>(), It.IsAny<Contact>())).Returns(false);

            // Act            
            var actual = _sut.Create(contact) as ViewResult;

            // Assert
            Assert.AreEqual("Error", actual.ViewName);            
        }
    }
}