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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.User
{
    [Binding]
    class EditUserStep
    {
        private ScenarioContext _scenarioContext;
        private IUserCEN _userCEN;
        private IUserCP _userCP;
        private UsersEN _userCreated;
        private string _id;
        private string _name;
        private string _lastName;
        private bool _promotion;
        private DateTime _birthday;

        public EditUserStep(ScenarioContext scenarioContext)
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
            userManagerMock.SetupForEditUser();

            _userCP = new UserCP(_userCEN, null, userManagerMock.userManager.Object, databaseTransactionFactory);
        }

        [Given(@"dado el usuario (.*), se quiere editar con los valores siguientes (.*),(.*),(.*),(.*)")]
        public void GivenDadoElUsuarioSeQuiereEditarConLosValoresSiguientes(string id, string name, string lastName, string promotion, string birthday)
        {
            _id = id;
            _name = name;
            _lastName = lastName;
            _promotion = promotion == "si";
            _birthday = Convert.ToDateTime(birthday);
        }

        [When(@"se invoca la función para actualizar los datos del usuario")]
        public async Task WhenSeInvocaLaFuncionParaActualizarLosDatosDelUsuario()
        {
            await _userCP.EditUser(new ApplicationUser
            {
                Id = _id
            },new AddUserInputDTO { 
                FirstName = _name,
                LastName = _lastName,
                BirthDay = _birthday,
                ReceivePromotion = _promotion
            });

            _userCreated = await _userCEN.GetUserCAD().FindById(_id);
        }

        [Then(@"se sobrescriben los datos nuevos sobre los viejos")]
        public void ThenSeSobrescribenLosDatosNuevosSobreLosViejos()
        {
            Assert.AreEqual(_name, _userCreated.FirstName);
            Assert.AreEqual(_lastName, _userCreated.LastName);
            Assert.AreEqual(_promotion, _userCreated.ReceivePromotion);
            Assert.AreEqual(_birthday, _userCreated.BirthDay);
        }

    }
}
