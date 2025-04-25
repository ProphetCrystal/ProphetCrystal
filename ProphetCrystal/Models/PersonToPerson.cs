using ProphetCrystal.Enums;

namespace ProphetCrystal.Models;

public class PersonToPerson
{
    public int FirstPersonId { get; set; }
    public int SecondPersonId { get; set; }
    public PersonToPersonRelation Relation { get; set; }

    public required Person FirstPerson { get; set; }
    public required Person SecondPerson { get; set; }
}