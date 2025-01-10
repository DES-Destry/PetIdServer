using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Domain.User.Commands.Login;
using PetIdServer.Application.Domain.User.Commands.Registration;
using PetIdServer.RestApi.Endpoints.Dto.User;

namespace PetIdServer.RestApi.Endpoints;

public class UserEndpoints : ICarterModule
{
    private const string EndpointBase = "api/user";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapPost("", CreateUser)
            .WithSummary("Create new account.")
            .WithDescription(
                "Create new account of user and get pair of tokens.")
            .Produces<TokenPairDto>();

        group.MapPost("login", LoginUser)
            .WithSummary("Login as user with specific role.")
            .WithDescription("Get new token pairs with user's creds.")
            .Produces<LoginResponseDto>();
    }

    private static async Task<IResult> CreateUser(
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
}
