using Newtonsoft.Json;

namespace libCommerciaux
{
    public static class PersisteServiceCommercial
    {
        // TypeNameHandling.Auto : conserve le type réel des notes (FraisTransport, RepasMidi, Nuite)
        // PreserveReferencesHandling : gère la référence circulaire Commercial <-> NoteFrais
        private static readonly JsonSerializerSettings parametres = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        };

        public static void sauve(ServiceCommercial sc, string nomFichier)
        {
            string json = JsonConvert.SerializeObject(sc, parametres);
            File.WriteAllText(nomFichier, json);
        }

        public static ServiceCommercial charge(string nomFichier)
        {
            string json = File.ReadAllText(nomFichier);
            ServiceCommercial? sc = JsonConvert.DeserializeObject<ServiceCommercial>(json, parametres);
            if (sc == null)
            {
                throw new InvalidDataException($"Le fichier {nomFichier} ne contient pas de service commercial");
            }
            return sc;
        }
    }
}
