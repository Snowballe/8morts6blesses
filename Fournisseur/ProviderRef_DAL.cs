using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class ProviderRef_DAL<Type_DAL>
    {

        public int ID_Provider { get; private set; }
        public int ID_reference{ get; private set; }
        

        public List<Point_DAL> Points { get; set; }


        public ProviderRef_DAL(IEnumerable<Point_DAL> desPoints) => (Points) = (desPoints.ToList());


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

                    ID = (int)commande.ExecuteScalar();
                }

                connexion.Close();

            }
        }
