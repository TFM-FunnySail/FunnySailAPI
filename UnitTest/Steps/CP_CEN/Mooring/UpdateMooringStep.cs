using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Mooring;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
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
    class UpdateMooringStep
    {

        private ScenarioContext _scenarioContext;
        private IMooringCEN _MooringCEN;
        private IMooringCAD _MooringCAD;
        private MooringEN _MooringEN;
        private UpdateMooringDTO _updateMooringDTO;

        public UpdateMooringStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _MooringCAD = new MooringCAD(applicationDbContextFake._dbContextFake);
            _MooringCEN = new MooringCEN(_MooringCAD);
        }

        [Given(@"un objeto amarre con los datos necesarios")]
        public void GivenUnObjetoAmarreConLosDatosNecesarios()
        {
            _updateMooringDTO = new UpdateMooringDTO
            {
                MooringId = 1,
                Alias = "Alias Modificado",
                PortId = 2,
                Type = MooringEnum.Big
            };
        }

        [When(@"se proceda a actualizar el amarre")]
        public async void WhenSeProcedaAActualizarElAmarre()
        {
            try
            {
                _MooringEN = await _MooringCEN.UpdateMooring(_updateMooringDTO);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se tomará el id del amarre del objeto y se sobreescribirán los datos que se introduzcan")]
        public void ThenSeTomaraElIdDelAmarreDelObjetoYSeSobreescribiranLosDatosQueSeIntroduzcan()
        {
            Assert.AreEqual(_updateMooringDTO.Alias, _MooringEN.Alias);
            Assert.AreEqual(_updateMooringDTO.PortId, _MooringEN.PortId);
            Assert.AreEqual(_updateMooringDTO.Type, _MooringEN.Type);

        }

        [Given(@"un objeto amarre con datos incorrectos")]
        public void GivenUnObjetoAmarreConDatosIncorrectos()
        {
            _updateMooringDTO = new UpdateMooringDTO
            {
                MooringId = 0,
                Alias = "Alias Modificado",
                PortId = 2,
                Type = MooringEnum.Big
            };
        }

        [Then(@"se devolverá un error con los datos del amarre")]
        public void ThenSeDevolveraUnErrorConLosDatosDelAmarre()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Mooring id is required.", ex.EnMessage);
        }

    }
}