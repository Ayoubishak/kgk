using Google.Apis.Books.v1;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblio
{
    public partial class WebLivre : Form
    {
        public WebLivre()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        public class BookApi
        {
            private readonly BooksService _booksService;
            public BookApi(string apiKey)
            {
                _booksService = new BooksService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey,
                    ApplicationName = this.GetType().ToString()
                });
            }
        }




        public void Ajouter()
        {
            DataTable dt = new DataTable();


            BooksService booksServic = new BooksService();


            var Txt = textEdit_Titre.Text.Trim();


            var listquery = booksServic.Volumes.List(Txt);
            //listquery.MaxResults = 20;
            //listquery.StartIndex = offset;
      
          
        

           var  res = listquery.Execute();


            if (res.TotalItems == 0)
            {
                return;
            }

            dt.Columns.Add(new DataColumn("Titre du livre"));
            dt.Columns.Add(new DataColumn("Nombre de pages"));
            dt.Columns.Add(new DataColumn("Date de publication"));

            dt.Columns.Add(new DataColumn("Version"));
            dt.Columns.Add(new DataColumn("languee"));
            dt.Columns.Add(new DataColumn("Sous Titres"));
          
            dt.Columns.Add(new DataColumn("Authors"));
            dt.Columns.Add(new DataColumn("Editeur"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("ISBN10"));
            dt.Columns.Add(new DataColumn("ISBN13"));
            dt.Columns.Add(new DataColumn("Type Impression"));
            dt.Columns.Add(new DataColumn("URL Image"));
         



            foreach (var item in res.Items)
            {

                var Author = item.VolumeInfo.Authors;
                var Description = item.VolumeInfo.Description;
                var ISBN = item.VolumeInfo.IndustryIdentifiers;
                var Titre = item.VolumeInfo.Title;
                var Soustitres = item.VolumeInfo.Subtitle;
                var Type10 = "";
                var Type13 = "";
                var Image = item.VolumeInfo.ImageLinks;




                var PageCount = item.VolumeInfo.PageCount;
                var Version = item.VolumeInfo.ContentVersion;
                var DatePub = item.VolumeInfo.PublishedDate ;
                var Editeur = item.VolumeInfo.Publisher;
                var TypeImpression = item.VolumeInfo.PrintType;
                var languee = item.VolumeInfo.Language;

                var ISBN13Aj = "";
                var AuthorAj = "";
                var DescriptionAj = "";
                var ISBN10Aj = "";
                var TitreAj = "";
                var SoustitresAJ = "";
                var ImagesAJ = "";
                var PageCountAJ = "";
                var VersionAJ = "";
                var DatePubAJ = "";
                var TypeImpressionAJ = "";
                var langueeAJ = "";

                var EditeurAJ = "";

                //   1 Author

                if (Author != null)
                {
                    AuthorAj = item.VolumeInfo.Authors[0];
                }
                else
                {
                    AuthorAj = "-";
                }



                if (languee != null)
                {
                    langueeAJ = item.VolumeInfo.Language;

                    if(langueeAJ=="fr" || langueeAJ == "FR")
                    {
                        langueeAJ = "Français";
                    }

                   else if (langueeAJ == "en" || langueeAJ == "EN")
                    {
                        langueeAJ = "Anglais";
                    }

                  else  if (langueeAJ == "ar" || langueeAJ == "AR")
                    {
                        langueeAJ = "Arabe";
                    }


                   else if (langueeAJ == "es" || langueeAJ == "ES")
                    {
                        langueeAJ = "Espagnol";
                    }


                    else if (langueeAJ == "de" || langueeAJ == "DS")
                    {
                        langueeAJ = "Allemand";
                    }


                    



                }
                else
                {
                    langueeAJ = "-";
                }




                if (TypeImpression != null)
                {
                    TypeImpressionAJ = item.VolumeInfo.PrintType;
                }
                else
                {
                    TypeImpressionAJ = "-";
                }



                if (Editeur != null)
                {
                    EditeurAJ = item.VolumeInfo.Publisher;
                }
                else
                {
                    EditeurAJ = "-";
                }




                if (DatePub != null)
                {
                    DatePubAJ = item.VolumeInfo.PublishedDate;
                }
                else
                {
                    DatePubAJ = "-";
                }




                if (PageCount != null)
                {
                    PageCountAJ = item.VolumeInfo.PageCount.ToString();
                }
                else
                {
                    PageCountAJ = "-";
                }


                if (Version != null)
                {
                    VersionAJ = item.VolumeInfo.ContentVersion;
                }
                else
                {
                    VersionAJ = "-";
                }

                // 2 Description

                if (Description != null)
                {
                    DescriptionAj = item.VolumeInfo.Description;
                }
                else
                {
                    DescriptionAj = "-";
                }




                // 3 Title

                if (Titre != null)
                {
                    TitreAj = item.VolumeInfo.Title;
                }
                else
                {
                    TitreAj = "-";
                }

                // 4 Subtitle

                if (Soustitres != null)
                {
                    SoustitresAJ = item.VolumeInfo.Subtitle;
                }
                else
                {
                    SoustitresAJ = "-";
                }




                try
                {
                    if (ISBN != null) {


                        Type10 = item.VolumeInfo.IndustryIdentifiers[0].Type;

                    }


                  
                }
                catch 
                {

                  
                }


                try
                {

                    if (ISBN != null)
                    {


                        Type13 = item.VolumeInfo.IndustryIdentifiers[1].Type;

                    }
                }
                catch
                {


                }

                if (ISBN != null && Type10=="ISBN_10")
                {


                    try
                    {
                        ISBN10Aj = item.VolumeInfo.IndustryIdentifiers[0].Identifier;

                        if (ISBN10Aj.Length == 13)
                        {
                            ISBN13Aj = ISBN10Aj;
                        }
                      
                    }
                    catch 
                    {

                        ISBN10Aj = "-";
                    }
               
                   
                }
                else
                {
                    ISBN10Aj = "-";

                }



                if (ISBN != null && Type13 == "ISBN_13")
                {
                    try
                    {
                        ISBN13Aj = item.VolumeInfo.IndustryIdentifiers[1].Identifier;
                    }
                    catch 
                    {
                        ISBN13Aj = "-";

                    }

                }
                else
                {
                    ISBN13Aj = "-";

                }



                if (Image != null)
                {
                    ImagesAJ = item.VolumeInfo.ImageLinks.SmallThumbnail;
                }
                else
                {
                    ImagesAJ = "-";
                }


                dt.Rows.Add(TitreAj, PageCountAJ, DatePubAJ, VersionAJ,langueeAJ, SoustitresAJ, AuthorAj, EditeurAJ, DescriptionAj, ISBN10Aj, ISBN13Aj, TypeImpressionAJ,ImagesAJ);

            }

            gridControl_Web.DataSource = dt;


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Ajouter();
        }

        private void gridView_Web_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "URL Image") != null)
            {


                var URLIm = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "URL Image").ToString();
       
                var Authorss = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "Authors").ToString();
                var Description = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "Description").ToString();
                var Titrelivre = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "Titre du livre").ToString();
                var ISBN10 = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "ISBN10").ToString();
                var ISBN13 = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "ISBN13").ToString();
                var NbrPage = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "Nombre de pages").ToString();
                var Editeur = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "Editeur").ToString();
                var Datepublication = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "Date de publication").ToString();
                var languee = gridView_Web.GetRowCellValue(gridView_Web.FocusedRowHandle, "languee").ToString();

                if (URLIm == "-" || URLIm == "")
                {
                    pictureBox1.Image = null;
                  
                }

                string url = URLIm;


           


                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                    pictureBox1.Image = new Bitmap(resp.GetResponseStream());



                   


                }
                catch 
                {
                    textEdit_Authors.Text = Authorss;
                    memoExEdit_resume.Text = Description;
                    textEdit_TitreLivre.Text = Titrelivre;
                    textEdit_ISBN10.Text = ISBN10;
                    textEdit_ISBN13.Text = ISBN13;
                    textEdit_NBRPage.Text = NbrPage;
                    textEdit_Editeur.Text = Editeur;
                    textEdit_Date.Text = Datepublication;
                    textEdit_lg.Text = languee;

                }

                textEdit_Authors.Text = Authorss;
                memoExEdit_resume.Text = Description;
                textEdit_TitreLivre.Text = Titrelivre;
                textEdit_ISBN10.Text = ISBN10;
                textEdit_ISBN13.Text = ISBN13;
                textEdit_NBRPage.Text = NbrPage;
                textEdit_Editeur.Text = Editeur;
                textEdit_Date.Text = Datepublication;
                textEdit_lg.Text = languee;

            }






        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            Livres.instance.textEdit_TitreLivre.Text = textEdit_TitreLivre.Text;
            Livres.instance.spinEdit_NbrPages.Text = textEdit_NBRPage.Text;
            Livres.instance.textEdit_ISBN10.Text = textEdit_ISBN10.Text;
            Livres.instance.textEdit_ISBN13.Text = textEdit_ISBN13.Text;
            Livres.instance.memoExEdit_Resume.Text = memoExEdit_resume.Text;
            Livres.instance.gridLookUpEditla_langue.EditValue = textEdit_lg.Text;
            var res = textEdit_Date.Text.Length;

            if(res > 10)
            {
                Livres.instance.dateTimeOffsetEdit_DateParution.EditValue = textEdit_Date.Text.Substring(0, 10);
            }
            else if (res == 4)
            {
                Livres.instance.dateTimeOffsetEdit_DateParution.EditValue = textEdit_Date.Text+"-01-01";
            }
            else if (res == 7)
            {
                Livres.instance.dateTimeOffsetEdit_DateParution.EditValue = textEdit_Date.Text + "-01";
            }
            else
            {
                Livres.instance.dateTimeOffsetEdit_DateParution.EditValue = textEdit_Date.Text;
            }

        
        }
    }
}
