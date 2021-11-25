using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class GlobalBasket_DAL
    {

        public int ID { get; private set; }
        public int semaine { get; set; }
        

        public void Insert()
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=Geometrie;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {

                connexion.Open();
                using (var commande = new SqlCommand())
                {

                    commande.Connection = connexion;

                    commande.CommandText = "insert into PanierGlobal(id, semaine)"
                                           + "values(@id, @semaine)";

                    commande.Parameters.Add(new SqlParameter("@id", id));
                    commande.Parameters.Add(new SqlParameter("@semaine", semaine));
                    

                }
                connexion.Close();

            }
        }
