using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Features.CP_CEN.Activities
{
    [Binding]
    public class DeactivateActivityStep
    {
  
        private ScenarioContext _scenarioContext;
        private IActivityCEN _activityCEN;
        private IActivityCAD _activityCAD;
        private ActivityEN _activityUpdated;
        private int _id;

        public DeactivateActivityStep(ScenarioContext scenarioContext)
        {

            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _activityCAD = new ActivityCAD(applicationDbContextFake._dbContextFake);
            _activityCEN = new ActivityCEN(_activityCAD);
        }

        [Given(@"se pide desactivar una actividad con (.*)")]
        public void GivenSePideDesactivarUnaActividadCon(int id)
        {
            _id = id;
        }
        
        [When(@"se desactiva la actividad seleccionada")]
        public async Task WhenSeDesactivaLaActividadSeleccionadaAsync()
        {
            try
            {
                _activityUpdated = await _activityCEN.DeactivateActivity(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }
        
        [Then(@"el resultado debe ser que la actividad con (.*) se encuentra ahora desactivada")]
        public void ThenElResultadoDebeSerQueLaActividadConSeEncuentraAhoraDesactivada(int id)
        {
            Assert.AreEqual(_activityUpdated.Active, false);
        }
    }
}
