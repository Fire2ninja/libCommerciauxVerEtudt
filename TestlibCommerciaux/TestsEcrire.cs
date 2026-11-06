using libCommerciaux;

namespace TestlibCommerciaux
{
    [TestClass]
    public class TestsEcrire
    {
        // Mission 1 - 6) ajouterNoteFrais
        [TestMethod]
        public void ajouterNoteFraisTest()
        {
            //Arranger
            Commercial c;
            NoteFrais f0, f1;
            c = new Commercial("Jean", "Dupond", 25, 'A');
            //Agir
            f0 = new NoteFrais(new DateTime(2022, 11, 12), c);
            f1 = new NoteFrais(new DateTime(2022, 11, 15), c);
            //Auditer
            Assert.AreEqual(2, c.getMesNoteFrais().Count);
            Assert.AreEqual(0, f0.getNumero());
            Assert.AreEqual(1, f1.getNumero());
        }

        // Mission 2 - 2) nbFraisNonRembourses
        [TestMethod]
        public void nbFraisNonRemboursesTest()
        {
            //Arranger
            ServiceCommercial sc = new ServiceCommercial();
            Commercial c1 = new Commercial("Jean", "Dupond", 25, 'A');
            Commercial c2 = new Commercial("Paul", "Martin", 4, 'B');
            sc.ajouterCommercial(c1);
            sc.ajouterCommercial(c2);
            NoteFrais f0 = new NoteFrais(new DateTime(2022, 11, 12), c1);
            NoteFrais f1 = new NoteFrais(new DateTime(2022, 11, 15), c1);
            NoteFrais f2 = new NoteFrais(new DateTime(2022, 11, 20), c2);
            //Agir
            f1.setRembourse();
            //Auditer
            Assert.AreEqual(2, sc.nbFraisNonRembourses());
        }

        [TestMethod]
        public void nbFraisNonRemboursesServiceVideTest()
        {
            ServiceCommercial sc = new ServiceCommercial();
            Assert.AreEqual(0, sc.nbFraisNonRembourses());
        }

        // Mission 3 - 2) FraisTransport
        [TestMethod]
        public void calculMontantARembourserTransportTest()
        {
            //Arranger
            Commercial c;
            NoteFrais T0;
            c = new Commercial("Jean", "Dupond", 8, 'A');
            //Agir
            T0 = new FraisTransport(new DateTime(2022, 11, 12), c, 250);
            //Auditer
            Assert.AreEqual(50, T0.getMontantARembourser(), 0.001);
        }

        [TestMethod]
        public void calculMontantARembourserTransportPetiteEtGrossePuissanceTest()
        {
            Commercial petite = new Commercial("Jean", "Dupond", 4, 'A');
            Commercial grosse = new Commercial("Paul", "Martin", 11, 'A');
            NoteFrais t1 = new FraisTransport(new DateTime(2022, 11, 12), petite, 100);
            NoteFrais t2 = new FraisTransport(new DateTime(2022, 11, 12), grosse, 100);
            Assert.AreEqual(10, t1.getMontantARembourser(), 0.001);
            Assert.AreEqual(30, t2.getMontantARembourser(), 0.001);
        }

        // Mission 3 - 3) RepasMidi
        [TestMethod]
        public void calculMontantARembourserMidi1Test()
        {
            //Arranger
            Commercial c = new Commercial("Jean", "Dupond", 8, 'A');
            //Agir
            NoteFrais r0 = new RepasMidi(new DateTime(2022, 11, 12), c, 35);
            //Auditer
            Assert.AreEqual(25, r0.getMontantARembourser(), 0.001);
        }

        [TestMethod]
        public void calculMontantARembourserMidi2Test()
        {
            //Arranger
            Commercial c = new Commercial("Jean", "Dupond", 8, 'A');
            //Agir
            NoteFrais r0 = new RepasMidi(new DateTime(2022, 11, 12), c, 15);
            //Auditer
            Assert.AreEqual(15, r0.getMontantARembourser(), 0.001);
        }

        // Mission 3 - 4) Nuite
        [TestMethod]
        public void calculMontantARembourserNuite1Test()
        {
            //Arranger
            Commercial c = new Commercial("Jean", "Dupond", 8, 'A');
            //Agir
            NoteFrais n0 = new Nuite(new DateTime(2022, 11, 12), c, 46, '2');
            //Auditer
            Assert.AreEqual(46, n0.getMontantARembourser(), 0.001);
        }

