using kp.Toaster;
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
using Tulpep.NotificationWindow;

namespace Biblio
{
    public partial class SousGenre : Form
    {


        DataAccessLayer DAL;
        public SousGenre()
        {
            InitializeComponent();
        }

        private void repositoryAjouter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }



        // Ajouter 
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button == windowsUIButtonPanel1.Buttons[0])
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




                SqlCommand cmdRe;
                SqlDataReader drRe;

                string sql3 = "select [S_Genr_Intitule] from [dbo].[P_Sous_Genre] where [S_Genr_Intitule]='" + textBox_intSouGenre.Text.Trim() + "'";
                //SqlConnection connection = new SqlConnection(connectionString);
                cmdRe = new SqlCommand(sql3, DAL.sqlconnection);
                drRe = cmdRe.ExecuteReader();

                if (drRe.HasRows)

                {
                    // Fermer La datareader Si Obligatoir


                    //var user = Properties.Settings.Default.User;


                    Toast.show(this, "Info - OptimumBiblio", "Code Intitule Sous Genre " + " " + textBox_intSouGenre.Text + "  --   " + " Existe déjà  Merci !!", ToastType.ERROR, ToastDuration.SHORT);

                    textBox_intSouGenre.Text = "";
                    textBox_IntGenre.Text = "";
                    label_Matricule.Text = "M";
                    label_Matricule.Visible = false;

                    drRe.Close();
                    return;

                }

                drRe.Close();

                var Mat = label_Matricule.Text;

                SqlCommand cmdAjouterB = new SqlCommand();

                cmdAjouterB.CommandText = "INSERT INTO [dbo].[P_Sous_Genre] ([S_Genr_Intitule],[S_Genr_Statut],[Genr_Id]) VALUES  (@IntituleSG,@Statut,@IDGenre)";
                cmdAjouterB.Connection = DAL.sqlconnection;

                cmdAjouterB.Parameters.AddWithValue("@IntituleSG", SqlDbType.VarChar);
                cmdAjouterB.Parameters["@IntituleSG"].Value = textBox_intSouGenre.Text.Trim();

                cmdAjouterB.Parameters.AddWithValue("@Statut", SqlDbType.VarChar);
                cmdAjouterB.Parameters["@Statut"].Value = "A";



                cmdAjouterB.Parameters.AddWithValue("@IDGenre", SqlDbType.Int);
                cmdAjouterB.Parameters["@IDGenre"].Value = Int32.Parse(Mat);

                cmdAjouterB.ExecuteNonQuery();


                PopupNotifier pop = new PopupNotifier();
                pop.Image = Properties.Resources.OK;
                pop.TitleText = "Se connecter au serveur";
                pop.ContentText = "Info - OptimumBiblio" + " -- " + "Enregistrement  a  été  effectué  avec  succès";
                pop.Popup();
                Toast.show(this, "Info - OptimumBiblio", "Enregistrement a  été effectué avec  succès.", ToastType.INFO, ToastDuration.SHORT);


                textBox_intSouGenre.Text = "";
                textBox_IntGenre.Text = "";
                label_Matricule.Text = "M";
                label_Matricule.Visible = false;






                SqlDataAdapter AdapterGenre = new SqlDataAdapter(
                             "select [Genr_Id] as [Code Genre],[Genr_Intitule],[Genr_Statut]  from [dbo].[P_Genre]", DAL.sqlconnection);
                SqlDataAdapter AdapterSousGenre = new SqlDataAdapter(
                   "SELECT SG.[S_Genr_Intitule] as [Intitule Sous Genre] ,SG.[Genr_Id] as [Code Genre],G.[Genr_Intitule] as [Intitule Genre] FROM [dbo].[P_Sous_Genre] SG,[dbo].[P_Genre] G where G.Genr_Id=SG.Genr_Id", DAL.sqlconnection);

                DataSet dataSet11 = new DataSet();
                //Create DataTable objects for representing database's tables
                AdapterGenre.Fill(dataSet11, "Genre");
                AdapterSousGenre.Fill(dataSet11, "SousGenre");




                DataColumn keyColumn = dataSet11.Tables["Genre"].Columns["Code Genre"];
                DataColumn foreignKeyColumn = dataSet11.Tables["SousGenre"].Columns["Code Genre"];
                dataSet11.Relations.Add("Intitule Sous Genre", keyColumn, foreignKeyColumn);

                //Bind the grid control to the data source
               gridControl_SouGenre1.DataSource = dataSet11.Tables["Genre"];
                gridControl_SouGenre1.ForceInitialize();



            }


            if (e.Button == windowsUIButtonPanel1.Buttons[4])
            {

                this.Close();

            }


        }


        private void repositoCons_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                label_Matricule.Text = gridView_SouGenre1.GetRowCellValue(gridView_SouGenre1.FocusedRowHandle, "Code Genre").ToString();
                textBox_IntGenre.Text = gridView_SouGenre1.GetRowCellValue(gridView_SouGenre1.FocusedRowHandle, "Genr_Intitule").ToString();
                var Statu = gridView_SouGenre1.GetRowCellValue(gridView_SouGenre1.FocusedRowHandle, "Genr_Statut").ToString();

                textBox_intSouGenre.Enabled = true;
                label_Matricule.Visible = true;

                label_Genre.Text = textBox_IntGenre.Text;
                textBox_intSouGenre.Focus();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
