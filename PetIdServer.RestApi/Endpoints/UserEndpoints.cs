using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.Users.Commands.ChangePassword;
using PetIdServer.Application.Users.Commands.Login;
using PetIdServer.Application.Users.Commands.Registration;
using PetIdServer.Application.Users.Dto.Tokens;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Endpoints.Dto.User;

namespace PetIdServer.RestApi.Endpoints;

public class UserEndpoints : ICarterModule
{
    private const string EndpointBase = "api/users";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapPost("registration", Registration)
            .WithSummary("Register account as a newbie.")
            .WithDescription(
                "Create a brand new user without any permissions in PetID. This user will be given with least privileges.")
            .Produces<TokenPairDto>();

        group.MapPost("login", LoginUser)
            .WithSummary("Login as user with specific role.")
            .WithDescription("Get new token pairs with user's creds.")
            .Produces<LoginResponseDto>();

        group.MapPut("password", ChangePassword)
            .WithSummary("Change user's password.")
            .WithDescription(
                "Change a password with knowledge of the old one. User without password can change password without providing old password. But they're will be deleted as soon as possible without a password.")
            .Produces<TokenPairDto>();
    }

    private static async Task<IResult> Registration(
        CreateUserDto dto,
        ISender sender,
        IMapper mapper)
    {
        RegistrationCommand? command = mapper.Map<CreateUserDto, RegistrationCommand>(dto);
        TokenPairDto response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> LoginUser(LoginUserDto dto, ISender sender, IMapper mapper)
    {
        LoginCommand? command = mapper.Map<LoginUserDto, LoginCommand>(dto);
        LoginResponseDto response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> ChangePassword(
        RequestUser user,
        ChangePasswordDto dto,
        ISender sender)
    {
        ChangePasswordCommand command = new()
        {
            Id = user.Id, OldPassword = dto.OldPassword, NewPassword = dto.NewPassword
        };
        TokenPairDto response = await sender.Send(command);

        return Results.Ok(response);
    }
}
