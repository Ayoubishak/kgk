using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Sql;
using kp.Toaster;
using DevExpress.XtraSplashScreen;
using Tulpep.NotificationWindow;
using System.Threading;
using System.Data.SqlClient;

namespace Biblio
{
    public partial class Connexion : DevExpress.XtraEditors.XtraUserControl
    {


       
        private static Connexion _instance;

        public static Connexion instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new Connexion();

                }
                return _instance;

            }
        }


        public Connexion()
        {
            InitializeComponent();

            try
            {
                comboBox_authe.SelectedIndex = 0;

                comboBoxEdit_motdepass.Enabled = true;
                comboBoxEditnomutilisateur.Enabled = true;
                comboBox_authe.SelectedIndex = 0;
                comboBoxEdit_motdepass.Text = Properties.Settings.Default.Password;
                comboBoxEditnomutilisateur.Text = Properties.Settings.Default.Id;
                comboBox_serve.Text = Properties.Settings.Default.Server;
            


            }
            catch
            {
                return;

            }

        }



        // Tester Index button


        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

            if (e.Button == windowsUIButtonPanel1.Buttons[0])
            {
                try
                {






                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true);

                    //var user = Properties.Settings.Default.User;

                     SplashScreenManager.Default.SetWaitFormCaption("Admin" + "   " + " Veuillez  Patienter  Svp  Merci !!");
                     SplashScreenManager.Default.SetWaitFormDescription("Chargement en Cours");


                    SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;

                    System.Data.DataTable table = instance.GetDataSources();
                    Application.UseWaitCursor = true;
                    comboBox_serve.Items.Clear();
                    foreach (System.Data.DataRow row in table.Rows)
                    {

                        Cursor.Current = Cursors.WaitCursor;
                        comboBox_serve.Items.Add(Environment.MachineName + "\\" + row["InstanceName"].ToString());
                    }


                    comboBox_serve.SelectedIndex = 0;
                    Application.UseWaitCursor = false;


                    Toast.show(this, "", "Opération Terminé avec Succès", ToastType.INFO, ToastDuration.SHORT);

                    SplashScreenManager.CloseForm();

                }
                catch
                {

                    SplashScreenManager.CloseForm();
                    return;
                }

            }


            if (e.Button == windowsUIButtonPanel1.Buttons[3])
            {

                string db = "Master";

                Properties.Settings.Default.Server = comboBox_serve.Text;
                Properties.Settings.Default.DataBase = db;
                Properties.Settings.Default.Id = comboBoxEditnomutilisateur.Text;
                Properties.Settings.Default.Password = comboBoxEdit_motdepass.Text;


                Properties.Settings.Default.Save();


                // Chaine Connxion

                string constring = string.Format("Data Source={0};Initial catalog={1};User ID={2};Password={3};", Properties.Settings.Default.Server, Properties.Settings.Default.DataBase, Properties.Settings.Default.Id, Properties.Settings.Default.Password);
                DataAccessLayer DAL = new DataAccessLayer(constring);
                var user = Properties.Settings.Default.User;

                try
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true);
                    SplashScreenManager.Default.SetWaitFormCaption("Veuillez patienter Svp" + " -- " + " (M)-(Mme)-(Mlle)");
                    SplashScreenManager.Default.SetWaitFormDescription(user + " -- " + " " + "Chargement en cours");
                    if (DAL.Iscon)
                    {
                       


                        ///// ******************************* La Creation des Base De Donner **********************************


                        //  declaré constringg  ( Instancier un Objet Connxion ) DAL
                        string constringg = string.Format("Data Source={0};Initial catalog={1};User ID={2};Password={3};", Properties.Settings.Default.Server, Properties.Settings.Default.DataBase, Properties.Settings.Default.Id, Properties.Settings.Default.Password);
                        DAL = new DataAccessLayer(constringg);


                        // Ouvre La Connexion
                        DAL.sqlconnection.Open();
                        //instancier un objet Command pour passer la Requete
                        SqlCommand cmd = new SqlCommand();
                        //Ici (Ajouter un Dossier qui s'appele Dossier Parametrage pour stoqué Les Nom de Entreprise
                        string dbNamee = Properties.Settings.Default.Soc;
                        // db creation query
                        string queryDossierPara = "CREATE DATABASE " + dbNamee + ";";
                        // Passer La valeur de la requete
                        cmd.CommandText = queryDossierPara;
                        //Connexion
                        cmd.Connection = DAL.sqlconnection;
                        //pour éviter la répétition de Dossier Parametrer 
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }

                        catch
                        {
                            Toast.show(this, user, "Vous  êtes  Connecté  à  " + "  " + Properties.Settings.Default.Server +""+" Merci !!", ToastType.INFO, ToastDuration.SHORT);
                            Thread.Sleep(10);
                            SplashScreenManager.CloseForm();

                            return; }





                        // Debut Ouvre Database Dossier Avec La methode USE 
                        SqlCommand cmd2 = new SqlCommand();
                        string queryDossierP = "use " + dbNamee + ";";
                        cmd2.CommandText = queryDossierP;
                        cmd2.Connection = DAL.sqlconnection;
                        //try
                        //{

                        cmd2.ExecuteNonQuery();
                        //}

                        //    catch { }
                        //*********************** Fin USE *********************************************//



                        SqlCommand cmd3 = new SqlCommand();
                        SqlCommand cmd5 = new SqlCommand();
                        SqlCommand cmd6 = new SqlCommand();
                        SqlCommand cmd7 = new SqlCommand();
                        SqlCommand cmd8 = new SqlCommand();
                        SqlCommand cmd9 = new SqlCommand();
                        SqlCommand cmd12 = new SqlCommand();
                        SqlCommand cmd13 = new SqlCommand();
                        SqlCommand cmd14 = new SqlCommand();
                        SqlCommand cmd15 = new SqlCommand();
                        SqlCommand cmd16 = new SqlCommand();
                        SqlCommand cmd17 = new SqlCommand();
                        SqlCommand cmd18 = new SqlCommand();
                        SqlCommand cmd19 = new SqlCommand();

                      
                        SqlCommand cmd21 = new SqlCommand();
                        SqlCommand cmd22 = new SqlCommand();
                        SqlCommand cmd23 = new SqlCommand();
                        SqlCommand cmd24 = new SqlCommand();





                        cmd3.CommandText = "CREATE TABLE P_Bibliotheque(Bib_Id INT IDENTITY PRIMARY KEY NOT NULL,Bib_Code NVARCHAR(20),Bib_Intitule NVARCHAR(MAX),Bib_Etablissement NVARCHAR(MAX),Bib_Adresse NVARCHAR(MAX),Bib_Ville NVARCHAR(MAX), Bib_Code_Postal NVARCHAR(40),Bib_Tel VARCHAR(30),Bib_Fax VARCHAR(30),Bib_Email VARCHAR(100),Bib_Logo VARCHAR(MAX),Bib_Regle_Pret Decimal(18,2))";

                        cmd5.CommandText = "CREATE TABLE T_Emprunteur (Emp_Id INT IDENTITY PRIMARY KEY NOT NULL,Emp_Matricule NVARCHAR(30),Emp_Badge NVARCHAR(50),Emp_Code_Barre NVARCHAR(50),Emp_Nom NVARCHAR(100),Emp_Prenom NVARCHAR(100),[Emp_CIN] NVARCHAR(20),Emp_Login NVARCHAR(100),Emp_Password NVARCHAR(100),Emp_Email NVARCHAR(100),Emp_Tel VARCHAR(30),Emp_Adresse NVARCHAR(200),Emp_Commentaire NVARCHAR(MAX),Emp_Date_Adhésion date,Emp_Date_Creation date,Emp_Statut NVARCHAR(20),Emp_Evaluation int,[Emp_Image] image)";

                        cmd6.CommandText = "CREATE TABLE P_Auteur (Aut_Id INT IDENTITY PRIMARY KEY NOT NULL, Aut_Intitule NVARCHAR(MAX),Aut_Statut VARCHAR(10))";
                        cmd7.CommandText = "CREATE TABLE P_Editeur (Edit_Id INT IDENTITY PRIMARY KEY NOT NULL, Edit_Intitule NVARCHAR(MAX),Edit_Statut VARCHAR(10))";
                        cmd8.CommandText = "CREATE TABLE P_Provenance (Prov_Id INT IDENTITY PRIMARY KEY NOT NULL,  Prov_Intitule NVARCHAR(MAX),Prov_Statut VARCHAR(10))";
                        cmd9.CommandText = "CREATE TABLE P_Prefacier (Pref_Id INT IDENTITY PRIMARY KEY NOT NULL, Pref_Intitule NVARCHAR(MAX),Pref_Statut VARCHAR(10))";


                        cmd12.CommandText = "CREATE TABLE P_Genre (Genr_Id INT IDENTITY PRIMARY KEY NOT NULL, Genr_Intitule NVARCHAR(MAX),Genr_Statut VARCHAR(10))";
                        cmd13.CommandText = "CREATE TABLE P_Sous_Genre (S_Genr_Id INT IDENTITY PRIMARY KEY NOT NULL,S_Genr_Intitule NVARCHAR(MAX),S_Genr_Statut VARCHAR(20),[Genr_Id] int)";



                        cmd14.CommandText = "CREATE TABLE P_Formats (Form_Id INT IDENTITY PRIMARY KEY NOT NULL,Form_Intitule NVARCHAR(MAX),Form_Statut VARCHAR(10))";

                        cmd15.CommandText = "CREATE TABLE P_Publics (Pub_Id INT IDENTITY PRIMARY KEY NOT NULL,Pub_Intitule NVARCHAR(MAX),Pub_Statut VARCHAR(10))";
                        //cmd16.CommandText = "CREATE TABLE P_Collection (Col_Id INT IDENTITY PRIMARY KEY NOT NULL,Col_Intitule NVARCHAR(MAX),Col_Statut VARCHAR(10))";
                        cmd17.CommandText = "CREATE TABLE P_Scenariste (Scen_Id INT IDENTITY PRIMARY KEY NOT NULL,Scen_Intitule NVARCHAR(MAX),Scen_Statut VARCHAR(10))";
                        cmd18.CommandText = "CREATE TABLE P_Dessinateur (Dess_Id INT IDENTITY PRIMARY KEY NOT NULL,Dess_Intitule NVARCHAR(MAX),Dess_Statut VARCHAR(10))";

                        cmd19.CommandText = "CREATE TABLE P_Emplacement (Empl_Id INT IDENTITY PRIMARY KEY NOT NULL,Code_Intitule NVARCHAR(10),Empl_Intitule NVARCHAR(MAX),Empl_Statut VARCHAR(10))";

                        cmd21.CommandText = "CREATE TABLE T_Cpt_Livre (Cpt_Id INT IDENTITY PRIMARY KEY NOT NULL,Liv_Id int,Cpt_Nbr_Exemplaire int)";

                        cmd22.CommandText = "CREATE TABLE T_Livre (Liv_Id INT IDENTITY PRIMARY KEY NOT NULL,Liv_Titre_Livre NVARCHAR(MAX),Liv_Titre_Original NVARCHAR(MAX),Liv_Resume NVARCHAR(MAX),Liv_Nbr_Exemplaire int,	Liv_Annee_Parution date,Liv_Nbr_Page int,Liv_Note float,Liv_Prix decimal(18,2),Liv_Etat VARCHAR(30),Liv_Modele NVARCHAR(30),Liv_Langue NVARCHAR(30),Liv_Personnage1 NVARCHAR(MAX),Liv_Personnage2 NVARCHAR(MAX),[Liv_ISBN10] VARCHAR(10),[Liv_ISBN13] VARCHAR(13),Liv_Id_Traduit_Par int,Liv_Date_Traduction date,Liv_Id_Prefacier int,Liv_Id_Auteur int,Liv_Id_Editeur int,Liv_Id_Genre int,Liv_Id_Sous_Genre int,Liv_Id_Provenance int,Liv_Id_Format int,Liv_Id_Public int,Liv_Id_Scenariste int,Liv_Id_Dessinateur int,Liv_Id_Emplacement int,Liv_Date_Creation date,Liv_Commentaire NVARCHAR(MAX),Liv_Image image,Liv_Statut VARCHAR(10))";

                        cmd23.CommandText = "CREATE TABLE P_TraduitPar (TraduitPar_Id INT IDENTITY PRIMARY KEY NOT NULL,TraduitPar_Intitule NVARCHAR(MAX),TraduitPar_Statut VARCHAR(10))";

                        cmd24.CommandText = "CREATE TABLE T_Pret (Pre_Id INT IDENTITY PRIMARY KEY NOT NULL,  Pre_Date_Pret date,Pre_Date_Retour_Prevu date,Pre_Date_Prolongement date,Pre_Date_Rappel date,Pre_Date_Rendu date,	Pre_Id_emprunteur int,Pre_Id_Livre int,Pre_Statut int,Pre_Commentaire NVARCHAR(MAX),[Pre_TotalJour] int,[Pre_Liv_Etat] varchar(50))";

                        cmd3.Connection = DAL.sqlconnection;
                        cmd5.Connection = DAL.sqlconnection;
                        cmd6.Connection = DAL.sqlconnection;
                        cmd7.Connection = DAL.sqlconnection;
                        cmd8.Connection = DAL.sqlconnection;
                        cmd9.Connection = DAL.sqlconnection;
                        cmd12.Connection = DAL.sqlconnection;
                        cmd13.Connection = DAL.sqlconnection;
                        cmd14.Connection = DAL.sqlconnection;
                        cmd15.Connection = DAL.sqlconnection;
                        //cmd16.Connection = DAL.sqlconnection;
                        cmd17.Connection = DAL.sqlconnection;
                        cmd18.Connection = DAL.sqlconnection;
                        cmd19.Connection = DAL.sqlconnection;
                        cmd21.Connection = DAL.sqlconnection;
                        cmd22.Connection = DAL.sqlconnection;
                        cmd23.Connection = DAL.sqlconnection;
                        cmd24.Connection = DAL.sqlconnection;

                        try
                        {







                            cmd3.ExecuteNonQuery();
                            cmd5.ExecuteNonQuery();
                            cmd6.ExecuteNonQuery();
                            cmd7.ExecuteNonQuery();
                            cmd8.ExecuteNonQuery();
                            cmd9.ExecuteNonQuery();
                            cmd12.ExecuteNonQuery();
                            cmd13.ExecuteNonQuery();
                            cmd14.ExecuteNonQuery();
                            cmd15.ExecuteNonQuery();
                            //cmd16.ExecuteNonQuery();
                            cmd17.ExecuteNonQuery();
                            cmd18.ExecuteNonQuery();
                            cmd19.ExecuteNonQuery();
                            cmd21.ExecuteNonQuery();
                            cmd22.ExecuteNonQuery();
                            cmd23.ExecuteNonQuery();
                            cmd24.ExecuteNonQuery();



                        }

                        catch(Exception ex)
                        {
                            Toast.show(this, user, ex.Message, ToastType.ERROR, ToastDuration.SHORT);
                            SplashScreenManager.CloseForm();
                            return;
                            }




                        // Insertion table férié
                        SqlCommand cmd20 = new SqlCommand();
                        cmd20.CommandText = "INSERT INTO [dbo].[P_Bibliotheque]([Bib_Code],[Bib_Intitule],[Bib_Etablissement],[Bib_Adresse],[Bib_Ville],[Bib_Code_Postal],[Bib_Tel],[Bib_Fax],[Bib_Email],[Bib_Logo],[Bib_Regle_Pret]) VALUES  ('B10','Intitule_Biblio','Etablissement_Biblio','Adresse_Biblio','Ville','Code_Postal','Tel','Fax','Email','Logo','0')";
                        cmd20.Connection = DAL.sqlconnection;

                        cmd20.ExecuteNonQuery();




                        Toast.show(this, user, "Vous  êtes  connecté  à  " + "  " + Properties.Settings.Default.Server, ToastType.INFO, ToastDuration.SHORT);
                        Thread.Sleep(10);
                        SplashScreenManager.CloseForm();

                        return;
                    }

                }
                catch
                {

                    Toast.show(this, user, "Impossible  de  se  connecter  à " + " " + Properties.Settings.Default.Server, ToastType.ERROR, ToastDuration.SHORT);

                 
                    //Thread.Sleep(10);
                    //SplashScreenManager.CloseForm();
               
                 
                    return;

                }

            }




         }

 
    }
}
