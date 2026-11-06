using Newtonsoft.Json;

namespace libCommerciaux
{
    [JsonObject(MemberSerialization.Fields)]
    public class Commercial
    {
        private string nom;
        private string prenom;
        private int puissanceVoiture;
        private char categorie;
        private List<NoteFrais> mesNotes;

        public Commercial(string nom, string prenom, int puissanceVoiture, char categorie)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.puissanceVoiture = puissanceVoiture;
            this.categorie = categorie;
            this.mesNotes = new List<NoteFrais>();
        }

        public string getNom()
        {
            return this.nom;
        }

        public string getPrenom()
        {
            return this.prenom;
        }

        public int getPuissanceVoiture()
        {
            return this.puissanceVoiture;
        }

        public char getCategorie()
        {
            return this.categorie;
        }

        public List<NoteFrais> getMesNoteFrais()
        {
            return this.mesNotes;
        }

        // Appelée par le constructeur de NoteFrais : la note s'enregistre elle-même
        public void ajouterNoteFrais(NoteFrais f)
        {
            this.mesNotes.Add(f);
        }

        // Ajoute une note de transport
        public void ajouterNote(DateTime date, int nbKm)
        {
            new FraisTransport(date, this, nbKm);
        }

        // Ajoute une note de repas de midi
        public void ajouterNote(DateTime date, double montantFacture)
        {
            new RepasMidi(date, this, montantFacture);
        }

        // Ajoute une note de nuité
        public void ajouterNote(DateTime date, double montantFacture, char region)
        {
            new Nuite(date, this, montantFacture, region);
        }

        // Cumul des montants des notes remboursées pour l'année passée en paramètre
        public double CumulNoteFraisRembourser(int annee)
        {
            double cumul = 0;
            foreach (NoteFrais f in this.mesNotes)
            {
                if (f.getEstRembourse() && f.getDateNoteFrais().Year == annee)
                {
                    cumul += f.getMontantARembourser();
                }
            }
            return cumul;
        }

        public override string ToString()
        {
            return $"Nom : {this.nom} Prénom : {this.prenom} Puissance voiture : {this.puissanceVoiture} Categorie : {this.categorie}";
        }
    }
}
