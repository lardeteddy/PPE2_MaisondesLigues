-- trigger qui va v�rifier qu'il n'y a pas d�j� un animateur sur l'atelier o� on essaye d'affecter l'intervenant
-- Le trigger a une condition de d�clanchement � savoir qu'il ne se d�clenche que si le statut qu'on
-- affecte � l'intervenant est ANI
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
    raise_application_error(-20112 ,'cet atelier a d�j� son animateur, inscription impossible');                                                                                
end;       
/
/*
trigger qui v�rifie qu'un b�n�vole ne peut s'inscrire qu'une fois.
Le seul probl�me est qu'un b�n�vole qui ne donne pas son num�ro de licence passera au travaers de ce controle
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
    raise_application_error(-20110, 'b�n�vole d�j� inscrit, \n vous devez faire une modification de b�n�vole');                   
   when others then                                                             
    raise_application_error(-20002, 'Erreur � l''enregistrement');              
end;
/