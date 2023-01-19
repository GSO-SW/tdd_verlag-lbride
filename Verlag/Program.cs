// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

Regex r = new Regex(@"^([\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Pc}\p{Lm}\s\-\']+)$", RegexOptions.IgnoreCase);

var x = r.Matches("Vórname 'nachnáme-test");

foreach (var item in x)
{
    Console.WriteLine(item);
}