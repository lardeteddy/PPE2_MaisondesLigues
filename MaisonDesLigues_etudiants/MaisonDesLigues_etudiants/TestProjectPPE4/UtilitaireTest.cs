using MaisonDesLigues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BaseDeDonnees;
using System.Data;
using System.Windows.Forms;

namespace TestProjectPPE4
{
    
    
    /// <summary>
    ///Classe de test pour UtilitaireTest, destinée à contenir tous
    ///les tests unitaires UtilitaireTest
    ///</summary>
    [TestClass()]
    public class UtilitaireTest
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
        ///Test pour RemplirComboBox
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void RemplirComboBoxTest()
        {
            Bdd_Accessor UneConnexion = null; // TODO: initialisez à une valeur appropriée
            DataTable MethodeDonneOracle = null; // TODO: initialisez à une valeur appropriée
            ComboBox UneCombo = null; // TODO: initialisez à une valeur appropriée
            string DisplayMember = string.Empty; // TODO: initialisez à une valeur appropriée
            string ValueMember = string.Empty; // TODO: initialisez à une valeur appropriée
            Utilitaire_Accessor.RemplirComboBox(UneConnexion, MethodeDonneOracle, UneCombo, DisplayMember, ValueMember);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }
    }
}
