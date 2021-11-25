using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class DetailsGlobalBasket_DAL
    {

        public int id { get; private set; }
        public int id_reference { get; set; }
        public int id_PanierGlobal { get; set; }


        public void Insert()
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=Geometrie;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {

                connexion.Open();
                using (var commande = new SqlCommand())
                {

                    commande.Connection = connexion;

                    commande.CommandText = "insert into PanierDetails(id,id_reference,id_PanierGlobal)"
                                           + "values(@id, @id_reference, @id_PanierGlobal)";

                    commande.Parameters.Add(new SqlParameter("@id", ID));
                    commande.Parameters.Add(new SqlParameter("@id_reference", id_reference));
                    commande.Parameters.Add(new SqlParameter("@id_PanierGlobal", id_PanierGlobal));

                }
                connexion.Close();

            }
        }
