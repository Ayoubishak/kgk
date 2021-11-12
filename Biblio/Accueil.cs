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

namespace Biblio
{
    public partial class Accueil : DevExpress.XtraEditors.XtraUserControl
    {

        private static Accueil _instance;

        public static Accueil instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new Accueil();

                }
                return _instance;

            }
        }




        public Accueil()
        {
            InitializeComponent();
        }
    }
}
