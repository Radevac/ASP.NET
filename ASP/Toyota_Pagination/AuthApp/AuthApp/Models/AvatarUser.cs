using Microsoft.AspNetCore.Identity;

namespace AuthApp.Models;

public class AvatarUser : IdentityUser
{
    public string? AvatarUrl
    {
        get;
        set;
    }

}