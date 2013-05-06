-- trigger qui va vérifier qu'il n'y a pas déjà un animateur sur l'atelier où on essaye d'affecter l'intervenant
-- Le trigger a une condition de déclanchement à savoir qu'il ne se déclenche que si le statut qu'on
-- affecte à l'intervenant est ANI
create or replace trigger trgbiu_intervenant before insert or update on intervenant
FOR EACH ROW                                                                    
WHEN(new.idstatut='ANI')                                                        
declare                                                                         
  nb integer:=0;                                                                
  dejaanimateur exception;                                                      
begin                                                                           
  select count(*) into nb from intervenant                                      
    where idatelier=:new.idatelier and idstatut='ANI';                                          
    if nb >0 then                                                               
      raise dejaanimateur;                                                      
    end if;                                                                     
exception                                                                       
  when others then                                                              
    raise_application_error(-20112 ,'cet atelier a déjà son animateur, inscription impossible');                                                                                
end;       
/
/*
trigger qui vérifie qu'un bénévole ne peut s'inscrire qu'une fois.
Le seul problème est qu'un bénévole qui ne donne pas son numéro de licence passera au travaers de ce controle
*/
create or replace
trigger trgbi_benevole before insert on benevole              
  for each row                                                                  
declare                                                                         
  nb integer;                                                                   
begin                                                                           
  select 1 into nb from dual where not exists(select numerolicence from benevole
 where numerolicence= :new.numerolicence) ;                                     
                                                                                
exception                                                                       
  when no_data_found then                                                       
    raise_application_error(-20110, 'bénévole déjà inscrit, \n vous devez faire une modification de bénévole');                   
   when others then                                                             
    raise_application_error(-20002, 'Erreur à l''enregistrement');              
end;
/