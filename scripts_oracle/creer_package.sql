-- -----------------------------------------------------------------------------
--             Génération d'une base de données pour
--                      Oracle Version 10g XE
--                        
-- -----------------------------------------------------------------------------
--      Projet : MaisonDesLigues
--      Auteur : Benoît ROCHE
--      Date de dernière modification : 19/01/2013 11:32:40
-- -----------------------------------------------------------------------------

-- -----------------------------------------------------------------------------
--      Partie 4: Création 
--				- des packages contenant les procédures et fonctions stockées
-- 				- des triggers
--
--		Ce script doit être exécuté par un l'utilisateur MDL
--		(celui qui vient d'être créé dans le script creer_user)
--- -----------------------------------------------------------------------------

-- -----------------------------------------------------------------------------
--       PACKAGE ATELIER
-- 
--       PACKAGE ATELIER ENTETE

/
--       PACKAGE ATELIER BODY

/


-- -----------------------------------------------------------------------------
--      FIN PACKAGE ATELIER
----------------------------------------------------------------


-- -----------------------------------------------------------------------------
--       PACKAGE PARTICIPANT
-- 
drop public synonym pckparticipant;
drop package pckparticipant;
--       PACKAGE PARTICIPANT ENTETE
create or replace
package pckparticipant
is

type tids IS TABLE OF integer  INDEX BY pls_integer range 0..9;
type tchars4 IS TABLE OF char(4)  INDEX BY pls_integer range 0..9;
type tchars1 IS TABLE OF char(1)  INDEX BY pls_integer range 0..9;
--type collection IS REF CURSOR ;
--type tids IS TABLE OF integer  INDEX BY pls_integer range 0..9;
/*
*/
procedure NOUVEAULICENCIE(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,  
  pLicence benevole.numerolicence%type,
  pQualite qualite.id%type,
  pLesAteliers tids,
  pNumCheque paiement.numerocheque%type,
  pMontantCheque paiement.montantcheque%type,
  pTypePaiement paiement.typepaiement%type
  );
procedure NOUVEAUBENEVOLE(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,
  pDateNaiss benevole.datenaissance%type,
  pLicence licencie.numerolicence%type,
  pLesdates tids
  );
  procedure NOUVELINTERVENANT(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,  
  pidatelier atelier.id%type,
  pstatutintervenant statut.id%type
  );
procedure NOUVELINTERVENANT(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,  
  pidatelier atelier.id%type,
  pstatutintervenant statut.id%type,
  plescategories tchars1,
  pleshotels tchars4,
  plesnuits tids
  );

procedure ENREGISTREPAIEMENT(
  pLicencie licencie.idlicencie%type,
  pNumCheque paiement.numerocheque%type,  
  pMontantCheque paiement.montantcheque%type,
  pTypePaiement paiement.typepaiement%type); 

end pckparticipant;
/
create or replace
package body pckparticipant
is
erreur exception;
/*
  Création d'une procédure privée qui va paermetre d'insérer une ligne dans la table participant
  Cette procédure est appelée par las procédures :
  -nouveaubenevole,
  -nouveaulicencié,
  -nouveauintervenant
  - le paramètre newid est un paramètre out pour renvoyer à la procédure appelante 
  l'id du participant créé. Cela évie dans les procédures appemantes d'avoir accès à la sesxxx.currval, car le currval ramené pourrait
  être différent de l'id du participant si qq a entre temps créé un nouveau participant
*/
  procedure creerparticipant(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,
  newid out participant.id%type)
  is  
  Begin
        insert into participant(id, nomparticipant, prenomparticipant, adresseparticipant1, adresseparticipant2,cpparticipant, villeparticipant,telparticipant, mailparticipant, dateinscription)
        values (seqparticipant.nextval, pNom,pPrenom,pAdr1,pAdr2,pCp,pVille,pTel,pMail, sysdate);
        newid:=seqparticipant.currval;  
Exception
  when others then
    raise_application_error(-20100, 'Erreur à la création du participant ');
end creerparticipant;
 
 /*
 La procédure NOUVEAULICENCIE va 
 1- créer un nouveau participant en appelent la procédure creerparticipant
 2- créer un enregistrement dans la table licencié
 3- enregistrer le paiement, OBLIGATOIRE à ce moment là.
 Ce paiement peut être ici : inscription ou tout
 */
 /*
procédure privée quii va inscrire un intervenant dans la table intervenant.
L'insertion déclenchera un trigger qui vérifiera si l'intervenant est animateur pour l'atelier choisi, 
et donc qu'il n'y a pas déjà un animateur pour cet atelier
*/
procedure creerintervenant(pidatelier atelier.id%type, pstatutintervenant statut.id%type, newid participant.id%type )
is
dejaanimateur exception;
pragma exception_init(dejaanimateur, -20112);
begin
    insert into intervenant(idintervenant, idatelier, idstatut) values(newid,pidatelier,pstatutintervenant);
Exception
    when dejaanimateur then
      raise_application_error(-20112 ,'cet atelier a déjà son animateur, inscription impossible');
    when others then
      raise_application_error(-20102, 'Erreur à la création de l''intervenant');
end;

procedure creercontenuhebergement(plescategories tchars1, pleshotels tchars4, plesnuits tids, newid participant.id%type)
is
vnumordre number(1) :=0;
begin
  FOR i IN plescategories.FIRST .. plescategories.LAST 
  LOOP
      vnumordre:=vnumordre + 1;
      insert into contenuhebergement(idparticipant, numordre,codehotel, idcategorie, iddatearriveenuitee)
       values (newid, vnumordre, pleshotels(i) ,plescategories(i), plesnuits(i));   
  END LOOP;
Exception
   when others then
      raise_application_error(-20104, 'Erreur à la création du contenu de l''hébergement');  
end creercontenuhebergement;
/*

*/
 procedure NOUVEAULICENCIE(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,
  pLicence benevole.numerolicence%type,
  pQualite qualite.id%type,
  pLesAteliers tids,
  pNumCheque paiement.numerocheque%type,
  pMontantCheque paiement.montantcheque%type,
  pTypePaiement paiement.typepaiement%type
  )
  is
  tropdateliers Exception;
  errparticipant Exception;
   pragma exception_init(tropdateliers, -20001);
   pragma exception_init(errparticipant, -20100);
   newid participant.id%type;

  begin
    creerparticipant(pNom,pPrenom,pAdr1,pAdr2,pCp,pVille,pTel,pMail,newid );
    insert into licencie(idlicencie, idqualite, numerolicence) 
        values(newid, pQualite, pLicence);
    enregistrepaiement(seqpaiement.nextval,newid ,pMontantCheque, pTypePaiement);
    FOR i IN pLesAteliers.FIRST .. pLesAteliers.LAST 
    LOOP
      insert into inscrire(idparticipant, idatelier) values (newid, pLesAteliers(i));
    END LOOP;

  exception
    when tropdateliers then
      raise_application_error(-20001 , 'Inscription impossible, nombre d''ateliers limité à 5');
    when errparticipant then
      raise_application_error(-20100 , 'Erreur à la création du participant ');
    when others then
      raise_application_error(-20103, 'erreur à la création du licencie ');        
  end;
 /*
 La procédure ENREGISTREPAIEMENT va enregistrer le paiement d'un congressiste.
 Elle peut être appelée par la procédure NOUVEAULICENCIE dans le cas de l'inscription d'un licencié
 ou encore directement du programme pour l'enregistrement d'un accompagnant.
 */
  
  procedure ENREGISTREPAIEMENT(
  pLicencie licencie.idlicencie%type,
  pNumCheque paiement.numerocheque%type,  
  pMontantCheque paiement.montantcheque%type,
  pTypePaiement paiement.typepaiement%type)
  is
  begin
    null;
  end;
  procedure creerbenevole (pLicence benevole.numerolicence%type,  pdatenaiss benevole.datenaissance%type, newid participant.id%type)
  is
  benevdejainscrit exception;
    pragma exception_init(benevdejainscrit, -20110);
  begin
    insert into benevole(idbenevole, numerolicence, datenaissance)
      values(newid, pLicence, pdatenaiss);
  EXCEPTION
    when benevdejainscrit then
      raise_application_error(-20110 , 'bénévole déjà inscrit, vous devez faire une modification de bénévole');
    when others then
      raise_application_error(-20101 , 'Erreur à la création du bénévole');
  end;
  
  procedure creeretrepresent(pLesdates tids, newid participant.id%type)
  is
  begin
    FOR i IN pLesdates.FIRST .. pLesdates.LAST 
    LOOP
      insert into etrepresent(idbenevole, IDDATEPRESENT) values (newid, pLesdates(i));
    END LOOP;
  EXCEPTION
    when others then
      raise_application_error(-20105 , 'Erreur à la création des présences du bénévole');
  end creeretrepresent;
  
  procedure NOUVEAUBENEVOLE( 
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,
  pDateNaiss benevole.datenaissance%type,
  pLicence licencie.numerolicence%type,
  pLesdates tids
  )
  is 
  newid participant.id%type;
  erreurbenevole exception;
  errparticipant exception;
  erreurpresencebenevole exception;
  benevdejainscrit exception;
  pragma exception_init(errparticipant, -20100);
  pragma exception_init(erreurbenevole, -20101);
  pragma exception_init(benevdejainscrit, -20110);
  pragma exception_init(erreurpresencebenevole, -20105);
  begin
    creerparticipant(pNom,pPrenom,pAdr1,pAdr2,pCp,pVille,pTel,pMail, newid );
    creerbenevole(plicence, pdatenaiss, newid);
    creeretrepresent(pLesdates, newid);
  exception
      when errparticipant then
        raise_application_error(-20100 , 'Erreur à la création du participant ');
      when benevdejainscrit then
        raise_application_error(-20110 , 'bénévole déjà inscrit, \n vous devez faire une modification de bénévole');
      when erreurbenevole then
        raise_application_error(-20101 , 'Erreur à la création du benevole ');
      when others then
        raise_application_error(-20202, 'erreur inattendue lors de la création d''un bénévole');  
end;


/*
Procédure qui inscrit un intervenant sans nuité
*/
procedure NOUVELINTERVENANT(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,  
  pidatelier atelier.id%type,
  pstatutintervenant statut.id%type
  )
  is
  newid participant.id%type;
  erreurparticipant Exception;
  erreurintervenant Exception;
  dejaanimateur Exception;
  pragma exception_init(dejaanimateur, -20112);
  pragma exception_init(erreurparticipant, -20100);
  pragma exception_init(erreurintervenant, -20102);
  begin
    creerparticipant(pNom,pPrenom,pAdr1,pAdr2,pCp,pVille,pTel,pMail,newid );
    creerintervenant(pidatelier,pstatutintervenant, newid);  
exception
    when erreurparticipant then
      raise_application_error(-20100, 'erreur à la création du participant');
    when erreurintervenant then
      raise_application_error(-20102, 'erreur à la création de l''intervenant ');
    when dejaanimateur then
      raise_application_error(-20112,'cet atelier a déjà son animateur, inscription impossible');
    when others then
      raise_application_error(-20203, 'Autre erreur innattendue lors de la création d''un intervenant');
  end;



/*
Procédure qui inscrit un intervenant avec nuité
Cette procédure va faire appel à la procédure surchargée NOUVELINTERVENANT
*/
procedure NOUVELINTERVENANT(
  pNom participant.nomparticipant%type,
  pPrenom participant.prenomparticipant%type,
  pAdr1 participant.adresseparticipant1%type,
  pAdr2 participant.adresseparticipant2%type,
  pCp participant.cpparticipant%type,
  pVille participant.villeparticipant%type,
  pTel participant.telparticipant%type,
  pMail participant.mailparticipant%type,  
  pidatelier atelier.id%type,
  pstatutintervenant statut.id%type,
  plescategories tchars1,
  pleshotels tchars4,
  plesnuits tids
  )
  is
  newid participant.id%type;
  erreurparticipant Exception;
  erreurintervenant Exception;
  erreurcontenuhebergement Exception;
  dejaanimateur Exception;
  pragma exception_init(erreurparticipant, -20100);
  pragma exception_init(erreurintervenant, -20102);
  pragma exception_init(erreurcontenuhebergement, -20104);
  pragma exception_init(dejaanimateur, -20110);  
  begin
    creerparticipant(pNom,pPrenom,pAdr1,pAdr2,pCp,pVille,pTel,pMail, newid);
    creerintervenant(pidatelier,pstatutintervenant, newid); 
    creercontenuhebergement(plescategories,pleshotels,plesnuits, newid);
exception
    when erreurparticipant then
      raise_application_error(-20100, 'erreur à la création du participant');
    when erreurintervenant then
      raise_application_error(-20102, 'erreur à la création de l''intervenant ');
    when erreurcontenuhebergement then
      raise_application_error(-20104,'Erreur à la création du contenu de l''hébergement');
    when dejaanimateur then
      raise_application_error(-20112,'cet atelier a déjà son animateur, inscription impossible');
    when others then
      raise_application_error(-20203, 'Autre erreur innattendue lors de la création d''un intervenant');
  end;

end pckparticipant;
/
create public synonym pckparticipant for mdl.pckparticipant;
--       PACKAGE PARTICIPANT BODY
 
/