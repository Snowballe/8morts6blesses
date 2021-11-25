using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class GlobalBasketRepo_DAL : Repo_DAL<GlobalBasket_DAL>
    {
        public override List<GlobalBasket_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id, semaine";
            var reader = commande.ExecuteReader();

            var listeDePanier = new List<GlobalBasket_DAL>();

            while (reader.Read())
            {
                var p = new GlobalBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),

                listeDePanier.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDePanier;
        }

        public List<GlobalBasket_DAL> GetAllByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select id, semaine";
            commande.Parameters.Add(new SqlParameter("@semaine", semaine));
            commande.Parameters.Add(new SqlParameter("@id", id));
            var reader = commande.ExecuteReader();

            var listeDePanier = new List<GlobalBasket_DAL>();

            while (reader.Read())
            {
                var p = new GlobalBasket_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetInt32(3));

                listeDePanier.Add(p);
            }

            DetruireConnexionEtCommande();

            return listeDePanier;
        }

        public override GlobalBasket_DAL Insert(GlobalBasket GlobalBasket)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into PanierGlobal(id, semaine)"
                                    + " values (@id, @semaine); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@semaine", semaine));


            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            GlobalBasket = ID;

            DetruireConnexionEtCommande();

            return GlobalBasket;
        }

        public override GlobalBasket_DAL Update(GlobalBasket GlobalBasket)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update PanierGlobal set id=@id, semaine=@semaine )"
                                    + " where id=@id";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@semaine", semaine));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour  {ID}");
            }

            DetruireConnexionEtCommande();

            return GlobalBasket;
        }

        public override void Delete(GlobalBasket GlobalBasket)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from PanierGlobal where id=@id";
            commande.Parameters.Add(new SqlParameter("@id", id));
            commande.Parameters.Add(new SqlParameter("@semaine", semaine));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer {ID}");
            }

            DetruireConnexionEtCommande();
        }

    }
}
