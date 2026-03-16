using System;
using System.Data;

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

            var sofer = new Sofer
            {

            };

            var masina = new Masina
            {

            };

            var traseu = new Traseu
            {

            };

            Console.WriteLine("CEVA MODIFICAT");
        }
        
    }
    
}