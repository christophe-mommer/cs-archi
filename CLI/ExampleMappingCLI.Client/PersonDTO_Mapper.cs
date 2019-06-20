
namespace ExampleMappingCLI.Client
{
    public static class PersonDTO_Mapper
    {
        public static PersonDTO Map(Person value)
        {
            var result = new PersonDTO();
result.Name = value.Name;
result.Birthday = value.Birthday;
result.Status = value.Status;
return result;

        }
    }
}
