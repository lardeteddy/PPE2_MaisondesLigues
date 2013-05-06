using MaisonDesLigues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProjectPPE
{
    
    
    /// <summary>
    ///Classe de test pour FrmPrincipaleTest, destinée à contenir tous
    ///les tests unitaires FrmPrincipaleTest
    ///</summary>
    [TestClass()]
    public class FrmPrincipaleTest
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
        ///Test pour Constructeur FrmPrincipale
        ///</summary>
        [TestMethod()]
        public void FrmPrincipaleConstructorTest()
        {
            FrmPrincipale target = new FrmPrincipale();
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour BtnAjoutAtelier_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void BtnAjoutAtelier_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.BtnAjoutAtelier_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour BtnAjoutTheme_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void BtnAjoutTheme_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.BtnAjoutTheme_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour BtnAjoutVacation_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void BtnAjoutVacation_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.BtnAjoutVacation_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour BtnEnregistreBenevole_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void BtnEnregistreBenevole_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.BtnEnregistreBenevole_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour BtnEnregistrerIntervenant_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void BtnEnregistrerIntervenant_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.BtnEnregistrerIntervenant_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour BtnModifVacation_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void BtnModifVacation_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.BtnModifVacation_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour ChkDateBenevole_CheckedChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void ChkDateBenevole_CheckedChangedTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.ChkDateBenevole_CheckedChanged(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour CmbAtVacUpdate_SelectionChangeCommitted_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void CmbAtVacUpdate_SelectionChangeCommitted_1Test()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.CmbAtVacUpdate_SelectionChangeCommitted_1(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour CmbAtelierIntervenant_TextChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void CmbAtelierIntervenant_TextChangedTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.CmbAtelierIntervenant_TextChanged(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour CmdQuitter2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void CmdQuitter2_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.CmdQuitter2_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour CmdQuitter_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void CmdQuitter_ClickTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.CmdQuitter_Click(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void DisposeTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            bool disposing = false; // TODO: initialisez à une valeur appropriée
            target.Dispose(disposing);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour FrmPrincipale_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void FrmPrincipale_LoadTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.FrmPrincipale_Load(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour GererAjoutAtelier
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void GererAjoutAtelierTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            target.GererAjoutAtelier();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour GererAjoutTheme
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void GererAjoutThemeTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            target.GererAjoutTheme();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour GererAjoutVacation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void GererAjoutVacationTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            target.GererAjoutVacation();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour GererInscriptionBenevole
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void GererInscriptionBenevoleTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            target.GererInscriptionBenevole();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour GererInscriptionIntervenant
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void GererInscriptionIntervenantTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            target.GererInscriptionIntervenant();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour GererModifVacation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void GererModifVacationTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            target.GererModifVacation();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void InitializeComponentTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            target.InitializeComponent();
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour RadTypeParticipant_Changed
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void RadTypeParticipant_ChangedTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.RadTypeParticipant_Changed(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour RdbNuiteIntervenant_CheckedChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void RdbNuiteIntervenant_CheckedChangedTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.RdbNuiteIntervenant_CheckedChanged(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour VerifBtnEnregistreIntervenant
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void VerifBtnEnregistreIntervenantTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            bool expected = false; // TODO: initialisez à une valeur appropriée
            bool actual;
            actual = target.VerifBtnEnregistreIntervenant();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour cmb_VacModifVac_SelectionChangeCommitted
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void cmb_VacModifVac_SelectionChangeCommittedTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.cmb_VacModifVac_SelectionChangeCommitted(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour radGestionTypeTable_changed
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void radGestionTypeTable_changedTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.radGestionTypeTable_changed(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour rdbStatutIntervenant_StateChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MaisonDesLigues.exe")]
        public void rdbStatutIntervenant_StateChangedTest()
        {
            FrmPrincipale_Accessor target = new FrmPrincipale_Accessor(); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            EventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.rdbStatutIntervenant_StateChanged(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }
    }
}
