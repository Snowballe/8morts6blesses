using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class ProviderRefRepo : Repo_DAL<ProviderRef_DAL>
    {
        public override List<ProviderRef_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id_references, id_fournisseurs from referencesfournisseur";
            var reader = commande.ExecuteReader();

            var listeDeFournisseur = new List<ProviderRef_DAL>();

            while (reader.Read())
            {
                var p = new ProviderRef_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),

                listeDeFournisseur.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDeFournisseur;
        }

        public List<ProviderRef_DAL> GetAllByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id_references, id_fournisseurs from referencesfournisseur where id_fournisseurs=@id_fournisseurs, id_references=@id_references";
            commande.Parameters.Add(new SqlParameter("@id_references", id_references));
            commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
            var reader = commande.ExecuteReader();

            var listeDeFournisseur = new List<ProviderRef_DAL>();

            while (reader.Read())
            {
                var p = new ProviderRef_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetInt32(3));

                listeDeFournisseur.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDeFournisseur;
        }

        public override ProviderRef_DAL Insert(FournisseurREF FournisseurREF)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into referencesfournisseurs(id_references,id_fournisseurs)"
                                    + " values (@id_references,id_fournisseurs); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
            commande.Parameters.Add(new SqlParameter("@id_references", id_references));
            

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            FournisseurREF = ID;

            DetruireConnexionEtCommande();

            return FournisseurREF;
        }

        public override ProviderRef_DAL Update(ProviderRef_DAL FournisseurREF)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update referencesfournisseurs set id_references=@id_references, id_fournisseurs=@id_fournisseurs )"
                                    + " where id_references=@id_references, id_fournisseurs=@id_fournisseurs";
            commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
            commande.Parameters.Add(new SqlParameter("@id_references", id_references));
            
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour  {ID}");
            }

            DetruireConnexionEtCommande();

            return FournisseurREF;
        }

        public override void Delete(ProviderRef_DAL Provider)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from referencesfournisseurs where id_fournisseurs=@id_fournisseurs, id_references=@id_references";
            commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
            commande.Parameters.Add(new SqlParameter("@id_references", id_references));
            
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer {ID}");
            }

            DetruireConnexionEtCommande();
        }

    }
}
