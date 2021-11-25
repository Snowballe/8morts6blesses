using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class ProviderRef_DAL
    {

        public int id_provider { get; private set; }
        public int id_reference{ get; private set; }
        

        public List<ProviderRef_DAL> Provider { get; set; }


        public ProviderRef_DAL(IEnumerable<ProviderRef_DAL> desProvider) => (Provider) = (desProvider.ToList());


        public void Insert()
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=Geometrie;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {

                connexion.Open();
                using (var commande = new SqlCommand())
                {

                    commande.Connection = connexion;

                    commande.CommandText = "insert into references_fournisseurs(id_fournisseurs,id_references)"
                                           + "values(@id_fournisseurs,@id_references)";

                   
                    commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
                    commande.Parameters.Add(new SqlParameter("@id_references", id_references));
                }

                connexion.Close();

            }
        }
