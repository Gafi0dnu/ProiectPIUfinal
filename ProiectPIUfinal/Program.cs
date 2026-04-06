using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Transporturi
{
    // lab 1 facem read me pe git si scriem succint ce face aplicatia noastra
    // lab 2 facem clasele pe care le folosim aprox, macar 3 clase, si metodele
    // lab 3 luam o clasa si creem functii pt a incarca date, un ajutor(nume grupa idee etc), exit etc

    public class Sofer
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Categorie { get; set; }
        public double KilometriiTotali { get; set; }

        public override string ToString() => $"{Nume} {Prenume}";
    }

    public class Masina
    {
        public string Marca { get; set; }
        public string Model { get; set; }
        public double Kilometraj { get; set; }
        public override string ToString() => $"{Marca} {Model}";
    }

    public class Traseu
    {
        public Sofer Sofer { get; set; }
        public Masina Masina { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
        public string Cursa { get; set; }
        public double Kilometrii { get; set; }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Aplicatie Transporturi");

            var soferi = new List<Sofer>();
            var masini = new List<Masina>();
            var traseuri = new List<Traseu>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Meniu:");
                Console.WriteLine("1) Adauga traseu");
                Console.WriteLine("2) Listeaza trasee");
                Console.WriteLine("3) Iesire");
                Console.Write("Alegere: ");
                var alegere = Console.ReadLine()?.Trim();

                if (alegere == "1")
                {
                    var soferInput = ReadSofer();
                    // cautam sofer existent dupa nume + prenume (ignoram case)
                    var sofer = soferi.FirstOrDefault(s =>
                        string.Equals(s.Nume, soferInput.Nume, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(s.Prenume, soferInput.Prenume, StringComparison.OrdinalIgnoreCase));

                    if (sofer == null)
                    {
                        sofer = soferInput;
                        soferi.Add(sofer);
                    }
                    else
                    {
                        // actualizam categorie daca este diferita si kilometrii totali daca user a introdus o valoare
                        if (!string.IsNullOrWhiteSpace(soferInput.Categorie))
                            sofer.Categorie = soferInput.Categorie;
                    }

                    var masinaInput = ReadMasina();
                    var masina = masini.FirstOrDefault(m =>
                        string.Equals(m.Marca, masinaInput.Marca, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(m.Model, masinaInput.Model, StringComparison.OrdinalIgnoreCase));

                    if (masina == null)
                    {
                        masina = masinaInput;
                        masini.Add(masina);
                    }

                    var traseu = ReadTraseu(sofer, masina);

                    // actualizam kilometri
                    sofer.KilometriiTotali += traseu.Kilometrii;
                    masina.Kilometraj += traseu.Kilometrii;

                    traseuri.Add(traseu);

                    Console.WriteLine("Traseu adaugat cu succes.");
                }
                else if (alegere == "2")
                {
                    Console.WriteLine();
                    Console.WriteLine("Trasee inregistrate:");
                    if (!traseuri.Any())
                    {
                        Console.WriteLine("Niciun traseu inregistrat.");
                    }
                    else
                    {
                        for (var i = 0; i < traseuri.Count; i++)
                        {
                            var t = traseuri[i];
                            Console.WriteLine($"{i + 1}) {t.Cursa} | Sofer: {t.Sofer} | Masina: {t.Masina} | {t.DataStart:yy-MM-dd} -> {t.DataEnd:yy-MM-dd} | Km: {t.Kilometrii}");
                        }
                    }
                }
                else if (alegere == "3" || string.Equals(alegere, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Iesire...");
                    break;
                }
                else
                {
                    Console.WriteLine("Optiune invalida. Reincercati.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Apasati Enter pentru a iesi...");
            Console.ReadLine();
        }

        private static Sofer ReadSofer()
        {
            Console.WriteLine();
            Console.WriteLine("Date sofer:");
            var s = new Sofer
            {
                Nume = ReadRequiredString("Nume"),
                Prenume = ReadRequiredString("Prenume"),
                Categorie = ReadRequiredString("Categorie"),
                KilometriiTotali = ReadDouble("Kilometrii totali")
            };
            return s;
        }

        private static Masina ReadMasina()
        {
            Console.WriteLine();
            Console.WriteLine("Date masina:");
            var m = new Masina
            {
                Marca = ReadRequiredString("Marca"),
                Model = ReadRequiredString("Model"),
                Kilometraj = ReadDouble("Kilometraj")
            };
            return m;
        }

        private static Traseu ReadTraseu(Sofer sofer, Masina masina)
        {
            Console.WriteLine();
            Console.WriteLine("Date traseu:");
            var t = new Traseu
            {
                Sofer = sofer,
                Masina = masina,
                Cursa = ReadRequiredString("Cursa")
            };

            t.DataStart = ReadDate("Data start (ex: 26-03-31)");
            while (true)
            {
                t.DataEnd = ReadDate("Data end (ex: 26-03-31)");
                if (t.DataEnd < t.DataStart)
                {
                    Console.WriteLine("Data end trebuie sa fie dupa sau aceeasi cu data start. Reincercati.");
                    continue;
                }
                break;
            }

            t.Kilometrii = ReadDouble("Kilometrii parcursi");
            return t;
        }

        private static string ReadRequiredString(string promt)
        {
            while (true)
            {
                Console.Write($"{promt}: ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"{promt} este obligatoriu. Incercati din nou.");
                    continue;
                }
                return input;
            }
        }

        private static double ReadDouble(string promt)
        {
            while (true)
            {
                Console.Write($"{promt}: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{promt} este obligatoriu. Incercati din nou.");
                    continue;
                }

                var normalized = input.Trim().Replace(',', '.');
                if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                    return value;
                Console.WriteLine($"Valoare invalida pentru {promt}. Incercati din nou.");
            }
        }

        private static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine();
                if (DateTime.TryParseExact(input,
                    new[] { "yy-MM-dd", "dd/MM/yy", "dd-MM-yy", "dd.MM.yy" },
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                {
                    return dt;
                }
                Console.WriteLine($"Format data invalid pentru {prompt}. Incercati din nou.");
            }
        }
    }
}