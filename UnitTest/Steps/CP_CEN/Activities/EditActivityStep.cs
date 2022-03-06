using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Activity;

namespace UnitTest.Steps.CP_CEN.Activities
{
    [Binding]
    public class EditActivityStep
    {
        private ScenarioContext _scenarioContext;
        private IActivityCEN _activityCEN;
        private IActivityCAD _activityCAD;
        private IActivityBookingCAD _activityBookingCAD;
        private ActivityEN _activityUpdated;
        private string _name;
        private int _id;

        public EditActivityStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _activityCAD = new ActivityCAD(applicationDbContextFake._dbContextFake);
            _activityBookingCAD = new ActivityBookingCAD(applicationDbContextFake._dbContextFake);
            _activityCEN = new ActivityCEN(_activityCAD);

        }

        [Given(@"se pretende editar el (.*) de la actividad con (.*)")]
        public void GivenSePretendeEditarElDeLaActividadCon(string name, int id)
        {
            _name = name;
            _id = id;
        }
        
        [Given(@"se pretende editar la actividad con (.*) dejando el nombre vacio")]
        public void GivenSePretendeEditarLaActividadConDejandoElNombreVacio(int id)
        {
            _id = id;
            _name = null;
        }
        
        [When(@"se edita la actividad")]
        public async Task WhenSeEditaLaActividadAsync()
        {
            try
            {
                _activityUpdated = await _activityCEN.EditActivity(new UpdateAcitivityDTO
                {
                    Id = _id,
                    Name = _name
                });
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullName", ex);
            }
        }
        
        [Then(@"devuelve la actividad editada en base de datos con los valores actualizados")]
        public void ThenDevuelveLaActividadEditadaEnBaseDeDatosConLosValoresActualizados()
        {
            Assert.AreEqual(_activityUpdated.Name, _name);
        }
        
        [Then(@"devuelve un error porque el nombre de la actividad es requerido")]
        public void ThenDevuelveUnErrorPorqueElNombreDeLaActividadEsRequerido()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullName");
            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }
    }
}
