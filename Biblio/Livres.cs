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
    public partial class Livres : DevExpress.XtraEditors.XtraUserControl
    {

        DataAccessLayer DAL;
        List<ListEta> ListLivre = new List<ListEta>();
        List<ModeleLivre> ModeleLivr = new List<ModeleLivre>();
        List<languee> langue = new List<languee>();
     




        Image ImageLivre;
        public string chainephoto;
      

        private static Livres _instance;

        public static Livres instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new Livres();

                }
                return _instance;

            }
        }


        public Livres()
        {
            InitializeComponent();
         
        }

    





        private void Livres_Load(object sender, EventArgs e)
        {


       
            ListLivre.Add(new ListEta {Name = "Neuf"});
            ListLivre.Add(new ListEta { Name = "Très bon état" });
            ListLivre.Add(new ListEta { Name = "Bon état" });
            ListLivre.Add(new ListEta { Name = "Peu Abimé" });
            ListLivre.Add(new ListEta { Name = "Très Abimé" });
            ListLivre.Add(new ListEta { Name = "Inutilisable" });

            gridLookUpEdit_EtatLivre.Properties.DataSource = ListLivre;
            gridLookUpEdit_EtatLivre.Properties.DisplayMember = "Name";
            gridLookUpEdit_EtatLivre.Properties.ValueMember = "Name";

            // Appele Methode Auteur


            // Modele Du Livre

            ModeleLivr.Add(new ModeleLivre { Name = "Classique" });
            ModeleLivr.Add(new ModeleLivre { Name = "Numérique" });


            gridLookUpEdit_ModelLivre.Properties.DataSource = ModeleLivr;
            gridLookUpEdit_ModelLivre.Properties.DisplayMember = "Name";
            gridLookUpEdit_ModelLivre.Properties.ValueMember = "Name";




            // Langue

            langue.Add(new languee { Name = "Arabe" });
            langue.Add(new languee { Name = "Anglais" });
            langue.Add(new languee { Name = "Français" });

            langue.Add(new languee { Name = "Espagnol" });
            langue.Add(new languee { Name = "Allemand" });
          
            langue.Add(new languee { Name = "Russe" });
            langue.Add(new languee { Name = "chinoise" });

            langue.Add(new languee { Name = "coréenne" });

            langue.Add(new languee { Name = "الفارسية" });
            langue.Add(new languee { Name = "العربية" });



            


            gridLookUpEditla_langue.Properties.DataSource = langue;
            gridLookUpEditla_langue.Properties.DisplayMember = "Name";
            gridLookUpEditla_langue.Properties.ValueMember = "Name";

            Auteur();
            Emplacement();
            Editeur();
            Scenariste();
            Public();
            Provenance();
            Dessinateur();
            Prefacier();
            Formats();
            Genre();
            TraduiPar();
            ListeLivre();

        }


        class ListEta
        {
            public string Name { get; set; }
        }

        class ModeleLivre
        {
            public string Name { get; set; }
        }


        class languee
        {
            public string Name { get; set; }
        }


        public void Auteur()
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

            string sql = "select Aut_Id,Aut_Intitule,Aut_Statut from P_Auteur";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");
           
           

            gridLookUpEdit_Auteur.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Auteur.Properties.DisplayMember = "Aut_Intitule";
            gridLookUpEdit_Auteur.Properties.ValueMember = "Aut_Id";
            gridLookUpEdit_Auteur.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }




        public void Emplacement()
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

            string sql = "select [Empl_Id],[Code_Intitule],[Empl_Intitule] from [dbo].[P_Emplacement]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Emplacement.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Emplacement.Properties.DisplayMember = "Empl_Intitule";
            gridLookUpEdit_Emplacement.Properties.ValueMember = "Empl_Id";
            gridLookUpEdit_Emplacement.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }




        public void Editeur()
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

            string sql = "select [Edit_Id],[Edit_Intitule] from [dbo].[P_Editeur]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Editeur.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Editeur.Properties.DisplayMember = "Edit_Intitule";
            gridLookUpEdit_Editeur.Properties.ValueMember = "Edit_Id";
            gridLookUpEdit_Editeur.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }





        public void Public()
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

            string sql = "select [Pub_Id],[Pub_Intitule] from [dbo].[P_Publics]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Public.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Public.Properties.DisplayMember = "Pub_Intitule";
            gridLookUpEdit_Public.Properties.ValueMember = "Pub_Id";
            gridLookUpEdit_Public.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }




        public void Scenariste()
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

            string sql = "select [Scen_Id],[Scen_Intitule] from [dbo].[P_Scenariste]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Scenariste.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Scenariste.Properties.DisplayMember = "Scen_Intitule";
            gridLookUpEdit_Scenariste.Properties.ValueMember = "Scen_Id";
            gridLookUpEdit_Scenariste.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }




        public void Provenance()
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

            string sql = "select [Prov_Id],[Prov_Intitule] from [dbo].[P_Provenance]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Provenance.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Provenance.Properties.DisplayMember = "Prov_Intitule";
            gridLookUpEdit_Provenance.Properties.ValueMember = "Prov_Id";
            gridLookUpEdit_Provenance.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }



        public void Dessinateur()
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

            string sql = "select [Dess_Id],[Dess_Intitule] from [dbo].[P_Dessinateur]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Dessinateur.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Dessinateur.Properties.DisplayMember = "Dess_Intitule";
            gridLookUpEdit_Dessinateur.Properties.ValueMember = "Dess_Id";
            gridLookUpEdit_Dessinateur.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }

        public void Prefacier()
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

            string sql = "select [Pref_Id],[Pref_Intitule] from [dbo].[P_Prefacier] ";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Prefacier.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Prefacier.Properties.DisplayMember = "Pref_Intitule";
            gridLookUpEdit_Prefacier.Properties.ValueMember = "Pref_Id";
            gridLookUpEdit_Prefacier.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }


        public void Formats()
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

            string sql = "select [Form_Id],[Form_Intitule] from [dbo].[P_Formats] ";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Format.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Format.Properties.DisplayMember = "Form_Intitule";
            gridLookUpEdit_Format.Properties.ValueMember = "Form_Id";
            gridLookUpEdit_Format.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }




        public void Genre()
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

            string sql = "select [Genr_Id],[Genr_Intitule] from [dbo].[P_Genre] ";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_Genre.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_Genre.Properties.DisplayMember = "Genr_Intitule";
            gridLookUpEdit_Genre.Properties.ValueMember = "Genr_Id";
            gridLookUpEdit_Genre.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }




        public void TraduiPar()
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

            string sql = "select [TraduitPar_Id],[TraduitPar_Intitule] from [dbo].[P_TraduitPar] ";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridLookUpEdit_TraduiPar.Properties.DataSource = ds.Tables[0];
            gridLookUpEdit_TraduiPar.Properties.DisplayMember = "TraduitPar_Intitule";
            gridLookUpEdit_TraduiPar.Properties.ValueMember = "TraduitPar_Id";
            gridLookUpEdit_TraduiPar.Properties.BestFitWidth.ToString();


            // gridView_Auteur.BestFitColumns();

        }










        public void ListeLivre()
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

            string sql = "select L.[Liv_Id],L.Liv_Titre_Livre,L.Liv_Titre_Original,L.Liv_Resume,L.Liv_Nbr_Exemplaire,L.Liv_Annee_Parution,L.Liv_Nbr_Page,L.Liv_Note,L.Liv_Prix,L.Liv_Etat,L.Liv_Modele,L.Liv_Langue,L.Liv_Personnage1,L.Liv_Personnage2,L.Liv_ISBN10,L.Liv_ISBN13,L.Liv_Date_Traduction,L.Liv_Date_Creation,L.Liv_Commentaire,L.Liv_Image,L.Liv_Statut,TP.TraduitPar_Intitule,pf.Pref_Intitule,Au.Aut_Intitule,Ed.Edit_Intitule,GE.Genr_Intitule,SOG.S_Genr_Intitule,Prov.Prov_Intitule,F.Form_Intitule,Pu.Pub_Intitule,Sc.Scen_Intitule,Dess.Dess_Intitule,Empe.Code_Intitule,Empe.Empl_Intitule from  [dbo].[T_Livre] L FULL OUTER JOIN [dbo].[P_TraduitPar] TP on L.Liv_Id_Traduit_Par=TP.TraduitPar_Id FULL OUTER JOIN [dbo].[P_Prefacier] Pf on L.Liv_Id_Prefacier=Pf.Pref_Id  FULL OUTER JOIN [dbo].[P_Auteur] Au on L.Liv_Id_Auteur=Au.Aut_Id FULL OUTER JOIN [dbo].[P_Editeur] Ed on L.Liv_Id_Editeur=Ed.Edit_Id FULL OUTER JOIN [dbo].[P_Genre] GE on L.Liv_Id_Genre=GE.Genr_Id FULL OUTER JOIN [dbo].[P_Sous_Genre] SOG on L.Liv_Id_Sous_Genre = SOG.S_Genr_Id FULL OUTER JOIN [dbo].[P_Provenance] Prov on L.Liv_Id_Provenance=Prov.Prov_Id  FULL OUTER JOIN [dbo].[P_Formats] F on L.Liv_Id_Format=F.Form_Id  FULL OUTER JOIN [dbo].[P_Publics] Pu on L.Liv_Id_Public=Pu.Pub_Id FULL OUTER JOIN [dbo].[P_Scenariste] Sc on L.Liv_Id_Scenariste=Sc.Scen_Id FULL OUTER JOIN [dbo].[P_Dessinateur] Dess on L.Liv_Id_Dessinateur=Dess.Dess_Id FULL OUTER JOIN [dbo].[P_Emplacement] Empe on L.Liv_Id_Emplacement=Empe.Empl_Id where L.Liv_Id is not null";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            //ds.Clear();
            dataadapter.Fill(ds, "table");



            gridControl_livre.DataSource = ds;
            gridControl_livre.DataMember = "table";
            gridView_Livre.BestFitColumns();


            // gridView_Auteur.BestFitColumns();

        }













        private void pictureEdit11_Click(object sender, EventArgs e)
        {
           
            gridLookUpEdit_Auteur.Text = "";
        }

        private void pictureEdit15_Click(object sender, EventArgs e)
        {
            gridLookUpEdit_EtatLivre.Text = "";
        }

        private void pictureBox_sup_Click(object sender, EventArgs e)
        {
            pictureBox_photoLivre.Image = null;

            //chainephoto = null;
            //chainephoto = "";
            pictureBox_photoLivre.Image = Properties.Resources.book2;
        }

        private void pictureBox_upload_Click(object sender, EventArgs e)
        {
            try
            {



                // rechercher emplacemment des photos 
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "ملفات الصور |*.png; *.jpg; *.gif; *.bmp";
                if (op.ShowDialog() == DialogResult.OK)
                    pictureBox_photoLivre.Image = Image.FromFile(op.FileName);
                ImageLivre = Image.FromFile(op.FileName);

                chainephoto = op.FileName;
                Properties.Settings.Default.PhotoLivre = chainephoto;
                Properties.Settings.Default.Save();


            }


            catch
            {
                PopupNotifier pop = new PopupNotifier();
                pop.Image = Properties.Resources.Erreur;
                pop.TitleText = "Informations";
                pop.ContentText = "Le  Chemin  d'accès  n'a   pas  une  Forme  Conforme.";
                pop.Popup();
                return;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Auteur();
            Emplacement();
            Editeur();
            Scenariste();
            Public();
            Provenance();
            Dessinateur();
            Prefacier();
            Formats();
            Genre();
            TraduiPar();
            ListeLivre();
        }

        private void gridLookUpEdit_Genre_EditValueChanged(object sender, EventArgs e)
        {

           
            var ID = gridLookUpEdit_Genre.EditValue;


            if(ID!=null || ID.ToString() != "")
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








                    gridLookUpEdit_SouGenre.Text = "";

                    string sql = "select [S_Genr_Id],[S_Genr_Intitule] from [dbo].[P_Sous_Genre] where [Genr_Id]='" + Convert.ToInt32(ID) + "' ";
                    //SqlConnection connection = new SqlConnection(connectionString);
                    SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "table");


                    gridLookUpEdit_SouGenre.Properties.DataSource = ds.Tables[0];
                   
                    gridLookUpEdit_SouGenre.Properties.DisplayMember = "S_Genr_Intitule";
                    gridLookUpEdit_SouGenre.Properties.ValueMember = "S_Genr_Id";
                    gridLookUpEdit_SouGenre.Properties.BestFitWidth.ToString();

                    gridLookUpEdit_SouGenre.Enabled = true;
                    gridLookUpEdit_SouGenre.Text = "";

                }
                catch (Exception ex)
                {

                    Toast.show(this, "OptimumBiblio", ex.Message, ToastType.INFO, ToastDuration.SHORT);
                }

            }
            else
            {

                gridLookUpEdit_SouGenre.Enabled = false;
            }


          



        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

            // Ajouter Position 0
            if (e.Button == windowsUIButtonPanel1.Buttons[0])




            {

                // Pour Tester la Valeur Titre Livre
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


                    SqlCommand cmdAjouterLiv = new SqlCommand();
                    SqlCommand cmdNbrExe = new SqlCommand();

                    cmdAjouterLiv.CommandText = "INSERT INTO [dbo].[T_Livre] ([Liv_Titre_Livre],[Liv_Titre_Original],[Liv_Resume],[Liv_Nbr_Exemplaire],[Liv_Annee_Parution],[Liv_Nbr_Page],[Liv_Note],[Liv_Prix],[Liv_Etat],[Liv_Modele],[Liv_Langue],[Liv_Personnage1],[Liv_Personnage2],[Liv_ISBN10],[Liv_ISBN13],[Liv_Id_Traduit_Par],[Liv_Date_Traduction],[Liv_Id_Prefacier],[Liv_Id_Auteur],[Liv_Id_Editeur],[Liv_Id_Genre],[Liv_Id_Sous_Genre],[Liv_Id_Provenance],[Liv_Id_Format],[Liv_Id_Public],[Liv_Id_Scenariste],[Liv_Id_Dessinateur],[Liv_Id_Emplacement],[Liv_Date_Creation],[Liv_Commentaire],[Liv_Image],[Liv_Statut]) VALUES  (@Liv_Titre_Livre,@Liv_Titre_Original,@Liv_Resume,@Liv_Nbr_Exemplaire,@Liv_Annee_Parution,@Liv_Nbr_Page,@Liv_Note,@Liv_Prix,@Liv_Etat,@Liv_Modele,@Liv_Langue,@Liv_Personnage1,@Liv_Personnage2,@Liv_ISBN10,@Liv_ISBN13,@Liv_Id_Traduit_Par,@Liv_Date_Traduction,@Liv_Id_Prefacier,@Liv_Id_Auteur,@Liv_Id_Editeur,@Liv_Id_Genre,@Liv_Id_Sous_Genre,@Liv_Id_Provenance,@Liv_Id_Format,@Liv_Id_Public,@Liv_Id_Scenariste,@Liv_Id_Dessinateur,@Liv_Id_Emplacement,@Liv_Date_Creation,@Liv_Commentaire,@Liv_Image,@Liv_Statut)";
                    cmdAjouterLiv.Connection = DAL.sqlconnection;

                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Titre_Livre", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Titre_Livre"].Value = textEdit_TitreLivre.Text.Trim();


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Titre_Original", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Titre_Original"].Value = textEdit_TitreOriginal.Text.Trim();


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Resume", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Resume"].Value = memoExEdit_Resume.Text.Trim();








                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Nbr_Exemplaire", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Nbr_Exemplaire"].Value = Convert.ToInt32(spinEdit_NbrExemplaire.Value);




                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Annee_Parution", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Annee_Parution"].Value = dateTimeOffsetEdit_DateParution.Text;


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Nbr_Page", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Nbr_Page"].Value = Convert.ToInt32(spinEdit_NbrPages.Value); ;







                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Note", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Note"].Value = Convert.ToInt32(ratingControl_Evaluation.EditValue);



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Prix", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Prix"].Value = Convert.ToDouble(textEdit_PrixLivre.Text.Replace('.', ','));



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Etat", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Etat"].Value = gridLookUpEdit_EtatLivre.Text;



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Modele", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Modele"].Value = gridLookUpEdit_ModelLivre.Text;


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Langue", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Langue"].Value = gridLookUpEditla_langue.Text;



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Personnage1", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Personnage1"].Value = textEdit_Perso1.Text;


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Personnage2", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Personnage2"].Value = textEdit_Perso2.Text;






                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_ISBN10", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_ISBN10"].Value = textEdit_ISBN10.Text;



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_ISBN13", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_ISBN13"].Value = textEdit_ISBN13.Text;


                    // Vaeiable

                    var IDTraduitPar = gridLookUpEdit_TraduiPar.EditValue;
                    var IDPrefacier = gridLookUpEdit_Prefacier.EditValue;
                    var IDAuteur = gridLookUpEdit_Auteur.EditValue;
                    var IDEditeur = gridLookUpEdit_Editeur.EditValue;
                    var IDFormat = gridLookUpEdit_Format.EditValue;
                    var IDGenre = gridLookUpEdit_Genre.EditValue;
                    var IDSousGenre = gridLookUpEdit_SouGenre.EditValue;
                    var IDPro = gridLookUpEdit_Provenance.EditValue;
                    var IDPublic = gridLookUpEdit_Public.EditValue;
                    var IDSce = gridLookUpEdit_Scenariste.EditValue;
                    var IDDessina = gridLookUpEdit_Dessinateur.EditValue;
                    var IDEmplace = gridLookUpEdit_Emplacement.EditValue;


                

                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Traduit_Par", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Traduit_Par"].Value = Convert.ToInt32(IDTraduitPar);






                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Date_Traduction", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Date_Traduction"].Value = dateTimeOffsetEditDateTraduction.Text;


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Prefacier", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Prefacier"].Value = Convert.ToInt32(IDPrefacier);



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Auteur", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Auteur"].Value = Convert.ToInt32(IDAuteur);

                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Editeur", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Editeur"].Value = Convert.ToInt32(IDEditeur);



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Genre", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Genre"].Value = Convert.ToInt32(IDGenre);


                    if (IDSousGenre.Equals(""))
                    {
                        IDSousGenre = 0;
                    }

                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Sous_Genre", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Sous_Genre"].Value = Convert.ToInt32(IDSousGenre);



                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Provenance", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Provenance"].Value = Convert.ToInt32(IDPro);


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Format", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Format"].Value = Convert.ToInt32(IDFormat);


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Public", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Public"].Value = Convert.ToInt32(IDPublic);


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Scenariste", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Scenariste"].Value = Convert.ToInt32(IDSce);




                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Dessinateur", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Dessinateur"].Value = Convert.ToInt32(IDDessina);

                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Id_Emplacement", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Id_Emplacement"].Value = Convert.ToInt32(IDEmplace);


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Date_Creation", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Date_Creation"].Value = DateTime.Now.ToString("yyyy-MM-dd");


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Commentaire", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Commentaire"].Value = memoExEdit_Commentaire.Text;



                    // Images 


                    ImageConverter Convertimg = new ImageConverter();
                    byte[] ImageByte = (byte[])Convertimg.ConvertTo(ImageLivre, typeof(byte[]));

                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Image", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Image"].Value = ImageByte;


                    cmdAjouterLiv.Parameters.AddWithValue("@Liv_Statut", SqlDbType.VarChar);
                    cmdAjouterLiv.Parameters["@Liv_Statut"].Value = "A";


                    cmdAjouterLiv.ExecuteNonQuery();

                    cmdAjouterLiv.CommandText = "select @@IDENTITY";
                    var IdLivre = Convert.ToInt32(cmdAjouterLiv.ExecuteScalar());



                    // Inseret In Table [dbo].[T_Cpt_Livre] -- Nbr Exemplaire



                    // Debut 


                    cmdNbrExe.CommandText = "INSERT INTO [dbo].[T_Cpt_Livre]([Liv_Id],[Cpt_Nbr_Exemplaire]) VALUES  (@LivId,@CptNbrExemplaire)";
                    cmdNbrExe.Connection = DAL.sqlconnection;

                    cmdNbrExe.Parameters.AddWithValue("@LivId", SqlDbType.VarChar);
                    cmdNbrExe.Parameters["@LivId"].Value = IdLivre;

                    cmdNbrExe.Parameters.AddWithValue("@CptNbrExemplaire", SqlDbType.VarChar);
                    cmdNbrExe.Parameters["@CptNbrExemplaire"].Value = Convert.ToInt32(spinEdit_NbrExemplaire.Value);

                    cmdNbrExe.ExecuteNonQuery();


                    // Fin 



                    Toast.show(this, "OptimumBiblio", "Enregistrement  " + "  a été effectuée avec succès  Merci !!", ToastType.INFO, ToastDuration.SHORT);


                    ListeLivre();
                    Vider();
                }
                else
                {

                }





            }

            if (e.Button == windowsUIButtonPanel1.Buttons[6])
            {
                Vider();
            }
        }



        // Naviguer Entre Les Ligne
        private void gridView_Livre_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {

                if (gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Titre_Livre") != null && gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Titre_Original") != null)
                {

                    gridLookUpEdit_SouGenre.Enabled = true;

                    textEdit_TitreLivre.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Titre_Livre").ToString();
                    textEdit_TitreOriginal.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Titre_Original").ToString();
                    memoExEdit_Resume.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Resume").ToString();
                    spinEdit_NbrExemplaire.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Nbr_Exemplaire").ToString();
                    var DateParu = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Annee_Parution");
                    dateTimeOffsetEdit_DateParution.EditValue = Convert.ToDateTime(DateParu).ToString("yyyy-MM-dd");
                    spinEdit_NbrPages.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Nbr_Page").ToString();
                    ratingControl_Evaluation.EditValue = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Note").ToString();
                    textEdit_PrixLivre.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Prix").ToString();
                    gridLookUpEdit_EtatLivre.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Etat").ToString();
                    gridLookUpEdit_ModelLivre.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Modele").ToString();
                    gridLookUpEditla_langue.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Langue").ToString();

                    textEdit_Perso1.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Personnage1").ToString();
                    textEdit_Perso2.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Personnage2").ToString();
                    textEdit_ISBN10.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_ISBN10").ToString();
                    textEdit_ISBN13.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_ISBN13").ToString();

                    var DateTraduction = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Date_Traduction");
                    dateTimeOffsetEditDateTraduction.EditValue = Convert.ToDateTime(DateTraduction).ToString("yyyy-MM-dd");
                    memoExEdit_Commentaire.Text = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Commentaire").ToString();
                    gridLookUpEdit_TraduiPar.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "TraduitPar_Intitule").ToString();
                    gridLookUpEdit_Prefacier.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Pref_Intitule").ToString();               
                    gridLookUpEdit_Auteur.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Aut_Intitule").ToString();
                    gridLookUpEdit_Editeur.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Edit_Intitule").ToString();
                    gridLookUpEdit_Genre.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Genr_Intitule").ToString();
                    gridLookUpEdit_SouGenre.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "S_Genr_Intitule").ToString();
                    gridLookUpEdit_Provenance.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Prov_Intitule").ToString();  
                    gridLookUpEdit_Format.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Form_Intitule").ToString();          
                    gridLookUpEdit_Public.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Pub_Intitule").ToString();
                    gridLookUpEdit_Scenariste.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Scen_Intitule").ToString();              
                    gridLookUpEdit_Dessinateur.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Dess_Intitule").ToString();      
                    gridLookUpEdit_Emplacement.Properties.NullValuePrompt = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Empl_Intitule").ToString();
           


                    var ImagePr = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Image");

                    MemoryStream Ms = new MemoryStream((byte[])ImagePr);
                    pictureBox_photoLivre.Image = new Bitmap(Ms);
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

        private void repositorSupprimer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                var ID = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Id").ToString();
                var Int = gridView_Livre.GetRowCellValue(gridView_Livre.FocusedRowHandle, "Liv_Titre_Livre").ToString();
             

                if (ID != "" || ID != null)
                {


                    DialogResult rslt = MessageBox.Show("" + "Admin" + "---" + "Voulez-vous Vraiment Supprimer " + "--" + Int + " -- --" + "  Maintenant  ??", "la Suppression " + "--" + Int + "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (rslt == DialogResult.Yes)
                    {






                        SqlCommand Del = new SqlCommand();
                        Del.CommandText = "delete [dbo].[T_Livre]  where [Liv_Id]='" + Int32.Parse(ID) + "'";
                        Del.Connection = DAL.sqlconnection;
                        Del.ExecuteNonQuery();


                        Toast.show(this, "Info - OptimumBiblio", "La Suppression a été effectuée avec succès - Merci !!!!!!", ToastType.INFO, ToastDuration.SHORT);

                        ListeLivre();
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

        public void Vider()
        {


            gridLookUpEdit_TraduiPar.Properties.NullValuePrompt="";
            gridLookUpEdit_Prefacier.Properties.NullValuePrompt = "";
            gridLookUpEdit_Auteur.Properties.NullValuePrompt = "";
            gridLookUpEdit_Editeur.Properties.NullValuePrompt = "";
            gridLookUpEdit_Format.Properties.NullValuePrompt = "";
            gridLookUpEdit_Genre.Properties.NullValuePrompt = "";
            gridLookUpEdit_SouGenre.Properties.NullValuePrompt = "";
            gridLookUpEdit_Provenance.Properties.NullValuePrompt = "";
            gridLookUpEdit_Public.Properties.NullValuePrompt = "";
            gridLookUpEdit_Scenariste.Properties.NullValuePrompt = "";
            gridLookUpEdit_Dessinateur.Properties.NullValuePrompt = "";
            gridLookUpEdit_Emplacement.Properties.NullValuePrompt = "";
            gridLookUpEdit_SouGenre.Enabled = false;
            textEdit_ISBN10.Text = "";
            textEdit_ISBN13.Text = "";
            textEdit_Perso1.Text = "";
            textEdit_Perso2.Text = "";
            textEdit_PrixLivre.Text = "0.00";
            textEdit_TitreLivre.Text = "";
            textEdit_TitreOriginal.Text = "";
            ratingControl_Evaluation.EditValue = 1;
            memoExEdit_Commentaire.Text = "";
            memoExEdit_Resume.Text = "";
            spinEdit_NbrExemplaire.Text = "0";
            spinEdit_NbrPages.Text = "0";

            gridLookUpEditla_langue.Text = "";
            gridLookUpEdit_EtatLivre.Text = "";
            gridLookUpEdit_ModelLivre.Text = "";
            pictureBox_photoLivre.Image = Properties.Resources.book2;

            gridLookUpEdit_TraduiPar.EditValue=0;
            gridLookUpEdit_Prefacier.EditValue = 0;
             gridLookUpEdit_Auteur.EditValue = 0;
            gridLookUpEdit_Editeur.EditValue = 0;
            gridLookUpEdit_Format.EditValue = 0;
            gridLookUpEdit_Genre.EditValue = 0;
             gridLookUpEdit_SouGenre.EditValue = 0;
            gridLookUpEdit_Provenance.EditValue = 0;
            gridLookUpEdit_Public.EditValue = 0;
            gridLookUpEdit_Scenariste.EditValue = 0;
            gridLookUpEdit_Dessinateur.EditValue = 0;
            gridLookUpEdit_Emplacement.EditValue = 0;



            dateTimeOffsetEdit_DateParution.EditValue = "";
            dateTimeOffsetEditDateTraduction.EditValue = "";

            textEdit_TitreLivre.Focus();
            textEdit_TitreLivre.Select();
        }

        private void pictureEdit2_Click(object sender, EventArgs e)
        {

            try
            {
                Auteur Auteu = new Auteur();
                Auteu.ShowDialog();
            }
            catch 
            {

                return;
            }
          
        }

        private void pictureEdit3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                Editeur Editeu = new Editeur();
                Auteur();
                Editeu.Show();



            }
            catch
            {

                return;
            }
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            try
            {
                Editeur Editeu = new Editeur();
                Editeur();
                Editeu.Show();
            }
            catch
            {

                return;
            }
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            WebLivre WebLivr = new WebLivre();

            WebLivr.ShowDialog();
        }
    }
}
