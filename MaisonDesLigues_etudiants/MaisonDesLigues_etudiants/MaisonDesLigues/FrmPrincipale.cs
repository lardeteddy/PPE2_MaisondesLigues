using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.ObjectModel;
using ComposantNuite;
using BaseDeDonnees;
using ComposantPPE;
namespace MaisonDesLigues
{
    public partial class FrmPrincipale : Form
    {

        /// <summary>
        /// constructeur du formulaire
        /// </summary>
        public FrmPrincipale()
        {
            InitializeComponent();
        }
        private Bdd UneConnexion;
        private String TitreApplication;
        private String IdStatutSelectionne = "";
        private ComposantPPE.ComposantPPE UnComposant;
        /// <summary>
        /// création et ouverture d'une connexion vers la base de données sur le chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrincipale_Load(object sender, EventArgs e)
        {
            UneConnexion = ((FrmLogin)Owner).UneConnexion;
            TitreApplication = ((FrmLogin)Owner).TitreApplication;
            this.Text = TitreApplication;
        }
        /// <summary>
        /// gestion de l'événement click du bouton quitter.
        /// Demande de confirmation avant de quitetr l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous quitter l'application ?", ConfigurationManager.AppSettings["TitreApplication"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                UneConnexion.FermerConnexion();
                Application.Exit();
            }
        }

        private void RadTypeParticipant_Changed(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "RadBenevole":
                    this.GererInscriptionBenevole();
                    break;
                case "RadLicencie":
                    //this.GererInscriptionLicencie();
                    break;
                case "RadIntervenant":
                    this.GererInscriptionIntervenant();
                    break;

                default:
                    throw new Exception("Erreur interne à l'application");
            }
        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un intervenant.
        /// </summary>
        private void GererInscriptionIntervenant()
        {

            GrpBenevole.Visible = false;
            GrpIntervenant.Visible = true;
            PanFonctionIntervenant.Visible = true;
            GrpIntervenant.Left = 23;
            GrpIntervenant.Top = 264;
            Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanFonctionIntervenant, "RadioButton", this.rdbStatutIntervenant_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierIntervenant, "VATELIER01");

            CmbAtelierIntervenant.Text = "Choisir";

        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie des disponibilités des bénévoles.
        /// </summary>
        private void GererInscriptionBenevole()
        {

            GrpBenevole.Visible = true;
            GrpBenevole.Left = 23;
            GrpBenevole.Top = 264;
            GrpIntervenant.Visible = false;

            Utilitaire.CreerDesControles(this, UneConnexion, "VDATEBENEVOLAT01", "ChkDateB_", PanelDispoBenevole, "CheckBox", this.rdbStatutIntervenant_StateChanged);
            // on va tester si le controle à placer est de type CheckBox afin de lui placer un événement checked_changed
            // Ceci afin de désactiver les boutons si aucune case à cocher du container n'est cochée
            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox")
                {
                    CheckBox UneCheckBox = (CheckBox)UnControle;
                    UneCheckBox.CheckedChanged += new System.EventHandler(this.ChkDateBenevole_CheckedChanged);
                }
            }


        }
        /// <summary>
        /// permet d'appeler la méthode VerifBtnEnregistreIntervenant qui déterminera le statu du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbStatutIntervenant_StateChanged(object sender, EventArgs e)
        {
            // stocke dans un membre de niveau form l'identifiant du statut sélectionné (voir règle de nommage des noms des controles : prefixe_Id)
            this.IdStatutSelectionne = ((RadioButton)sender).Name.Split('_')[1];
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }
        /// <summary>
        /// Permet d'intercepter le click sur le bouton d'enregistrement d'un bénévole.
        /// Cetteméthode va appeler la méthode InscrireBenevole de la Bdd, après avoir mis en forme certains paramètres à envoyer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistreBenevole_Click(object sender, EventArgs e)
        {
            Collection<Int16> IdDatesSelectionnees = new Collection<Int16>();
            Int64? NumeroLicence;
            if (TxtLicenceBenevole.MaskCompleted)
            {
                NumeroLicence = System.Convert.ToInt64(TxtLicenceBenevole.Text);
            }
            else
            {
                NumeroLicence = null;
            }


            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox" && ((CheckBox)UnControle).Checked)
                {
                    /* Un name de controle est toujours formé come ceci : xxx_Id où id représente l'id dans la table
                     * Donc on splite la chaine et on récupére le deuxième élément qui correspond à l'id de l'élément sélectionné.
                     * on rajoute cet id dans la collection des id des dates sélectionnées
                        
                    */
                    IdDatesSelectionnees.Add(System.Convert.ToInt16((UnControle.Name.Split('_'))[1]));
                }
            }
            UneConnexion.InscrireBenevole(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToDateTime(TxtDateNaissance.Text), NumeroLicence, IdDatesSelectionnees);

        }
        /// <summary>
        /// Cetet méthode teste les données saisies afin d'activer ou désactiver le bouton d'enregistrement d'un bénévole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkDateBenevole_CheckedChanged(object sender, EventArgs e)
        {
            BtnEnregistreBenevole.Enabled = (TxtLicenceBenevole.Text == "" || TxtLicenceBenevole.MaskCompleted) && TxtDateNaissance.MaskCompleted && Utilitaire.CompteChecked(PanelDispoBenevole) > 0;
        }
        /// <summary>
        /// Méthode qui permet d'afficher ou masquer le controle panel permettant la saisie des nuités d'un intervenant.
        /// S'il faut rendre visible le panel, on teste si les nuités possibles ont été chargés dans ce panel. Si non, on les charges 
        /// On charge ici autant de contrôles ResaNuit qu'il y a de nuits possibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdbNuiteIntervenant_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Name == "RdbNuiteIntervenantOui")
            {
                PanNuiteIntervenant.Visible = true;
                if (PanNuiteIntervenant.Controls.Count == 0) // on charge les nuites possibles possibles et on les affiche
                {
                    //DataTable LesDateNuites = UneConnexion.ObtenirDonnesOracle("VDATENUITE01");
                    //foreach(Dat
                    Dictionary<Int16, String> LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 5 + (24 * i++);
                        unResaNuit.Visible = true;
                        //unResaNuit.click += new System.EventHandler(ComposantNuite_StateChanged);
                        PanNuiteIntervenant.Controls.Add(unResaNuit);
                    }

                }

            }
            else
            {
                PanNuiteIntervenant.Visible = false;

            }
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();

        }

        /// <summary>
        /// Cette procédure va appeler la procédure .... qui aura pour but d'enregistrer les éléments 
        /// de l'inscription d'un intervenant, avec éventuellment les nuités à prendre en compte        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistrerIntervenant_Click(object sender, EventArgs e)
        {
            try
            {
                if (RdbNuiteIntervenantOui.Checked)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    foreach (Control UnControle in PanNuiteIntervenant.Controls)
                    {
                        if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                        {
                            // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                            //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                            CategoriesSelectionnees.Add(((ResaNuite)UnControle).GetTypeChambreSelectionnee());
                            HotelsSelectionnes.Add(((ResaNuite)UnControle).GetHotelSelectionne());
                            NuitsSelectionnes.Add(((ResaNuite)UnControle).IdNuite);
                        }

                    }
                    if (NuitsSelectionnes.Count == 0)
                    {
                        MessageBox.Show("Si vous avez sélectionné que l'intervenant avait des nuités\n in faut qu'au moins une nuit soit sélectionnée");
                    }
                    else
                    {
                        UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes);
                        MessageBox.Show("Inscription intervenant effectuée");
                    }
                }
                else
                { // inscription sans les nuitées
                    UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne);
                    MessageBox.Show("Inscription intervenant effectuée");

                }


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Méthode privée testant le contrôle combo et la variable IdStatutSelectionne qui contient une valeur
        /// Cette méthode permetra ensuite de définir l'état du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <returns></returns>
        private Boolean VerifBtnEnregistreIntervenant()
        {
            return CmbAtelierIntervenant.Text != "Choisir" && this.IdStatutSelectionne.Length > 0;
        }
        /// <summary>
        /// Méthode permettant de définir le statut activé/désactivé du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAtelierIntervenant_TextChanged(object sender, EventArgs e)
        {
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }



        //********************************************************Debut des modifications************************************************

        /// <summary>
        /// Permet lors de la selection d'un radio bouton d'executer la fonction associée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radGestionTypeTable_changed(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "radAtelier":
                    this.GererAjoutAtelier();
                    break;
                case "radTheme":
                    this.GererAjoutTheme();
                    break;
                case "radVacation":
                    this.GererAjoutVacation();
                    break;
                case "radModifVacation":
                    this.GererModifVacation();
                    break;
                default:
                    throw new Exception("Erreur interne à l'application");
            }
        }

        /// <summary>
        /// Fonction permettant d'afficher le groupBox AjoutAtelier
        /// </summary>
        private void GererAjoutAtelier()
        {
            grpAjoutAtelier.Visible = true;
            grpAjoutTheme.Visible = false;
            grpAjoutVacation.Visible = false;
            grpAjoutAtelier.Left = 264;
            grpAjoutAtelier.Top = 88;
            grpModifVacation.Visible = false;
        }

        /// <summary>
        /// Fonction permettant d'afficher le groupBox AjoutTheme
        /// </summary>
        private void GererAjoutTheme()
        {
            grpAjoutAtelier.Visible = false;
            grpAjoutTheme.Visible = true;
            grpAjoutVacation.Visible = false;
            grpAjoutTheme.Left = 264;
            grpAjoutTheme.Top = 88;
            grpModifVacation.Visible = false;

            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierTheme, "VATELIER01");
            CmbAtelierTheme.Text = "Choisir un Atelier dans cette liste";
        }

        /// <summary>
        /// Fonction permettant d'afficher le groupBox AjoutVacation
        /// </summary>
        private void GererAjoutVacation()
        {
            grpAjoutAtelier.Visible = false;
            grpAjoutTheme.Visible = false;
            grpAjoutVacation.Visible = true;
            grpModifVacation.Visible = false;
            grpAjoutVacation.Left = 264;
            grpAjoutVacation.Top = 88;

            Utilitaire.RemplirComboBox(UneConnexion, CmbAtVacInsert, "VATELIER01");
            CmbAtVacInsert.Text = "Choisir un Atelier dans cette liste";

        }

        /// <summary>
        /// Fonction permettant d'afficher le groupBox ModifVacation
        /// </summary>
        private void GererModifVacation()
        {
            grpAjoutAtelier.Visible = false;
            grpAjoutTheme.Visible = false;
            grpAjoutVacation.Visible = false;
            grpModifVacation.Visible = true;
            grpModifVacation.Left = 264;
            grpModifVacation.Top = 88;

            Utilitaire.RemplirComboBox(UneConnexion, CmbAtVacUpdate, "VATELIER01");
            CmbAtVacUpdate.Text = "Choisir un Atelier dans cette liste";

        }

        /// <summary>
        /// Bouton permettant d'arreter le programme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdQuitter2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous quitter l'application ?", ConfigurationManager.AppSettings["TitreApplication"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                UneConnexion.FermerConnexion();
                Application.Exit();
            }
        }

        /// <summary>
        /// Bouton permettant l'ajout d'un atelier, d'un theme et d'une vacation associé a l'atelier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjoutAtelier_Click(object sender, EventArgs e)
        {
            // Concatenation de la date de la vacation, avec l'heure de debut ou de fin de vacation recuperer du composant
            String HeureDeb = Convert.ToString(composantPPE1.GetDate()) + " " + Convert.ToString(composantPPE1.GetHeureDeb());
            String HeureFin = Convert.ToString(composantPPE1.GetDate()) + " " + Convert.ToString(composantPPE1.GetHeureFin());

            UneConnexion.creerAtelier(TxtLibelleAtelier.Text, Convert.ToInt32(TxtNbPlacesMax.Text), txt_LibelleThemeAjoutAtelier.Text, HeureDeb, HeureFin);
            MessageBox.Show("L'Atelier a bien été créé !");
        }

        /// <summary>
        /// Bouton permettant l'ajout d'une vacation pour un atelier choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjoutVacation_Click(object sender, EventArgs e)
        {
            try
            {
                // Concatenation de la date de la vacation, avec l'heure de debut ou de fin de vacation recuperer du composant
                String HeureDeb = Convert.ToString(composantVac.GetDate()) + " " + Convert.ToString(composantVac.GetHeureDeb());
                String HeureFin = Convert.ToString(composantVac.GetDate()) + " " + Convert.ToString(composantVac.GetHeureFin());

                //MessageBox.Show(HeureDeb);
                UneConnexion.creerVacation(Convert.ToInt32(CmbAtVacInsert.SelectedValue), HeureDeb, HeureFin);
                MessageBox.Show("La vacation a bien été créé !");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Bouton permettant l'ajout d'un theme en rapport avec l'atelier choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjoutTheme_Click(object sender, EventArgs e)
        {
            try
            {
                UneConnexion.creerTheme(Convert.ToInt16(CmbAtelierTheme.SelectedValue), TxtLibelleTheme.Text);
                MessageBox.Show("Le theme a bien été créé !");
            }
            catch (Exception)
            {
                MessageBox.Show("Le theme n'a pu être créé !");
            }
        }

        /// <summary>
        /// procédure privée permettant à la séléction d'un atelier dans la combobox CmbAtVacUpdate de charger la combobox CmbVacation Modif
        /// Ainsi que de remplir celle-ci.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAtVacUpdate_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            //permet d'effacer le composant quand la valeur de CmbAtVacUpdate est modifier
            if (this.grpModifVacation.Controls.Contains(UnComposant))
            {
                this.grpModifVacation.Controls.Remove(UnComposant);
            }

            //Permet de remplir la comboBox suivante avec les données d'oacle
            Utilitaire.RemplirComboBox(UneConnexion, UneConnexion.ObtenirDonnesOracleVacation(Convert.ToInt16(CmbAtVacUpdate.SelectedValue)), cmb_VacModifVac, "numero", "numero");
            cmb_VacModifVac.Text = "Choisir une Vacation dans cette liste";
        }

        /// <summary>
        /// procédure privée permettant à la séléction d'une vacation d'un atelier dans la combobox cmb_VacModifVac de charger le composant associé a la vacation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_VacModifVac_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //permet d'effacer le composant quand la valeur de cmb_VacModifVac est modifier
            if (this.grpModifVacation.Controls.Contains(UnComposant))
            {
                this.grpModifVacation.Controls.Remove(UnComposant);
            }

            //Crée un composant avec les valeurs d'oracle
            UnComposant = new ComposantPPE.ComposantPPE(UneConnexion.HeureVac(Convert.ToInt16(cmb_VacModifVac.SelectedValue), "heuredebut"), UneConnexion.HeureVac(Convert.ToInt16(cmb_VacModifVac.SelectedValue), "heuredebut"), UneConnexion.HeureVac(Convert.ToInt16(cmb_VacModifVac.SelectedValue), "heurefin"));
            grpModifVacation.Controls.Add(UnComposant);
            UnComposant.Left = 49;
            UnComposant.Top = 150;
            UnComposant.Visible = true;
            BtnModifVacation.Enabled = true;
        }

        /// <summary>
        /// Bouton permettant la modification d'une vacation d'un atelier choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifVacation_Click(object sender, EventArgs e)
        {
            try
            {
                // Concatenation de la date de la vacation, avec l'heure de debut ou de fin de vacation recuperer du composant
                String DateHeureDeb = UnComposant.GetDate() + " " + Convert.ToString(UnComposant.GetHeureDeb());
                String DateHeureFin = UnComposant.GetDate() + " " + Convert.ToString(UnComposant.GetHeureFin());

                UneConnexion.MajVacation(Convert.ToDateTime(DateHeureDeb), Convert.ToDateTime(DateHeureFin), Convert.ToInt16(cmb_VacModifVac.SelectedValue));
                MessageBox.Show("La Vacation a bien été maj !");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }



    }
}
