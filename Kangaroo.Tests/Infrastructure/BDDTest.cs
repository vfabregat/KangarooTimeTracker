using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kangaroo.Tests.Infrastructure
{
    public class BDDTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Given();
            When();
        }

        protected virtual void Given()
        {
        }
        protected virtual void When()
        {

        }

    }
}
