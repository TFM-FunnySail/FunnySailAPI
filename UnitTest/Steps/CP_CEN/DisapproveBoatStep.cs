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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    public class DisapproveBoatStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatCP _boatCP;
        private BoatEN _boatUpdated;
        private ReviewEN _lastReview;
        private int _id;
        private string _adminId;
        private string _observ;

        public DisapproveBoatStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();

            //Construyendo databaseFactory
            IDatabaseTransactionFactory databaseTransactionFactory = new DatabaseTransactionFactory
                (applicationDbContextFake._dbContextFake);

            //Construyendo los CAD necesarios
            IBoatCAD boatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            IReviewCAD reviewCAD = new ReviewCAD(applicationDbContextFake._dbContextFake);
            IUserCAD userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);

            //Construyendo los CEN necesarios
            IBoatCEN boatCEN = new BoatCEN(boatCAD);
            IReviewCEN reviewCEN = new ReviewCEN(reviewCAD);
            IUserCEN userCEN = new UserCEN(userCAD, null,null, databaseTransactionFactory);


            _boatCP = new BoatCP(boatCEN, null, null, null, null, null, databaseTransactionFactory,
                reviewCEN, userCEN);
        }

        [Given(@"que se quiere desaprobar el barco de id (.*), el admin de id (.*)")]
        public void GivenSeQuiereDesaprobarElBarcoDeId(int id,int adminId)
        {
            _id = id;
            _adminId = adminId.ToString();
            _observ = "";
        }

        [When(@"se desaprueba el barco")]
        public async Task WhenSeDesapruebaElBarco()
        {
            try
            {
                _boatUpdated = await _boatCP.DisapproveBoat(new DisapproveBoatInputDTO
                {
                    AdminId = _adminId,
                    BoatId = _id,
                    Observation = _observ
                });
                _lastReview = _boatUpdated.reviews.LastOrDefault();
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_Boat", ex);
            }
        }

        [Then(@"no desaprueba el barco y devuelve un error porque el barco no existe")]
        public void ThenNoDesapruebaElBarcoYDevuelveUnErrorPorqueElBarcoNoExiste()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_Boat");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
            Assert.AreEqual("Boat", ex.Message);
        }

        [Given(@"que se quiere desaprobar el barco de id (.*), el admin de id (.*), sin observación")]
        public void GivenQueSeQuiereDesaprobarElBarcoDeIdElAdminDeIdSinObservacion(int id, int adminId)
        {
            _id = id;
            _adminId = adminId.ToString();
            _observ = null;
        }

        [Then(@"devuelve un error diciendo que la observación es requerida")]
        public void ThenDevuelveUnErrorDiciendoQueLaObservacionEsRequerida()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_Boat");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
            Assert.AreEqual("Observation", ex.Message);
        }


        [Then(@"devuelve un error diciendo que el admin no existe")]
        public void ThenDevuelveUnErrorDiciendoQueElAdminNoExiste()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_Boat");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
            Assert.AreEqual("Admin", ex.Message);
        }

        [Given(@"que se quiere desaprobar el barco de id (.*), el admin de id (.*) y la observación (.*)")]
        public void GivenQueSeQuiereDesaprobarElBarcoDeIdElAdminDeIdYLaObservacion(string id, string adminId, string observ)
        {
            _id = int.Parse(id);
            _adminId = adminId;
            _observ = observ;
        }

        [Then(@"devuelve el barco desaprobado")]
        public void ThenDevuelveElBarcoDesaprobado()
        {
            Assert.AreEqual(_boatUpdated.Active, false);
            Assert.AreEqual(_boatUpdated.PendingToReview, false);
            
            
            Assert.IsNotNull(_lastReview);
            Assert.AreEqual(_lastReview.AdminId, _adminId);
            Assert.AreEqual(_lastReview.Description, _observ);
        }

    }
}
