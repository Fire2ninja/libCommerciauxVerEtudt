using Newtonsoft.Json;

namespace libCommerciaux
{
    [JsonObject(MemberSerialization.Fields)]
    public class Nuite : NoteFrais
    {
        private double montantFacture;
        private char region;

        public Nuite(DateTime dateNoteFrais, Commercial leCommercial, double montantFacture, char region)
            : base(dateNoteFrais, leCommercial)
        {
            this.montantFacture = montantFacture;
            this.region = region;
            setMontantARembourser();
        }

        public override double calculMontantARembourser()
        {
            double tarif;
            switch (getLeCommercial().getCategorie())
            {
                case 'A':
                    tarif = 65;
                    break;
                case 'B':
                    tarif = 55;
                    break;
                default:
                    tarif = 50;
                    break;
            }

            double coefficient;
            switch (this.region)
            {
                case '1':
                    coefficient = 0.90;
                    break;
                case '3':
                    coefficient = 1.15;
                    break;
                default:
                    coefficient = 1;
                    break;
            }

            double plafond = Math.Round(tarif * coefficient, 2);
            // Le plafond n'est remboursé que si la facture lui est supérieure
            return Math.Min(this.montantFacture, plafond);
        }

        public override string ToString()
        {
            return $"Nuité - {base.ToString()} - payé : {this.montantFacture} € - {this.region} -";
        }
    }
}
