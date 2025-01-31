namespace PetIdServer.AppHost;

public static class Constants
{
    public const string PostgresServer = "PetIdPostgresServer";
    public const string PostgresDatabase = "PetIdPostgresDb";
    public const string PostgresDatabaseName = "pet-id";
    public const string PostgresVolumeName = "petidserver_pet-id_pgdata";
    public const int PostgresPort = 5432;

    public const string MigrationRunnerName = "PetIdMigrationRunner";

    public const string AppName = "PetIdApp";
}
