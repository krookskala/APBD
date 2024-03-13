namespace Task_02;
using System;

public static class CalculateAverage
{
    public static double Average(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
        {
            throw new ArgumentException("Array Can Not Be Null Or Empty.");
        }

        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        return (double)sum / numbers.Length;
    }
}
