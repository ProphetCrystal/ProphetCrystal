using ProphetCrystal.Enums;

namespace ProphetCrystal.Models;

public class OrganizationPeople
{
    public int PersonId { get; set; }
    public int OrganizationId { get; set; }
    public PersonToOrganizationRelation Relation { get; set; }

    public required Person Person { get; set; }
    public required Organization Organization { get; set; }
}