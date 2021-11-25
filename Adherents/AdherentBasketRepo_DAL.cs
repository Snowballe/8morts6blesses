using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class AdherentBasketRepo_DAL : Repo_DAL<AdherentBasket_DAL>
    {
        public override List<AdherentBasket_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select ID,ID_Adherent,ID_Panier, from PanierAdherents";
            var reader = commande.ExecuteReader();

            var listeDAdherents = new List<AdherentBasket_DAL>();

            while (reader.Read())
            {
                var p = new AdherentBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),

                listeDAdherents.Add(p));
            }

            DetruireConnexionEtCommande();

            return listeDAdherents;
        }

        public List<AdherentBasket_DAL> GetAllByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select ID, ID_Adherent, ID_Panier from PanierAdherents where ID_Adherent=@ID_Adherent";
            commande.Parameters.Add(new SqlParameter("@ID_Adherent", IDAdherentBasket));
            var reader = commande.ExecuteReader();

            var listeDAdherents = new List<AdherentBasket_DAL>();

            while (reader.Read())
            {
                var p = new AdherentBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetInt32(3));

                listeDAdherents.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDAdherents;
        }

        public override AdherentBasket_DAL GetByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select ID,ID_Adherent from PanierAdherents where ID=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = commande.ExecuteReader();

            var listeDAdherents = new List<AdherentBasket_DAL>();

            AdherentBasket_DAL p;
            if (reader.Read())
            {
                p = new AdherentBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetInt32(3));
            }
            else
                throw new Exception($"Pas d'adherent dans la BDD avec l'ID {ID}");

            DetruireConnexionEtCommande();

            return p;
        }

        public override AdherentBasket_DAL Insert(AdherentBasket PanierAdherent)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into PanierAdherents(ID, ID_Adherent, ID_Panier)"
                                    + " values (@ID, @ID_Adherent, @ID_Panier); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            commande.Parameters.Add(new SqlParameter("@ID_Adherent", ID_Adherent));
            commande.Parameters.Add(new SqlParameter("@ID_Panier", ID_Panier));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            PanierAdherent = ID;

            DetruireConnexionEtCommande();

            return PanierAdherent;
        }

        public override AdherentBasket_DAL Update(AdherentBasket_DAL AdherentUPT)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update PanierAdherents set ID=@ID, ID_Adherent=@ID_Adherent, ID_Panier=@ID_Panier)"
                                    + " where ID=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            commande.Parameters.Add(new SqlParameter("@ID_Adherent", ID_Adherent));
            commande.Parameters.Add(new SqlParameter("@ID_Panier", ID_Panier));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour {ID_Adherent}");
            }

            DetruireConnexionEtCommande();

            return AdherentUPT;
        }

        public override void Delete(AdherentBasket_DAL Adherents)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from PanierAdherents where ID=@ID, ID_Adherent=@ID_Adherent";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            commande.Parameters.Add(new SqlParameter("@ID_Adherent", ID_Adherent));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer {ID_Adherent}");
            }

            DetruireConnexionEtCommande();
        }


    }
}
