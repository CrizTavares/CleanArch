using System.Runtime.CompilerServices;

namespace CleanArch.Domain.Account
{
    public interface ISeedUserRoleInitial
    {
        Task SeedUsersAsync();
        Task SeedRolesAsync();
    }
}
