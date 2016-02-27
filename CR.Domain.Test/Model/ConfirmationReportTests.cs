using CR.Application.Abstractions.Models;
using CR.Domain.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Domain.Test.Model
{
    [TestFixture]
    public class ConfirmationReportTests
    {
        [Test]
        public void Constructor__new_instance_should_be_with_DRAFT_status()
        {
            // Arrange
            var report = new ConfirmationReport();

            // Assert
            Assert.That(report.Status, Is.EqualTo(Domain.Model.ReportStatus.Draft));

        }

        [Test]
        public void Constructor__new_instance_should_have_empty_initialized_details_list()
        {
            // Arrange
            var report = new ConfirmationReport();

            // Assert
            Assert.That(report.Details, Is.Not.Null);
            Assert.That(report.Details, Is.Empty);
            Assert.That(report.Details.Count(), Is.EqualTo(0));
        }

        // TDD Approach: two details are overlapped if:
        // the Date dat is the same on both details AND
        // A.From <= B.From && A.To >= B.From
        // ||
        // A.From >= B.From && B.To >= A.From
        [TestCaseSource(typeof(CheckOverlappedDetailsTestCaseFactory), "GetTestCases")]
        public bool CheckOverlappedDetails__TestMethod(ConfirmationReportDetail A, ConfirmationReportDetail B)
        {
            // Arrange
            var report = new ConfirmationReport();
            report.AddDetail(A);       
            report.AddDetail(B);

            // Act
            return report.CheckOverlappedDetails();
        }


        [Test]
        public void Save__Report__should_be_marked_as_COMPLETED()
        {
            // Arrange
            var reportView = new ConfirmationReportViewModel { };
            var report = new ConfirmationReport();

            // Act
            report.Save(reportView);

            // Assert
            Assert.That(report.Status, Is.EqualTo(Domain.Model.ReportStatus.Completed));
        }

        [Test]
        [ExpectedException(typeof(OverlappedDetailException))]
        public void Save__overlapped_details__Throw_OverlappedDetailException()
        {
            // Arrange
            var reportView = new ConfirmationReportViewModel
            {
                Details = new List<ConfirmationReportDetailViewModel>
                {
                    new ConfirmationReportDetailViewModel
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    },
                    new ConfirmationReportDetailViewModel
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }
                }
            };

            var report = new ConfirmationReport();
            //Act
            report.Save(reportView);
        }

        [Test]
        public void SaveDraft__Report__should_be_marked_as_DRAFT()
        {
            // Arrange
            var reportView = new ConfirmationReportViewModel { };
            var report = new ConfirmationReport();

            // Act
            report.SaveDraft(reportView);

            // Assert
            Assert.That(report.Status, Is.EqualTo(Domain.Model.ReportStatus.Draft));
        }

        [Test]
        [ExpectedException(typeof(OverlappedDetailException))]
        public void SaveDraft__overlapped_details__Throw_OverlappedDetailException()
        {
            // Arrange
            var reportView = new ConfirmationReportViewModel
            {
                Details = new List<ConfirmationReportDetailViewModel>
                {
                    new ConfirmationReportDetailViewModel
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    },
                    new ConfirmationReportDetailViewModel
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }
                }
            };

            var report = new ConfirmationReport();
            //Act
            report.Save(reportView);
        }

    }
}
