using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    internal class AlumnoDAO
    {
        private conexion conn = new conexion();
        public int Insertar(Alumno a)
        {
            SqlConnection c = conn.ObtenerConexion();

            conn.Abrir(c);

            SqlCommand cmd = new SqlCommand(
            $"INSERT INTO Alumnos (nombre, legajo, nota1, nota2, nota3) VALUES ('{a.Nombre}', {a.Legajo}, {a.Notas[0]}, {a.Notas[1]}, {a.Notas[2]})"
            , c);

            int filasAfectadas = cmd.ExecuteNonQuery();

            c.Close();
            return filasAfectadas;
        }
        public ArrayList Seleccionar()
        {
            SqlConnection c = conn.ObtenerConexion();

            conn.Abrir(c);

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Alumnos", c);

            SqlDataReader r = cmd.ExecuteReader();

            ArrayList array = new ArrayList();

            int[] notas = new int[3];
            while (r.Read())
            {
                int id = Convert.ToInt32(r["id"]);
                string nombre = r["nombre"].ToString();
                int legajo = Convert.ToInt32(r["legajo"]);
                notas[0] = Convert.ToInt32(r["nota1"]);
                notas[1] = Convert.ToInt32(r["nota2"]);
                notas[2] = Convert.ToInt32(r["nota3"]);
                Alumno a = new Alumno(id, nombre, legajo, notas);
                array.Add(a);
            }
            r.Close();
            c.Close();
            return array;
        }
        public Alumno BuscarPorId(string identificador)
        {
            Alumno a = null;
            SqlConnection c = conn.ObtenerConexion();

            conn.Abrir(c);

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Alumnos WHERE id = {identificador}", c);

            SqlDataReader r = cmd.ExecuteReader();
            
            int[] notas = new int[3];

            int id = int.Parse(identificador);
            
            if (r.Read())
            {
                string nombre = r["nombre"].ToString();
                int legajo = Convert.ToInt32(r["legajo"]);
                notas[0] = Convert.ToInt32(r["nota1"]);
                notas[1] = Convert.ToInt32(r["nota2"]);
                notas[2] = Convert.ToInt32(r["nota3"]);
                a = new Alumno(id, nombre, legajo, notas);
            }
            r.Close();
            c.Close();
            return a;
        }
    }
}
