﻿using elp.Extensions;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace elpExtsTests
{
    [TestClass]
    public class CSVReaderTest
    {
        private class TestClass
        {
            public int[] array {get;set;}

            public TestClass()
            {
                array = new int[5];
            }

            public TestClass(int[] array)
            {
                this.array = array;
            }
        }

        private class MethodTestClass
        {
            #region Constructors
            public MethodTestClass() { }
            public MethodTestClass(string s, int i)
            {
                this.propertyS = s;
                this.propertyI = i;
            }
            #endregion
            #region Properties
            public string propertyS { get; private set; }
            public int propertyI { get; private set; }  
            #endregion

            #region Methods
            public void SetString(string value)
            {
                this.propertyS = value;
            }

            public void SetInt(int value)
            {
                this.propertyI = value;
            }
            #endregion


        }

        [TestMethod]
        public void TestArrays()
        {
            List<TestClass> list = new List<TestClass>();
            CSVReader csv = new CSVReader(@"Res\csvReader-Array.csv", list);
            csv.hasHeader = false;
            csv.AddColumnArray("array", 0, 0);
            csv.AddColumnArray("array", 1, 1);
            csv.AddColumnArray("array", 2, 2);
            csv.AddColumnArray("array", 3, 3);
            csv.AddColumnArray("array", 4, 4);
            list = (List<TestClass>)csv.finalList;

            List<TestClass> expList = new List<TestClass>();
            expList.Add(new TestClass(new int[] { 0, 1, 2, 3, 4 }));
            expList.Add(new TestClass(new int[] { 10, 20, 30, 40, 50 }));
            expList.Add(new TestClass(new int[] { 51, 52, 53, 54, 55 }));

            Assert.AreEqual(expList.Count, list.Count);
            for (int i = 0; i < expList.Count; i++)
            {
                CollectionAssert.AreEqual(expList[i].array, list[i].array);
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<MethodTestClass> list = new List<MethodTestClass>();
            CSVReader csv = new CSVReader(@"Res\csvReader-Method1.csv", list);
            csv.hasHeader = false;
            csv.AddColumnMethod1("SetString", 0);
            csv.AddColumnMethod1("SetInt", 1);
            list = (List<MethodTestClass>)csv.finalList;

            List<MethodTestClass> expList = new List<MethodTestClass>();
            expList.Add(new MethodTestClass("abc", 123));
            expList.Add(new MethodTestClass("def", 456));
            expList.Add(new MethodTestClass("ghi", 789));

            Assert.AreEqual(expList.Count, list.Count);
            for (int i = 0; i < expList.Count; i++)
            {
                Assert.AreEqual(expList[i].propertyI, list[i].propertyI);
                Assert.AreEqual(expList[i].propertyS, list[i].propertyS);
            }
        }
    }
}
