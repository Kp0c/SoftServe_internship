﻿using System;
using System.Data;
using System.Data.Common;

namespace DatabaseConnectionAdminTests
{
    class MockDbCommand : DbCommand
    {
        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        public override int CommandTimeout { get; set; }
        public override CommandType CommandType { get; set; }
        public override UpdateRowSource UpdatedRowSource { get; set; }
        protected override DbConnection DbConnection { get; set; }
        protected override DbParameterCollection DbParameterCollection
        {
            get { throw new NotImplementedException(); }
        }

        protected override DbTransaction DbTransaction { get; set; }
        public override bool DesignTimeVisible { get; set; }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }
        public override string CommandText { get; set; }

        public string ExecutedCommandText { get; private set; }

        public override int ExecuteNonQuery()
        {
            ExecutedCommandText = CommandText;

            return 0;
        }
    }
}
