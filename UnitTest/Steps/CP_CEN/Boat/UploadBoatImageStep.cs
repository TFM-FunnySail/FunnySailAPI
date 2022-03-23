using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.Boat
{
    [Binding]
    public class UploadBoatImageStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatCP _boatCP;
        private IResourcesCAD _resourceCAD;
        private IBoatResourceCAD _boatResourceCAD;
        private BoatResourceEN _boatResourceDeleted;
        private ResourcesEN _resourceDeleted;
        private int _idResource;
        private int _idBoat;
        private ApplicationUser _user;
        private IList<string> _roles;

        public UploadBoatImageStep(ScenarioContext scenarioContext)
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
            IMooringCAD mooringCAD = new MooringCAD(applicationDbContextFake._dbContextFake);
            _resourceCAD = new ResourcesCAD(applicationDbContextFake._dbContextFake);
            _boatResourceCAD = new BoatResourceCAD(applicationDbContextFake._dbContextFake);

            //Construyendo los CEN necesarios
            IBoatCEN boatCEN = new BoatCEN(boatCAD);
            IReviewCEN reviewCEN = new ReviewCEN(reviewCAD);
            IMooringCEN mooringCEN = new MooringCEN(mooringCAD);
            IUserCEN userCEN = new UserCEN(userCAD, null, null);
            IResourcesCEN resourcesCEN = new ResourcesCEN(_resourceCAD, null);
            IBoatResourceCEN boatResourceCEN = new BoatResourceCEN(_boatResourceCAD);

            _boatCP = new BoatCP(boatCEN, null, null, boatResourceCEN, null, null, databaseTransactionFactory,
                reviewCEN, userCEN, mooringCEN, resourcesCEN);
        }

        [Given(@"se tiene una imagen de tipo jit")]
        public void GivenSeTieneUnaImagenDeTipoJit()
        {
            _scenarioContext.Pending();
        }

        [When(@"se invoca la función para publicar imagen")]
        public void WhenSeInvocaLaFuncionParaPublicarImagen()
        {
            _scenarioContext.Pending();
        }

        [Then(@"da un error porque la extencion no es válida")]
        public void ThenDaUnErrorPorqueLaExtencionNoEsValida()
        {
            _scenarioContext.Pending();
        }

        [Given(@"se subira una imagen a un barco que no existe")]
        public void GivenSeSubiraUnaImagenAUnBarcoQueNoExiste()
        {
            _scenarioContext.Pending();
        }

        [Then(@"da un error de tipo NotFound")]
        public void ThenDaUnErrorDeTipoNotFound()
        {
            _scenarioContext.Pending();
        }

        [Given(@"se subira una imagen correcta a un barco existente")]
        public void GivenSeSubiraUnaImagenCorrectaAUnBarcoExistente()
        {
            _scenarioContext.Pending();
        }

        [Then(@"la imagen ha sido agregada en base de datos")]
        public void ThenLaImagenHaSidoAgregadaEnBaseDeDatos()
        {
            _scenarioContext.Pending();
        }

    }
}
