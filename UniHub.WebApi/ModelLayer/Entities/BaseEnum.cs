using System.ComponentModel.DataAnnotations;

public class BaseEnum
{
    [Key]
    public int Id { get; set; }
    public string Value { get; set; }
}