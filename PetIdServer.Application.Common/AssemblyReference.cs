using System.Reflection;

namespace PetIdServer.Application.Common;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
