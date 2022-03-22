using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.Boat
{
    [Binding]
    class UpdateBoat
    {
        private ScenarioContext _scenarioContext;
        private IBoatCEN _boatCEN;
        private IBoatCAD _boatCAD;
        private IBoatCP _boatCP;
        private IBoatInfoCAD _boatInfoCAD;
        private IBoatInfoCEN _boatInfoCEN;
        private IBoatTypeCAD _boatTypeCAD;
        private IBoatTypeCEN _boatTypeCEN;
        private IBoatResourceCAD _boatResourceCAD;
        private IBoatResourceCEN _boatResourceCEN;
        private IBoatPricesCAD _boatPricesCAD;
        private IBoatPricesCEN _boatPricesCEN;
        private IRequiredBoatTitleCAD _requiredBoatTitlesCAD;
        private IRequiredBoatTitlesCEN _requiredBoatTitlesCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;
        private IReviewCAD _reviewCAD;
        private IReviewCEN _reviewCEN;
        private IUserCAD _userCAD;
        private IUserCEN _userCEN;
        private IMooringCAD _mooringCAD;
        private IMooringCEN _mooringCEN;
        private IResourcesCAD _resourcesCAD;
        private IResourcesCEN _resourcesCEN;
        
        private Task<BoatEN> _boatEN;
        private int _boatId;
        private int _boatTypeId;
        private int _mooringId;

        public UpdateBoat(ScenarioContext scenarioContext, SignInManager<ApplicationUser> signInManager,
                       UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            _boatCEN = new BoatCEN(_boatCAD);
            _boatInfoCAD = new BoatInfoCAD(applicationDbContextFake._dbContextFake);
            _boatInfoCEN = new BoatInfoCEN(_boatInfoCAD);
            _boatTypeCAD = new BoatTypeCAD(applicationDbContextFake._dbContextFake);
            _boatTypeCEN = new BoatTypeCEN(_boatTypeCAD);
            _boatResourceCAD = new BoatResourceCAD(applicationDbContextFake._dbContextFake);
            _boatResourceCEN = new BoatResourceCEN(_boatResourceCAD);
            _boatPricesCAD = new BoatPricesCAD(applicationDbContextFake._dbContextFake);
            _boatPricesCEN = new BoatPricesCEN(_boatPricesCAD);
            _requiredBoatTitlesCAD = new RequiredBoatTitleCAD(applicationDbContextFake._dbContextFake);
            _requiredBoatTitlesCEN = new RequiredBoatTitlesCEN(_requiredBoatTitlesCAD);
            _reviewCAD = new ReviewCAD(applicationDbContextFake._dbContextFake);
            _reviewCEN = new ReviewCEN(_reviewCAD);
            _userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);
            _userCEN = new UserCEN(_userCAD, signInManager, userManager);
            _mooringCAD = new MooringCAD(applicationDbContextFake._dbContextFake);
            _mooringCEN = new MooringCEN(_mooringCAD);
            _resourcesCAD = new ResourcesCAD(applicationDbContextFake._dbContextFake);
            _resourcesCEN = new ResourcesCEN(_resourcesCAD, environment);
            _databaseTransactionFactory = new DatabaseTransactionFactory(applicationDbContextFake._dbContextFake);
            _boatCP = new BoatCP(_boatCEN, _boatInfoCEN, _boatTypeCEN, _boatResourceCEN, _boatPricesCEN, _requiredBoatTitlesCEN,
                       _databaseTransactionFactory,_reviewCEN,_userCEN,_mooringCEN,_resourcesCEN);
        }

        [Given(@"los datos del actualización de la embarcación sin boatType")]
        public void GivenLosDatosDelActualizacionDeLaEmbarcacionSinBoatType()
        {
            _boatId = 1;
            _boatTypeId = -1;
            _mooringId = 1;
        }

        [When(@"se actualizan los datos de la embarcación")]
        public void WhenSeActualizanLosDatosDeLaEmbarcacion()
        {
            try
            {
                _boatEN = _boatCP.UpdateBoat(new UpdateBoatInputDTO
                {
                    BoatId = _boatId,
                    BoatTypeId = _boatTypeId,
                    MooringId = _mooringId,
                    Active = true,
                    PendingToReview = false
                });
            }
            catch (Exception ex)
            {
                _scenarioContext.Add("Exception_NullDesc", ex);
            }
        }

        [Then(@"salta una excepción boat type dont exist")]
        public void ThenSaltaUnaExcepcionBoatTypeDontExist()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullDesc");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

        [Given(@"los datos del actualización de la embarcación sin MooringId")]
        public void GivenLosDatosDelActualizacionDeLaEmbarcacionSinMooringId()
        {
            _boatId = 1;
            _boatTypeId = 1;
            _mooringId = -1;
        }

        [Then(@"salta una excepción MooringId dont exist")]
        public void ThenSaltaUnaExcepcionMooringIdDontExist()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullDesc");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

        [Given(@"los datos del actualización de la embarcación sin BoatID")]
        public void GivenLosDatosDelActualizacionDeLaEmbarcacionSinBoatID()
        {
            _boatId = -1;
            _boatTypeId = 1;
            _mooringId = 1;
        }

        [Then(@"salta una excepción BoatID dont exist")]
        public void ThenSaltaUnaExcepcionBoatIDDontExist()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullDesc");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

        [Given(@"los datos del actualización de la embarcación")]
        public void GivenLosDatosDelActualizacionDeLaEmbarcacion()
        {
            _boatId = 1;
            _boatTypeId = 1;
            _mooringId = 1;
        }

        [Then(@"se actualizan los datos")]
        public void ThenSeActualizanLosDatos()
        {
            Assert.AreEqual(true, _boatEN.Result.Active);
            Assert.AreEqual(false, _boatEN.Result.PendingToReview);
            Assert.AreEqual(_boatId, _boatEN.Result.Id);
            Assert.AreEqual(_boatTypeId, _boatEN.Result.BoatTypeId);
            Assert.AreEqual(_mooringId, _boatEN.Result.MooringId);
        }

    }
}
