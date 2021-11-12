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
    public partial class RecupererLivre : Form
    {

        DataAccessLayer DAL;
        public int NbrExemplaire;
        public RecupererLivre()
        {
            InitializeComponent();
            dateTimeOffsetEdit_DateParution.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

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


                var Id = labelControl_ID.Text;
                var Etat = "";


                if (radioButton_Neuf.Checked == true)
                {
                    Etat = "Neuf";
                }

                else if (radioButton_Tresbonetat.Checked == true)
                {
                    Etat = "Très bon état";
                }



                else if (radioButton_Bonetat.Checked == true)
                {
                    Etat = "Bon état";
                }

                else if (radioButton_PeuAbime.Checked == true)
                {
                    Etat = "Peu Abimé";
                }

                else if (radioButton_TresAbime.Checked == true)
                {
                    Etat = "Très Abimé";
                }


                else if (radioButton_Inutilisable.Checked == true)
                {
                    Etat = "Inutilisable";
                }





                SqlCommand Del = new SqlCommand();
                Del.CommandText = "update [dbo].[T_Pret]  set [Pre_Liv_Etat]='" + Etat.Trim() + "',[Pre_Date_Rendu]='"+ dateTimeOffsetEdit_DateParution .EditValue+ "' where [Pre_Id]='" + Int32.Parse(Id) + "'";
                Del.Connection = DAL.sqlconnection;
                Del.ExecuteNonQuery();


                PopupNotifier pop = new PopupNotifier();
                pop.Image = Properties.Resources.OK;
                pop.TitleText = "Se connecter au serveur";
                pop.ContentText = "OptimumBiblio" + " -- " + "Enregistrement  a  été  effectué  avec  succès";
                pop.Popup();
                Toast.show(this, "OptimumBiblio", "Enregistrement a  été effectué avec  succès.", ToastType.INFO, ToastDuration.SHORT);

                this.Hide();



                // Compte Livre 






                SqlDataReader DrLivre;
                SqlCommand cmdlivre = new SqlCommand();

                var IDlivre = labelControl_Idlivre.Text;


                string sql2 = "select [Cpt_Nbr_Exemplaire]  from [dbo].[T_Cpt_Livre]  where [Liv_Id]='" + Convert.ToInt32(IDlivre) + "'";
                //SqlConnection connection = new SqlConnection(connectionString);
                cmdlivre = new SqlCommand(sql2, DAL.sqlconnection);
                DrLivre = cmdlivre.ExecuteReader();


                if (DrLivre.Read())

                {

                    NbrExemplaire = DrLivre.GetInt32(0);

                }

                DrLivre.Close();


                int res = NbrExemplaire + 1;


                var ResNbr = labelControl_nbrRee.Text;


                if(res > Convert.ToInt32(ResNbr))
                {

                    res = Convert.ToInt32(ResNbr);
                }


                SqlCommand Upd = new SqlCommand();
                Upd.CommandText = "update [dbo].[T_Cpt_Livre] set [Cpt_Nbr_Exemplaire]='" + Convert.ToInt32(res) + "' where [Liv_Id]='" + Convert.ToInt32(IDlivre) + "'";
                Upd.Connection = DAL.sqlconnection;
                Upd.ExecuteNonQuery();


                // Compte livre 

                ListeLivree();




       



            }
        }


        public void ListeLivree()
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

            string sql = "select [Pre_Id],[Pre_Date_Pret],[Pre_Date_Retour_Prevu],[Pre_Date_Prolongement],[Pre_Date_Rappel],[Pre_Date_Rendu],[Pre_Statut],[Pre_Commentaire],[Pre_TotalJour],[Pre_Liv_Etat],[Pre_Id_Livre],Emp.Emp_Nom, Liv.Liv_Titre_Livre,Liv.Liv_Nbr_Exemplaire,CL.Cpt_Nbr_Exemplaire,Emp.Emp_Prenom from [dbo].[T_Pret]  FULL OUTER JOIN [dbo].[T_Emprunteur] Emp on Emp.Emp_Id=[dbo].[T_Pret].[Pre_Id_emprunteur]  FULL OUTER JOIN [dbo].[T_Livre] Liv on [dbo].[T_Pret].[Pre_Id_Livre]=Liv.Liv_Id FULL OUTER JOIN [dbo].[T_Cpt_Livre] CL on [dbo].[T_Pret].Pre_Id_Livre=CL.Liv_Id where [dbo].[T_Pret].[Pre_Id] is not null  order by [dbo].[T_Pret].[Pre_Id] desc";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");




          




            ListeEmprunteur.instance.gridControl_EmprLivre.DataSource = ds;
            ListeEmprunteur.instance.gridControl_EmprLivre.DataMember = "table";
            ListeEmprunteur.instance.gridView_EmprLivre.BestFitColumns();

        




            // gridView_Auteur.BestFitColumns();

        }

















    }
}
