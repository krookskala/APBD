using LAB_05.Models;

namespace LAB_05.Validator;

public class IdValidator
{
    public static bool IsValid(int id, List<Animal> animals)
    {
        return id > 0 && !animals.Any(a => a.Id == id);
    }       
    
    public static bool IsValid(int id, List<Visit> visits)
    {
        return id > 0 && !visits.Any(v => v.Id == id);
    }
}