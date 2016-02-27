using CR.Application.Abstractions.Models;
using CR.Application.Abstractions.Services;
using CR.Application.Workers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CR.Application.Test.Workers
{
    [TestFixture]
    public class ConfirmationReportWorkerTests
    {
        private IConfirmationReportCommandService command;
        private IConfirmationReportQueryService query;

        [SetUp]
        public void CreateStubs()
        {
            command = Substitute.For<IConfirmationReportCommandService>();
            query = Substitute.For<IConfirmationReportQueryService>();
        }

        [TearDown]
        public void DistroyStubs()
        {
            command = null;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SaveDraft__with_Report_null__Throw_ArgumentNullException()
        {
            // Arrange
            ConfirmationReportViewModel model = null;
            command.SaveDraft(model).Returns(model);

            //Act
            var worker = new ConfirmationReportWorker(command, query);
            await worker.SaveDraft(model);
        }

        [Test]
        public async Task SaveDraft__with_Report_not_null__call_worker()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { };
            command.SaveDraft(model).Returns(model);

            // Act
            var worker = new ConfirmationReportWorker(command, query);
            var actual = await worker.SaveDraft(model);

            // Assert
            Assert.That(actual, Is.SameAs(model));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Save__with_Report_null__Throw_ArgumentNullException()
        {
            // Arrange
            ConfirmationReportViewModel model = null;
            command.Save(model).Returns(model);

            // Act
            var worker = new ConfirmationReportWorker(command, query);
            await worker.Save(model);
        }

        [Test]
        public async Task Save__with_Report_not_null__call_worker()
        {
            // Arrange
            ConfirmationReportViewModel model = new ConfirmationReportViewModel { };
            command.Save(model).Returns(model);

            // Act
            var worker = new ConfirmationReportWorker(command, query);
            var actual = await worker.Save(model);

            // Assert
            Assert.That(actual, Is.SameAs(model));
        }

        [TestCase(0, ExpectedException = typeof(ArgumentException))]
        [TestCase(-1, ExpectedException = typeof(ArgumentException))]
        public async Task FindByNumber__with_reportNumber_lesser_or_equal_0__Throw_ArgumentException(int reportNumber)
        {
            //Act
            var worker = new ConfirmationReportWorker(command, query);
            await worker.FindByNumber(reportNumber);
        }

        [Test]
        public async Task FindByNumber__with_reportNumber_greater_than_0__call_service()
        {
            // Arrange
            int reportNumber = 3;
            var expected = new ConfirmationReportViewModel { };
            query.FindByNumber(reportNumber).Returns(expected);

            //Act
            var worker = new ConfirmationReportWorker(command, query);
            var actual = await worker.FindByNumber(reportNumber);

            // Assert
            Assert.That(actual, Is.SameAs(expected));
        }

        [TestCase(0, ExpectedException = typeof(ArgumentException))]
        [TestCase(-1, ExpectedException = typeof(ArgumentException))]
        public async Task FindById__with_reportNumber_lesser_or_equal_0__Throw_ArgumentException(int reportNumber)
        {
            //Act
            var worker = new ConfirmationReportWorker(command, query);
            await worker.FindById(reportNumber);
        }

        [Test]
        public async Task FindById__with_reportNumber_greater_than_0__call_service()
        {
            // Arrange
            int reportNumber = 3;
            var expected = new ConfirmationReportViewModel { };
            query.FindById(reportNumber).Returns(expected);

            //Act
            var worker = new ConfirmationReportWorker(command, query);
            var actual = await worker.FindById(reportNumber);

            // Assert
            Assert.That(actual, Is.SameAs(expected));
        }

        [Test]
        public async Task FindNewReportNumber__call_query_service()
        {
            // Arrange
            var expected = 5;
            query.FindNewReportNumber().Returns(expected);

            //Act
            var worker = new ConfirmationReportWorker(command, query);
            var actual = await worker.FindNewReportNumber();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("", null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(null, null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase("", ReportStatus.Draft, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(null, ReportStatus.Draft, ExpectedException = typeof(ArgumentNullException))]
        [TestCase("", ReportStatus.Completed, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(null, ReportStatus.Completed, ExpectedException = typeof(ArgumentNullException))]
        public async Task FindAllByOwner__with_ownerName_null_or_empty__Throw_ArgumentNullException(string ownerName, ReportStatus? status)
        {
            //Act
            var worker = new ConfirmationReportWorker(command, query);
            await worker.FindAllByOwner(ownerName, status);
        }

        [TestCase("wilver", null)]
        [TestCase("wilver", ReportStatus.Completed)]
        [TestCase("wilver", ReportStatus.Draft)]
        public async Task FindAllByOwner__with_ownerName_not_null__return_List_if_ConfirmationReport(string ownerName, ReportStatus? status)
        {
            // Arrange
            var expected = new List<ConfirmationReportViewModel>() { };
            query.FindAllByOwner(ownerName, status).Returns(expected);

            //Act
            var worker = new ConfirmationReportWorker(command, query);
            var actual = await worker.FindAllByOwner(ownerName, status);

            // Assert
            Assert.That(actual, Is.SameAs(expected));
        }
    }

}
