using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class Adherent_DAL<Type_DAL>
    {

        public int ID { get; private set; }
        public string Societe { get; private set; }
        public string NomContact { get; private set; }
        public string PrenomContact { get; private set; }
        public string Email { get;  set; }
        public string DateAdhesion { get; set; }

        public List<Adherent_DAL> adherent { get; set; }


        public Adherent_DAL(IEnumerable<Adherent_DAL> desAdherents) => (adherent) = (desAdherents.ToList());


        public void Insert()
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=tettet;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {

                connexion.Open();
                using (var commande = new SqlCommand())
                {

                    commande.Connection = connexion;

                    commande.CommandText = "insert into 8M6B(DateAdhesion,Email,NomContact,PrenomContact)"
                                           + "values(@DateAdhesion,@Email,@NomContact,@PrenomContact)";
                    commande.Parameters.Add(new SqlParameter("@DateAdhesion", DateTime.Now));
                    commande.Parameters.Add(new SqlParameter("@Email", Email));
                    commande.Parameters.Add(new SqlParameter("@NomContact", NomContact));
                    commande.Parameters.Add(new SqlParameter("@PrenomContact", PrenomContact));

                    ID = (int)commande.ExecuteScalar();
                }

                connexion.Close();

            }
        }

    }
}
