using Candidates.DataAccess.Repository;
using System;
using System.Configuration;
using System.Linq;
using Xunit;

namespace Candidates.Tests
{
    public class CandidateRepositoryTests
    {
        [Fact]
        public void TestMethod1()
        {
            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            var repository = new CandidateRepository(cs);
            Assert.Throws(typeof(NullReferenceException), ()=> { repository.Save(null); });
        }
    }
}
