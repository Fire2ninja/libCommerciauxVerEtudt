using Newtonsoft.Json;

namespace libCommerciaux
{
    [JsonObject(MemberSerialization.Fields)]
    public class NoteFrais
    {
        private int numero;
        private DateTime dateNoteFrais;
        private double montantARembourser;
        private bool estRembourse;
        private Commercial leCommercial;

        public NoteFrais(DateTime dateNoteFrais, Commercial leCommercial)
        {
            this.dateNoteFrais = dateNoteFrais;
            this.leCommercial = leCommercial;
            this.estRembourse = false;
            this.montantARembourser = 0;
            // Numéro géré automatiquement : position de la note dans la liste du commercial (0 pour la première)
            this.numero = leCommercial.getMesNoteFrais().Count;
            leCommercial.ajouterNoteFrais(this);
        }

        public int getNumero()
        {
            return this.numero;
        }

        public DateTime getDateNoteFrais()
        {
            return this.dateNoteFrais;
        }

        public double getMontantARembourser()
        {
            return this.montantARembourser;
        }

        public Commercial getLeCommercial()
        {
            return this.leCommercial;
        }

        public bool getEstRembourse()
        {
            return this.estRembourse;
        }

        public void setRembourse()
        {
            this.estRembourse = true;
        }

        public void setMontantARembourser()
        {
            this.montantARembourser = calculMontantARembourser();
        }

        virtual public double calculMontantARembourser()
        {
            return 0;
        }

        public override string ToString()
        {
            string etat = this.estRembourse ? "Remboursé" : "Non remboursé";
            // Affichage : la première note du commercial porte le numéro 1
            return $"Numéro : {this.numero + 1} - Date : {this.dateNoteFrais} - Montant à rembourser: {this.montantARembourser} euros - {etat}";
        }
    }
}
