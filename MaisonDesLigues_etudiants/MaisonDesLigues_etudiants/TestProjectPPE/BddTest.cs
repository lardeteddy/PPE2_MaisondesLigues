using BaseDeDonnees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using Oracle.DataAccess.Client;
using System.Data;
using System.Collections.Generic;

namespace TestProjectPPE
{
    
    
    /// <summary>
    ///Classe de test pour BddTest, destinée à contenir tous
    ///les tests unitaires BddTest
    ///</summary>
    [TestClass()]
    public class BddTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Test pour creerVacation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void creerVacationTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            int pIdAtelier = 0; // TODO: initialisez à une valeur appropriée
            string pHeureDebut = string.Empty; // TODO: initialisez à une valeur appropriée
            string pHeureFin = string.Empty; // TODO: initialisez à une valeur appropriée
            target.creerVacation(pIdAtelier, pHeureDebut, pHeureFin);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour InscrireIntervenant
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void InscrireIntervenantTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            string pNom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pPrenom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse1 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse2 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pCp = string.Empty; // TODO: initialisez à une valeur appropriée
            string pVille = string.Empty; // TODO: initialisez à une valeur appropriée
            string pTel = string.Empty; // TODO: initialisez à une valeur appropriée
            string pMail = string.Empty; // TODO: initialisez à une valeur appropriée
            short pIdAtelier = 0; // TODO: initialisez à une valeur appropriée
            string pIdStatut = string.Empty; // TODO: initialisez à une valeur appropriée
            target.InscrireIntervenant(pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail, pIdAtelier, pIdStatut);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour InscrireBenevole
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void InscrireBenevoleTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            string pNom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pPrenom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse1 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse2 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pCp = string.Empty; // TODO: initialisez à une valeur appropriée
            string pVille = string.Empty; // TODO: initialisez à une valeur appropriée
            string pTel = string.Empty; // TODO: initialisez à une valeur appropriée
            string pMail = string.Empty; // TODO: initialisez à une valeur appropriée
            DateTime pDateNaissance = new DateTime(); // TODO: initialisez à une valeur appropriée
            Nullable<long> pNumeroLicence = new Nullable<long>(); // TODO: initialisez à une valeur appropriée
            Collection<short> pDateBenevolat = null; // TODO: initialisez à une valeur appropriée
            target.InscrireBenevole(pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail, pDateNaissance, pNumeroLicence, pDateBenevolat);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour HeureVac
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void HeureVacTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            short pNumVac = 0; // TODO: initialisez à une valeur appropriée
            string heurevac = string.Empty; // TODO: initialisez à une valeur appropriée
            DateTime expected = new DateTime(); // TODO: initialisez à une valeur appropriée
            DateTime actual;
            actual = target.HeureVac(pNumVac, heurevac);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour GetMessageOracle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void GetMessageOracleTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            string unMessage = string.Empty; // TODO: initialisez à une valeur appropriée
            string expected = string.Empty; // TODO: initialisez à une valeur appropriée
            string actual;
            actual = target.GetMessageOracle(unMessage);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour FermerConnexion
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void FermerConnexionTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            target.FermerConnexion();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour Constructeur Bdd
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void BddConstructorTest()
        {
            string UnLogin = string.Empty; // TODO: initialisez à une valeur appropriée
            string UnPwd = string.Empty; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(UnLogin, UnPwd);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour InscrireIntervenant
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void InscrireIntervenantTest1()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            string pNom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pPrenom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse1 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse2 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pCp = string.Empty; // TODO: initialisez à une valeur appropriée
            string pVille = string.Empty; // TODO: initialisez à une valeur appropriée
            string pTel = string.Empty; // TODO: initialisez à une valeur appropriée
            string pMail = string.Empty; // TODO: initialisez à une valeur appropriée
            short pIdAtelier = 0; // TODO: initialisez à une valeur appropriée
            string pIdStatut = string.Empty; // TODO: initialisez à une valeur appropriée
            Collection<string> pLesCategories = null; // TODO: initialisez à une valeur appropriée
            Collection<string> pLesHotels = null; // TODO: initialisez à une valeur appropriée
            Collection<short> pLesNuits = null; // TODO: initialisez à une valeur appropriée
            target.InscrireIntervenant(pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail, pIdAtelier, pIdStatut, pLesCategories, pLesHotels, pLesNuits);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour creerTheme
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void creerThemeTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            short pIdAtelier = 0; // TODO: initialisez à une valeur appropriée
            string pLibelleTheme = string.Empty; // TODO: initialisez à une valeur appropriée
            target.creerTheme(pIdAtelier, pLibelleTheme);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour creerAtelier
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void creerAtelierTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            string pLibelleAtelier = string.Empty; // TODO: initialisez à une valeur appropriée
            int pNbPlacesMax = 0; // TODO: initialisez à une valeur appropriée
            string pLibelleTheme = string.Empty; // TODO: initialisez à une valeur appropriée
            string pHeureDebut = string.Empty; // TODO: initialisez à une valeur appropriée
            string pHeureFin = string.Empty; // TODO: initialisez à une valeur appropriée
            target.creerAtelier(pLibelleAtelier, pNbPlacesMax, pLibelleTheme, pHeureDebut, pHeureFin);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour ParamsSpecifiquesIntervenant
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void ParamsSpecifiquesIntervenantTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            OracleCommand Cmd = null; // TODO: initialisez à une valeur appropriée
            short pIdAtelier = 0; // TODO: initialisez à une valeur appropriée
            string pIdStatut = string.Empty; // TODO: initialisez à une valeur appropriée
            target.ParamsSpecifiquesIntervenant(Cmd, pIdAtelier, pIdStatut);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour ParamCommunsNouveauxParticipants
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void ParamCommunsNouveauxParticipantsTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            OracleCommand Cmd = null; // TODO: initialisez à une valeur appropriée
            string pNom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pPrenom = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse1 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pAdresse2 = string.Empty; // TODO: initialisez à une valeur appropriée
            string pCp = string.Empty; // TODO: initialisez à une valeur appropriée
            string pVille = string.Empty; // TODO: initialisez à une valeur appropriée
            string pTel = string.Empty; // TODO: initialisez à une valeur appropriée
            string pMail = string.Empty; // TODO: initialisez à une valeur appropriée
            target.ParamCommunsNouveauxParticipants(Cmd, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour ObtenirDonnesOracleVacation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void ObtenirDonnesOracleVacationTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            short IdAtelier = 0; // TODO: initialisez à une valeur appropriée
            DataTable expected = null; // TODO: initialisez à une valeur appropriée
            DataTable actual;
            actual = target.ObtenirDonnesOracleVacation(IdAtelier);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour ObtenirDonnesOracle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void ObtenirDonnesOracleTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            string UneTableOuVue = string.Empty; // TODO: initialisez à une valeur appropriée
            DataTable expected = null; // TODO: initialisez à une valeur appropriée
            DataTable actual;
            actual = target.ObtenirDonnesOracle(UneTableOuVue);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour ObtenirDatesNuites
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void ObtenirDatesNuitesTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            Dictionary<short, string> expected = null; // TODO: initialisez à une valeur appropriée
            Dictionary<short, string> actual;
            actual = target.ObtenirDatesNuites();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour MajVacation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void MajVacationTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            DateTime pHeureDebutVac = new DateTime(); // TODO: initialisez à une valeur appropriée
            DateTime pHeureFinVac = new DateTime(); // TODO: initialisez à une valeur appropriée
            short pNumeroVac = 0; // TODO: initialisez à une valeur appropriée
            target.MajVacation(pHeureDebutVac, pHeureFinVac, pNumeroVac);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }
    }
}
