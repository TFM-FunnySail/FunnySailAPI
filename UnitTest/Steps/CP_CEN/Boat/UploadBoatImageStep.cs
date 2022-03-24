using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private BoatResourceEN _boatResource;
        private ResourcesEN _resource;
        private int _idResource;
        private int _idBoat;
        private IFormFile _formFile;

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
            IBoatResourceCEN boatResourceCEN = new BoatResourceCEN(_boatResourceCAD);
            var mockResourceCEN = new Mock<ResourcesCEN>(_resourceCAD, null)
                .As<IResourcesCEN>();
            mockResourceCEN.CallBase = true;
            _formFile = new FormFile(null, 0, 0, "35", "35.jpg");
            mockResourceCEN.Setup(x => x.UploadImage(_formFile)).ReturnsAsync(_formFile.FileName);
            
            IResourcesCEN resourcesCEN = mockResourceCEN.Object;

            _boatCP = new BoatCP(boatCEN, null, null, boatResourceCEN, null, null, databaseTransactionFactory,
                reviewCEN, userCEN, mooringCEN, resourcesCEN);
        }

        [Given(@"se tiene una imagen de tipo jit")]
        public void GivenSeTieneUnaImagenDeTipoJit()
        {
            _idBoat = 1;
            _formFile = new FormFile(null, 0, 0, "35", "35.jit");
        }

        [When(@"se invoca la función para publicar imagen")]
        public async Task WhenSeInvocaLaFuncionParaPublicarImagen()
        {
            try
            {
                _idResource = await _boatCP.AddImage(_idBoat, _formFile,true);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex", ex);
            }
        }

        [Then(@"da un error porque la extencion no es válida")]
        public void ThenDaUnErrorPorqueLaExtencionNoEsValida()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex");

            Assert.IsNotNull(ex);
        }

        [Given(@"se subira una imagen a un barco que no existe")]
        public void GivenSeSubiraUnaImagenAUnBarcoQueNoExiste()
        {
            _idBoat = -1;
            
        }

        [Then(@"da un error de tipo NotFound")]
        public void ThenDaUnErrorDeTipoNotFound()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

        [Given(@"se subira una imagen correcta a un barco existente")]
        public void GivenSeSubiraUnaImagenCorrectaAUnBarcoExistente()
        {
            _idBoat = 1;
        }

        [When(@"se extraen los datos resultantes para comprobarlo")]
        public async Task WhenSeExtraemPsDatosResultantesParaComprobarlo()
        {
            _resource = await _resourceCAD.FindById(_idResource);

            var query = _boatResourceCAD.GetIQueryable();
            int idResource = _resource?.Id ?? 0;
            query = query.Where(x => x.BoatId == _idBoat && x.ResourceId == idResource);
            _boatResource = (await _boatResourceCAD.Get(query)).FirstOrDefault();
        }


        [Then(@"la imagen ha sido agregada en base de datos")]
        public void ThenLaImagenHaSidoAgregadaEnBaseDeDatos()
        {
            Assert.IsNotNull(_boatResource);
            Assert.IsNotNull(_resource);
        }

    }
}
