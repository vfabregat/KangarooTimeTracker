using System;
using System.Collections.Generic;
using Kangaroo.Infrastructure;
using Kangaroo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using MongoDB.Bson;
using Kangaroo.Infrastructure.Commands;

namespace Kangaroo.Tests.Infrastructure.Commands
{

    public class CreateOrUpdateTimeSheetHandlerTest
    {
        [TestClass]

        public class Given_a_time_entries_when_new_time_entries_and_time_entries_to_update_are_passed_then_the_result_is_ok : BDDTest
        {
            ISession mockSession;
            TimeEntry timeEntryToUpdate = new TimeEntry()
            {
                Id = ObjectId.GenerateNewId(),
                Date = DateTime.Now,
                Project = "project 1",
                Quantity = 4
            };

            protected override void Given()
            {
                mockSession = new MockSession();
            }

            protected override void When()
            {
                mockSession.Add(timeEntryToUpdate);
            }

            [TestMethod]
            public void UpdateAndInsertItems()
            {
                var list = new List<TimeEntry>();
                var timeEntryToAdd = new TimeEntry()
            {
                Id = ObjectId.GenerateNewId(),
                Date = DateTime.Now,
                Project = "project 2",
                Quantity = 8
            };

                timeEntryToUpdate.Quantity = 8;
                list.Add(timeEntryToUpdate);
                list.Add(timeEntryToAdd);

                var command = new CreateOrUpdateTimeSheet(list, "FakeUser");
                var commandHandler = new CreateOrUpdateTimeSheetHandler(mockSession);
                commandHandler.Execute(command);

                Assert.AreEqual(2, ((MockSession)mockSession).collection.Count);
                Assert.AreEqual(8, mockSession.GetQueryable<TimeEntry>().Single(c => c.Id == timeEntryToUpdate.Id).Quantity);
                Assert.AreEqual(8, mockSession.GetQueryable<TimeEntry>().Single(c => c.Id == timeEntryToAdd.Id).Quantity);
                Assert.AreEqual("project 2", mockSession.GetQueryable<TimeEntry>().Single(c => c.Id == timeEntryToAdd.Id).Project);

                Assert.AreEqual(timeEntryToUpdate.Date, mockSession.GetQueryable<TimeEntry>().Single(c => c.Id == timeEntryToUpdate.Id).Date);
                Assert.AreEqual(timeEntryToAdd.Date, mockSession.GetQueryable<TimeEntry>().Single(c => c.Id == timeEntryToAdd.Id).Date);
            }

            [TestMethod]
            public void UpdateItemWithQuantityZero()
            {
                var list = new List<TimeEntry>();

                timeEntryToUpdate.Quantity = 0;
                list.Add(timeEntryToUpdate);

                var command = new CreateOrUpdateTimeSheet(list, "FakeUser");
                var commandHandler = new CreateOrUpdateTimeSheetHandler(mockSession);
                commandHandler.Execute(command);

                Assert.AreEqual(1, ((MockSession)mockSession).collection.Count);
                Assert.AreEqual(0, mockSession.GetQueryable<TimeEntry>().Single(c => c.Id == timeEntryToUpdate.Id).Quantity);
            }

            [TestMethod]
            public void UpdateItemWithQuantityNull()
            {
                var list = new List<TimeEntry>();

                timeEntryToUpdate.Quantity = null;
                list.Add(timeEntryToUpdate);

                var command = new CreateOrUpdateTimeSheet(list, "FakeUser");
                var commandHandler = new CreateOrUpdateTimeSheetHandler(mockSession);
                commandHandler.Execute(command);

                Assert.AreEqual(1, ((MockSession)mockSession).collection.Count);
                Assert.AreEqual(null, mockSession.GetQueryable<TimeEntry>().Single(c => c.Id == timeEntryToUpdate.Id).Quantity);
            }
        }
    }
}
