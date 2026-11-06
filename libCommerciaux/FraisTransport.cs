using Newtonsoft.Json;

namespace libCommerciaux
{
    [JsonObject(MemberSerialization.Fields)]
    public class FraisTransport : NoteFrais
    {
        private int nbKm;

        public FraisTransport(DateTime dateNoteFrais, Commercial leCommercial, int nbKm)
            : base(dateNoteFrais, leCommercial)
        {
            this.nbKm = nbKm;
            setMontantARembourser();
        }

        public override double calculMontantARembourser()
        {
            int puissance = getLeCommercial().getPuissanceVoiture();
            double tarifKm;
            if (puissance < 5)
            {
                tarifKm = 0.1;
            }
            else if (puissance <= 10)
            {
                tarifKm = 0.2;
            }
            else
            {
                tarifKm = 0.3;
            }
            return Math.Round(this.nbKm * tarifKm, 2);
        }

        public override string ToString()
        {
            return $"Transport - {base.ToString()} - {this.nbKm} km-";
        }
    }
}
