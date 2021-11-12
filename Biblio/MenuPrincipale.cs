using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Biblio
{
    public partial class MenuPrincipale : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {

        DataAccessLayer DAL;
        public MenuPrincipale()
        {
            InitializeComponent();


            








            if (!Container.Controls.Contains(Connexion.instance))
            {
                Container.Controls.Add(Connexion.instance);
                Connexion.instance.Dock = DockStyle.Fill;
                Connexion.instance.BringToFront();
            }
            Connexion.instance.BringToFront();




            if (!Container.Controls.Contains(Biblio.instance))
            {
                Container.Controls.Add(Biblio.instance);
                Biblio.instance.Dock = DockStyle.Fill;
                Biblio.instance.BringToFront();
            }
            Biblio.instance.BringToFront();



            if (!Container.Controls.Contains(Accueil.instance))
            {
                Container.Controls.Add(Accueil.instance);
                Accueil.instance.Dock = DockStyle.Fill;
                Accueil.instance.BringToFront();
            }
            Accueil.instance.BringToFront();



        }




        // Appele la procedure Connxion 
        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            if (!Container.Controls.Contains(Connexion.instance))
            {
                Container.Controls.Add(Connexion.instance);
                Connexion.instance.Dock = DockStyle.Fill;
                Connexion.instance.BringToFront();
            }
            Connexion.instance.BringToFront();

        }


        // Appele la procedure Biblio 
        private void accordionControlElement2_Click(object sender, EventArgs e)
        {

            //try
            //{
                //  declaré constringg  ( Instancier un Objet Connxion ) DAL
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



                //*********************** Fin USE *********************************************//

                // debut Load les information Socéité Choisi
           
                string sql = "select [Bib_Id],[Bib_Code],[Bib_Intitule],[Bib_Etablissement],[Bib_Adresse],[Bib_Ville],[Bib_Code_Postal],[Bib_Tel],[Bib_Fax],[Bib_Email],[Bib_Logo],[Bib_Regle_Pret] from [dbo].[P_Bibliotheque]";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "table");
                Biblio.instance.gridControl_Biblio.DataSource = ds;
                Biblio.instance.gridControl_Biblio.DataMember = "table";
                Biblio.instance.gridView_Biblio.BestFitColumns();

            //Soc.instance.gridView1.Columns["Bib_Id"].OptionsColumn.ReadOnly = true;
            //Soc.instance.gridView1.Columns["Bib_Code"].OptionsColumn.ReadOnly = true;
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            if (!Container.Controls.Contains(Biblio.instance))
            {
                Container.Controls.Add(Biblio.instance);
                Biblio.instance.Dock = DockStyle.Fill;
                Biblio.instance.BringToFront();
            }
            Biblio.instance.BringToFront();

        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
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



            //*********************** Fin USE *********************************************//

            // debut Load les information Socéité Choisi

            string sql = "select [Emp_Id],[Emp_Matricule],[Emp_Badge],[Emp_Code_Barre],[Emp_Nom],[Emp_Prenom],[Emp_CIN],[Emp_Login],[Emp_Password],[Emp_Email],[Emp_Tel],[Emp_Adresse],[Emp_Commentaire],[Emp_Date_Adhésion],[Emp_Date_Creation],[Emp_Statut],[Emp_Evaluation],[Emp_Image] from [dbo].[T_Emprunteur]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");
            Emprunteur.instance.gridControl_Emprunteur.DataSource = ds;
            Emprunteur.instance.gridControl_Emprunteur.DataMember = "table";



            


            Emprunteur.instance.gridView_Emprunteur.BestFitColumns();







            if (!Container.Controls.Contains(Emprunteur.instance))
            {
                Container.Controls.Add(Emprunteur.instance);
                Emprunteur.instance.Dock = DockStyle.Fill;
                Emprunteur.instance.BringToFront();
            }
            Emprunteur.instance.BringToFront();
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            Auteur Aut = new Auteur();
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

            string sql = "select [Aut_Id],[Aut_Intitule],[Aut_Statut] from [dbo].[P_Auteur]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Aut.gridControl_Auteur.DataSource = ds;
            Aut.gridControl_Auteur.DataMember = "table";
            Aut.gridView_Auteur.BestFitColumns();





            Aut.ShowDialog();


        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            Editeur Edit = new Editeur();
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

            string sql = "select [Edit_Id],[Edit_Intitule],[Edit_Statut] from [dbo].[P_Editeur]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Edit.gridControl_Editeur.DataSource = ds;
            Edit.gridControl_Editeur.DataMember = "table";
            Edit.gridView_Editeur.BestFitColumns();





            Edit.ShowDialog();
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            Provenance Prov = new Provenance();
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

            string sql = "select [Prov_Id],[Prov_Intitule],[Prov_Statut] from [dbo].[P_Provenance]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Prov.gridControl_Provena.DataSource = ds;
            Prov.gridControl_Provena.DataMember = "table";
            Prov.gridView_Provena.BestFitColumns();





            Prov.ShowDialog();
        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            Formats Forma = new Formats();
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

            string sql = "select [Form_Id],[Form_Intitule],[Form_Statut] from [dbo].[P_Formats]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Forma.gridControl_Formats.DataSource = ds;
            Forma.gridControl_Formats.DataMember = "table";
            Forma.gridView_Formats.BestFitColumns();





            Forma.ShowDialog();
        }

        private void accordionControlElement10_Click(object sender, EventArgs e)
        {
            Publics Publi = new Publics();
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

            string sql = "select [Pub_Id],[Pub_Intitule],[Pub_Statut] from [dbo].[P_Publics]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Publi.gridControl_Publics.DataSource = ds;
            Publi.gridControl_Publics.DataMember = "table";
            Publi.gridView_Publics.BestFitColumns();





            Publi.ShowDialog();
        }

        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            Collection Collect = new Collection();
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

            string sql = "select [Col_Id],[Col_Intitule],[Col_Statut] from [dbo].[P_Collection]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Collect.gridControl_Collection.DataSource = ds;
            Collect.gridControl_Collection.DataMember = "table";
            Collect.gridView_Collection.BestFitColumns();





            Collect.ShowDialog();
        }

        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            Scenariste Scena = new Scenariste();
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

            string sql = "select [Scen_Id],[Scen_Intitule],[Scen_Statut]  from [dbo].[P_Scenariste]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Scena.gridControl_Scenariste.DataSource = ds;
            Scena.gridControl_Scenariste.DataMember = "table";
            Scena.gridView_Scenariste.BestFitColumns();





            Scena.ShowDialog();
        }

        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            Genre Gen = new Genre();
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

            string sql = "select [Genr_Id],[Genr_Intitule],[Genr_Statut]  from [dbo].[P_Genre]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            Gen.gridControl_Genre.DataSource = ds;
            Gen.gridControl_Genre.DataMember = "table";
            Gen.gridView_Genre.BestFitColumns();





            Gen.ShowDialog();
        }

        private void accordionControlElement14_Click(object sender, EventArgs e)
        {
            SousGenre SousGenr = new SousGenre();
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
            SousGenr.gridControl_SouGenre1.DataSource = dataSet11.Tables["Genre"];
             SousGenr.gridControl_SouGenre1.ForceInitialize();

             //SousGenr.gridView_SouGenre1.Columns["Code Genre"].Visible = false;


            SousGenr.ShowDialog();

        }

        private void accordionControlElement16_Click(object sender, EventArgs e)
        {
            Emplacements Emplace = new Emplacements();
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

            //string sql = "select [Genr_Id],[Genr_Intitule],[Genr_Statut]  from [dbo].[P_Genre]";
            ////SqlConnection connection = new SqlConnection(connectionString);
            //SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            //DataSet ds = new DataSet();
            //dataadapter.Fill(ds, "table");

            //Emplace.gridControl_Genre.DataSource = ds;
            //Emplace.gridControl_Genre.DataMember = "table";
            //Emplace.gridView_Genre.BestFitColumns();



          //  Emplace.textBox_Allee.Focus();

            Emplace.ShowDialog();
        }

        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            try
            {
                AffichageCodeEmplacement Aff = new AffichageCodeEmplacement();
                Aff.ShowDialog();
            }
            catch (Exception)
            {

                return;
            }
        }

     

        private void accordionControlElement17_Click(object sender, EventArgs e)
        {

            if (!Container.Controls.Contains(Livres.instance))
            {
                Container.Controls.Add(Livres.instance);
                Livres.instance.Dock = DockStyle.Fill;
                Livres.instance.BringToFront();
            }
            Livres.instance.BringToFront();


           



        }

        private void accordionControlElement22_Click(object sender, EventArgs e)
        {
            try
            {
                Prefacier Prefaci = new Prefacier();

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

                string sql = "select [Pref_Id],[Pref_Intitule],[Pref_Statut] from [dbo].[P_Prefacier]";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "table");

                Prefaci.gridControl_Prefacier.DataSource = ds;
                Prefaci.gridControl_Prefacier.DataMember = "table";
                Prefaci.gridView_Prefacier.BestFitColumns();





                Prefaci.ShowDialog();
            }
            catch (Exception)
            {

                return;
            }

        
        }

        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            try
            {
                Dessinateur Dessinate = new Dessinateur();

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

                string sql = "select [Dess_Id],[Dess_Intitule],[Dess_Statut] from [dbo].[P_Dessinateur]";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "table");

                Dessinate.gridControl_Dist.DataSource = ds;
                Dessinate.gridControl_Dist.DataMember = "table";
                Dessinate.gridView_Dist.BestFitColumns();

                Dessinate.ShowDialog();

              
            }
            catch (Exception)
            {

                return;
            }
        }

        private void accordionControlElement24_Click(object sender, EventArgs e)
        {
            try
            {
                TraduitPar TraduitP = new TraduitPar();

            
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

                string sql = "select [TraduitPar_Id],[TraduitPar_Intitule],[TraduitPar_Statut] from [dbo].[P_TraduitPar]";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "table");

                TraduitP.gridControl_TraduitPar.DataSource = ds;
                TraduitP.gridControl_TraduitPar.DataMember = "table";
                TraduitP.gridView_TraduitPar.BestFitColumns();





                TraduitP.ShowDialog();













               
            }
            catch (Exception)
            {

                return;
            }
        }

        private void accordionCont_accueil_Click(object sender, EventArgs e)
        {
            if (!Container.Controls.Contains(Accueil.instance))
            {
                Container.Controls.Add(Accueil.instance);
                Accueil.instance.Dock = DockStyle.Fill;
                Accueil.instance.BringToFront();
            }
            Accueil.instance.BringToFront();
        }

        private void accordionControlElement25_Click(object sender, EventArgs e)
        {
          
        }

        private void accordionControlElement26_Click(object sender, EventArgs e)
        {
            if (!Container.Controls.Contains(ListeEmprunteur.instance))
            {
                Container.Controls.Add(ListeEmprunteur.instance);
                ListeEmprunteur.instance.Dock = DockStyle.Fill;
                ListeEmprunteur.instance.BringToFront();
            }
            ListeEmprunteur.instance.BringToFront();
        }
    }
    }

