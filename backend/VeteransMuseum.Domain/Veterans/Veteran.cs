using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.Shared;

namespace VeteransMuseum.Domain.Veterans;

public class Veteran : Entity, IAuditable
{
    private Veteran(
        Guid id,
        FirstName firstName,
        MiddleName middleName,
        LastName lastName,
        DateTime? birthDate,
        DateTime? deathDate, 
        Biography biography,
        Rank rank,
        Award awards,
        MilitaryUnit militaryUnit,
        Battle battles,
        ImageUrl imageUrl, 
        DateTime createdAt,
        long createdBy,
        DateTime? updatedAt,
        long? updatedBy)
        : base(id)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        BirthDate = birthDate;
        DeathDate = deathDate;
        Biography = biography;
        Rank = rank;
        Awards = awards;
        MilitaryUnit = militaryUnit;
        Battles = battles;
        ImageUrl = imageUrl;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
    
    private Veteran() {}
    
    public FirstName FirstName { get; private set; }
    
    public LastName LastName { get; private set; }
    
    public MiddleName MiddleName { get; private set; }
    
    public DateTime? BirthDate { get; private set; }
    
    public DateTime? DeathDate { get; private set; }
    
    public Biography Biography { get; private set; }
    
    public Rank Rank { get; private set; }
    
    public Award Awards { get; private set; }
    
    public MilitaryUnit MilitaryUnit { get; private set; }
    
    public Battle Battles { get; private set; }
    
    public ImageUrl ImageUrl { get; private set; }
    
    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }

    public static Veteran Add(
        FirstName firstName,
        LastName lastName,
        MiddleName middleName,
        DateTime? birthDate,
        DateTime? deathDate,
        Biography biography,
        Rank rank,
        Award awards,
        MilitaryUnit militaryUnit,
        Battle battles,
        ImageUrl? imageUrl,
        DateTime utcNow)
    {
        var veteran = new Veteran
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            BirthDate = birthDate,
            DeathDate = deathDate,
            Biography = biography,
            Rank = rank,
            Awards = awards,
            MilitaryUnit = militaryUnit,
            Battles = battles,
            ImageUrl = imageUrl,
            CreatedAt = utcNow,
            CreatedBy = 0, // Укажите ID пользователя, если есть контекст
            UpdatedAt = null,
            UpdatedBy = null
        };
        
        return veteran;
    }
    
    public void Update(
        FirstName? firstName = null,
        LastName? lastName = null,
        MiddleName? middleName = null,
        DateTime? birthDate = null,
        DateTime? deathDate = null,
        Biography? biography = null,
        Rank? rank = null,
        Award? awards = null,
        MilitaryUnit? militaryUnit = null,
        Battle? battles = null,
        ImageUrl? imageUrl = null,
        DateTime? utcNow = null,
        long? updatedBy = null)
    {
        FirstName = firstName ?? FirstName;
        LastName = lastName ?? LastName;
        MiddleName = middleName ?? MiddleName;
        BirthDate = birthDate ?? BirthDate;
        DeathDate = deathDate ?? DeathDate;
        Biography = biography ?? Biography;
        Rank = rank ?? Rank;
        Awards = awards ?? Awards;
        MilitaryUnit = militaryUnit ?? MilitaryUnit;
        Battles = battles ?? Battles;
        ImageUrl = imageUrl ?? ImageUrl;

        if (firstName != null || lastName != null || middleName != null ||
            birthDate != null || deathDate != null || biography != null ||
            rank != null || awards != null || militaryUnit != null ||
            battles != null || imageUrl != null)
        {
            UpdatedAt = utcNow ?? DateTime.UtcNow;
            UpdatedBy = updatedBy ?? 0; // Replace with actual user ID if available
        }
    }
}