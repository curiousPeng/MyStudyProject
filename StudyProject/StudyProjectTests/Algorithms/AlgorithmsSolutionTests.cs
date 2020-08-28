using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyProject.Algorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Algorithms.Tests
{
    [TestClass()]
    public class AlgorithmsSolutionTests
    {
        [TestMethod()]
        public void TwoSumTest()
        {
            int[] data = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var result = AlgorithmsSolution.TwoSum(data, 5);
            Assert.AreEqual(result[0], 0);
            Assert.AreEqual(result[1], 3);

            result = AlgorithmsSolution.TwoSum(data, 20);

            Assert.IsNull(result);

            result = AlgorithmsSolution.TwoSum(data, 19);
            Assert.AreEqual(result[0], 8);
            Assert.AreEqual(result[1], 9);
        }
    }
}