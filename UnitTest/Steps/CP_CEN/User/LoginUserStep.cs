using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Output.Account;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services;
using FunnySailAPI.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
    class LoginUserStep
    {
        private ScenarioContext _scenarioContext;
        private UserManager<ApplicationUser> _userManager;
        private IAuthRefreshTokenCEN _authRefreshTokenCEN;
        private IAccountService _accountService;
        private IOptions<AppSettings> _appSettings;
        private LoginUserInputDTO _loginUserInput;
        private AuthenticateResponseDTO _authenticationResponse;

        public LoginUserStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            AppSettings appSettings = new AppSettings();
            _appSettings = Options.Create(appSettings);

            var applicationDbContextFake = new ApplicationDbContextFake();

            //Construyendo databaseFactory
            IDatabaseTransactionFactory databaseTransactionFactory = new DatabaseTransactionFactory
                (applicationDbContextFake._dbContextFake);


            var userManagerMock = new UserManagerMock();
            userManagerMock.SetupForFindByEmail();

            _userManager = userManagerMock.userManager.Object;

            var authRefreshTokenMock = new AuthRefreshTokenCENMock(_appSettings);
            authRefreshTokenMock.SetupForGenerateToken();
            _authRefreshTokenCEN = authRefreshTokenMock.authRefreshToken.Object;
        }

        [Given(@"el usuario escribe una contraseña incorrecta")]
        public void GivenElUsuarioEscribeUnaContrasenaIncorrecta()
        {
            _loginUserInput = new LoginUserInputDTO
            {
                Email = "jbdfk",
                Password = "ddsfsdfsd"
            };

            var signanagerMock = new SignInManagerMock();
            signanagerMock.SetupForLoginPassFailed();

            _accountService = new AccountService(signanagerMock.singInManager.Object,_userManager,_appSettings,
                _authRefreshTokenCEN);
        }

        [When(@"se intenta loguear el usuario")]
        public async Task WhenSeIntentaLoguearElUsuario()
        {
            try
            {
                _authenticationResponse = await _accountService.LoginUser(_loginUserInput, "127.0.0.1");
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex", ex);
            }
        }

        [Then(@"debe devolver una excepción")]
        public void ThenDebeDevolverUnaExcepcion()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Ex");
            
            Assert.IsNotNull(ex);
        }

        [Given(@"el usuario escribe los datos correctamente")]
        public void GivenElUsuarioEscribeLosDatosCorrectamente()
        {
            _loginUserInput = new LoginUserInputDTO
            {
                Email = "jbdfk",
                Password = "ddsfsdfsd"
            };

            var signanagerMock = new SignInManagerMock();
            signanagerMock.SetupForLoginPassSuccess();

            _accountService = new AccountService(signanagerMock.singInManager.Object, _userManager, _appSettings,
                _authRefreshTokenCEN);
        }

        [Then(@"debe loguearse correctamente")]
        public void ThenDebeLoguearseCorrectamente()
        {
            Assert.IsNotNull(_authenticationResponse);
        }

    }
}
