using System;
using Xunit;
using LongTalkDemo.Tables;
using LongTalkDemo.Other;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class UnitTest1
    {
        private Database _db = new Database();

        [Fact]
        public void Test1()
        {
            var (flaskApp, injector) = new FlaskAppBuilder().Build();

            var typeName = nameof(Assay); 
            Base tableInstance = flaskApp.TestDb(typeName);

            Assert.Equal("1", tableInstance.GetId());
        }

        [Fact]
        public void Test2()
        {
            var typeName = "staff";
            var entityId = 1;
            string staffName = TestDb(typeName, entityId);

            Assert.Equal("John Smith", staffName);
        }

        // Using explicit types rather than inference for demonstration.
        public string TestDb(string table, int entityId)
        {
            ScopedSession session = _db.GetSession();

            if (table == "assay")
            {
                IEnumerable<Assay> result = session.Query<Assay>();
                Assay assay = result.First(assay => assay.AssayId == entityId);
                return assay.AssayId.ToString();
            }

            if (table == "staff")
            {
                IEnumerable<Staff> result = session.Query<Staff>();
                Staff staff = result.First(assay => assay.StaffId == entityId);
                return staff.FirstName;
            }

            throw new ArgumentException(nameof(table));
        }
    }
}
