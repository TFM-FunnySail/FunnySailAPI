using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.User
{
    [Binding]
    class CreateUserStep
    {
        private ScenarioContext _scenarioContext;
        private IUserCEN _userCEN;
        private IUserCP _userCP;
        private UsersEN _userCreated;
        private ApplicationUser _applicationUserCreated;
        private string _email;
        private string _name;
        private string _lastName;
        private bool _promotion;

        public CreateUserStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();

            //Construyendo databaseFactory
            IDatabaseTransactionFactory databaseTransactionFactory = new DatabaseTransactionFactory
                (applicationDbContextFake._dbContextFake);

            //Construyendo los CAD necesarios
            IUserCAD userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);

            //Construyendo los CEN necesarios
            _userCEN = new UserCEN(userCAD, null, null);

            var userManagerMock = new UserManagerMock();
            userManagerMock.SetupForCreateUser();

            _userCP = new UserCP(_userCEN, null, userManagerMock.userManager.Object, databaseTransactionFactory);
        }

        [Given(@"un usuario con datos (.*),(.*),(.*),(.*)")]
        public void GivenUnUsuarioConDatos(string email, string name, string lastName, string promotion)
        {
            _email = email;
            _lastName = lastName;
            _name = name;
            _promotion = promotion == "si";
        }

        [When(@"se invoca la función de registro")]
        public async Task WhenSeInvocaLaFuncionDeRegistro()
        {
            (var result, var user) = await _userCP.CreateUser(new AddUserInputDTO
            {
                Email = _email,
                FirstName = _name,
                LastName = _lastName,
                Password = "kghdsfvhsdf****asdsad",
                PhoneNumber = "76767",
                ReceivePromotion = _promotion
            });
            _applicationUserCreated = user;
            var query = _userCEN.GetUserCAD().GetIQueryable();
            _userCreated = query.FirstOrDefault(x => x.UserId == user.Id);
        }

        [Then(@"se crea un usuario con los mismos datos")]
        public void ThenSeCreaUnUsuarioConLosMismosDatos()
        {
            Assert.AreEqual(_email,_applicationUserCreated.Email);
            Assert.AreEqual(_name, _userCreated.FirstName);
            Assert.AreEqual(_lastName, _userCreated.LastName);
            Assert.AreEqual(_promotion, _userCreated.ReceivePromotion);
            Assert.IsFalse(_userCreated.BoatOwner);
        }

    }
}
