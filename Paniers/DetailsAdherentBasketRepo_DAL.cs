using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class DetailsAdherentBasketRepo_DAL : Repo_DAL<DetailsAdherentBasket_DAL>
    {
        public override List<DetailsAdherentBasket_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id,quantite,id_ref,id_panier from PanierAdherentDetails";
            var reader = commande.ExecuteReader();

            var listeDetailsBasketAdherent = new List<DetailsAdherentBasket_DAL>();

            while (reader.Read())
            {
                var p = new DetailsAdherentBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),

                listeDetailsBasketAdherent.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDetailsBasketAdherent;
        }

        public List<DetailsAdherentBasket_DAL> GetAllByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id from PanierAdherentDetails where id=@id";
            commande.Parameters.Add(new SqlParameter("@id", id));
            var reader = commande.ExecuteReader();

            var listeDetailsBasketAdherent = new List<DetailsAdherentBasket_DAL>();

            while (reader.Read())
            {
                var p = new DetailsAdherentBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetInt32(3));

                listeDetailsBasketAdherent.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDetailsBasketAdherent;
        }

        public override DetailsAdherentBasket_DAL Insert(DetailsBasketAdherent DetailsBasketAdherent)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into PanierAdherentDetails(id,quantite,id_ref_id_panier )"
                                    + " values (@id, @quantite, @id_ref, @id_panier); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@quantite", quantite));
            commande.Parameters.Add(new SqlParameter("@id_ref", id_ref));
            commande.Parameters.Add(new SqlParameter("@id_panier", id_panier));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            DetailsBasketAdherent = ID;

            DetruireConnexionEtCommande();

            return DetailsBasketAdherent;
        }

        public override DetailsAdherentBasket_DAL Update(DetailsBasketAdherent DetailsBasketAdherent)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update PanierAdherentDetails set id=@id, quantite=@quantite, id_ref=@id_ref, id_panier=@id_panier)"
                                    + " where id=@id";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@quantite", quantite));
            commande.Parameters.Add(new SqlParameter("@id_ref", id_ref));
            commande.Parameters.Add(new SqlParameter("@id_panier", id_panier));
            
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour{ID}");
            }

            DetruireConnexionEtCommande();

            return DetailsBasketAdherent;
        }

        public override void Delete(DetailsBasketAdherent DetailsBasketAdherent)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from PanierAdherentDetails where id=@Iid";
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
