# WPF-OrganizationOfMartialArtsCompetitions

**Scénář**
Software pro organizaci závodů bojového úmění. Obsahuje seznam závodníků, kteří mají jméno, příjmení, email, věk a technický stupeň (1-20). Podle věku a technického stupně pak můžu závodníky filtrovat a přidávat do závodu - závod je věkově a technickým stupněm omezen. 
Samotný závod pak rozdělí závodníky náhodně do skupin (poolů) po třech, kde všichni bojujů proti všem. Vítěz skupiny pak zápasí proti druhému z opačné skupiny a podobně u dalších skupin. Z toho vznikne pavouk, kde se zápasí dokud není jediný vítěz.

**Požadavky**
- Vytváření závodníků - jméno, příjmení, email, věk, technický stupeň
- Vytváření turnajů - jméno, omezení věku, omezení technického stupně
- Automatické vyfiltrování závodníků podle omezení turnaje
- Přiřazení závodníka k turnaji
- Automatické rozdělení závodníků do skupin (poolů) po 3
- Zadávání výsledků jednotlivých zápasů (zápasí se na dva vítězné body, může být i remíza)
- Automatický postup závodníků v pavouku po dokončení zápasů
 
**Technologie**
- WPF .NET - C# + XAML
- EF
- LINQ
- SQL
 
**Časový plán (44 hodin)**
 1) Zjištění zadání (2 hod)
 2) Vypracování scénaře, požadavků, technologie, časového plánu, otázek (2 hod)
 3) Návrh datové mapy (2 hod)
 4) Návrh uživatelského rozhraní (3 hod)
 5) Naplánování logiky aplikace (2 hod)
 6) Návrh SQL databáze (1 hod)
 8) Programování datové mapy a návrhu UI (4 hod)
 9) Kontrola/Testování (1 hod)
 10) Propojení s DB (2 hod)
 11) Kontrola/Testování (1 hod)
 12) Programování datové mapy a návrhu UI (6 hod)
 13) Kontrola/Testování (1 hod)
 14) Programování datové mapy a návrhu UI (6 hod)
 15) Ladění a testování současného návrhu (4 hod)
 16) Refactoring kódu (2 hod)
 17) Testování aplikace (3 hod)
 18) Příprava výsledného produktu (2 hod)
 
**Otázky (na zadání pro zadavatele)**
- Minimální počet závodníků pro turnaj?
- Zápasí se na dva vítězné body, tyto písmena M,D,K,T tedy značí výhru?
- Pokud nebude počet závodníků dělitelný 3?
- Lichý počet poolů?
- Kolik je různých technických stupňů u závodníků?
- Může nastat, že všichni v poolu mají stejný počet bodů po dokončení všech zápasů v poolu, co dělat pak?
- Nebylo by lepší zadávat u závodníků jejich datum narození, aby se každý rok nemusel věk u každého jendotlivého závodníka měnit?
 
