
using Task_02;

Console.WriteLine("Hello, World!");
Console.WriteLine("Ismail Sariarslan");
Console.WriteLine("s22899");
Console.WriteLine("APBD_LAB_02");

int[] numbers = { 7, 13, 31, 77, 100 };
double average = CalculateAverage.Average(numbers);
Console.WriteLine("Average: " + average);
double maximum = CalculateMax.FindMax(numbers);
Console.WriteLine("Maximum: " + maximum);