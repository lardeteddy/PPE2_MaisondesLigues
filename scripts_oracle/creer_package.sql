-- -----------------------------------------------------------------------------
--             G�n�ration d'une base de donn�es pour
--                      Oracle Version 10g XE
--                        
-- -----------------------------------------------------------------------------
--      Projet : MaisonDesLigues
--      Auteur : Beno�t ROCHE
--      Date de derni�re modification : 19/01/2013 11:32:40
-- -----------------------------------------------------------------------------

-- -----------------------------------------------------------------------------
--      Partie 4: Cr�ation 
--				- des packages contenant les proc�dures et fonctions stock�es
-- 				- des triggers
--
--		Ce script doit �tre ex�cut� par un l'utilisateur MDL
--		(celui qui vient d'�tre cr�� dans le script creer_user)
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
  Cr�ation d'une proc�dure priv�e qui va paermetre d'ins�rer une ligne dans la table participant
  Cette proc�dure est appel�e par las proc�dures :
  -nouveaubenevole,
  -nouveaulicenci�,
  -nouveauintervenant
  - le param�tre newid est un param�tre out pour renvoyer � la proc�dure appelante 
  l'id du participant cr��. Cela �vie dans les proc�dures appemantes d'avoir acc�s � la sesxxx.currval, car le currval ramen� pourrait
  �tre diff�rent de l'id du participant si qq a entre temps cr�� un nouveau participant
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
    raise_application_error(-20100, 'Erreur � la cr�ation du participant ');
end creerparticipant;
 
 /*
 La proc�dure NOUVEAULICENCIE va 
 1- cr�er un nouveau participant en appelent la proc�dure creerparticipant
 2- cr�er un enregistrement dans la table licenci�
 3- enregistrer le paiement, OBLIGATOIRE � ce moment l�.
 Ce paiement peut �tre ici : inscription ou tout
 */
 /*
proc�dure priv�e quii va inscrire un intervenant dans la table intervenant.
L'insertion d�clenchera un trigger qui v�rifiera si l'intervenant est animateur pour l'atelier choisi, 
et donc qu'il n'y a pas d�j� un animateur pour cet atelier
*/
procedure creerintervenant(pidatelier atelier.id%type, pstatutintervenant statut.id%type, newid participant.id%type )
is
dejaanimateur exception;
pragma exception_init(dejaanimateur, -20112);
begin
    insert into intervenant(idintervenant, idatelier, idstatut) values(newid,pidatelier,pstatutintervenant);
Exception
    when dejaanimateur then
      raise_application_error(-20112 ,'cet atelier a d�j� son animateur, inscription impossible');
    when others then
      raise_application_error(-20102, 'Erreur � la cr�ation de l''intervenant');
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
      raise_application_error(-20104, 'Erreur � la cr�ation du contenu de l''h�bergement');  
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
      raise_application_error(-20001 , 'Inscription impossible, nombre d''ateliers limit� � 5');
    when errparticipant then
      raise_application_error(-20100 , 'Erreur � la cr�ation du participant ');
    when others then
      raise_application_error(-20103, 'erreur � la cr�ation du licencie ');        
  end;
 /*
 La proc�dure ENREGISTREPAIEMENT va enregistrer le paiement d'un congressiste.
 Elle peut �tre appel�e par la proc�dure NOUVEAULICENCIE dans le cas de l'inscription d'un licenci�
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
      raise_application_error(-20110 , 'b�n�vole d�j� inscrit, vous devez faire une modification de b�n�vole');
    when others then
      raise_application_error(-20101 , 'Erreur � la cr�ation du b�n�vole');
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
      raise_application_error(-20105 , 'Erreur � la cr�ation des pr�sences du b�n�vole');
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
        raise_application_error(-20100 , 'Erreur � la cr�ation du participant ');
      when benevdejainscrit then
        raise_application_error(-20110 , 'b�n�vole d�j� inscrit, \n vous devez faire une modification de b�n�vole');
      when erreurbenevole then
        raise_application_error(-20101 , 'Erreur � la cr�ation du benevole ');
      when others then
        raise_application_error(-20202, 'erreur inattendue lors de la cr�ation d''un b�n�vole');  
end;


/*
Proc�dure qui inscrit un intervenant sans nuit�
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
      raise_application_error(-20100, 'erreur � la cr�ation du participant');
    when erreurintervenant then
      raise_application_error(-20102, 'erreur � la cr�ation de l''intervenant ');
    when dejaanimateur then
      raise_application_error(-20112,'cet atelier a d�j� son animateur, inscription impossible');
    when others then
      raise_application_error(-20203, 'Autre erreur innattendue lors de la cr�ation d''un intervenant');
  end;



/*
Proc�dure qui inscrit un intervenant avec nuit�
Cette proc�dure va faire appel � la proc�dure surcharg�e NOUVELINTERVENANT
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
      raise_application_error(-20100, 'erreur � la cr�ation du participant');
    when erreurintervenant then
      raise_application_error(-20102, 'erreur � la cr�ation de l''intervenant ');
    when erreurcontenuhebergement then
      raise_application_error(-20104,'Erreur � la cr�ation du contenu de l''h�bergement');
    when dejaanimateur then
      raise_application_error(-20112,'cet atelier a d�j� son animateur, inscription impossible');
    when others then
      raise_application_error(-20203, 'Autre erreur innattendue lors de la cr�ation d''un intervenant');
  end;

end pckparticipant;
/
create public synonym pckparticipant for mdl.pckparticipant;
--       PACKAGE PARTICIPANT BODY
 
/