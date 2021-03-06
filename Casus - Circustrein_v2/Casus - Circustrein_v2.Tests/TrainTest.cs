// <copyright file="TrainTest.cs">Copyright ©  2019</copyright>
using System;
using System.Collections.Generic;
using Casus___Circustrein_v2;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Casus___Circustrein_v2.Tests
{
    /// <summary>This class contains parameterized unit tests for Train</summary>
    [PexClass(typeof(Train))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class TrainTest
    {
        /// <summary>Test stub for FillWagon(List`1&lt;Animal&gt;)</summary>
        [PexMethod]
        public void FillWagonTest([PexAssumeUnderTest]Train target, List<Animal> allAnimals)
        {
            target.FillWagon(allAnimals);
            // TODO: add assertions to method TrainTest.FillWagonTest(Train, List`1<Animal>)
        }

        [PexMethod]
        public List<Animal> SortAnimals([PexAssumeUnderTest]Train target, List<Animal> unorderedAnimals)
        {
            List<Animal> result = target.SortAnimals(unorderedAnimals);
            return result;
            // TODO: add assertions to method TrainTest.SortAnimals(Train, List`1<Animal>)
        }
    }
}
