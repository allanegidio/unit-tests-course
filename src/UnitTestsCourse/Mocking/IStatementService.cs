using System;

namespace TestNinja.Mocking
{
    public interface IStatementService
    {
        string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }
}