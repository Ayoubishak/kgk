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
using System.Data.SqlClient;
using Tulpep.NotificationWindow;
using kp.Toaster;

namespace Biblio
{
    public partial class Biblio : DevExpress.XtraEditors.XtraUserControl
    {


        DataAccessLayer DAL;
        public string image;
        public string CodeBiblio;
        private static Biblio _instance;

        public static Biblio instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new Biblio();

                }
                return _instance;

            }
        }


        public Biblio()
        {
            InitializeComponent();

            try
            {
                pictureBox_logo.ImageLocation = Properties.Settings.Default.imgLogo;
            }
            catch 
            {

                return;
            }
        }

        private void gridControl_Biblio_Click(object sender, EventArgs e)
        {

        }


        // Supprimer Logo
        private void pictureBox_sup_Click(object sender, EventArgs e)
        {

            pictureBox_logo.Image = Properties.Resources.plus;
            Properties.Settings.Default.imgLogo = image;
            Properties.Settings.Default.Save();

        }


        // Upload logo 
        private void pictureBox_upload_Click(object sender, EventArgs e)
        {
            // rechercher emplacemment des photos 
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "ملفات الصور |*.png; *.jpg; *.gif; *.bmp";
            if (op.ShowDialog() == DialogResult.OK)
                pictureBox_logo.Image = Image.FromFile(op.FileName);
            image = op.FileName;

            Properties.Settings.Default.imgLogo = image;
            Properties.Settings.Default.Save();
            pictureBox_logo.Enabled = true;
        }

        private void repository_Consulter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            try
            {
                textBox_Inte.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Intitule").ToString();
                textBox_Etab.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Etablissement").ToString();
                textBox_Adress.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Adresse").ToString();
                textBox_Ville.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Ville").ToString();

                textBox_CodePostal.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Code_Postal").ToString();
                textBox_tele.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Tel").ToString();
                textBox_Fax.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Fax").ToString();


                textBox_Email.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Email").ToString();
                textBox_ReglePret.Text = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Regle_Pret").ToString();

                CodeBiblio = gridView_Biblio.GetRowCellValue(gridView_Biblio.FocusedRowHandle, "Bib_Code").ToString();


            }
            catch 
            {

             
            }
     
    }

        private void windowsUIButtonPanel2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            // Ajouter
            if (e.Button == windowsUIButtonPanel2.Buttons[0])
            {



                if (textBox_Etab.Text != "" && textBox_Inte.Text != "" && CodeBiblio != "" && CodeBiblio != null)
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


                    DialogResult rslt = MessageBox.Show("" + "Admin" + "---" + "Voulez-vous Vraiment Modifier " + "--" + textBox_Inte.Text + " -- --" + textBox_Etab.Text + "  Maintenant  ??", "la Modification " + "--" + textBox_Etab.Text + "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (rslt == DialogResult.Yes && textBox_Inte.Text != null && textBox_Etab.Text != "")
                    {



                        double Res1 = double.Parse(textBox_ReglePret.Text.Replace('.',','));
                        var Res2 = string.Format("{0:N1}", Res1);

                        Properties.Settings.Default.RegleJour = Res2;
                        Properties.Settings.Default.Save();

                        SqlCommand Del = new SqlCommand();
                        Del.CommandText = "update [dbo].[P_Bibliotheque] set [Bib_Intitule]='"+textBox_Inte.Text.Trim()+"',[Bib_Etablissement]='"+textBox_Etab.Text.Trim()+"',[Bib_Adresse]='"+textBox_Adress.Text.Trim()+"',[Bib_Ville]='"+textBox_Ville.Text.Trim()+"',[Bib_Code_Postal]='"+textBox_CodePostal.Text.Trim()+"',[Bib_Tel]='"+textBox_tele.Text.Trim()+"',[Bib_Fax]='"+textBox_Fax.Text.Trim()+ "',[Bib_Email]='"+textBox_Email.Text.Trim()+ "',[Bib_Regle_Pret]='"+Res2.Replace(',','.')+"' where [Bib_Code]='"+ CodeBiblio.Trim() + "'";
                        Del.Connection = DAL.sqlconnection;
                        Del.ExecuteNonQuery();



                        // Afficher 



                        string sql = "select [Bib_Id],[Bib_Code],[Bib_Intitule],[Bib_Etablissement],[Bib_Adresse],[Bib_Ville],[Bib_Code_Postal],[Bib_Tel],[Bib_Fax],[Bib_Email],[Bib_Logo],[Bib_Regle_Pret] from [dbo].[P_Bibliotheque]";
                        //SqlConnection connection = new SqlConnection(connectionString);
                        SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
                        DataSet ds = new DataSet();
                        dataadapter.Fill(ds, "table");
                        gridControl_Biblio.DataSource = ds;
                        gridControl_Biblio.DataMember = "table";
                        gridView_Biblio.BestFitColumns();



                        // Fin Afficher 



                        toastNotificationsManager1.ShowNotification("ab262be1-e5ec-47a1-a52b-82d8cc20d26a");
                        //Toast.show(this, "OptimumBiblio", "La Modification   a   été   effectuée   avec   succès  -    Merci   !!!!!!  .", ToastType.INFO, ToastDuration.SHORT);

                        Vider();
                        
                        return;

                    }


                  







                }

                else
                {
                    //Toast.show(this,"Admin", "Veuillez svp Sélectionner la ligne à  Modifier -    Merci   !!!!!!  .", ToastType.INFO, ToastDuration.SHORT);

                    



                    toastNotificationsManager1.ShowNotification("032552b9-b034-4976-8ec6-edb85e0837b5");

                  


                



                   

                    return;
                }




            }
       }



        // Procedur Vider;
        public void Vider()
        {
            textBox_Inte.Text = "";
            textBox_Etab.Text = "";
            textBox_Adress.Text = "";
            textBox_Ville.Text = "";
            textBox_CodePostal.Text = "";
            textBox_tele.Text = "";
            textBox_Fax.Text = "";
            textBox_Email.Text = "";
            textBox_ReglePret.Text = "";
            CodeBiblio = "";
            textBox_Inte.Focus();

        }
    }
}
