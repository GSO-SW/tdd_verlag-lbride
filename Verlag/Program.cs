// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

Console.WriteLine(Regex.IsMatch("Leander Bride-test", @"^([\w]+)[\s\-]?([^\W\d]+)$"));