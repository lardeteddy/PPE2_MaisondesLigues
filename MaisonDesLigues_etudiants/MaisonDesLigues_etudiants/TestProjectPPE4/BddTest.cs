﻿using BaseDeDonnees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace TestProjectPPE4
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
        ///Test pour HeureVac
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void HeureVacTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            Bdd_Accessor target = new Bdd_Accessor(param0); // TODO: initialisez à une valeur appropriée
            short pNumVac = 1; // TODO: initialisez à une valeur appropriée
            string heurevac = '12/08/2012 10:00:00'; // TODO: initialisez à une valeur appropriée
            DateTime expected = new DateTime(); // TODO: initialisez à une valeur appropriée
            DateTime actual;
            actual = target.HeureVac(pNumVac, heurevac);
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
    }
}
