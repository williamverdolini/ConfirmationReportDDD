using CR.Application.Abstractions.Models;
using CR.Domain.Persistence.EF.Models;
using CR.Domain.Persistence.EF.Services;
using CR.Infrastructure.Db;
using CR.Infrastructure.Mappings;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace CR.Domain.Persistence.EF.Test.Services
{
    class ConfirmationReportQueryServiceTests
    {
        private IMapper mapper;
        private IDatabase<ConfirmationReport> db;

        [SetUp]
        public void CreateStubs()
        {
            mapper = Substitute.For<IMapper>();
            db = Substitute.For<IDatabase<ConfirmationReport>>();
        }

        [TearDown]
        public void DistroyStubs()
        {
            mapper = null;
            db = null;
        }

        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase("", ExpectedException = typeof(ArgumentNullException))]
        public async void FindAllByOwner__If_owner_name_is_null_or_empty__Throw_ArgumentNullException(string ownerName)
        {
            // Arrange
            ReportStatus? status = null;
            var service = new ConfirmationReportQueryService(db, mapper);

            // Act
            var actual = await service.FindAllByOwner(ownerName, status);
        }

        [Test]
        public async void FindAllByOwner__If_Reports_DbSet_is_empty__Return_empty_list()
        {
            // Arrange
            var data = new List<ConfirmationReport> { };
            var mappedData = new List<ConfirmationReportViewModel> {};

            // Create a DbSet substitute.
            var set = Substitute.For<DbSet<ConfirmationReport>, IQueryable<ConfirmationReport>, IDbAsyncEnumerable<ConfirmationReport>>()
                                .SetupData(data);
            db.DbSet.Returns(set);
            db.DbSet.AsNoTracking().Returns(set);
            ReportStatus? status = null;
            // mapping
            mapper.Map<List<ConfirmationReportViewModel>>(Arg.Any<List<ConfirmationReport>>()).Returns(mappedData);
            var service = new ConfirmationReportQueryService(db, mapper);

            // Act
            var actual = await service.FindAllByOwner("wilver", status);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.Count(), Is.EqualTo(0));
        }

        [TestCase("wilver", null, ExpectedResult = 0)]
        [TestCase("matraf", null, ExpectedResult = 3)]
        [TestCase("giofae", null, ExpectedResult = 1)]
        [TestCase("matraf", ReportStatus.Draft, ExpectedResult = 2)]
        [TestCase("matraf", ReportStatus.Completed, ExpectedResult = 1)]
        [TestCase("giofae", ReportStatus.Draft, ExpectedResult = 1)]
        [TestCase("giofae", ReportStatus.Completed, ExpectedResult = 0)]
        public async Task<int> FindAllByOwner__If_Reports_contains_owner_items__Return_item_list(string owner, ReportStatus? status)
        {
            // Arrange
            var data = new List<ConfirmationReport> {
                new ConfirmationReport { Id = 1, OwnerName = "matraf", Status=Model.ReportStatus.Draft },
                new ConfirmationReport { Id = 2, OwnerName = "matraf", Status=Model.ReportStatus.Draft },
                new ConfirmationReport { Id = 3, OwnerName = "matraf", Status=Model.ReportStatus.Completed },
                new ConfirmationReport { Id = 4, OwnerName = "giofae", Status=Model.ReportStatus.Draft } };

            // Create a DbSet substitute.
            var set = Substitute.For<DbSet<ConfirmationReport>, IQueryable<ConfirmationReport>, IDbAsyncEnumerable<ConfirmationReport>>()
                                .SetupData(data);
            db.DbSet.Returns(set);
            db.DbSet.AsNoTracking().Returns(set);
            // mapping not relevant for test
            List<ConfirmationReport> result = null;
            mapper.Map<List<ConfirmationReportViewModel>>(Arg.Do<List<ConfirmationReport>>(l => result = l));
            var service = new ConfirmationReportQueryService(db, mapper);

            // Act
            var actual = await service.FindAllByOwner(owner, status);

            // Assert
            return result.Count();
        }

        [TestCase(0, ExpectedException = typeof(ArgumentException))]
        [TestCase(-1, ExpectedException = typeof(ArgumentException))]
        public async void FindById__If_id_is_lesser_or_equal_than_zero__Throw_ArgumentException(int id)
        {
            // Arrange
            var service = new ConfirmationReportQueryService(db, mapper);

            // Act
            var actual = await service.FindById(id);
        }

        [TestCase(1, ExpectedResult = "Customer 1")]
        [TestCase(3, ExpectedResult = "Customer 3")]
        public async Task<string> FindById__if_Ids_match__Return_item(int id)
        {
            // Arrange
            var data = new List<ConfirmationReport> {
                new ConfirmationReport { Id = 1, CustomerName="Customer 1"},
                new ConfirmationReport { Id = 2, CustomerName="Customer 2" },
                new ConfirmationReport { Id = 3, CustomerName="Customer 3" },
                new ConfirmationReport { Id = 4, CustomerName="Customer 4" } };

            // Create a DbSet substitute.
            var set = Substitute.For<DbSet<ConfirmationReport>, IQueryable<ConfirmationReport>, IDbAsyncEnumerable<ConfirmationReport>>()
                                .SetupData(data);
            db.DbSet.Returns(set);
            // mapping not relevant for test
            ConfirmationReport result = null;
            mapper.Map<ConfirmationReportViewModel>(Arg.Do<ConfirmationReport>(l => result = l));
            var service = new ConfirmationReportQueryService(db, mapper);

            // Act
            var actual = await service.FindById(id);

            // Assert
            return result.CustomerName;
        }

        [Test]
        public async void FindById__if_Ids_do_not_match__Return_null()
        {
            // Arrange
            var data = new List<ConfirmationReport> {
                new ConfirmationReport { Id = 1, CustomerName="Customer 1"},
                new ConfirmationReport { Id = 2, CustomerName="Customer 2" },
                new ConfirmationReport { Id = 3, CustomerName="Customer 3" },
                new ConfirmationReport { Id = 4, CustomerName="Customer 4" } };

            // Create a DbSet substitute.
            var set = Substitute.For<DbSet<ConfirmationReport>, IQueryable<ConfirmationReport>, IDbAsyncEnumerable<ConfirmationReport>>()
                                .SetupData(data);
            db.DbSet.Returns(set);
            // mapping not relevant for test
            ConfirmationReport result = null;
            mapper.Map<ConfirmationReportViewModel>(Arg.Do<ConfirmationReport>(l => result = l));
            var service = new ConfirmationReportQueryService(db, mapper);

            // Act
            var actual = await service.FindById(5);

            // Assert
            Assert.IsNull(result);
        }
    }
}
