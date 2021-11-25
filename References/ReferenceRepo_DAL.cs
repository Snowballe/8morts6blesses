using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class ReferenceRepo_DAL : Repo_DAL<Reference_DAL>
    {
        public override List<Reference_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id,reference,libelle,marque,id_Fournisseurs,desactive where desactive=0";
            var reader = commande.ExecuteReader();

            var listeDeReferences = new List<Reference_DAL>();

            while (reader.Read())
            {
                var p = new Reference_DAL(reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3),
                                        reader.GetInt32(4),

                listeDeReferences.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDeReferences;
        }

        public List<ProviderBasket_DAL> GetAllByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id,reference,libelle,marque,id_Fournisseurs where id_Fournisseurs=@id_fournisseurs";
            commande.Parameters.Add(new SqlParameter("@id_fournisseurs", IDFournisseur));
            
            var reader = commande.ExecuteReader();

            var listeDeRefs = new List<Reference_DAL>();

            while (reader.Read())
            {
                var p = new Reference_DAL(reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3));
                                        reader.GetInt32(3));

                listeDeRefs.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDeRefs;
        }

        public override Reference_DAL Insert(Reference_DAL reference)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into references(id,reference,libelle,marque,id_Fournisseurs,desactive)"
                                    + " values (@id, @reference, @libelle, @marque,@id_Fournisseurs,@desactive); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@reference", reference));
            commande.Parameters.Add(new SqlParameter("@libelle", libelle));
            commande.Parameters.Add(new SqlParameter("@marque", marque));
            commande.Parameters.Add(new SqlParameter("@id_Fournisseurs", id_Fournisseurs));
            commande.Parameters.Add(new SqlParameter("@desactive", 0));


            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            id_Fournisseurs = ID;

            DetruireConnexionEtCommande();

            return id_Fournisseurs;
        }

        public override Reference_DAL Update(Reference_DAL reference)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update references set id=@id,reference=@reference,libelle=@libelle,marque=@marque,id_Fournisseurs=@id_Fournisseurs,desactive=@desactive)"
                                    + " where reference=@reference, id=@id,libelle=@libelle,marque=@marque,id_Fournisseurs=@id_Fournisseurs,desactive=@desactive";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@reference", reference));
            commande.Parameters.Add(new SqlParameter("@libelle", libelle));
            commande.Parameters.Add(new SqlParameter("@marque", marque));
            commande.Parameters.Add(new SqlParameter("@id_Fournisseurs", id_Fournisseurs));
            commande.Parameters.Add(new SqlParameter("@desactive", desactive));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour  {ID}");
            }

            DetruireConnexionEtCommande();

            return reference;
        }

        public override void Delete(Reference_DAL reference)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from fournisseurs_panier where id=@id";
            commande.Parameters.Add(new SqlParameter("@id", id));


            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer {ID}");
            }

            DetruireConnexionEtCommande();
        }

    }
}
