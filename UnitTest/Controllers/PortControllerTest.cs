using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Port;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.ApplicationCore.Services;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Controllers;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.FakeFactories;

namespace UnitTest.Controllers
{
    [TestClass]
    public class PortControllerTest
    {
        private PortController _PortController;
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private PortFilters _PortFilters;
        public PortControllerTest() {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _PortController = new PortController(_UnitOfWork);
        }

        [TestMethod]
        public void GetPorts_ShouldReturnAllPorts() {
            _PortFilters = new PortFilters{
                Location = "c/Río Tajo"
            };
            var ports = _PortController.GetPorts(_PortFilters, new Pagination());
            Assert.IsNotNull(ports);
            Assert.AreEqual(ports.Result.Value.Total, 5);
        }

        [TestMethod]
        public void GetPort_ShouldReturnOnePort()
        {
            _PortFilters = new PortFilters
            {
                Id = 1
            };
            var ports = _PortController.GetPorts(_PortFilters, new Pagination());
            Assert.IsNotNull(ports);
            Assert.AreEqual(ports.Result.Value.Total, 1);
        }

        [TestMethod]
        public void CreatePort_ShouldReturnPortCreated()
        {
            var newPort = new AddPortInputDTO
            {
                Name = "El puerto feroz",
                Location = "La tempestad"
            };
            var port = _PortController.CreatePort(newPort);
            Assert.IsNotNull(port);
            Assert.IsNotNull(port.Id);
        }

        [TestMethod]
        public void CreatePort_ShouldReturnError422()
        {
            var newPort = new AddPortInputDTO
            {
                Name = "El puerto feroz"
            };
            var port = _PortController.CreatePort(newPort);
            Assert.IsNotNull(port);
        }

        [TestMethod]
        public void UpdatePort_ShouldReturnNoContent()
        {
            var editPort = new UpdatePortDTO
            {
                Id = 2,
                Name = "El puerto feroz",
                Location = "La calle"
            };
            var port = _PortController.EditPort(editPort);
            Assert.IsNotNull(port);
        }

        [TestMethod]
        public void DeletePort_ShouldReturnNoContent()
        {
            var port = _PortController.DeletePort(1);
            Assert.IsNotNull(port);
        }
    }
}
