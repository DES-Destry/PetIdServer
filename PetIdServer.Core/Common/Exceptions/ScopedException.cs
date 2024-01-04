namespace PetIdServer.Core.Common.Exceptions;

public abstract class ScopedException : Exception
{
    protected ScopedException()
    {
    }

    protected ScopedException(string? message) : base(message)
    {
    }

    protected abstract string DefaultScope { get; }
    public abstract string Code { get; protected set; }

    public void ApplyScope(string scope)
    {
        if (Code.StartsWith(DefaultScope))
        {
            Code = Code?.Replace(DefaultScope, $"{scope}.")!;
            return;
        }

        Code = $"{scope}.{Code}";
    }
}