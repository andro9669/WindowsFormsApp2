using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Alumno
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int Legajo { get; private set; }
        public int[] Notas { get; private set; }

        public Alumno(int id, string nombre, int legajo, int[] notas)
        {
            Id = id;
            Nombre = nombre;
            Legajo = legajo;
            Notas = notas;
        }
        public Alumno(string nombre, int legajo, int[] notas)
        {
            Nombre = nombre;
            Legajo = legajo;
            Notas = notas;
        }
        public double CalcularPromedio()
        {
            double sumatoria = 0;
            foreach (int nota in Notas)
            {
                sumatoria += nota;
            }
            return sumatoria / Notas.Length;
        }

        public bool Aprobar()
        {
            return CalcularPromedio() >= 6;
        }
    }
}
