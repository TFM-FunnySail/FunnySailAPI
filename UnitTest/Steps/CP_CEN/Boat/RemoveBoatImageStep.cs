using FunnySailAPI.ApplicationCore.Constants;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    class RemoveBoatImageStep
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

        public RemoveBoatImageStep(ScenarioContext scenarioContext)
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

        [Given(@"se pasan el id de la imagen (.*)")]
        public void GivenSePasanElIdDeLaImagen(int id)
        {
            _idBoat = 1;
            _idResource = id;
            _roles = new List<string> { UserRolesConstant.ADMIN };
            _user = new ApplicationUser { Id = "1" };
        }

        [When(@"se invoca la función para eliminar la imagen")]
        public async Task WhenSeInvocaLaFuncionParaEliminarLaImagen()
        {
            try
            {
                await _boatCP.RemoveImage(_idBoat,_idResource,_user,_roles);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex", ex);
            }
        }

        [Then(@"lanza una excepcion NotFound")]
        public void ThenLanzaUnaExcepcionNotFound()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

        [Given(@"se pasan los identificadores correctos")]
        public void GivenSePasanLosIdentificadoresCorrectos()
        {
            _idBoat = 1;
            _idResource = 2;
            _user = new ApplicationUser { Id = "1" };
            _roles = new List<string> { UserRolesConstant.BOAT_OWNER };
        }

        [When(@"se comprueban los datos eliminados")]
        public async Task WhenSeCompruebanLosDatosEliminados()
        {
            _resourceDeleted = await _resourceCAD.FindById(_idResource);
            var query = _boatResourceCAD.GetIQueryable();
            query = query.Where(x => x.BoatId == _idBoat && _idResource == x.ResourceId);
            _boatResourceDeleted = (await _boatResourceCAD.Get(query)).FirstOrDefault();
        }


        [Then(@"se comprueba que los datos fueron eliminados")]
        public void ThenSeEliminanLosDatosDeLaImagenDeLaImagenEnLaBD()
        {
            Assert.IsNull(_resourceDeleted);
            Assert.IsNull(_boatResourceDeleted);
        }

        [Given(@"un usuario no propietario del barco ni admin quiere eliminar la imagen")]
        public void GivenUnUsuarioNoPropietarioDelBarcoNiAdminQuiereEliminarLaImagen()
        {
            _idBoat = 1;
            _idResource = 2;
            _user = new ApplicationUser { Id = "2" };
            _roles = new List<string> { UserRolesConstant.BOAT_OWNER };
        }

        [Then(@"lanza una excepcion Forbidden")]
        public void ThenLanzaUnaExcepcionForbidden()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.Forbidden, ex.ExceptionType);
        }

    }
}
