using CR.Domain.Model;
using NUnit.Framework;
using System;
using System.Collections;

namespace CR.Domain.Test.Model
{
    class CheckOverlappedDetailsTestCaseFactory
    {
        public static IEnumerable GetTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    },
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 2),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }).Returns(false).SetName("Details_in_different_days__Return_false");

                yield return new TestCaseData(
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 12, 0, 0)
                    },
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 12, 30, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }).Returns(false).SetName("Details_in_same_day_but_not_overlapped_hours__Return_false");

                yield return new TestCaseData(
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 12, 0, 0)
                    },
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 12, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }).Returns(false).SetName("Details_in_same_day_with_consecutive_periods__Return_false");


                yield return new TestCaseData(
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 12, 30, 0)
                    },
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 12, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }).Returns(true).SetName("Details_in_same_day_with_overlapped_periods__Return_true");

                yield return new TestCaseData(
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1, 12,0,0),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 12, 30, 0)
                    },
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1, 18, 0, 0),
                        FromTime = new DateTime(2016, 2, 1, 12, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }).Returns(true).SetName("Details_in_same_day_and_different_daytime_with_overlapped_periods__Return_true");


                yield return new TestCaseData(
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 30, 0)
                    },
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 12, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 0, 0)
                    }).Returns(true).SetName("Details_in_same_day_with_included_periods__Return_true");

                yield return new TestCaseData(
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 12, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 18, 30, 0)
                    },
                    new ConfirmationReportDetail
                    {
                        Date = new DateTime(2016, 2, 1),
                        FromTime = new DateTime(2016, 2, 1, 8, 0, 0),
                        ToTime = new DateTime(2016, 2, 1, 12, 0, 0)
                    }).Returns(false).SetName("Details_in_same_day_with_previous_not_overlapped_periods__Return_false");
            }
        }
    }


}
