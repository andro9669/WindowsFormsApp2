using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        conexion conn = new conexion();
        public Form1()
        {
            InitializeComponent();
            AlumnoDAO dao = new AlumnoDAO();
            CargarIDs(dao);
        }

        private void button1Guardar_Click(object sender, EventArgs e)
        {
            SqlConnection c = conn.ObtenerConexion();
            AlumnoDAO dao = new AlumnoDAO();

            string[] notas = { textBox3Nota1.Text, textBox4Nota2.Text, textBox5Nota3.Text };
            int[] notasParseadas = CargarNotas(notas);
            int legajo = CargarLegajo(textBox2Legajo.Text);

            if (notasParseadas == null || legajo == -1)
                return;

            Alumno a = new Alumno(textBox1Nombre.Text, legajo, notasParseadas);

            int filasAfectadas = dao.Insertar(a);
            if (filasAfectadas == 0)
            {
                MessageBox.Show("No se ha podido cargar el alumno.\nEnvie este mensaje a nuestro contacto \'elmoncho@gmail.com\' y obtenga su respuesta en un plazo de 60 dias.", "Error 20349587"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Ya puede buscar al alumno con su ID correspondiente.", "Carga exitosa!");

            CargarIDs(dao);
        }

        private void button2Consultar_Click(object sender, EventArgs e)
        {
            if (comboBoxAlumnos.SelectedIndex == -1) 
            {
                MessageBox.Show("Se exige seleccionar un alumno antes de consultar", "Error de logica", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            label2Promedio.Text = "Promedio";
            label3Estado.Text = "Estado";

            SqlConnection c = conn.ObtenerConexion();
            AlumnoDAO dao = new AlumnoDAO();

            Alumno a = dao.BuscarPorId(comboBoxAlumnos.SelectedItem.ToString());

            MessageBox.Show($"ID: {a.Id}\nNombre: {a.Nombre}\nLegajo: {a.Legajo}\nNotas: {a.Notas[0]}, {a.Notas[1]}, {a.Notas[2]}");

            label2Promedio.Text += ": " + a.CalcularPromedio().ToString();
            string estado = a.CalcularPromedio() >= (double)6 ? ": Aprobado" : ": Desaprobado";
            label3Estado.Text += estado;
        }

        int[] CargarNotas(string[] vector)
        {
            int[] notas = new int[3];

            for (int i = 0; i < vector.Length; i++)
            {
                if (!int.TryParse(vector[i], out notas[i]) || notas[i] < 0)
                {
                    MessageBox.Show("Las notas deben ser numeros enteros mayores a cero.");
                    return null;
                }
            }
            return notas;
        }

        int CargarLegajo(string entrada)
        {
            if (!int.TryParse(entrada, out int n) || n < 0)
            {
                MessageBox.Show("Se exige que el legajo sea un numero entero mayor a cero.");
                return -1;
            }
            return n;
        }
        void CargarIDs(AlumnoDAO dao)
        {
            ArrayList alumnos = dao.Seleccionar();
            foreach (Alumno alumno in alumnos)
            {
                comboBoxAlumnos.Items.Add(alumno.Id);
            }
        }
    }
}
