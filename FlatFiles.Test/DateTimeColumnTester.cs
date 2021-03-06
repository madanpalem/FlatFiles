﻿using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFiles.Test
{

    /// <summary>
    /// Tests the DateTimeColumn class.
    /// </summary>
    [TestClass]
    public class DateTimeColumnTester
    {
        public DateTimeColumnTester()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        /// <summary>
        /// An exception should be thrown if name is blank.
        /// </summary>
        [TestMethod]
        public void TestCtor_NameBlank_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => new DateTimeColumn("    "));
        }

        /// <summary>
        /// If someone tries to pass a name that contains leading or trailing whitespace, it will be trimmed.
        /// The name will also be made lower case.
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsName_Trimmed()
        {
            DateTimeColumn column = new DateTimeColumn(" Name   ");
            Assert.AreEqual("Name", column.ColumnName);
        }

        /// <summary>
        /// If no format string is provided, a generic parse will be attempted.
        /// </summary>
        [TestMethod]
        public void TestParse_NoFormatString_ParsesGenerically()
        {
            DateTimeColumn column = new DateTimeColumn("created");
            DateTime actual = (DateTime)column.Parse(null, "1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// If no format string is provided, a generic parse will be attempted.
        /// </summary>
        [TestMethod]
        public void TestParse_FormatProvider_NoFormatString_ParsesGenerically()
        {
            DateTimeColumn column = new DateTimeColumn("created")
            {
                FormatProvider = CultureInfo.CurrentCulture
            };
            DateTime actual = (DateTime)column.Parse(null, "1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// If no format string is provided, an exact parse will be attempted.
        /// </summary>
        [TestMethod]
        public void TestParse_FormatString_ParsesExactly()
        {
            DateTimeColumn column = new DateTimeColumn("created")
            {
                InputFormat = "d"
            };
            DateTime actual = (DateTime)column.Parse(null, "1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// If no format string is provided, an exact parse will be attempted.
        /// </summary>
        [TestMethod]
        public void TestParse_FormatProvider_FormatString_ParsesExactly()
        {
            DateTimeColumn column = new DateTimeColumn("created")
            {
                InputFormat = "d",
                FormatProvider = CultureInfo.CurrentCulture
            };
            DateTime actual = (DateTime)column.Parse(null, "1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// If the value is blank and the field is not required, null will be returned.
        /// </summary>
        [TestMethod]
        public void TestParse_ValueBlank_NullReturned()
        {
            DateTimeColumn column = new DateTimeColumn("created");
            DateTime? actual = (DateTime?)column.Parse(null, "    ");
            DateTime? expected = null;
            Assert.AreEqual(expected, actual);
        }
    }
}
