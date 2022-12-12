using ErrorOr;

namespace JH.CodeAssignment.Api.ServiceErrors;

public static class Errors
{
    public static class TwitterInfo
    {
        public static Error NotFound => Error.NotFound(
            code: "TwitterInfo.NotFound",
            description: "TwitterInfo not found");
        public static Error InvalidName => Error.Validation(
            code: "TwitterInfo.InvalidName",
            description: $"TwitterInfo Name must between {Models.TwitterInfo.NameMinLength} and {Models.TwitterInfo.NameMaxLength} characters long");
        public static Error InvalidDescription => Error.Validation(
            code: "TwitterInfo.InvalidDescription",
            description: $"TwitterInfo Description must between {Models.TwitterInfo.DescriptionMinLength} and {Models.TwitterInfo.DescriptionMaxLength} characters long");
        public static Error InvalidDuration => Error.Validation(
            code: "TwitterInfo.InvalidDuration",
            description: $"TwitterInfo Duration must between {Models.TwitterInfo.DurationMin} and {Models.TwitterInfo.DurationMax} seconds");
        public static Error TwitterError (string desc) => Error.Failure(
            code: "TwitterInfo.TwitterError",
            description: $"TwitterError: {desc}");
    }
}