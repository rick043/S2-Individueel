using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Casus___Circustrein_v2;
// <copyright file="TrainTest.SortAnimals.g.cs">Copyright ©  2019</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace Casus___Circustrein_v2.Tests
{
    public partial class TrainTest
    {

[TestMethod]
[PexGeneratedBy(typeof(TrainTest))]
public void SortAnimals991()
{
    Train train;
    List<Animal> list;
    List<Animal> list1;
    train = new Train();
    train.Wagons = (List<Wagon>)null;
    Animal[] animals = new Animal[0];
    list = new List<Animal>((IEnumerable<Animal>)animals);
    list1 = this.SortAnimals(train, list);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)train);
    Assert.IsNull((object)(train.Wagons));
}
    }
}
