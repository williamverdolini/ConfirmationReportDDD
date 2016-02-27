using CR.Application.Abstractions.Models;
using CR.Domain.Model;
using CR.Domain.Persistence.EF.Services;
using CR.Infrastructure.Mappings;
using CR.Infrastructure.Repo;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CR.Domain.Persistence.EF.Test.Services
{
    [TestFixture]
    class ConfirmationReportCommandServiceTests
    {
        private IMapper mapper;
        private IRepository<ConfirmationReport> repo;

        [SetUp]
        public void CreateStubs()
        {
            mapper = Substitute.For<IMapper>();
            repo = Substitute.For<IRepository<ConfirmationReport>>();
        }

        [TearDown]
        public void DistroyStubs()
        {
            mapper = null;
            repo = null;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SaveDraft__ConfirmationReportViewModel_null__Throw_ArgumentNullException()
        {
            // Arrange
            ConfirmationReportViewModel model = null;
            mapper.Map<ConfirmationReport>(null).Returns(null as ConfirmationReport);
            mapper.Map<ConfirmationReportViewModel>(null).Returns(null as ConfirmationReportViewModel);

            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            var actual = await service.SaveDraft(model);
        }

        [Test]
        public async Task SaveDraft__ConfirmationReportViewModel_with_Id_Zero__Should_not_get_ConfirmationReport_from_Repo()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { };
            ConfirmationReport domainModel = new ConfirmationReport { };
            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            await service.SaveDraft(model);

            // Assert
            await repo.DidNotReceive().GetById(Arg.Any<int>());
        }

        [Test]
        public async Task SaveDraft__ConfirmationReportViewModel_with_Id_not_Zero__Should_get_ConfirmationReport_from_Repo()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { Id = 1};
            ConfirmationReport domainModel = new ConfirmationReport {};
            repo.GetById(model.Id).Returns(domainModel);
            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            await service.SaveDraft(model);

            // Assert
            await repo.Received().GetById(model.Id);
        }

        [Test]
        public async Task SaveDraft__ConfirmationReportViewModel_not_Empty__Should_call_SaveDraft_on_Domain()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { Id = 1 };
            ConfirmationReport domainModel = Substitute.For<ConfirmationReport>();
            repo.GetById(model.Id).Returns(domainModel);
            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            await service.SaveDraft(model);

            // Assert
            domainModel.Received().SaveDraft(model);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Save__ConfirmationReportViewModel_null__Throw_ArgumentNullException()
        {
            // Arrange
            ConfirmationReportViewModel model = null;
            mapper.Map<ConfirmationReport>(null).Returns(null as ConfirmationReport);
            mapper.Map<ConfirmationReportViewModel>(null).Returns(null as ConfirmationReportViewModel);

            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            var actual = await service.Save(model);
        }

        [Test]
        public async Task Save__ConfirmationReportViewModel_with_Id_Zero__Should_not_get_ConfirmationReport_from_Repo()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { };
            ConfirmationReport domainModel = new ConfirmationReport { };
            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            await service.Save(model);

            // Assert
            await repo.DidNotReceive().GetById(Arg.Any<int>());
        }

        [Test]
        public async Task Save__ConfirmationReportViewModel_with_Id_not_Zero__Should_get_ConfirmationReport_from_Repo()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { Id = 1 };
            ConfirmationReport domainModel = new ConfirmationReport { };
            repo.GetById(model.Id).Returns(domainModel);
            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            await service.Save(model);

            // Assert
            await repo.Received().GetById(model.Id);
        }

        [Test]
        public async Task Save__ConfirmationReportViewModel_not_Empty__Should_call_SaveDraft_on_Domain()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { Id = 1 };
            ConfirmationReport domainModel = Substitute.For<ConfirmationReport>();
            repo.GetById(model.Id).Returns(domainModel);
            var service = new ConfirmationReportCommandService(repo, mapper);

            // Act
            await service.Save(model);

            // Assert
            domainModel.Received().Save(model);
        }
    }
}
