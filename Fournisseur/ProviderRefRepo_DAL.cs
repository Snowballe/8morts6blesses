using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DAL
{
    public class ProviderRefRepo : Repo_DAL
    {
        public override List<ProviderRef_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select ID,societe,Nomcontact,PrenomContact,civilite,Adressenormee from Fournisseur";
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

        public List<ProviderRef_DAL> GetAllByIDAdherent(int IDFournisseurs)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select ID from Fournisseur where ID=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
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

        public override ProviderRef_DAL Insert(Fournisseur Fournisseur)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into Fournisseurs(ID,societe,Nomcontact,PrenomContact,civilite,Adressenormee)"
                                    + " values (@ID, @societe, @Nomcontact, @PrenomContact, @civilite, @Adressenormee); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            commande.Parameters.Add(new SqlParameter("@Nomcontact", Nomcontact));
            commande.Parameters.Add(new SqlParameter("@PrenomContact", PrenomContact));
            commande.Parameters.Add(new SqlParameter("@civilite", civilite));
            commande.Parameters.Add(new SqlParameter("@Adressenormee", Adressenormee));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            Fournisseur = ID;

            DetruireConnexionEtCommande();

            return Fournisseur;
        }

        public override ProviderRef_DAL Update(ProviderRef_DAL Fournisseur)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update Fournisseur set ID=@ID, societe=@societe, Nomcontact=@Nomcontact, PrenomContact=@PrenomContact, civilite=@civilite, Adressenormee=@Adressenormee )"
                                    + " where ID=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            commande.Parameters.Add(new SqlParameter("@societe", societe));
            commande.Parameters.Add(new SqlParameter("@Nomcontact", Nomcontact));
            commande.Parameters.Add(new SqlParameter("@PrenomContact", PrenomContact));
            commande.Parameters.Add(new SqlParameter("@civilite", civilite));
            commande.Parameters.Add(new SqlParameter("@Adressenormee", Adressenormee));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour  {ID}");
            }

            DetruireConnexionEtCommande();

            return Fournisseur;
        }

        public override void Delete(ProviderRef_DAL Provider)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from Adherent where ID=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            commande.Parameters.Add(new SqlParameter("@societe", societe));
            commande.Parameters.Add(new SqlParameter("@Nomcontact", Nomcontact));
            commande.Parameters.Add(new SqlParameter("@PrenomContact", PrenomContact));
            commande.Parameters.Add(new SqlParameter("@civilite", Email));
            commande.Parameters.Add(new SqlParameter("@Adressenormee", DateAdhesion));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer {ID}");
            }

            DetruireConnexionEtCommande();
        }

    }
}
