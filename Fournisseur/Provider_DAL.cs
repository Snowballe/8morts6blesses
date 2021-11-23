using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class Provider_DAL<Type_DAL>
    {

        public int ID { get; private set; }
        public char Societe { get; private set; }
        public char NomContact { get; private set; }
        public char PrenomContact { get; private set; }
        public char Adressenormee { get; private set; }
        public DateTime DateAdhesion { get; set; }



        public void Insert()
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=Geometrie;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {

                connexion.Open();
                using (var commande = new SqlCommand())
                {

                    commande.Connection = connexion;

                    commande.CommandText = "insert into fournisseurs(ID,societe,Nomcontact,PrenomContact,Adressenormee,DateAdhesion)"
                                           + "values(@ID,@societe,@Nomcontact,@PrenomContact,@Adressenormee,@DateAdhesion)";

                    commande.Parameters.Add(new SqlParameter("@ID", ID));
                    commande.Parameters.Add(new SqlParameter("@societe", Societe));
                    commande.Parameters.Add(new SqlParameter("@Nomcontact", Nomcontact));
                    commande.Parameters.Add(new SqlParameter("@PrenomContact", PrenomContact));
                    commande.Parameters.Add(new SqlParameter("@Adressenormee", Adressenormee));
                    commande.Parameters.Add(new SqlParameter("@DateAdhesion", DateAdhesion));
                 
                }
                connexion.Close();

            }
        }
