using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblio
{
    public partial class AffichageCodeEmplacement : Form
    {


        DataAccessLayer DAL;
        public AffichageCodeEmplacement()
        {
            InitializeComponent();
        }

        private void AffichageCodeEmplacement_Load(object sender, EventArgs e)
        {

            string constringg = string.Format("Data Source={0};Initial catalog={1};User ID={2};Password={3};", Properties.Settings.Default.Server, Properties.Settings.Default.DataBase, Properties.Settings.Default.Id, Properties.Settings.Default.Password);
            DAL = new DataAccessLayer(constringg);

            // ********************  Debut Load GridVies ******************************//
            // Debut Ouvre Database Dossier Avec La methode USE 
            SqlCommand cmd3 = new SqlCommand();
            string queryDossierP2 = "use " + Properties.Settings.Default.Soc + ";";
            cmd3.CommandText = queryDossierP2;
            DAL.sqlconnection.Open();
            cmd3.Connection = DAL.sqlconnection;
            cmd3.ExecuteNonQuery();


            string sql = "select [Code_Intitule],[Empl_Intitule] from [dbo].[P_Emplacement]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            gridControl_CodeEm.DataSource = ds;
            gridControl_CodeEm.DataMember = "table";
            gridView_CodeEm.BestFitColumns();









        }
    }
}
