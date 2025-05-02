using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Domain.Users;

public class User : Entity
{
    private readonly List<Role> _roles = new();
    
    private User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email) 
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private User() { }
    
    public FirstName FirstName { get; private set; }
    
    public LastName LastName { get; private set; }
    
    public Email Email { get; set; }
    
    public string IdentityId { get; private set; } = string.Empty;
    
    public IReadOnlyCollection<Role> Roles => _roles.ToList();
    
    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email);
        
        user._roles.Add(Role.Admin);
        
        return user;
    }
    
    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}