﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Jlw.ModularContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using TRepo = Jlw.ModularContent.LocalizedContentFieldRepository;

namespace Jlw.Data.LocalizedContent.Tests
{
    [TestClass]
    public class LocalizedContentFieldRepositoryTestFixture : BaseModelFixture<TRepo, LocalizedContentFieldRepositoryTestSchema>
    {
        [TestMethod]
        public void SqlParams_Should_Match_For_GetRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new ModularContentField(new { Id = 1 });
            var output = new ModularContentField(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);
            TRepo sut = new TRepo(mockClient.Object, "");
            var o = sut.GetRecord(input);

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));

            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@id"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@groupfilter"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@groupkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fieldkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fieldtype"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fielddata"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fieldclass"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@parentkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@defaultlabel"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@wrapperclass"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@wrapperhtmlstart"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@wrapperhtmlend"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@order"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@auditchangeby"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Count() == 2)));

            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Match_For_GetAllRecords()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            //var input = new TModel(new { Id = 1 });
            IEnumerable<ModularContentField> output = new []{ new ModularContentField(new { GroupKey = "testGroup" }) };
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordList<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);
            TRepo sut = new TRepo(mockClient.Object, "");
            var o = sut.GetAllRecords();

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));
            mockClient.Verify(m => m.GetRecordList<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@groupKey"))));
            mockClient.Verify(m => m.GetRecordList<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fieldKey"))));
            mockClient.Verify(m => m.GetRecordList<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@language"))));
            mockClient.Verify(m => m.GetRecordList<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@text"))));
            mockClient.Verify(m => m.GetRecordList<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@auditchangeby"))));
            mockClient.Verify(m => m.GetRecordList<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => !def.Parameters.Any())));
            
            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Match_For_SaveRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new ModularContentField(new { Id = 1 });
            var output = new ModularContentField(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            var o = sut.SaveRecord(input);

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));

            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@id"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@groupfilter"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@groupkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@fieldkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@fieldtype"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@fielddata"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@fieldclass"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@parentkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@defaultlabel"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@wrapperclass"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@wrapperhtmlstart"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@wrapperhtmlend"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@order"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@auditchangeby"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Count() == 14)));

            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Match_For_DeleteRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new ModularContentField(new { Id = 1 });
            var output = new ModularContentField(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            var o = sut.DeleteRecord(input);

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));

            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@id"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@groupfilter"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@groupkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fieldkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fieldtype"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fielddata"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@fieldclass"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@parentkey"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@defaultlabel"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@wrapperclass"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@wrapperhtmlstart"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@wrapperhtmlend"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@order"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@auditchangeby"))));
            mockClient.Verify(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Count() == 3)));

            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Match_For_GetDataTableList()
        {
            var mockClient = new Mock<IModularDbClient>();
            var paramList = new MockDataParameterCollection();
            var input = JsonConvert.DeserializeObject<LocalizedContentFieldDataTablesInput>(@"{draw: 1,columns: [ {data: 'Id'} ], order: [{column:0, dir: 'asc'}],start: 0,length: -1,search: {value: 'some text', regex: false} }");
            input.FieldType = DataUtility.GenerateRandom<string>();
            input.FieldKey = DataUtility.GenerateRandom<string>();
            input.ParentKey = DataUtility.GenerateRandom<string>();
            input.GroupKey = DataUtility.GenerateRandom<string>();
            input.GroupFilter = DataUtility.GenerateRandom<string>();
            input.Language = DataUtility.GenerateRandom<string>();
            var output = new DataTablesOutput(input);
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(new DbConnectionStringBuilder());
            mockClient.Setup(m => m.GetConnection(It.IsAny<string>())).Returns((string connString)=>
            {
                var dbConnection = new Mock<IDbConnection>();
                dbConnection.Setup(m => m.CreateCommand()).Returns(() =>
                {
                    var dbCommand = new Mock<IDbCommand>();
                    dbCommand.Setup(m => m.Parameters).Returns(paramList);
                    dbCommand.Setup(m => m.ExecuteReader()).Returns(() =>
                    {
                        return new Mock<IDataReader>().Object;
                    });
                    return dbCommand.Object;
                });
                return dbConnection.Object;
            });
            mockClient.Setup(m => m.GetCommand(It.IsAny<string>(), It.IsAny<IDbConnection>())).Returns((string cmd, IDbConnection dbConnection)=>
            {
                return dbConnection.CreateCommand();
            });
            TRepo sut = new TRepo(mockClient.Object, "");

            var o = sut.GetDataTableList(input);
            var order = input.order?.FirstOrDefault();
            var colName = input.columns?.ToList().ElementAt(order?.column ?? 0).data;

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));
            mockClient.Verify(m => m.GetConnection(It.IsAny<string>()));
            mockClient.Verify(m => m.GetCommand(It.IsAny<string>(), It.IsAny<IDbConnection>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "@sSearch"), It.Is<string>(s=>s=="%" + input.search.value + "%"), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "@nRowStart"), It.Is<int>(n=>n==input.start), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "@nPageSize"), It.Is<int>(n=>n==input.length), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sSortDir"), It.Is<string>(s => s == order.dir ), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sSortCol"), It.Is<string>(s=>s==colName), It.IsAny<IDbCommand>()));
            
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sFieldType"), It.Is<string>(s => s == input.FieldType), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sFieldKey"), It.Is<string>(s => s == input.FieldKey), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sParentKey"), It.Is<string>(s => s == input.ParentKey), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sGroupKey"), It.Is<string>(s => s == input.GroupKey), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sGroupFilter"), It.Is<string>(s => s == input.GroupFilter), It.IsAny<IDbCommand>()));
            mockClient.Verify(m => m.AddParameterWithValue(It.Is<string>(s => s == "sLanguage"), It.Is<string>(s => s == input.Language), It.IsAny<IDbCommand>()));
            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Throw_NotImplementedException_For_UpdateRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new ModularContentField(new { Id = 1 });
            var output = new ModularContentField(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            Assert.ThrowsException<NotImplementedException>(() => sut.UpdateRecord(input));
        }

        [TestMethod]
        public void SqlParams_Should_Throw_NotImplementedException_For_InsertRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new ModularContentField(new { Id = 1 });
            var output = new ModularContentField(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<ModularContentField>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            Assert.ThrowsException<NotImplementedException>(() => sut.InsertRecord(input));
        }


    }
}
