using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class DetailsGlobaltBasketRepo_DAL : Repo_DAL<DetailsGlobalBasket_DAL>
    {
        public override List<DetailsGlobalBasket_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id, id_reference, id_PanierGlobal from PanierDetails";
            var reader = commande.ExecuteReader();

            var listeGlobalBasketDetails = new List<DetailsGlobalBasket_DAL>();

            while (reader.Read())
            {
                var p = new DetailsGlobalBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),

                listeGlobalBasketDetails.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeGlobalBasketDetails;
        }

        public List<DetailsGlobalBasket_DAL> GetAllByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id from PanierDetails where id=@id";
            commande.Parameters.Add(new SqlParameter("@id", id));
            var reader = commande.ExecuteReader();

            var listeGlobalBasketDetails = new List<DetailsGlobalBasket_DAL>();

            while (reader.Read())
            {
                var p = new DetailsGlobalBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetInt32(3));

                listeGlobalBasketDetails.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeGlobalBasketDetails;
        }

        public override DetailsGlobalBasket_DAL Insert(GlobalBasketDetails GlobalBasketDetails)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into panierDetails(id,id_reference,id_PanierGlobal )"
                                    + " values (@id, @id_reference, @id_PanierGlobal); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@id_reference", id_reference));
            commande.Parameters.Add(new SqlParameter("@id_PanierGlobal", id_PanierGlobal));
            

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            GlobalBasketDetails = ID;

            DetruireConnexionEtCommande();

            return GlobalBasketDetails;
        }

        public override DetailsGlobalBasket_DAL Update(GlobalBasketDetails GlobalBasketDetails)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update PanierDetails set id=@id, id_reference=@id_reference, id_PanierGlobal=@id_PanierGlobal)"
                                    + " where id=@id";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@id_reference", id_reference));
            commande.Parameters.Add(new SqlParameter("@id_PanierGlobal", id_PanierGlobal));
            
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour{ID}");
            }

            DetruireConnexionEtCommande();

            return GlobalBasketDetails;
        }

        public override void Delete(GlobalBasketDetails GlobalBasketDetails)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from PanierDetails where id=@Iid";
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
