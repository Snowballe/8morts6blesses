using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class ProviderBasketRepo_DAL : Repo_DAL<ProviderBasket_DAL>
    {
        public override List<ProviderBasket_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id,id_fournisseurs,PrixUnitaireHT,id_PaniersDetails";
            var reader = commande.ExecuteReader();

            var listeDePanierFournisseurs = new List<ProviderBasket_DAL>();

            while (reader.Read())
            {
                var p = new ProviderBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),

                listeDePanierFournisseurs.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDePanierFournisseurs;
        }

        public List<ProviderBasket_DAL> GetAllByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id,id_fournisseurs,PrixUnitaireHT,id_PaniersDetails";
            commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@PrixUnitaireHT", PrixUnitaireHT));
            commande.Parameters.Add(new SqlParameter("@id_PaniersDetails", id_PaniersDetails));
            var reader = commande.ExecuteReader();

            var listeDePanierFournisseurs = new List<ProviderBasket_DAL>();

            while (reader.Read())
            {
                var p = new ProviderBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetInt32(3));

                listeDePanierFournisseurs.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDePanierFournisseurs;
        }

        public override ProviderBasket_DAL Insert(FournisseurBasket FournisseurBasket)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into fournisseurs_panier(id,id_fournisseurs,PrixUnitaireHT,id_PaniersDetails)"
                                    + " values (@id, @id_fournisseurs, @PrixUnitaireHT, @id_PaniersDetails); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@id_fournisseurs", id_fournisseurs));
            commande.Parameters.Add(new SqlParameter("@PrixUnitaireHT", PrixUnitaireHT));
            commande.Parameters.Add(new SqlParameter("@id_PaniersDetails", id_PaniersDetails));


            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            FournisseurBasket = ID;

            DetruireConnexionEtCommande();

            return FournisseurBasket;
        }

        public override ProviderBasket_DAL Update(FournisseurBasket FournisseurBasket)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update PanierGlobal set id=@id, PrixUnitaireHT=@PrixUnitaireHT )"
                                    + " where id_fournisseurs=@id_fournisseurs, id=@id, id_PaniersDetails=@id_PaniersDetails";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@PrixUnitaireHT", PrixUnitaireHT));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour  {ID}");
            }

            DetruireConnexionEtCommande();

            return FournisseurBasket;
        }

        public override void Delete(FournisseurBasket FournisseurBasket)
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
