using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;  // bibliothèque pour les expressions régulières
using MaisonDesLigues;



namespace BaseDeDonnees
{
    class Bdd
    {
        //
        // propriétés membres
        //
        private OracleConnection CnOracle;
        private OracleCommand UneOracleCommand;
        private OracleDataAdapter UnOracleDataAdapter;
        private DataTable UneDataTable;
        private OracleTransaction UneOracleTransaction;
        //
        // méthodes
        //
        /// <summary>
        /// constructeur de la connexion
        /// </summary>
        /// <param name="UnLogin">login utilisateur</param>
        /// <param name="UnPwd">mot de passe utilisateur</param>
        public Bdd(String UnLogin, String UnPwd)
        {
            try
            {
                /// <remarks>on commence par récupérer dans CnString les informations contenues dans le fichier app.config
                /// pour la connectionString de nom StrConnMdl
                /// </remarks>
                ConnectionStringSettings CnString = ConfigurationManager.ConnectionStrings["StrConnMdl"];
                ///<remarks>
                /// on va remplacer dans la chaine de connexion les paramètres par le login et le pwd saisis
                ///dans les zones de texte. Pour ça on va utiliser la méthode Format de la classe String. 
                /// </remarks>
                CnOracle = new OracleConnection(string.Format(CnString.ConnectionString, UnLogin, UnPwd));
                CnOracle.Open();
            }
            catch (OracleException Oex)
            {
                throw new Exception("Erreur à la connexion" + Oex.Message);
            }
        }
        /// <summary>
        /// Méthode permettant de fermer la connexion
        /// </summary>
        public void FermerConnexion()
        {
            this.CnOracle.Close();
        }
        /// <summary>
        /// méthode permettant de renvoyer un message d'erreur provenant de la bd
        /// après l'avoir formatté. On ne renvoie que le message, sans code erreur
        /// </summary>
        /// <param name="unMessage">message à formater</param>
        /// <returns>message formaté à afficher dans l'application</returns>
        private String GetMessageOracle(String unMessage)
        {
            String[] message = Regex.Split(unMessage, "ORA-");
            return (Regex.Split(message[1], ":"))[1];
        }
        /// <summary>
        /// permet de récupérer le contenu d'une table ou d'une vue. 
        /// </summary>
        /// <param name="UneTableOuVue"> nom de la table ou la vue dont on veut récupérer le contenu</param>
        /// <returns>un objet de type datatable contenant les données récupérées</returns>
        public DataTable ObtenirDonnesOracle(String UneTableOuVue)
        {
            string Sql = "select * from " + UneTableOuVue;
            this.UneOracleCommand = new OracleCommand(Sql, CnOracle);
            UnOracleDataAdapter = new OracleDataAdapter();
            UnOracleDataAdapter.SelectCommand = this.UneOracleCommand;
            UneDataTable = new DataTable();
            UnOracleDataAdapter.Fill(UneDataTable);
            return UneDataTable;
        }
        /// <summary>
        /// méthode privée permettant de valoriser les paramètres d'un objet commmand communs aux licenciés, bénévoles et intervenants
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        private void ParamCommunsNouveauxParticipants(OracleCommand Cmd, String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail)
        {
            Cmd.Parameters.Add("pNom", OracleDbType.Varchar2, ParameterDirection.Input).Value = pNom;
            Cmd.Parameters.Add("pPrenom", OracleDbType.Varchar2, ParameterDirection.Input).Value = pPrenom;
            Cmd.Parameters.Add("pAdr1", OracleDbType.Varchar2, ParameterDirection.Input).Value = pAdresse1;
            Cmd.Parameters.Add("pAdr2", OracleDbType.Varchar2, ParameterDirection.Input).Value = pAdresse2;
            Cmd.Parameters.Add("pCp", OracleDbType.Varchar2, ParameterDirection.Input).Value = pCp;
            Cmd.Parameters.Add("pVille", OracleDbType.Varchar2, ParameterDirection.Input).Value = pVille;
            Cmd.Parameters.Add("pTel", OracleDbType.Varchar2, ParameterDirection.Input).Value = pTel;
            Cmd.Parameters.Add("pMail", OracleDbType.Varchar2, ParameterDirection.Input).Value = pMail;
        }
        /// <summary>
        /// procédure qui va se charger d'invoquer la procédure stockée qui ira inscrire un participant de type bénévole
        /// </summary>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pDateNaissance">mail du bénévole</param>
        /// <param name="pNumeroLicence">numéro de licence du bénévole ou null</param>
        /// <param name="pDateBenevolat">collection des id des dates où le bénévole sera présent</param>
        public void InscrireBenevole(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, DateTime pDateNaissance, Int64? pNumeroLicence, Collection<Int16> pDateBenevolat)
        {
            try
            {
                UneOracleCommand = new OracleCommand("pckparticipant.nouveaubenevole", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                UneOracleCommand.Parameters.Add("pDateNaiss", OracleDbType.Date, ParameterDirection.Input).Value = pDateNaissance;
                UneOracleCommand.Parameters.Add("pLicence", OracleDbType.Int64, ParameterDirection.Input).Value = pNumeroLicence;
                //UneOracleCommand.Parameters.Add("pLesDates", OracleDbType.Array, ParameterDirection.Input).Value = pDateBenevolat;
                OracleParameter pLesDates = new OracleParameter();
                pLesDates.ParameterName = "pLesDates";
                pLesDates.OracleDbType = OracleDbType.Int16;
                pLesDates.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                pLesDates.Value = pDateBenevolat.ToArray();
                pLesDates.Size = pDateBenevolat.Count;
                UneOracleCommand.Parameters.Add(pLesDates);
                UneOracleCommand.ExecuteNonQuery();
                MessageBox.Show("inscription bénévole effectuée");
            }
            catch (OracleException Oex)
            {
                MessageBox.Show("Erreur Oracle \n" + Oex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Autre Erreur  \n" + ex.Message);
            }

        }
        /// <summary>
        /// méthode privée permettant de valoriser les paramètres d'un objet commmand spécifiques intervenants
        /// </summary>
        /// <param name="Cmd"> nom de l'objet command concerné par les paramètres</param>
        /// <param name="pIdAtelier"> Id de l'atelier où interviendra l'intervenant</param>
        /// <param name="pIdStatut">statut de l'intervenant pour l'atelier : animateur ou intervenant</param>
        private void ParamsSpecifiquesIntervenant(OracleCommand Cmd,Int16 pIdAtelier, String pIdStatut)
        {
            Cmd.Parameters.Add("pIdAtelier", OracleDbType.Int16, ParameterDirection.Input).Value = pIdAtelier;
            Cmd.Parameters.Add("pIdStatut", OracleDbType.Char, ParameterDirection.Input).Value = pIdStatut;
        }
        /// <summary>
        /// Procédure publique qui va appeler la procédure stockée permettant d'inscrire un nouvel intervenant sans nuité
        /// </summary>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pIdAtelier"> Id de l'atelier où interviendra l'intervenant</param>
        /// <param name="pIdStatut">statut de l'intervenant pour l'atelier : animateur ou intervenant</param>
        public void InscrireIntervenant(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, Int16 pIdAtelier, String pIdStatut)
        {
            /// <remarks>
            /// procédure qui va créer :
            /// 1- un enregistrement dans la table participant
            /// 2- un enregistrement dans la table intervenant 
            ///  en cas d'erreur Oracle, appel à la méthode GetMessageOracle dont le rôle est d'extraire uniquement le message renvoyé
            /// par une procédure ou un trigger Oracle
            /// </remarks>
            /// 
            String MessageErreur = "";
            try
            {
                UneOracleCommand = new OracleCommand("pckparticipant.nouvelintervenant", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle il vaut mieux gérer les transactions dans l'applicatif que dans la bd dans les procédures stockées.
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                // on appelle la procédure ParamCommunsNouveauxParticipants pour charger les paramètres communs aux intervenants
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                // on appelle la procédure ParamsCommunsIntervenant pour charger les paramètres communs aux intervenants
                this.ParamsSpecifiquesIntervenant(UneOracleCommand, pIdAtelier, pIdStatut);        
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
            }
            catch (OracleException Oex)
            {
                MessageErreur = "Erreur Oracle \n" + this.GetMessageOracle(Oex.Message);
             }
            catch (Exception ex)
            {

                MessageErreur = "Autre Erreur, les informations n'ont pas été correctement saisies" + ex;
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }
        /// <summary>
        /// Procédure publique qui va appeler la procédure stockée permettant d'inscrire un nouvel intervenant qui aura des nuités
        /// </summary>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pIdAtelier"> Id de l'atelier où interviendra l'intervenant</param>
        /// <param name="pIdStatut">statut de l'intervenant pour l'atelier : animateur ou intervenant</param>
        /// <param name="pLesCategories">tableau contenant la catégorie de chambre pour chaque nuité à réserver</param>
        /// <param name="pLesHotels">tableau contenant l'hôtel pour chaque nuité à réserver</param>
        /// <param name="pLesNuits">tableau contenant l'id de la date d'arrivée pour chaque nuité à réserver</param>
        public void InscrireIntervenant(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, Int16 pIdAtelier, String pIdStatut, Collection<string> pLesCategories, Collection<string> pLesHotels, Collection<Int16>pLesNuits)
        {
            /// <remarks>
            /// procédure qui va  :
            /// 1- faire appel à la procédure 
            /// un enregistrement dans la table participant
            /// 2- un enregistrement dans la table intervenant 
            /// 3- un à 2 enregistrements dans la table CONTENUHEBERGEMENT
            /// 
            /// en cas d'erreur Oracle, appel à la méthode GetMessageOracle dont le rôle est d'extraire uniquement le message renvoyé
            /// par une procédure ou un trigger Oracle
            /// </remarks>
            /// 
            String MessageErreur="";
            try
            {                
                // pckparticipant.nouvelintervenant est une procédure surchargée
                UneOracleCommand = new OracleCommand("pckparticipant.nouvelintervenant", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle : il vaut mieyx gérer les transactions dans l'applicatif que dans la bd.
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                this.ParamsSpecifiquesIntervenant(UneOracleCommand, pIdAtelier, pIdStatut);

                //On va créer ici les paramètres spécifiques à l'inscription d'un intervenant qui réserve des nuits d'hôtel.
                // Paramètre qui stocke les catégories sélectionnées
                OracleParameter pOraLescategories = new OracleParameter();
                pOraLescategories.ParameterName = "pLesCategories";
                pOraLescategories.OracleDbType = OracleDbType.Char;
                pOraLescategories.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLescategories.Value = pLesCategories.ToArray();
                pOraLescategories.Size = pLesCategories.Count;
                UneOracleCommand.Parameters.Add(pOraLescategories);
               
                // Paramètre qui stocke les hotels sélectionnées
                OracleParameter pOraLesHotels = new OracleParameter();
                pOraLesHotels.ParameterName = "pLesHotels";
                pOraLesHotels.OracleDbType = OracleDbType.Char;
                pOraLesHotels.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesHotels.Value = pLesHotels.ToArray();
                pOraLesHotels.Size = pLesHotels.Count;
                UneOracleCommand.Parameters.Add(pOraLesHotels);
                
                // Paramètres qui stocke les nuits sélectionnées
                OracleParameter pOraLesNuits = new OracleParameter();
                pOraLesNuits.ParameterName = "pLesNuits";
                pOraLesNuits.OracleDbType = OracleDbType.Int16;
                pOraLesNuits.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesNuits.Value = pLesNuits.ToArray();
                pOraLesNuits.Size = pLesNuits.Count;
                UneOracleCommand.Parameters.Add(pOraLesNuits);
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
               
            }
            catch (OracleException Oex)
            {
                //MessageErreur="Erreur Oracle \n" + this.GetMessageOracle(Oex.Message);
                MessageBox.Show(Oex.Message);
            }
            catch (Exception ex)
            {
                
                MessageErreur= "Autre Erreur, les informations n'ont pas été correctement saisies" + ex;
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }             
            }
        }
        /// <summary>
        /// fonction permettant de construire un dictionnaire dont l'id est l'id d'une nuité et le contenu une date
        /// sous la la forme : lundi 7 janvier 2013        /// 
        /// </summary>
        /// <returns>un dictionnaire dont l'id est l'id d'une nuité et le contenu une date</returns>
        public Dictionary<Int16, String> ObtenirDatesNuites()
        {
            Dictionary<Int16, String> LesDatesARetourner = new Dictionary<Int16, String>();
            DataTable LesDatesNuites = this.ObtenirDonnesOracle("VDATENUITE01");
            foreach (DataRow UneLigne in LesDatesNuites.Rows)
            {
                LesDatesARetourner.Add(System.Convert.ToInt16(UneLigne["id"]), UneLigne["libelle"].ToString());
            }
            return LesDatesARetourner;

        }


        // ******************************************************* Debut des modifications **********************************************************************//
        
 

        /// <summary>
        /// Création d'un nouvel atelier
        /// </summary>
        /// <param name="pLibelleAtelier">Libelle de l'atelier</param>
        /// <param name="pNbPlacesMax">Nombre de places maximum de l'atelier</param>
        /// <param name="pLibelleTheme">Libelle du theme</param>
        /// <param name="pHeureDebut">Heure de début de la vacation</param>
        /// <param name="pHeureFin">Heure de fin de la vacation</param>
        public void creerAtelier(String pLibelleAtelier, Int32 pNbPlacesMax, String pLibelleTheme, String pHeureDebut, String pHeureFin)
        {
            String MessageErreur = "";
            try
            {
                UneOracleCommand = new OracleCommand("MDL.PCKGESTION.PAJOUT_ATELIER", CnOracle); //Package.procedure
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle il vaut mieux gérer les transactions dans l'applicatif que dans la bdd dans les procédures stockées.
                UneOracleCommand.Parameters.Add("pLIBELLEATELIER", OracleDbType.Varchar2, ParameterDirection.Input).Value = pLibelleAtelier;
                UneOracleCommand.Parameters.Add("pNBJOURS", OracleDbType.Int32, ParameterDirection.Input).Value = pNbPlacesMax;
                UneOracleCommand.Parameters.Add("pLIBELLETHEME", OracleDbType.Varchar2, ParameterDirection.Input).Value = pLibelleTheme;
                UneOracleCommand.Parameters.Add("pHEUREDEBUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureDebut;
                UneOracleCommand.Parameters.Add("pHEUREFIN", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureFin;
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();

            }
            catch (OracleException Oex)
            {
                UneOracleTransaction.Rollback();
                throw new Exception("Erreur chargement packatage" + Oex.Message);
            }
            catch (Exception ex)
            {
                UneOracleTransaction.Rollback();
                throw new Exception("Erreur creation cours" + ex.Message);
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }

        /// <summary>
        /// Crer une vacation de l'atelier
        /// </summary>
        /// <param name="pIdAtelier">Id de l'atelier</param>
        /// <param name="pHeureDebut">Heure de debut de la vacation</param>
        /// <param name="pHeureFin">Heure de fin de la vacation</param>
        public void creerVacation(Int32 pIdAtelier, String pHeureDebut, String pHeureFin)
        {
            String MessageErreur = "";
            try
            {
                UneOracleCommand = new OracleCommand("mdl.pckgestion.pajout_vacation", CnOracle); //Package.procedure
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle il vaut mieux gérer les transactions dans l'applicatif que dans la bdd dans les procédures stockées.
                UneOracleCommand.Parameters.Add("pIDATELIER", OracleDbType.Int32, ParameterDirection.Input).Value = pIdAtelier;
                UneOracleCommand.Parameters.Add("pHEUREDEBUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureDebut;
                UneOracleCommand.Parameters.Add("pHEUREFIN", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureFin;
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                // excecution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
            }
            catch (OracleException Oex)
            {
                throw new Exception("Erreur chargement packatage" + Oex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception("Erreur creation vacation" + ex.Message);
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }

        /// <summary>
        /// Creer un theme de l'atelier
        /// </summary>
        /// <param name="pIdAtelier">Id de l'atelier</param>
        /// <param name="pLibelleTheme">Libelle du theme</param>
        public void creerTheme(Int16 pIdAtelier, String pLibelleTheme)
        {
            String MessageErreur = "";
            try
            {
                UneOracleCommand = new OracleCommand("MDL.PCKGESTION.PAJOUT_THEME", CnOracle); //Package.procedure
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle il vaut mieux gérer les transactions dans l'applicatif que dans la bdd dans les procédures stockées.
                UneOracleCommand.Parameters.Add("pIDATELIER", OracleDbType.Int16, ParameterDirection.Input).Value = pIdAtelier;
                UneOracleCommand.Parameters.Add("pLIBELLETHEME", OracleDbType.Varchar2, ParameterDirection.Input).Value = pLibelleTheme;
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                // excecution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();

            }
            catch (OracleException Oex)
            {
                throw new Exception("Erreur chargement packatage" + Oex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception("Erreur creation theme" + ex.Message);
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }

        /// <summary>
        /// permet de récupérer le contenu d'une table ou d'une vue. 
        /// </summary>
        /// <param name="IdAtelier"> id de l'atelier permettant de faire la requête</param>
        /// <returns>un objet de type datatable contenant les données récupérées</returns>
        public DataTable ObtenirDonnesOracleVacation(Int16 IdAtelier)
        {
            //requête permettant de remplir la combobox CmbVacationModif par rapport à l'atelier séléctionné par l'utilisateur
            string Sql = "select * from vacation where IDATELIER=" + IdAtelier;
            this.UneOracleCommand = new OracleCommand(Sql, CnOracle);
            UnOracleDataAdapter = new OracleDataAdapter();
            UnOracleDataAdapter.SelectCommand = this.UneOracleCommand;
            UneDataTable = new DataTable();
            UnOracleDataAdapter.Fill(UneDataTable);
            return UneDataTable;
        }

        /// <summary>
        /// Fonction publique qui va appeler la fonction stockée permettant de récuperer l'heure de début et de fin d'une vacation donnée
        /// </summary>
        /// <param name="pNumVac">Numero de la vacation</param>
        /// <param name="heurevac">Nom de la fonction stockée présente de le package mdl.pckgestion</param>
        /// <returns>Heure de début ou de fin d'une vacation donnée</returns>
        public DateTime HeureVac(Int16 pNumVac, String heurevac)
        {
            UneOracleCommand = new OracleCommand();
            UneOracleCommand.Connection = CnOracle;
            UneOracleCommand.CommandText = "select mdl.pckgestion." + heurevac + "(:pnumero) from dual";
            UneOracleCommand.Parameters.Add("pnumero", OracleDbType.Int16, ParameterDirection.Input).Value = pNumVac;
            DateTime Heure = Convert.ToDateTime(UneOracleCommand.ExecuteScalar());
            return Heure;
        }

        /// <summary>
        /// Procédure publique qui va modifier la vacation avec les paramètre de la procédure
        /// </summary>
        /// <param name="pHeureDebutVac">Heure de début de la vacation modifiée</param>
        /// <param name="pHeureFinVac">Heure de fin de la vacation modifiée</param>
        /// <param name="pNumeroVac">Numéro de la vacation a modifier</param>
        public void MajVacation(DateTime pHeureDebutVac, DateTime pHeureFinVac, Int16 pNumeroVac)
        {
            String MessageErreur = "";
            try
            {
                UneOracleCommand = new OracleCommand("MDL.pckgestion.pmodif_vacation", CnOracle); //Package.procedure
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle il vaut mieux gérer les transactions dans l'applicatif que dans la bd dans les procédures stockées.
                UneOracleCommand.Parameters.Add("pNumero", OracleDbType.Int16, ParameterDirection.Input).Value = pNumeroVac;
                UneOracleCommand.Parameters.Add("pHeureDebut", OracleDbType.Date, ParameterDirection.Input).Value = pHeureDebutVac;
                UneOracleCommand.Parameters.Add("pHeureFin", OracleDbType.Date, ParameterDirection.Input).Value = pHeureFinVac;
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
            }
            catch (OracleException Oex)
            {
                UneOracleTransaction.Rollback();
                throw new Exception("Erreur chargement packatage" + Oex.Message);
            }
            catch (Exception ex)
            {
                UneOracleTransaction.Rollback();
                throw new Exception("Erreur de la mise à jour de la vacation" + ex.Message);
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }
    }
}
