using Newtonsoft.Json;

namespace libCommerciaux
{
    [JsonObject(MemberSerialization.Fields)]
    public class ServiceCommercial
    {
        private List<Commercial> lesCommerciaux;

        public ServiceCommercial()
        {
            this.lesCommerciaux = new List<Commercial>();
        }

        public List<Commercial> getLesCommerciaux()
        {
            return this.lesCommerciaux;
        }

        public void ajouterCommercial(Commercial unCommercial)
        {
            this.lesCommerciaux.Add(unCommercial);
        }

        // Nombre de notes de frais non remboursées pour tous les commerciaux du service
        public int nbFraisNonRembourses()
        {
            int nb = 0;
            foreach (Commercial c in this.lesCommerciaux)
            {
                foreach (NoteFrais f in c.getMesNoteFrais())
                {
                    if (!f.getEstRembourse())
                    {
                        nb++;
                    }
                }
            }
            return nb;
        }

        // Ajoute une note de transport
        public void ajouterNote(Commercial unCommercial, DateTime date, int nbKm)
        {
            unCommercial.ajouterNote(date, nbKm);
        }

        // Ajoute une note de repas de midi
        public void ajouterNote(Commercial unCommercial, DateTime date, double montantFacture)
        {
            unCommercial.ajouterNote(date, montantFacture);
        }

        // Ajoute une note de nuité
        public void ajouterNote(Commercial unCommercial, DateTime date, double montantFacture, char region)
        {
            unCommercial.ajouterNote(date, montantFacture, region);
        }

        // Rembourse la note de frais portant ce numéro pour ce commercial
        public void rembourserNote(Commercial unCommercial, int numero)
        {
            foreach (NoteFrais f in unCommercial.getMesNoteFrais())
            {
                if (f.getNumero() == numero)
                {
                    f.setRembourse();
                }
            }
        }
    }
}
