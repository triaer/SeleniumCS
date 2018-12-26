﻿using System;
using System.Diagnostics;
using System.IO;
using KiewitTeamBinder.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using KiewitTeamBinder.Common.Helper;
using System.Collections.Generic;

namespace KiewitTeamBinder.Api.Tests
{
    [TestClass]
    public abstract class ApiTestBase
    {
        private string excelUserSourcePath = string.Empty;
        private string localTempExcelUserTargetPath = string.Empty;
        protected string environment = string.Empty;
        protected string captureLocation = "c:\\temp\\testresults\\";
        protected List<KeyValuePair<string, bool>> validations = new List<KeyValuePair<string, bool>>();
        protected List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
        public TestContext TestContext { get; set; }
        protected string teamBinderVersion;

        [TestInitialize]
        public void TestInitialize()
        {            
            if (TestContext.Properties.Contains("teamBinderVersion"))
            {
                teamBinderVersion = TestContext.Properties["teamBinderVersion"].ToString();
            }

            if (TestContext.Properties.Contains("excelUserSourcePath"))
            {
                excelUserSourcePath = TestContext.Properties["excelUserSourcePath"].ToString();
            }

            if (TestContext.Properties.Contains("localTempExcelUserTargetPath"))
            {
                localTempExcelUserTargetPath = TestContext.Properties["localTempExcelUserTargetPath"].ToString();
            }
           
            if (!Directory.Exists(captureLocation))
            {
                Directory.CreateDirectory(captureLocation);
            }

            if (TestContext.Properties.Contains("environment"))
            {
                environment = TestContext.Properties["environment"].ToString();
            }
        }
        
        protected TestAccount GetTestAccount(string role, string environment, string type, string tbUserRole = "AdminAccount1")
        {
            string sourceFilePath = excelUserSourcePath; // @"C:\working\DT_CommonDataKiewitTeamBinder.xls";
            string localTempFilePath = localTempExcelUserTargetPath;
            var user = TestAccountAccess.GetTestAccount(role, environment, type, tbUserRole, sourceFilePath, localTempFilePath);

            return user;
        }
                
        [TestCleanup]
        public void TestCleanup()
        {

        }
    }
}