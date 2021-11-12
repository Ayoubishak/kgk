using DevExpress.XtraEditors;
using kp.Toaster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace Biblio
{
    public partial class ListeEmprunteur : DevExpress.XtraEditors.XtraUserControl
    {

        DataAccessLayer DAL;
    
      
          
        private static ListeEmprunteur _instance;
        public int NbrExemplaire=0;
        public string NbrR;


        public static ListeEmprunteur instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new ListeEmprunteur();

                }
                return _instance;

            }
        }
        public ListeEmprunteur()
        {
            InitializeComponent();
        }



        public void ListeEmprunteure()
        {

        


            string sql = "select [Emp_Id],[Emp_Matricule],[Emp_Nom],[Emp_Prenom] from [dbo].[T_Emprunteur]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Empru.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Empru.Properties.DisplayMember = "Emp_Nom";
            gridLookUpEdit_Empru.Properties.ValueMember = "Emp_Id";
            gridLookUpEdit_Empru.Properties.BestFitWidth.ToString();


        }



        public void ListeLivre()
        {

           
             

                string sql = "select [Liv_Id],[Liv_Titre_Livre],[Liv_Nbr_Exemplaire] from [dbo].[T_Livre]";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                //ds.Clear();
                dataadapter.Fill(ds, "table");



                gridLookUpEdit_Livre.Properties.DataSource = ds.Tables[0];
                gridLookUpEdit_Livre.Properties.DisplayMember = "Liv_Titre_Livre";
                gridLookUpEdit_Livre.Properties.ValueMember = "Liv_Id";
                gridLookUpEdit_Livre.Properties.BestFitWidth.ToString();


                // gridView_Auteur.BestFitColumns();

            
        }








       



        private void ListeEmprunteur_Load(object sender, EventArgs e)
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


            ListeEmprunteure();
            ListeLivre();
            ListeLivree();

        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

            if (e.Button == windowsUIButtonPanel1.Buttons[0])
            {
                
                 var Val = dxValidationProvider1.Validate();

                if (Val == true)
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

                    // Insert Test  ******************************************************************************************************************************************

                    var IDEprun = gridLookUpEdit_Empru.EditValue;
                    var IDLivre = gridLookUpEdit_Livre.EditValue;

                    var DatePre2 = dateTimeOffsetEdit_DatePret.Text.Trim();
                    var DateRetourPrevu2 = dateTimeOffsetEdit_DateRetourPrevu.Text.Trim();


                    SqlCommand cmdTest, cmdTest2;
                    SqlDataReader DrTest, DrTest2;







                    SqlDataReader DrLivre2;
                    SqlCommand cmdlivre2 = new SqlCommand();


                    string sql22 = "select [Cpt_Nbr_Exemplaire]  from [dbo].[T_Cpt_Livre]  where [Liv_Id]='" + Convert.ToInt32(IDLivre) + "'";
                    //SqlConnection connection = new SqlConnection(connectionString);
                    cmdlivre2 = new SqlCommand(sql22, DAL.sqlconnection);
                    DrLivre2 = cmdlivre2.ExecuteReader();


                    if (DrLivre2.Read())

                    {

                        NbrExemplaire = DrLivre2.GetInt32(0);


                        if (NbrExemplaire == 0)
                        {

                            labelControl_Err.Visible = true;
                            labelControl_rem.Visible = true;
                            pictureEdit_rem.Visible = true;
                           

                            labelControl_Err.Text = "Veuillez svp verifier le  Nbr Exemplaire de ce livre  : " + NbrExemplaire + "   Merci ";

                            return;

                        }

                    }

                    DrLivre2.Close();



















                    string sql = "select Top(1)[Pre_Id_Livre],[Pre_Id_emprunteur],[Pre_Date_Pret],[Pre_Date_Rendu],[Pre_Id_Livre],[Pre_Id_emprunteur] from [dbo].[T_Pret]  where [Pre_Id_Livre]='" + Convert.ToInt32(IDLivre) + "' and [Pre_Id_emprunteur] ='"+ Convert.ToInt32(IDEprun) + "'  order by [Pre_Id] desc";
                    //SqlConnection connection = new SqlConnection(connectionString);
                    cmdTest2 = new SqlCommand(sql, DAL.sqlconnection);
                    DrTest2 = cmdTest2.ExecuteReader();


                    if (DrTest2.Read())

                    {
                        var DatePretTest = DrTest2.GetDateTime(2).ToString("yyyy-MM-dd");
                        var DateRenduTest = DrTest2.GetDateTime(3).ToString("yyyy-MM-dd");






                        if ((Convert.ToDateTime(DatePre2) <= Convert.ToDateTime(DatePretTest)))
                        {


                            labelControl_Err.Visible = true;
                            labelControl_rem.Visible = true;
                            pictureEdit_rem.Visible = true;
                            dateTimeOffsetEdit_DateRetourPrevu.BackColor = Color.White;
                            dateTimeOffsetEdit_DatePret.BackColor = Color.White;

                            labelControl_Err.Text = "Ce livre  est  déja  emprunté et  sera  rendu le : " + DateRenduTest + "   Merci de Indiquez  une date  supérieur au Date Rendu";

                            return;
                        }




                        if ((Convert.ToDateTime(DatePre2) >= Convert.ToDateTime(DatePretTest)) && (Convert.ToDateTime(DatePre2) <= Convert.ToDateTime(DateRenduTest)))
                        {


                            labelControl_Err.Visible = true;
                            labelControl_rem.Visible = true;
                            pictureEdit_rem.Visible = true;
                            dateTimeOffsetEdit_DateRetourPrevu.BackColor = Color.White;
                            dateTimeOffsetEdit_DatePret.BackColor = Color.White;


                            labelControl_Err.Text = "Ce livre  est  déja  emprunté et  sera  rendu le : " + DateRenduTest + "   Merci de Indiquez  une date  supérieur au Date Rendu";

                            return;
                        }


                    }






                        DrTest2.Close();






                    string sql3 = "select Top(1)[Pre_Id_Livre],[Pre_Id_emprunteur],[Pre_Date_Pret],[Pre_Date_Rendu],[Pre_Id_Livre],[Pre_Id_emprunteur] from [dbo].[T_Pret]  where [Pre_Id_Livre]='" + Convert.ToInt32(IDLivre) + "' and [Pre_Id_emprunteur] ='" + Convert.ToInt32(IDEprun) + "'  order by [Pre_Id] desc";
                    //SqlConnection connection = new SqlConnection(connectionString);
                    cmdTest = new SqlCommand(sql3, DAL.sqlconnection);
                    DrTest = cmdTest.ExecuteReader();

                    if (DrTest.Read())

                    {

                        var DatePret = DrTest.GetDateTime(2).ToString("yyyy-MM-dd");
                        var DateRendu = DrTest.GetDateTime(3).ToString("yyyy-MM-dd");

                     

                        if ((Convert.ToDateTime(DateRetourPrevu2) >= Convert.ToDateTime(DateRendu)) && (Convert.ToDateTime(DatePre2) >= Convert.ToDateTime(DateRendu)))
                        {


                            labelControl_Err.Visible = false;
                            labelControl_rem.Visible = false;
                            pictureEdit_rem.Visible = false;
                            dateTimeOffsetEdit_DateRetourPrevu.BackColor = Color.White;
                            dateTimeOffsetEdit_DatePret.BackColor = Color.White;
                        }
                        else
                        {
                            PopupNotifier pop = new PopupNotifier();
                            pop.Image = Properties.Resources.Info;
                            pop.ShowOptionsButton = true;
                            pop.TitleFont = new Font("Tahoma", 10);
                            pop.ContentFont = new Font("Tahoma", 10);
                       
                            pop.TitlePadding = new Padding(10);
                            pop.ContentPadding = new Padding(0);
                            pop.TitleText = "Info OptimumBiblio";
                            pop.ContentText = "" + "Ce livre  est  déja emprunté  et sera  rendu le : " + DateRendu + "   Merci de Indiquez  une date  supérieur au Date Rendu";
                            pop.Popup();

                            //Toast.show(this, "OptimumBiblio", "Ce livre est déja emprunté et sera rendu le : " + DateRendu + " ", ToastType.INFO, ToastDuration.SHORT);
                            dateTimeOffsetEdit_DateRetourPrevu.BackColor = Color.IndianRed;
                            dateTimeOffsetEdit_DatePret.BackColor = Color.IndianRed;

                            labelControl_Err.Visible = true;
                            labelControl_rem.Visible = true;
                            pictureEdit_rem.Visible = true;
                            labelControl_Err.Text = "Ce livre  est  déja  emprunté et  sera  rendu le : " + DateRendu + "   Merci de Indiquez  une date  supérieur au Date Rendu";



                            return;
                        }

                   
                    }
                    DrTest.Close();

                    // Fin Insert Test

                    SqlCommand cmdAjouterEmprunt = new SqlCommand();


                    cmdAjouterEmprunt.CommandText = "INSERT INTO [dbo].[T_Pret] ([Pre_Date_Pret],[Pre_Date_Retour_Prevu],[Pre_Date_Prolongement],[Pre_Date_Rappel],[Pre_Date_Rendu],[Pre_Id_emprunteur],[Pre_Id_Livre],[Pre_Statut],[Pre_Commentaire],[Pre_TotalJour]) VALUES  (@PreDatePret,@PreDateRetourPrevu,@PreDateProlongement,@PreDateRappel,@PreDateRendu,@PreIdemprunteur,@PreIdLivre,@PreStatut,@PreCommentaire,@PreTotalJour)";
                    cmdAjouterEmprunt.Connection = DAL.sqlconnection;

                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreDatePret", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreDatePret"].Value = dateTimeOffsetEdit_DatePret.Text.Trim();


                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreDateRetourPrevu", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreDateRetourPrevu"].Value = dateTimeOffsetEdit_DateRetourPrevu.Text.Trim();


                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreDateProlongement", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreDateProlongement"].Value = dateTimeOffsetEdit_DateProlon.Text.Trim();





                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreDateRappel", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreDateRappel"].Value = dateTimeOffsetEdit_DateRappel.Text.Trim();


                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreDateRendu", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreDateRendu"].Value = dateTimeOffsetEdit_DateRendu.Text.Trim();


                    //if (dateTimeOffsetEdit_DateRetourPrevu.EditValue == nu)
                    //{

                    //    dateTimeOffsetEdit_DateRetourPrevu.EditValue = DateTime.Now.ToString("yyyy-MM-dd");


                    //}

                    //if (dateTimeOffsetEdit_DatePret.EditValue == null)
                    //{

                    //    dateTimeOffsetEdit_DatePret.EditValue = DateTime.Now.ToString("yyyy-MM-dd");


                    //}


                    var TotalJour = (Convert.ToDateTime(dateTimeOffsetEdit_DateRetourPrevu.Text) - Convert.ToDateTime(dateTimeOffsetEdit_DatePret.Text)).TotalDays ;

                    var TestTotalJour = Convert.ToString(TotalJour);
                    if (TestTotalJour.Contains("-"))
                    {

                        Toast.show(this, "OptimumBiblio", "La Date du Prêt doit être inférieure ou égale la Date Retour Prévu  Merci !! ", ToastType.ERROR, ToastDuration.SHORT);
                        return;
                    }


                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreTotalJour", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreTotalJour"].Value = Convert.ToInt32(TotalJour);


  

                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreIdemprunteur", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreIdemprunteur"].Value = Convert.ToInt32(IDEprun);


                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreIdLivre", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreIdLivre"].Value = Convert.ToInt32(IDLivre);




                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreCommentaire", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreCommentaire"].Value = memoExEdit_Commentaire.Text.Trim();


                    cmdAjouterEmprunt.Parameters.AddWithValue("@PreStatut", SqlDbType.VarChar);
                    cmdAjouterEmprunt.Parameters["@PreStatut"].Value = 1;




                    


                    cmdAjouterEmprunt.ExecuteNonQuery();


                    Toast.show(this, "OptimumBiblio", "Enregistrement  " + "  a été effectuée avec succès  Merci !!", ToastType.INFO, ToastDuration.SHORT);



                    SqlDataReader DrLivre;
                    SqlCommand cmdlivre = new SqlCommand();


                    string sql2 = "select [Cpt_Nbr_Exemplaire]  from [dbo].[T_Cpt_Livre]  where [Liv_Id]='" + Convert.ToInt32(IDLivre) + "'";
                    //SqlConnection connection = new SqlConnection(connectionString);
                    cmdlivre = new SqlCommand(sql2, DAL.sqlconnection);
                    DrLivre = cmdlivre.ExecuteReader();


                    if (DrLivre.Read())

                    {

                        NbrExemplaire = DrLivre.GetInt32(0);

                    }

                    DrLivre.Close();









                    int res = NbrExemplaire - 1;


                    SqlCommand Upd = new SqlCommand();
                    Upd.CommandText = "update [dbo].[T_Cpt_Livre] set [Cpt_Nbr_Exemplaire]='" + Convert.ToInt32(res) + "' where [Liv_Id]='" + Convert.ToInt32(IDLivre) + "'";
                    Upd.Connection = DAL.sqlconnection;
                    Upd.ExecuteNonQuery();

                    labelControl_Err.Visible = false;
                    labelControl_rem.Visible = false;
                    pictureEdit_rem.Visible = false;

                    ListeLivree();
                    Vider();

                }
                else { }

            }


            if (e.Button == windowsUIButtonPanel1.Buttons[0])
            {
                Vider();

            }



            }


        public void Vider()
        {

            gridLookUpEdit_Empru.Properties.NullValuePrompt = "";
            gridLookUpEdit_Livre.Properties.NullValuePrompt = "";
           
         
            memoExEdit_Commentaire.Text = "";
          

            gridLookUpEdit_Empru.Text = "";
            gridLookUpEdit_Livre.Text = "";
          
            pictureBox_photoLivre.Image = Properties.Resources.book2;
            pictureBox_emp.Image = Properties.Resources.q;

            gridLookUpEdit_Livre.EditValue = 0;
            gridLookUpEdit_Empru.EditValue = 0;
            dateTimeOffsetEdit_DatePret.EditValue = "";
            dateTimeOffsetEdit_DateProlon.EditValue = "";
            dateTimeOffsetEdit_DateRappel.EditValue = "";
            dateTimeOffsetEdit_DateRendu.EditValue = "";
            dateTimeOffsetEdit_DateRetourPrevu.EditValue = "";

            gridLookUpEdit_Empru.Focus();
            gridLookUpEdit_Empru.Select();
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



            gridControl_EmprLivre.DataSource = ds;
            gridControl_EmprLivre.DataMember = "table";
            gridView_EmprLivre.BestFitColumns();


            // gridView_Auteur.BestFitColumns();

        }

        private void gridLookUpEdit_Empru_EditValueChanged(object sender, EventArgs e)
        {

            try
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


                var Id = gridLookUpEdit_Empru.EditValue;


                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand("select [Emp_Image] from [dbo].[T_Emprunteur] where [Emp_Id]='" + Convert.ToInt32(Id) + "'", DAL.sqlconnection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 1)
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dataSet.Tables[0].Rows[0]["Emp_Image"]);
                    MemoryStream mem = new MemoryStream(data);
                    pictureBox_emp.Image = Image.FromStream(mem);
                }
            }
            catch
            {

                return;
            }
     

        }



       

        private void repositoryItemButtonEdit19_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                var ID = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Id").ToString();
                var IntNom = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Emp_Nom").ToString();
                var IntTitre = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Titre_Livre").ToString();


                if (ID != "" || ID != null)
                {


                    DialogResult rslt = MessageBox.Show("" + "Admin" + "---" + "Voulez-vous Vraiment Supprimer " + "--"+ IntNom + "--" + IntTitre + " -- --" + "  Maintenant  ??", "la Suppression " + "--" + IntTitre + "--" + IntNom + "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (rslt == DialogResult.Yes)
                    {






                        SqlCommand Del = new SqlCommand();
                        Del.CommandText = "delete [dbo].[T_Pret]  where [Pre_Id]='" + Int32.Parse(ID) + "'";
                        Del.Connection = DAL.sqlconnection;
                        Del.ExecuteNonQuery();


                        Toast.show(this, "Info - OptimumBiblio", "La Suppression a été effectuée avec succès - Merci !!!!!!", ToastType.INFO, ToastDuration.SHORT);

                        ListeLivree();
                        Vider();
                    }
                    else
                    {
                        return;
                    }
                }









            }
            catch (Exception ex)
            {

                Toast.show(this, "Infi - OptimumBiblio", ex.Message, ToastType.INFO, ToastDuration.SHORT);
            }
        }

        private void dateTimeOffsetEdit_DateRetourPrevu_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                dateTimeOffsetEdit_DateRendu.EditValue = dateTimeOffsetEdit_DateRetourPrevu.EditValue;
                dateTimeOffsetEdit_DateProlon.EditValue = dateTimeOffsetEdit_DateRetourPrevu.EditValue;




                dateTimeOffsetEdit_DateRappel.EditValue = dateTimeOffsetEdit_DateProlon.DateTimeOffset.AddDays(-1);
            }
            catch 
            {

                return;
            }        

        }

        private void repositorySupprimer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                var ID = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Id").ToString();
                var IntNom = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Emp_Nom").ToString();
                var IntPrenom = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Emp_Prenom").ToString();
                var IntTiteLivre = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Titre_Livre").ToString();


                labelControl_Err.Visible = false;
                labelControl_rem.Visible = false;
                pictureEdit_rem.Visible = false;

                if (ID != "" || ID != null)
                {


                    DialogResult rslt = MessageBox.Show("" + "Admin" + "---" + "Voulez-vous Vraiment Supprimer " + "--" + IntTiteLivre + " -- " + IntNom +"--" + IntPrenom +  "  Maintenant  ??", "la Suppression " + "--" + IntTiteLivre + "--" + IntNom + "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (rslt == DialogResult.Yes)
                    {






                        SqlCommand Del = new SqlCommand();
                        Del.CommandText = "delete [dbo].[T_Pret]  where [Pre_Id]='" + Int32.Parse(ID) + "'";
                        Del.Connection = DAL.sqlconnection;
                        Del.ExecuteNonQuery();


                        Toast.show(this, "Info - OptimumBiblio", "La Suppression a été effectuée avec succès - Merci !!!!!!", ToastType.INFO, ToastDuration.SHORT);

                        ListeLivree();
                        Vider();
                    }
                    else
                    {
                        return;
                    }
                }









            }
            catch (Exception ex)
            {

                Toast.show(this, "Infi - OptimumBiblio", ex.Message, ToastType.INFO, ToastDuration.SHORT);
            }
        }

        private void gridLookUpEdit_Livre_EditValueChanged(object sender, EventArgs e)
        {

            try
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


                var Id = gridLookUpEdit_Livre.EditValue;


                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand("select [Liv_Image] from [dbo].[T_Livre] where [Liv_Id]='" + Convert.ToInt32(Id) + "'", DAL.sqlconnection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 1)
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dataSet.Tables[0].Rows[0]["Liv_Image"]);
                    MemoryStream mem = new MemoryStream(data);
                    pictureBox_photoLivre.Image = Image.FromStream(mem);
                }
            }
            catch
            {

                return;
            }
        }

        private void gridLookUpEdit_Empru_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dateTimeOffsetEdit_DatePret_EditValueChanged(object sender, EventArgs e)
        {

            //try
            //{

                //var D = Convert.ToDecimal(Properties.Settings.Default.RegleJour);

                //var  dd = D;


             dateTimeOffsetEdit_DateRetourPrevu.EditValue = dateTimeOffsetEdit_DatePret.DateTimeOffset.AddDays(5).ToString("yyyy-MM-dd");

            //}
            //catch
            //{



            //}
    }

        private void gridView_EmprLivre_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {

                if (gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Id") != null && gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Date_Pret") != null)
                {

                  

                    gridLookUpEdit_Empru.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Emp_Nom").ToString();
                    gridLookUpEdit_Livre.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Titre_Livre").ToString();



                    var DateTraduction = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Date_Pret");

                    dateTimeOffsetEdit_DatePret.EditValue = Convert.ToDateTime(DateTraduction).ToString("yyyy-MM-dd");
                    memoExEdit_Commentaire.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Commentaire").ToString();

                    //textEdit_TitreOriginal.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Titre_Original").ToString();
                    //memoExEdit_Resume.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Resume").ToString();
                    //spinEdit_NbrExemplaire.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Nbr_Exemplaire").ToString();
                    //var DateParu = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Annee_Parution");
                    //dateTimeOffsetEdit_DateParution.EditValue = Convert.ToDateTime(DateParu).ToString("yyyy-MM-dd");
                    //spinEdit_NbrPages.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Nbr_Page").ToString();
                    //ratingControl_Evaluation.EditValue = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Note").ToString();
                    //textEdit_PrixLivre.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Prix").ToString();
                    //gridLookUpEdit_EtatLivre.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Etat").ToString();
                    //gridLookUpEdit_ModelLivre.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Modele").ToString();
                    //gridLookUpEditla_langue.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Langue").ToString();

                    //textEdit_Perso1.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Personnage1").ToString();
                    //textEdit_Perso2.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Personnage2").ToString();
                    //textEdit_ISBN10.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_ISBN10").ToString();
                    //textEdit_ISBN13.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_ISBN13").ToString();

                    //var DateTraduction = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Date_Traduction");
                    //dateTimeOffsetEditDateTraduction.EditValue = Convert.ToDateTime(DateTraduction).ToString("yyyy-MM-dd");
                    //memoExEdit_Commentaire.Text = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Commentaire").ToString();
                    //gridLookUpEdit_TraduiPar.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "TraduitPar_Intitule").ToString();
                    //gridLookUpEdit_Prefacier.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pref_Intitule").ToString();
                    //gridLookUpEdit_Auteur.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Aut_Intitule").ToString();
                    //gridLookUpEdit_Editeur.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Edit_Intitule").ToString();
                    //gridLookUpEdit_Genre.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Genr_Intitule").ToString();
                    //gridLookUpEdit_SouGenre.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "S_Genr_Intitule").ToString();
                    //gridLookUpEdit_Provenance.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Prov_Intitule").ToString();
                    //gridLookUpEdit_Format.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Form_Intitule").ToString();
                    //gridLookUpEdit_Public.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pub_Intitule").ToString();
                    //gridLookUpEdit_Scenariste.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Scen_Intitule").ToString();
                    //gridLookUpEdit_Dessinateur.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Dess_Intitule").ToString();
                    //gridLookUpEdit_Emplacement.Properties.NullValuePrompt = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Empl_Intitule").ToString();



                    //var ImagePr = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Image");

                    //MemoryStream Ms = new MemoryStream((byte[])ImagePr);
                    //pictureBox_photoLivre.Image = new Bitmap(Ms);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                Toast.show(this, "Info - OptimumBiblio", ex.Message, ToastType.INFO, ToastDuration.SHORT);
            }
        }

        private void repositoryRenduLivre_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            RecupererLivre RecupererLivr = new RecupererLivre();





            var ID = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Id").ToString();
            var IntNom = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Emp_Nom").ToString();
            var IntPrenom = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Emp_Prenom").ToString();
            var IntTiteLivre = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Titre_Livre").ToString();
            var NbrActuell = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Cpt_Nbr_Exemplaire").ToString();
            var NbrReel = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Nbr_Exemplaire").ToString();

            var IDlivre = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Pre_Id_Livre").ToString();






            NbrR = gridView_EmprLivre.GetRowCellValue(gridView_EmprLivre.FocusedRowHandle, "Liv_Nbr_Exemplaire").ToString();



            RecupererLivr.labelControl_Nom.Text = IntNom;
            RecupererLivr.labelControl_Prenom.Text = IntPrenom;
            RecupererLivr.labelControl_livre.Text = IntTiteLivre;
            RecupererLivr.labelControl_R.Text = NbrReel +" / " + NbrActuell;
            RecupererLivr.labelControl_ID.Text = ID;

            RecupererLivr.labelControl_Idlivre.Text = IDlivre;
            RecupererLivr.labelControl_nbrRee.Text = NbrR;



            if(NbrReel == NbrActuell)
            {
                RecupererLivr.groupBox1.Enabled = false;
                RecupererLivr.dateTimeOffsetEdit_DateParution.Enabled = false;
            }
            else
            {
                RecupererLivr.groupBox1.Enabled = true;
                RecupererLivr.dateTimeOffsetEdit_DateParution.Enabled = true;
            }

            RecupererLivr.ShowDialog();
        }
    }
}
