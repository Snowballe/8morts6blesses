using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class AdherentBasket_DAL<Type_DAL>
    {

        public int ID { get; private set; }
        public int ID_Adherent { get; private set; }
        public int ID_Basket { get; private set; }



        public void Insert(SqlConnection connection)
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=Geometrie;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {
                
                connexion.Open();
                using (var commande = new SqlCommand())
                {
                    
                    commande.Connection = connexion;

                    commande.CommandText = "insert into PanierAdherents(ID,ID_Adherent,ID_Panier)"
                                           + "values(@ID,@ID_Adherent,@ID_Basket)";

                    commande.Parameters.Add(new SqlParameter("@ID", ID));
                    commande.Parameters.Add(new SqlParameter("@ID_Adherent", ID_Adherent));
                    commande.Parameters.Add(new SqlParameter("@ID_BAsket", ID_Panier));

                    commande.ExecuteNonQuery();
                }

                connexion.Close();
    }
}
