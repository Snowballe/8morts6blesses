using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class Reference_DAL
    {

        public int ID { get; private set; }
        public char reference { get; set; }
        public char libelle { get; set; }
        public char marque { get; set; }
        public int id_fournisseurs { get; set; }
        public bool desactive { get; set; }

      



        public void Insert()
        {
            var chaineDeConnexion = "Data Source=DESKTOP-H6H5VR2;Initial Catalog=Geometrie;Integrated Security=True";

            using (var connexion = new SqlConnection(chaineDeConnexion))
            {

                connexion.Open();
                using (var commande = new SqlCommand())
                {

                    commande.Connection = connexion;

                    commande.CommandText = "insert into references(ID,reference,libelle,marque,id_fournisseurs, desactive)"
                                           + "values(@ID,@reference,@libelle,@marque,@id_fournisseurs, @desactive)";

                    commande.Parameters.Add(new SqlParameter("@ID", ID));
                    commande.Parameters.Add(new SqlParameter("@reference", reference));
                    commande.Parameters.Add(new SqlParameter("@libelle", libelle));
                    commande.Parameters.Add(new SqlParameter("@marque", marque));
                    commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
                    commande.Parameters.Add(new SqlParameter("@desactive", desactive));

                }
                connexion.Close();

            }
        }
