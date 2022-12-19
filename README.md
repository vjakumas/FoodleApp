
# Foodle

## Sistemos paskirtis
Projekto tikslas – sumažinti maisto atliekų kiekį sekant jų galiojimo laiką ir naudojantis rekomenduojamais patiekalų receptais.

Veikimo principas – pačią kuriamą platformą sudaro 2 dalys:

 1. Internetinė aplikacija kuria naudosis aplikacijos naudotojas, svečias ir sistemos administratorius;
 2. Aplikacijų programavimo sąsaja (API).

Naudotojai norintys pilnai išnaudoti sistemos privalumus turi užsiregistruoti prie sistemos per tinklalapio aplikaciją. Užsiregistravės naudotojas galės sudaryti savo turimų produktų sąrašą ir įvesti jų galiojimo laiką. Taip naudotojas galės sekti produktų galiojimo laiką ir sužinoti kurie produktai artėja prie galiojimo termino pabaigos.

Naudotojui gali būti pateikti receptai, kurių sudetį sudaro naudotojo turimi produktai. Taip pat receptai gali būti filtruojami arba randami per paiešką. Tokiu atvėju naudotojas lengviau nuspręs kokį patiekalą gali pasigaminti iš turimų produktų ir tai padės įvykdyti pagrindinį sistemos tikslą – sumažinti maisto atliekų kiekį.

## Funkciniai reikalavimai
3 CRUD - Kategorija -> Receptas -> Ingridientas.

### Neregistruotas sistemos naudotojas (Svečias):

 - [ ] **Peržiūrėti sistemos reprezentacinį puslapį;**
 - [ ] **Peržiūrėti receptus;**
 - [ ] **Prisjungti prie sistemos.**


### Registruotas sistemos naudotojas (Naudotojas):
 - [ ] **Atsijungti nuo sistemos;**
 
 - [ ] **Peržiūrėti receptų sąrašą;**
 - [ ] **Peržiūrėti recepto informacija;**
 - [ ] **Sukurti receptą;**
 - [ ] **Šalinti receptą;**
 - [ ] **Redaguoti receptą.**
 
 - [ ] **Pridėti ingridientą;**
 - [ ] **Išmesti ingridientą;**
 - [ ] **Peržiūrėti ingridientų sąrašą;**
 - [ ] **Peržiūrėti ingridiento informaciją;**
 - [ ] **Redaguoti ingridiento informaciją;**


### Sistemos administratorius (Administratorius):

 - [ ] **Sukurti paskyrą;**
 - [ ] **Šalinti paskyrą;**
 - [ ] **Redaguoti paskyrą;**
 
 - [ ] **Pridėti kategoriją;**
 - [ ] **Ištrinti kategoriją;**
 - [ ] **Redaguoti kategorija;**
 - [ ] **Peržiūrėti kategorijų sąrašą;**
 - [ ] **Peržiūrėti kategorijos informaciją;**
 
 - [ ] **Šalinti kitų receptus;**
 - [ ] **Redaguoti kitų receptus.**



## Sistemos architektūra
Sistemos architektūros sudedamos dalys:

 - Kliento pusė (angl. Front-end) - naudojant React.js
 - Serverio pusė (angl. Back-end) - naudojant .NET 6
 - Duomenų bazė: MySQL

### Likusią dokumentaciją galite peržiūrėti "Projekto ataskaita.pdf" faile.
