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
    class EditUserStep
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
        public EditUserStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);
            _userCEN = new UserCEN(_userCAD, _signInManager, _userManager, _databaseTransactionFactory);
        }

        [Given(@"dos objetos usuarios correctos")]
        public void GivenDosObjetosUsuariosCorrectos()
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

        [When(@"se quiera actualizar los datos")]
        public async void WhenSeQuieraActualizarLosDatos()
        {
            try
            {
                _identityResult = await _userCEN.EditUser(_applicationUser, _addUserInputDTO);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se sustituirán los datos antiguos por los nuevos")]
        public void ThenSeSustituiranLosDatosAntiguosPorLosNuevos()
        {
            Assert.IsNotNull(_identityResult);
        }

        [Given(@"los datos del usuario pasados de forma incorrecta")]
        public void GivenLosDatosDelUsuarioPasadosDeFormaIncorrecta()
        {
            _addUserInputDTO = null;
            _applicationUser = new ApplicationUser();
        }

        [When(@"se proceda a la actualización de los datos")]
        public async void WhenSeProcedaALaActualizacionDeLosDatos()
        {
            try
            {
                _identityResult = await _userCEN.EditUser(_applicationUser, _addUserInputDTO);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se devolverá un error en los datos introducidos para el usuario")]
        public void ThenSeDevolveraUnErrorEnLosDatosIntroducidosParaElUsuario()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.IsNull(_identityResult);
        }

    }
}
