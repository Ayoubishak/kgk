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
    public partial class Prefacier : Form
    {
        DataAccessLayer DAL;
        public Prefacier()
        {
            InitializeComponent();
        }

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

                string sql3 = "select [Pref_Intitule] from [dbo].[P_Prefacier] where [Pref_Intitule]='" + textBox_Inti.Text.Trim() + "'";
                //SqlConnection connection = new SqlConnection(connectionString);
                cmdRe = new SqlCommand(sql3, DAL.sqlconnection);
                drRe = cmdRe.ExecuteReader();

                if (drRe.HasRows)

                {
                    // Fermer La datareader Si Obligatoir


                    //var user = Properties.Settings.Default.User;


                    Toast.show(this, "Info - OptimumBiblio", "Code Intitule " + " " + textBox_Inti.Text + "  --   " + " Existe déjà  Merci !!", ToastType.ERROR, ToastDuration.SHORT);

                    textBox_Inti.Text = "";
                    textBox_CodePref.Text = "";
                    drRe.Close();
                    return;

                }

                drRe.Close();



                SqlCommand cmdAjouterB = new SqlCommand();

                cmdAjouterB.CommandText = "INSERT INTO [dbo].[P_Prefacier] ([Pref_Intitule],[Pref_Statut]) VALUES  (@Intitule,@Statut)";
                cmdAjouterB.Connection = DAL.sqlconnection;

                cmdAjouterB.Parameters.AddWithValue("@Intitule", SqlDbType.VarChar);
                cmdAjouterB.Parameters["@Intitule"].Value = textBox_Inti.Text.Trim();

                cmdAjouterB.Parameters.AddWithValue("@Statut", SqlDbType.VarChar);
                cmdAjouterB.Parameters["@Statut"].Value = "A";



                cmdAjouterB.ExecuteNonQuery();


                PopupNotifier pop = new PopupNotifier();
                pop.Image = Properties.Resources.OK;
                pop.TitleText = "Se connecter au serveur";
                pop.ContentText = "Info - OptimumBiblio" + " -- " + "Enregistrement  a  été  effectué  avec  succès";
                pop.Popup();
                Toast.show(this, "Info - OptimumBiblio", "Enregistrement a  été effectué avec  succès.", ToastType.INFO, ToastDuration.SHORT);
                textBox_Inti.Text = "";
                textBox_CodePref.Text = "";






                string sql = "select [Pref_Id],[Pref_Intitule],[Pref_Statut] from [dbo].[P_Prefacier]";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "table");
                gridControl_Prefacier.DataSource = ds;
                gridControl_Prefacier.DataMember = "table";
                gridView_Prefacier.BestFitColumns();



            }


            if (e.Button == windowsUIButtonPanel1.Buttons[4])
            {

                this.Close();

            }
        }

        private void repositConsulter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                textBox_CodePref.Text = gridView_Prefacier.GetRowCellValue(gridView_Prefacier.FocusedRowHandle, "Pref_Id").ToString();
                textBox_Inti.Text = gridView_Prefacier.GetRowCellValue(gridView_Prefacier.FocusedRowHandle, "Pref_Intitule").ToString();
                var Statu = gridView_Prefacier.GetRowCellValue(gridView_Prefacier.FocusedRowHandle, "Pref_Statut").ToString();


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void reposSup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                string constringg = string.Format("Data Source={0};Initial catalog={1};User ID={2};Password={3};", Properties.Settings.Default.Server, Properties.Settings.Default.DataBase, Properties.Settings.Default.Id, Properties.Settings.Default.Password);
                DAL = new DataAccessLayer(constringg);

                string nomsoc = Properties.Settings.Default.Soc;

                SqlCommand cmd3 = new SqlCommand();
                string queryDossierP2 = "use " + nomsoc + ";";
                cmd3.CommandText = queryDossierP2;
                DAL.sqlconnection.Open();
                cmd3.Connection = DAL.sqlconnection;
                //try
                //{
                cmd3.ExecuteNonQuery();

                var ID = gridView_Prefacier.GetRowCellValue(gridView_Prefacier.FocusedRowHandle, "Pref_Id").ToString();

                if (ID != "" || ID != null)
                {

                    SqlCommand Del = new SqlCommand();
                    Del.CommandText = "delete [dbo].[P_Prefacier]  where [Pref_Id]='" + Int32.Parse(ID) + "'";
                    Del.Connection = DAL.sqlconnection;
                    Del.ExecuteNonQuery();

                    textBox_CodePref.Text = "";
                    textBox_Inti.Text = "";

                    Toast.show(this, "Info - OptimumBiblio", "La Suppression a été effectuée avec succès - Merci !!!!!!", ToastType.INFO, ToastDuration.SHORT);
                }









                string sql = "select [Pref_Id],[Pref_Intitule],[Pref_Statut] from [dbo].[P_Prefacier] ";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "table");

                gridControl_Prefacier.DataSource = ds;
                gridControl_Prefacier.DataMember = "table";
                gridView_Prefacier.BestFitColumns();
            }
            catch (Exception ex)
            {

                Toast.show(this, "Infi - OptimumBiblio", ex.Message, ToastType.INFO, ToastDuration.SHORT);
            }
        }
    }
}
