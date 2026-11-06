using Newtonsoft.Json;

namespace libCommerciaux
{
    [JsonObject(MemberSerialization.Fields)]
    public class RepasMidi : NoteFrais
    {
        private double montantFacture;

        public RepasMidi(DateTime dateNoteFrais, Commercial leCommercial, double montantFacture)
            : base(dateNoteFrais, leCommercial)
        {
            this.montantFacture = montantFacture;
            setMontantARembourser();
        }

        public override double calculMontantARembourser()
        {
            double plafond;
            switch (getLeCommercial().getCategorie())
            {
                case 'A':
                    plafond = 25;
                    break;
                case 'B':
                    plafond = 22;
                    break;
                default:
                    plafond = 20;
                    break;
            }
            // Remboursé au montant de la facture s'il n'atteint pas le plafond
            return Math.Min(this.montantFacture, plafond);
        }

        public override string ToString()
        {
            return $"Repas - {base.ToString()} - payé : {this.montantFacture} €";
        }
    }
}
