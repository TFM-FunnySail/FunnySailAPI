using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    class DeleteMooringStep
    {
        private ScenarioContext _scenarioContext;
        private IMooringCEN _MooringCEN;
        private IMooringCAD _MooringCAD;
        private MooringEN _MooringEN;
        private int _idPort;
        private string _alias;
        private MooringEnum _type;
        private int _id;
        private Task _task;


        public DeleteMooringStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _MooringCAD = new MooringCAD(applicationDbContextFake._dbContextFake);
            _MooringCEN = new MooringCEN(_MooringCAD);
        }

        [Given(@"un punto de amarre que no tiene un barco asignado (.*)")]
        public void GivenUnPuntoDeAmarreQueNoTieneUnBarcoAsignado(int idMooring)
        {
            _id = idMooring;
        }

        [When(@"se intente eliminar el amarre")]
        public async void WhenSeIntenteEliminarElAmarre()
        {
            try
            {
                 await _MooringCEN.DeleteMooring(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se eliminará el amarre con éxito")]
        public void ThenSeEliminaraElAmarreConExito()
        {
            Assert.IsNull(_task);
        }

        [Given(@"un punto de amarre que tiene un barco asignado (.*)")]
        public void GivenUnPuntoDeAmarreQueTieneUnBarcoAsignado(int idMooring)
        {
            _id = idMooring;
        }

        [When(@"se quiera eliminar el amarre")]
        public async void WhenSeQuieraEliminarElAmarre()
        {
            try
            {
                await _MooringCEN.DeleteMooring(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"el sistema devolverá error porque habrá un barco en ese amarre")]
        public void ThenElSistemaDevolveraErrorPorqueHabraUnBarcoEnEseAmarre()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Mooring not found.", ex.EnMessage);
        }

    }
}