        [TestMethod]
        public void calculMontantARembourserNuite2Test()
        {
            //Arranger
            Commercial c = new Commercial("Jean", "Dupond", 8, 'A');
            //Agir
            NoteFrais n0 = new Nuite(new DateTime(2022, 11, 12), c, 80, '3');
            //Auditer
            Assert.AreEqual(74.75, n0.getMontantARembourser(), 0.001);
        }

        // Mission 4 - ajout des notes depuis le service commercial
        [TestMethod]
        public void nbFraisNonRemboursesSCTest()
        {
            //Arranger
            ServiceCommercial sc = new ServiceCommercial();
            Commercial c1 = new Commercial("Dupond", "Jean", 7, 'B');
            sc.ajouterCommercial(c1);
            //Agir
            sc.ajouterNote(c1, new DateTime(2013, 11, 15), 100);      // transport
            sc.ajouterNote(c1, new DateTime(2013, 11, 21), 15.5);     // repas
            sc.ajouterNote(c1, new DateTime(2013, 11, 25), 105, '2'); // nuité
            //Auditer
            Assert.AreEqual(3, sc.nbFraisNonRembourses());
            Assert.IsInstanceOfType(c1.getMesNoteFrais()[0], typeof(FraisTransport));
            Assert.IsInstanceOfType(c1.getMesNoteFrais()[1], typeof(RepasMidi));
            Assert.IsInstanceOfType(c1.getMesNoteFrais()[2], typeof(Nuite));
        }

        [TestMethod]
        public void rembourserNoteTest()
        {
            //Arranger
            ServiceCommercial sc = new ServiceCommercial();
            Commercial c1 = new Commercial("Dupond", "Jean", 7, 'B');
            sc.ajouterCommercial(c1);
            sc.ajouterNote(c1, new DateTime(2013, 11, 15), 100);
            sc.ajouterNote(c1, new DateTime(2013, 11, 21), 15.5);
            //Agir
            sc.rembourserNote(c1, 0);
            //Auditer
            Assert.AreEqual(1, sc.nbFraisNonRembourses());
            Assert.IsTrue(c1.getMesNoteFrais()[0].getEstRembourse());
        }

        [TestMethod]
        public void CumulNoteFraisRembourserTest()
        {
            //Arranger
            Commercial c1 = new Commercial("Dupond", "Jean", 7, 'B');
            c1.ajouterNote(new DateTime(2013, 11, 15), 100);       // 20 €
            c1.ajouterNote(new DateTime(2013, 11, 21), 15.5);      // 15,5 €
            c1.ajouterNote(new DateTime(2014, 1, 10), 105, '2');   // 55 € mais en 2014
            c1.ajouterNote(new DateTime(2013, 12, 1), 30.0);       // 22 € non remboursé
            //Agir
            c1.getMesNoteFrais()[0].setRembourse();
            c1.getMesNoteFrais()[1].setRembourse();
            c1.getMesNoteFrais()[2].setRembourse();
            //Auditer
            Assert.AreEqual(35.5, c1.CumulNoteFraisRembourser(2013), 0.001);
            Assert.AreEqual(55, c1.CumulNoteFraisRembourser(2014), 0.001);
        }

        // Mission 5 - sérialisation JSON
        [TestMethod]
        public void serializationCommerciauxTest()
        {
            //Arranger
            ServiceCommercial sc = new ServiceCommercial();
            Commercial c1 = new Commercial("Dupond", "Jean", 7, 'B');
            Commercial c2 = new Commercial("Martin", "Paul", 12, 'A');
            sc.ajouterCommercial(c1);
            sc.ajouterCommercial(c2);
            sc.ajouterNote(c1, new DateTime(2013, 11, 15), 100);
            sc.ajouterNote(c1, new DateTime(2013, 11, 21), 15.5);
            sc.ajouterNote(c2, new DateTime(2013, 11, 25), 105, '2');
            string fichier = Path.Combine(Path.GetTempPath(), "service.sr");
            //Agir
            PersisteServiceCommercial.sauve(sc, fichier);
            ServiceCommercial sc1 = PersisteServiceCommercial.charge(fichier);
            //Auditer
            Assert.AreEqual(sc.getLesCommerciaux().Count, sc1.getLesCommerciaux().Count);
            Assert.AreEqual(sc.nbFraisNonRembourses(), sc1.nbFraisNonRembourses());
            NoteFrais note = sc1.getLesCommerciaux()[1].getMesNoteFrais()[0];
            Assert.IsInstanceOfType(note, typeof(Nuite));
            Assert.AreSame(sc1.getLesCommerciaux()[1], note.getLeCommercial());
        }
    }
}
