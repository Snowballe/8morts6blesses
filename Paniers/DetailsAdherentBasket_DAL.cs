using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class DetailsAdherentBasket_DAL
    {

        public int id { get; private set; }
        public int quantite { get; private set; }
        public int id_ref { get; private set; }
        public int id_panier { get; private set; }

        public void Insert()
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=tettet;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {

                connexion.Open();
                using (var commande = new SqlCommand())
                {

                    commande.Connection = connexion;

                    commande.CommandText = "insert into PanierAdherentDetails(id, quantite, id_ref, id_panier)"
                                           + "values(@id, @quantite, @id_ref, @id_panier)";
                    commande.Parameters.Add(new SqlParameter("@id", id));
                    commande.Parameters.Add(new SqlParameter("@quantite", quantite));
                    commande.Parameters.Add(new SqlParameter("@id_ref", id_ref));
                    commande.Parameters.Add(new SqlParameter("@id_panier", id_panier));

                    ID = (int)commande.ExecuteScalar();
                }

                connexion.Close();

            }
        }

    }
}
