using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    class CreateUserStep
    {
        private ScenarioContext _scenarioContext;
        private IUserCEN _userCEN;
        private IUserCAD _userCAD;
        private UsersEN _UserEN;
        private int _id;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IDatabaseTransactionFactory _databaseTransactionFactory;
        private ApplicationUser _applicationUser;
        private AddUserInputDTO _addUserInputDTO;
        private IdentityResult _identityResult;
        public CreateUserStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);
            _userCEN = new UserCEN(_userCAD, _signInManager , _userManager, _databaseTransactionFactory);  
        }

        [Given(@"los datos de un nuevo usuario correctos")]
        public void GivenLosDatosDeUnNuevoUsuarioCorrectos()
        {
            _addUserInputDTO = new AddUserInputDTO
            {

                Email = "andresll706@gmail.com",
                FirstName = "Andres",
                Password = "Password",
                ConfirmPassword = "Password",
                UserRole = FunnySailAPI.ApplicationCore.Models.Globals.UserRoleEnum.Client,
                PhoneNumber = "619954039"
            };

            _applicationUser = new ApplicationUser();
        }

        [When(@"se registra el usuario")]
        public async void WhenSeRegistraElUsuario()
        {
            try
            {
                _identityResult =  await _userCEN.CreateUser(_applicationUser, _addUserInputDTO);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se devuelve un mensaje de éxito en el registro")]
        public void ThenSeDevuelveUnMensajeDeExitoEnElRegistro()
        {
            Assert.IsNotNull(_identityResult);

        }

        [Given(@"unos datos incorrectos de registro de usuario")]
        public void GivenUnosDatosIncorrectosDeRegistroDeUsuario()
        {
            _addUserInputDTO = new AddUserInputDTO
            {
                FirstName = "Andres",
                Password = "Password",
                ConfirmPassword = "Password",
                UserRole = FunnySailAPI.ApplicationCore.Models.Globals.UserRoleEnum.Client,
                PhoneNumber = "619954039"
            };
        }

        [When(@"se intenta proceder con el registro")]
        public async void WhenSeIntentaProcederConElRegistro()
        {
            try
            {
                _identityResult = await _userCEN.CreateUser(_applicationUser, _addUserInputDTO);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"no se permite continuar hasta solventar los fallos")]
        public void ThenNoSePermiteContinuarHastaSolventarLosFallos()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
        }

    }
}
