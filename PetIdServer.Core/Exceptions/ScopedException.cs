namespace PetIdServer.Core.Exceptions;

public abstract class ScopedException : Exception
{
    protected abstract string DefaultScope { get; }
    public abstract string Code { get; protected set; }

    protected ScopedException() { }
    protected ScopedException(string? message) : base(message) { }

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