using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Verlag
{
    public class Buch
    {
        public readonly string Titel;

        private string autor;
        public string Autor
        {
            get
            {
                return autor;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value), "Der angegebene Wert ist leer");
                }

                autor = Regex.IsMatch(value, @"^([\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Pc}\p{Lm}\s\-\'\.]+)$", RegexOptions.IgnoreCase) switch
                {
                    true => value,
                    false => throw new ArgumentException("Der angegebene Wert enthält unerlaubte Zeichen", nameof(value), new FormatException())
                };
            }
        }


        private int auflage;
        public int Auflage
        {
            get
            {
                return auflage;
            }
            set
            {
                auflage = value switch
                {
                    < 1 => throw new ArgumentOutOfRangeException(nameof(value), "Der angegebene Wert liegt außerhalb des Gültigkeitsbereichs"),
                    _ => value
                };
            }
        }

        private string isbn13;
        public string ISBN13
        {
            get
            {
                return isbn13;
            }
            set
            {
                isbn13 = value.Replace("-", "") switch
                {
                    { Length: 12 } => value + PruefZiffer13(value),
                    { Length: 13 } => value,
                    _ => throw new ArgumentException("Der Angegebene Wert ist nicht korrekt", nameof(value), new FormatException())
                };
            }
        }

        public string ISBN10
        {
            get
            {
                //Index start = new(4);
                //Index ende = new(1, true);

                //Range ohneErsteVierStellen = new(start, ende);

                //string isbn10 = isbn13[ohneErsteVierStellen];

                string isbn10 = isbn13[4..^1];

                int pruefZiffer = PruefZiffer10(isbn10);

                return isbn10 + pruefZiffer switch
                {
                    10 => 'X',
                    _ => pruefZiffer
                };
            }
        }

        public Buch(string autor, string titel, int auflage = 1)
        {
            Autor = autor;
            Titel = titel;
            Auflage = auflage;
        }

        private int PruefZiffer10(string isbn)
        {
            var asInts = SplitAndRemoveNonDigits(isbn);

            for (int i = 0; i < asInts.Length;)
            {
                asInts[i] *= ++i;
            }

            return asInts.Sum() % 11;
        }

        private int PruefZiffer13(string isbn)
        {
            var asInts = SplitAndRemoveNonDigits(isbn);

            bool even = false;

            for (int i = 0; i < asInts.Length; i++)
            {
                if (even)
                {
                    asInts[i] *= 3;
                }

                even = !even;
            }

            return 10 - Math.Abs(asInts.Sum() % 10);
        }

        private int[] SplitAndRemoveNonDigits(string text)
        {
            return (from digit in text
                    where Char.IsDigit(digit)
                    select Int32.Parse(digit.ToString())
                    ).ToArray();
        }
    }
}
