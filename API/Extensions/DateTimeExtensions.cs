namespace API.Extensions;

public static class DateTimeExtensions // Extend the DateOnly Class 
{
    public static int CalculateAge(this DateOnly dob)
    {
        // Not a perfect calculation (leap days!) but close enough 
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dob.Year;
        if (dob > today.AddYears(-1 * age)) age--;
        return age;
    }
}
