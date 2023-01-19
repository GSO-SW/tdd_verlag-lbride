using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Verlag
{
    public class Buch
    {
        public string Titel { get; set; }

        private string autor;
        public string Autor
        {
            get => autor;
            set
            {
                if (value is null or "")
                {
                    throw new ArgumentException("Der Autor muss einen Namen haben", new ArgumentNullException());
                }

                autor = Regex.IsMatch(value, @"^([\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Pc}\p{Lm}\s\-\']+)$", RegexOptions.IgnoreCase) switch
                {
                    false => value,
                    true => throw new ArgumentException("Unerlaubte Zeichen", new FormatException())
                };
            }
        }


        private int auflage;
        public int Auflage
        {
            get => auflage;
            set
            {
                auflage = value switch
                {
                    < 1 => throw new ArgumentOutOfRangeException(),
                    _ => value
                };
            }
        }

        public Buch(string autor, string titel, int auflage = 1)
        {
            Autor = autor;
            Titel = titel;
            Auflage = auflage;
        }
    }
}
