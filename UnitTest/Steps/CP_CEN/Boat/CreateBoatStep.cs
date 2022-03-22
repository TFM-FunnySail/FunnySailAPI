using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    class CreateBoatStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatCEN _boatCEN;
        private IBoatCAD _boatCAD;
        private BoatEN _boatEN;
        private int _id;

        public CreateBoatStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            _boatCEN = new BoatCEN(_boatCAD);

        }

        [Given(@"se indican el tipo de barco y el id del amarre correctos")]
        public void GivenSeIndicanElTipoDeBarcoYElIdDelAmarreCorrectos()
        {
            _boatEN = new BoatEN {
                Active = false, 
                PendingToReview = true, 
                MooringId = 1, 
                CreatedDate = DateTime.Now,
                BoatTypeId = 1
            };
        }

        [When(@"se aceptan los datos como correctos")]
        public async void WhenSeAceptanLosDatosComoCorrectos()
        {
            try
            {
                _id = await _boatCEN.CreateBoat(_boatEN);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se crea la entidad barco correspondiente")]
        public void ThenSeCreaLaEntidadBarcoCorrespondiente()
        {
            Assert.AreEqual(3, _id);
        }

    }
}
