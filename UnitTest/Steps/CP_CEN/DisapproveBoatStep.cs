using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CP.FunnySail;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN
{
    public class DisapproveBoatStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatCP _boatCP;
        private BoatEN _boatUpdated;
        private int _id;

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
            IUserCEN userCEN = new UserCEN(userCAD,null, databaseTransactionFactory);


            _boatCP = new BoatCP(boatCEN,null,null,null,null,null, databaseTransactionFactory,
                reviewCEN, userCEN);
        }


    }
}
