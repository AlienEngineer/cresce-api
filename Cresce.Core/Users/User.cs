using System;

namespace Cresce.Core.Users
{
    public abstract record User
    {
        public string Id { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;

        public abstract string Role { get; }
    }

    public record AdminUser : User
    {
        public override string Role => "Admin";
    }

    public record BasicUser : User
    {
        public override string Role => "Basic";
    }

}
