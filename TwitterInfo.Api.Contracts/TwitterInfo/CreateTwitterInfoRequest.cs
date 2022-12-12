namespace JH.CodeAssignment.Api.Contracts.TwitterInfo;

public record CreateTwitterInfoRequest(
    string Name,
    string Description,
    int Duration);