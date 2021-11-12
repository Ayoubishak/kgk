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
using Tulpep.NotificationWindow;
using System.Data.SqlClient;
using kp.Toaster;
using System.IO;

namespace Biblio
{
    public partial class Emprunteur : DevExpress.XtraEditors.XtraUserControl
    {


        DataAccessLayer DAL;
        public string chainephoto;
        Image ImageEmprunteur;
        private static Emprunteur _instance;

        public static Emprunteur instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new Emprunteur();

                }
                return _instance;

            }
        }



        public Emprunteur()
        {
            InitializeComponent();
        }


        // Supprimer Photo

        private void pictureBox_sup_Click(object sender, EventArgs e)
        {


            pictureBox_photo.Image = null;
         
            //chainephoto = null;
            //chainephoto = "";
            pictureBox_photo.Image = Properties.Resources.q;
          

        }


        // Upload Photo

        private void pictureBox_upload_Click(object sender, EventArgs e)
        {
            try
            {

               

                // rechercher emplacemment des photos 
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "ملفات الصور |*.png; *.jpg; *.gif; *.bmp";
                if (op.ShowDialog() == DialogResult.OK)
                    pictureBox_photo.Image = Image.FromFile(op.FileName);
                ImageEmprunteur= Image.FromFile(op.FileName);

                chainephoto = op.FileName;
                Properties.Settings.Default.phot = chainephoto;
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



        // Evennement Ajouter
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button == windowsUIButtonPanel1.Buttons[0])
            {

                if (textBox_Matricule.Text != "" && textBox_Nom.Text != "" && textBox_Prenom.Text != "")
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




                    //try
                    //{

                    SqlCommand cmdInsert = new SqlCommand();

                    cmdInsert.CommandText = "INSERT INTO [dbo].[T_Emprunteur] ([Emp_Matricule],[Emp_Badge],[Emp_Code_Barre],[Emp_Nom],[Emp_Prenom],[Emp_CIN],[Emp_Login],[Emp_Password],[Emp_Email],[Emp_Tel],[Emp_Adresse],[Emp_Commentaire],[Emp_Date_Adhésion],[Emp_Date_Creation],[Emp_Statut],[Emp_Evaluation],[Emp_Image]) VALUES  (@Matricule,@Badge,@CodeBarre,@Nom,@Prenom,@CIN,@Login,@Password,@Email,@Tel,@Adresse,@Commentaire,@DateAdhesion,@DateCreation,@Statut,@Evaluation,@Image)";
                    cmdInsert.Connection = DAL.sqlconnection;





                    dateTimePicker_DateAdhesion.Format = DateTimePickerFormat.Custom;
                    dateTimePicker_DateAdhesion.Format = DateTimePickerFormat.Custom;
                    dateTimePicker_DateAdhesion.CustomFormat = "yyyy-MM-dd";
                    dateTimePicker_DateAdhesion.CustomFormat = "yyyy-MM-dd";
                    var DateAdhesion = dateTimePicker_DateAdhesion.Value.ToString("yyyy-MM-dd");

                    var DateCriation = DateTime.Now.ToString("yyyy-MM-dd");



                    cmdInsert.Parameters.AddWithValue("@Matricule", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Matricule"].Value = textBox_Matricule.Text.Trim();


                    if (textBox_Badge.Text.Trim() == "" || textBox_Badge.Text.Trim() == null)
                    {
                        textBox_Badge.Text = "-";
                    }





                    cmdInsert.Parameters.AddWithValue("@Badge", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Badge"].Value = textBox_Badge.Text.Trim();



                    if (textBox_CodeBare.Text.Trim() == "" || textBox_CodeBare.Text.Trim() == null)
                    {
                        textBox_CodeBare.Text = "-";
                    }


                    cmdInsert.Parameters.AddWithValue("@CodeBarre", SqlDbType.VarChar);
                    cmdInsert.Parameters["@CodeBarre"].Value = textBox_CodeBare.Text.Trim();


                    cmdInsert.Parameters.AddWithValue("@Nom", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Nom"].Value = textBox_Nom.Text.Trim();



                    cmdInsert.Parameters.AddWithValue("@Prenom", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Prenom"].Value = textBox_Prenom.Text.Trim();




                    if (textBox_CIN.Text.Trim() == "" || textBox_CIN.Text.Trim() == null)
                    {
                        textBox_CIN.Text = "-";
                    }


                    cmdInsert.Parameters.AddWithValue("@CIN", SqlDbType.VarChar);
                    cmdInsert.Parameters["@CIN"].Value = textBox_CIN.Text.Trim();





                    if (textBox_Identity.Text.Trim() == "" || textBox_Identity.Text.Trim() == null)
                    {
                        textBox_Identity.Text = "-";
                    }





                    cmdInsert.Parameters.AddWithValue("@Login", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Login"].Value = textBox_Identity.Text.Trim();


                    if (textBox_Passe.Text.Trim() == "" || textBox_Passe.Text.Trim() == null)
                    {
                        textBox_Passe.Text = "-";
                    }




                    cmdInsert.Parameters.AddWithValue("@Password", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Password"].Value = textBox_Passe.Text.Trim();


                    if (textBox_Email.Text.Trim() == "" || textBox_Email.Text.Trim() == null)
                    {
                        textBox_Email.Text = "-";
                    }


                    cmdInsert.Parameters.AddWithValue("@Email", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Email"].Value = textBox_Email.Text.Trim();




                    if (textBox_Tele.Text.Trim() == "" || textBox_Tele.Text.Trim() == null)
                    {
                        textBox_Tele.Text = "-";
                    }

                    cmdInsert.Parameters.AddWithValue("@Tel", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Tel"].Value = textBox_Tele.Text.Trim();



                    cmdInsert.Parameters.AddWithValue("@Adresse", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Adresse"].Value = memoExEdit_Adress.Text;



                    cmdInsert.Parameters.AddWithValue("@Commentaire", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Commentaire"].Value = memoExEdit_CommentaireE.Text;



                    cmdInsert.Parameters.AddWithValue("@DateAdhesion", SqlDbType.VarChar);
                    cmdInsert.Parameters["@DateAdhesion"].Value = DateAdhesion;







                    cmdInsert.Parameters.AddWithValue("@DateCreation", SqlDbType.VarChar);
                    cmdInsert.Parameters["@DateCreation"].Value = DateCriation;




                    cmdInsert.Parameters.AddWithValue("@Statut", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Statut"].Value = "A";



                    var Start = ratingControl_Evaluation.EditValue;
                    if (Start == null)
                    {

                        ratingControl_Evaluation.EditValue = 0;
                    }




                    cmdInsert.Parameters.AddWithValue("@Evaluation", SqlDbType.VarChar);
                    cmdInsert.Parameters["@Evaluation"].Value = ratingControl_Evaluation.EditValue;



                    ImageConverter Convertimg = new ImageConverter();
                    byte[] ImageByte = (byte[])Convertimg.ConvertTo(ImageEmprunteur, typeof(byte[]));




                    cmdInsert.Parameters.AddWithValue("@Image", SqlDbType.Image);
                    cmdInsert.Parameters["@Image"].Value = ImageByte;






                    cmdInsert.ExecuteNonQuery();

                   




                    //}
                    //catch 
                    //{


                    //}





                    // debut Load les information Socéité Choisi

                    string sql = "select [Emp_Id],[Emp_Matricule],[Emp_Badge],[Emp_Code_Barre],[Emp_Nom],[Emp_Prenom],[Emp_CIN],[Emp_Login],[Emp_Password],[Emp_Email],[Emp_Tel],[Emp_Adresse],[Emp_Commentaire],[Emp_Date_Adhésion],[Emp_Date_Creation],[Emp_Statut],[Emp_Evaluation],[Emp_Image] from [dbo].[T_Emprunteur]";
                    //SqlConnection connection = new SqlConnection(connectionString);
                    SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "table");
                    gridControl_Emprunteur.DataSource = ds;
                    gridControl_Emprunteur.DataMember = "table";






                    gridView_Emprunteur.BestFitColumns();


                    Toast.show(this, "Admin", "Enregistrement  " + "  a été effectuée avec succès  Merci !!", ToastType.INFO, ToastDuration.SHORT);


                    Vider();




                }



            }

            if (e.Button == windowsUIButtonPanel1.Buttons[6])
            {
                Vider();
            }


        

            if (e.Button == windowsUIButtonPanel1.Buttons[4])
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





                SqlCommand Del = new SqlCommand();
                Del.CommandText = "delete [dbo].[T_Emprunteur]  where [Emp_Id]='" +Int32.Parse(label_Mat.Text) + "'";
                Del.Connection = DAL.sqlconnection;
                Del.ExecuteNonQuery();






                string sql = "select [Emp_Id],[Emp_Matricule],[Emp_Badge],[Emp_Code_Barre],[Emp_Nom],[Emp_Prenom],[Emp_CIN],[Emp_Login],[Emp_Password],[Emp_Email],[Emp_Tel],[Emp_Adresse],[Emp_Commentaire],[Emp_Date_Adhésion],[Emp_Date_Creation],[Emp_Statut],[Emp_Evaluation],[Emp_Image] from [dbo].[T_Emprunteur]";
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "table");

                Emprunteur.instance.gridControl_Emprunteur.DataSource = ds;
                Emprunteur.instance.gridControl_Emprunteur.DataMember = "table";
                Emprunteur.instance.gridView_Emprunteur.BestFitColumns();



                toastNotificationsManager1.ShowNotification("0f6a4f52-3e1c-4989-afb4-4cf8b9226470");

                Vider();
            }

        }

        private void repository_Consulter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {


            try
            {
                var Eva = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Evaluation").ToString();

                if (Eva == "5")
                {
                    ratingControl_Evaluation.EditValue = 5;
                }

                else if (Eva == "1")
                {
                    ratingControl_Evaluation.EditValue = 1;
                }


                else if (Eva == "2")
                {
                    ratingControl_Evaluation.EditValue = 2;
                }

                else if (Eva == "3")
                {
                    ratingControl_Evaluation.EditValue = 3;
                }



                else if (Eva == "4")
                {
                    ratingControl_Evaluation.EditValue = 4;
                }
                else
                {
                    ratingControl_Evaluation.EditValue = 0;

                }


                textBox_Matricule.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Matricule").ToString();
                textBox_Badge.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Badge").ToString();
                textBox_CodeBare.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Code_Barre").ToString();
                textBox_Nom.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Nom").ToString();
                textBox_Prenom.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Prenom").ToString();
                textBox_Identity.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Login").ToString();
                textBox_Identity.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Login").ToString();
                textBox_Passe.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Password").ToString();
                textBox_Email.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Email").ToString();
                textBox_Tele.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Tel").ToString();
                memoExEdit_Adress.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Adresse").ToString();
                memoExEdit_CommentaireE.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Commentaire").ToString();

                dateTimePicker_DateAdhesion.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Date_Adhésion").ToString();

                textBox_CIN.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_CIN").ToString();


                label_Mat.Text = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Id").ToString();

                




                var ImagePr = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Image");
                MemoryStream Ms = new MemoryStream((byte[])ImagePr);
                pictureBox_photo.Image = new Bitmap(Ms);
            }
            catch 
            {
                pictureBox_photo.Image = Properties.Resources.q;


            }



       




        }

      



        public void Vider()
        {

            textBox_Matricule.Text = "";
            textBox_Nom.Text = "";
            textBox_Prenom.Text = "";
            textBox_CIN.Text = "";
            textBox_Badge.Text = "";
            textBox_CodeBare.Text = "";
            textBox_Identity.Text = "";
            textBox_Tele.Text = "";
            dateTimePicker_DateAdhesion.Text = DateTime.Now.ToLongDateString();
            textBox_Email.Text = "";
            ratingControl_Evaluation.EditValue = 0;
            textBox_Passe.Text = "";
            textBox_Matricule.Focus();
            pictureBox_photo.Image = Properties.Resources.q;
            memoExEdit_Adress.Text = "";
            memoExEdit_CommentaireE.Text = "";

        }

        private void barButtonItem_impression_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }


        public void listeEmp()
        {
            string sql = "select [Emp_Id],[Emp_Matricule],[Emp_Badge],[Emp_Code_Barre],[Emp_Nom],[Emp_Prenom],[Emp_CIN],[Emp_Login],[Emp_Password],[Emp_Email],[Emp_Tel],[Emp_Adresse],[Emp_Commentaire],[Emp_Date_Adhésion],[Emp_Date_Creation],[Emp_Statut],[Emp_Evaluation],[Emp_Image] from [dbo].[T_Emprunteur]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");
            gridControl_Emprunteur.DataSource = ds;
            gridControl_Emprunteur.DataMember = "table";
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

                var ID = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Id").ToString();
                var Matricule = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Matricule").ToString();
                var Nom = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Nom").ToString();
                var Prenom = gridView_Emprunteur.GetRowCellValue(gridView_Emprunteur.FocusedRowHandle, "Emp_Prenom").ToString();



                if (ID != "" || ID != null)
                {


                    DialogResult rslt = MessageBox.Show("" + "Admin" + "---" + "Voulez-vous Vraiment Supprimer " + "--" + Nom + " -- --" + Prenom + "  Maintenant  ??", "la Suppression " + "--" + Nom +  " --- " +  Prenom + " ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (rslt == DialogResult.Yes)
                    {






                        SqlCommand Del = new SqlCommand();
                        Del.CommandText = "delete [dbo].[T_Emprunteur]  where [Emp_Id]='" + Int32.Parse(ID) + "'";
                        Del.Connection = DAL.sqlconnection;
                        Del.ExecuteNonQuery();


                        Toast.show(this, "Info - OptimumBiblio", "La Suppression a été effectuée avec succès - Merci !!!!!!", ToastType.INFO, ToastDuration.SHORT);

                        listeEmp();
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
    }
}
