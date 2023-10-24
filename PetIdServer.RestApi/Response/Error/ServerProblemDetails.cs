using Microsoft.AspNetCore.Mvc;

namespace PetIdServer.RestApi.Response.Error;

public class ServerProblemDetails : ProblemDetails
{
    /// <summary>Internal API error code.</summary>
    /// <example>CORE.TAG_NOT_FOUND</example>
    public string Code { get; set; } = string.Empty;
    /// <summary>Exception stack trace. Available only on debug API build.</summary>
    /// <example>ConsoleApplication1.MyCustomException: some message .... ---> System.Exception: Oh noes!
    /// at ConsoleApplication1.SomeObject.OtherMethod() in C:\ConsoleApplication1\SomeObject.cs:line 24
    /// at ConsoleApplication1.SomeObject..ctor() in C:\ConsoleApplication1\SomeObject.cs:line 14
    /// --- End of inner exception stack trace ---
    /// at ConsoleApplication1.SomeObject..ctor() in C:\ConsoleApplication1\SomeObject.cs:line 18
    /// at ConsoleApplication1.Program.DoSomething() in C:\ConsoleApplication1\Program.cs:line 23
    /// at ConsoleApplication1.Program.Main(String[] args) in C:\ConsoleApplication1\Program.cs:line 13</example>
    public string? StackTrace { get; set; } = null;
    /// <summary>Some custom metadata that can be helpful. Different errors will have different metadata.</summary>
    /// <example><typeparam name="object">{}</typeparam></example>
    public object? Metadata { get; set; }
}