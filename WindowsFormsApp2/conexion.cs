using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    internal class conexion
    {
        private string connString = "Data Source=DESKTOP-K9SG051\\SQLEXPRESS;Database=xd;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True";
        public void Abrir(SqlConnection c)
        {
            if (c.State != System.Data.ConnectionState.Open)
            {
                c.Open();
            }
        }
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connString);
        }
    }
}
