namespace IntegrationTests.Configuration;

internal class SqlContainerConfiguration
{
    public static string Hostname => "localhost";
    public static string Username => "sa";
    public static string Password => "f*kyAd9*&&$?ZK2H";
    public static string DatabaseName => "BingeIntegrationTests";
    public static int Port => 1433;

    public static string ImageName => "mcr.microsoft.com/mssql/server:2022-latest";

    public static IReadOnlyDictionary<string, string>? EnvironmentVariables => new Dictionary<string, string>
    {
        ["ACCEPT_EULA"] = "Y",
        ["MSSQL_SA_PASSWORD"] = Password
    };
}
