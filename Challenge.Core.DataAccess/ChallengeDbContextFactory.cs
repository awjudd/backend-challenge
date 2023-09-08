using Microsoft.EntityFrameworkCore.Design;

namespace Challenge.Core.DataAccess;

public class ChallengeDbContextFactory: IDesignTimeDbContextFactory<ChallengeDbContext>
{
    public ChallengeDbContext CreateDbContext(string[] args)
    {
        return new ChallengeDbContext("Data Source=MigrationDb.db");
    }
}