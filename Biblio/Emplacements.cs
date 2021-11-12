using kp.Toaster;
using System;
using System.Collections;
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
    public partial class Emplacements : Form
    {


        DataAccessLayer DAL;
        public string  xxx4;

        public List<string> Lettre2 = new List<string> { };
        public Emplacements()
        {
            InitializeComponent();
            textBox_Allee.Focus();
            textBox_Allee.Select();
        }




        //  Page Suivant Avec le Code
        private void windowsPanel_P1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

            if (e.Button == windowsPanel_P1.Buttons[0])
            {
                if (textBox_Allee.Text == "0" || textBox_Allee.Text == null || textBox_Allee.Text == "")
                {


                    textBox_Allee.Focus();
                    textBox_Allee.Select();

                    Toast.show(this, "Info - OptimumBiblio", "Veuillez  Svp  remplir le  Champ  Allées  Merci !", ToastType.INFO, ToastDuration.SHORT);
                  
                }

                else
                {
                    panel_P2.Visible = true;
                    panel_P1.Visible = false;
                    label_Codification.Text = label_CompteAllee.Text;
                    textBox_PremierAllee.Focus();
                    textBox_PremierAllee.Select();
                }
            }

            if (e.Button == windowsPanel_P1.Buttons[3])
            {
                panel_P2.Visible = false;
                panel_P1.Visible = true;
            }
        }

        private void textBox_Allee_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;

              if (!char.IsDigit(chr) && chr!=8)
            {
                label_CompteAllee.Text ="0";
                e.Handled = true;
            }
          
        }

        private void textBox_Allee_TextChanged(object sender, EventArgs e)
        {

            var Nombre = textBox_Allee.Text;


            if (textBox_Allee.Text == "" || textBox_Allee.Text == null)
            {
                label_CompteAllee.Text = "0";
            }
            else if(Int32.Parse(Nombre)>26)
            {
                label_CompteAllee.Text ="26";
                textBox_Allee.Text="26";
            }

            else
            {
                label_CompteAllee.Text = textBox_Allee.Text;
            }
           

          
        }

        private void textBox_Trav_TextChanged(object sender, EventArgs e)
        {
            var Allee = textBox_Allee.Text;
            var Trav = textBox_Trav.Text;

            if ((textBox_Trav.Text == "" || textBox_Trav.Text == null || textBox_Trav.Text == "0"))
            {
                label_CompteAllee.Text = "0";

                if ((textBox_Allee.Text != "" || textBox_Allee.Text != null || textBox_Allee.Text != "0"))
                {
                    label_CompteAllee.Text = textBox_Allee.Text;
                }


            }
            else if (Int32.Parse(Trav) > 26)
            {
                label_CompteAllee.Text = "26";
                textBox_Trav.Text = "26";
            }


            else if ((textBox_Allee.Text != "" || textBox_Allee.Text != null || textBox_Allee.Text != "0") && (textBox_Trav.Text != "" || textBox_Trav.Text != null || textBox_Trav.Text != "0"))
            {
                var Res = (Int32.Parse(Trav)) * (Int32.Parse(Allee));
                label_CompteAllee.Text = Res.ToString();
            }

            else if ((textBox_Allee.Text != "" || textBox_Allee.Text != null || textBox_Allee.Text != "0") && (textBox_Trav.Text == "" || textBox_Trav.Text == null || textBox_Trav.Text == "0"))
            {
               
                label_CompteAllee.Text = textBox_Allee.Text;
            }


            else
            {
                label_CompteAllee.Text = textBox_Trav.Text;
            }
        }

        private void textBox_Trav_KeyPress(object sender, KeyPressEventArgs e)
      {
            char chr = e.KeyChar;

            if (!char.IsDigit(chr) && chr != 8)
            {
                label_CompteAllee.Text = "0";
                e.Handled = true;
            }
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

            if (e.Button == windowsUIButtonPanel1.Buttons[0])
            {
                panel_P2.Visible = false;
                panel_P1.Visible = true;
            }

            if (e.Button == windowsUIButtonPanel1.Buttons[3])
            {
                // tEST cOLLECTION

                this.Close();
            }

        }


        private void textBox_PremierAllee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) == true)
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
        }

        private void textBox_PremierAllee_TextChanged(object sender, EventArgs e)
        {

            var Lett = textBox_PremierAllee.Text;
            var Com = label_Codification.Text;


            List<string> Lettre = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            List<string> Lettre3 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26" };


          
            {



                foreach (string i in Lettre)
                {

                    if (i == Lett)
                    {
                        var xx2 = i;
                        var xx3 = Lettre.IndexOf(xx2);



                        int res = (xx3 + Int32.Parse(Com)) - 1;

                        if (res >= 26)
                        {
                            res = 25;
                        }


                        xxx4 = Lettre.ElementAt(res);

                        textBox_DernierAllee.Text = xxx4.ToString();
                        label_Dernier.Text = xxx4.ToString();


                        //Lettre2.Add(xxx4);

                        for (int j = xx3; j < res + 1; j++)
                        {



                            var ff = Lettre.ElementAt(j);

                            Lettre2.Add(ff);




                        }

                    }





                }

            }
        }

        private void textBox_IntituleAlle_TextChanged(object sender, EventArgs e)
        {
            var T = textBox_IntituleAlle.Text;
            var D = textBox_DernierAllee.Text;
            var PR = textBox_PremierAllee.Text;


            if ((T!="" && T!=null) && (D != "" && D != null) && (PR != "" && PR != null))
            {
                simpleButton_generer.Visible = true;
                pictureEdit_arrow.Visible = true;
            }
            else
            {
                simpleButton_generer.Visible = false;
                pictureEdit_arrow.Visible = false;
            }
        }

        private void simpleButton_generer_Click(object sender, EventArgs e)
        {
            //var Res =Int32.Parse(label_CompteAllee.Text);

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


            foreach (string i in Lettre2)
            {

                SqlCommand cmdAjouterB = new SqlCommand();

                cmdAjouterB.CommandText = "INSERT INTO [dbo].[P_Emplacement] ([Code_Intitule],[Empl_Intitule],[Empl_Statut]) VALUES  (@CodeIntitule,@EmplIntitule,@EmplStatut)";
                cmdAjouterB.Connection = DAL.sqlconnection;

                cmdAjouterB.Parameters.AddWithValue("@CodeIntitule", SqlDbType.VarChar);
                cmdAjouterB.Parameters["@CodeIntitule"].Value = i;

                cmdAjouterB.Parameters.AddWithValue("@EmplIntitule", SqlDbType.VarChar);
                cmdAjouterB.Parameters["@EmplIntitule"].Value = textBox_IntituleAlle.Text + " " + i;


                cmdAjouterB.Parameters.AddWithValue("@EmplStatut", SqlDbType.VarChar);
                cmdAjouterB.Parameters["@EmplStatut"].Value = "A";


                cmdAjouterB.ExecuteNonQuery();


               
            }



            Lettre2.Clear();

            AffichageCodeEmplacement AffichageCodeEmplacemen = new AffichageCodeEmplacement();

            string sql = "select [Code_Intitule],[Empl_Intitule] from [dbo].[P_Emplacement]";
            //SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, DAL.sqlconnection);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "table");

            AffichageCodeEmplacemen.gridControl_CodeEm.DataSource = ds;
            AffichageCodeEmplacemen.gridControl_CodeEm.DataMember = "table";
            AffichageCodeEmplacemen.gridView_CodeEm.BestFitColumns();


    

            PopupNotifier pop = new PopupNotifier();
            pop.Image = Properties.Resources.OK;
            pop.TitleText = "Se connecter au serveur";
            pop.ContentText = "OptimumBiblio" + " -- " + "Enregistrement  a  été  effectué  avec  succès";
            pop.Popup();
            //Toast.show(this, "OptimumBiblio", "Enregistrement a  été effectué avec  succès.", ToastType.INFO, ToastDuration.SHORT);
            AffichageCodeEmplacemen.ShowDialog();
        }
    }
}
