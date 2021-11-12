using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Biblio
{
    class DataAccessLayer
    {
        // declarer un variable globale pour tester la cx si 1 true si !=1 false 
        public int test = 1;
        //Declarer un objet connexion
        public SqlConnection sqlconnection;
        public DataAccessLayer(string conctionString)
        { 
            sqlconnection = new SqlConnection(conctionString);
        }
        // function qui retourn true ou false pour tester 
        //la connxion entre la base et application
        public bool Iscon
        {
            get
            {
                if (sqlconnection.State==System.Data.ConnectionState.Closed)
                
                    sqlconnection.Open();
                    return true;
            }
        }

    }
}
