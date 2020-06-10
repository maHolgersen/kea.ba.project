using System;
using KEA.BA.Project.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KRA.BA.UnitTestProject
{
    [TestClass]
    public class CalculatorControllerUnitTest
    {
        [TestMethod]
        public void TestCalculateGroupSizes()
        {
            int expected = 100;
            int storeSize = 400;
            int groupDistance = 2;

            CalculatorController cc = new CalculatorController();
            int actual = cc.CalculateGroupSizes(storeSize, groupDistance);

            Assert.AreEqual(expected, actual, "failed to calculate");
        }
    }
}
