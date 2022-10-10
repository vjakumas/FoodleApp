
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

**Neregistruotas sistemos naudotojas (Svečias):**

 - [ ] **Peržiūrėti sistemos reprezentacinį puslapį;**
 - [ ] **Peržiūrėti receptus;**
 - [ ] **Prisjungti prie sistemos;**
 - [ ] Susigeneruoti naują slaptažodį (jei naudotojas užmiršo savo slaptažodį);

**Registruotas sistemos naudotojas (Naudotojas) gali:**


 - [ ] **Atsijungti nuo sistemos;**
 - [ ] **Pridėti produktą į produktų sąrašą;**
 - [ ] **Išmesti produktą iš produktų sarašo;**
 - [ ] **Matyti produktų sąrašą ir jų informaciją;**
 - [ ] **Redaguoti produkto informaciją;**
 - [ ] Ieškoti receptų:
	 - [ ] Pagal turimus produktus;
	 - [ ] Pagal pavadinimą;
	 - [ ] Pagal konkrečius ingridientus.
 - [ ] Filtruoti turimų produktų sąrašą;
 - [ ] **Sukurti receptą;**
 - [ ] Paskelbti receptą;
 - [ ] **Šalinti receptą;**
 - [ ] **Redaguoti receptą;**
 - [ ] Įvertinti kitų receptą;
 - [ ] Peržiūrėti statistiką:
	 - [ ] Kiek produktų išmesta, ir už kokią sumą išmesta produktų;
	 - [ ] Kiek išleista pinigų maistui;


**Sistemos administratorius (Administratorius) gali:**

 - [ ] **Sukurti paskyrą;**
 - [ ] **Šalinti paskyrą;**
 - [ ] Užblokuoti paskyrą (laikinai);
 - [ ] **Redaguoti paskyrą;**
 - [ ] **Pridėti kategoriją;**
 - [ ] **Ištrinti kategoriją;**
 - [ ] **Redaguoti kategorija;**
 - [ ] **Matyti kategorijas;**
 - [ ] **Šalinti kitų receptus;**
 - [ ] **Redaguoti kitų receptus;**



## Sistemos architektūra
Sistemos architektūros sudedamos dalys:

 - Kliento pusė (angl. Front-end) - naudojant React.js
 - Serverio pusė (angl. Back-end) - naudojant .NET 6
 - Duomenų bazė: MySQL
