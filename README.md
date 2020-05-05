# Projecten Workshops II - Dossier .NET - G09
> 7 mei 2020 - 09:00

## 1. Teamleden

| Naam              | Github                                                      | Email                                                                           |
| :---------------- | :---------------------------------------------------------- | :------------------------------------------------------------------------------ |
| Sam Brysbaert     | [brysbaertsam](https://github.com/brysbaertsam)             | [sam.brysbaert@student.hogent.be](mailto:sam.brysbaert@student.hogent.be)       |
| Dante De Ruwe     | [dantederuwe-hogent](https://github.com/dantederuwe-hogent) | [dante.deruwe@student.hogent.be](mailto:dante.deruwe@student.hogent.be)         |
| Arne De Schrijver | [ArneDeSchrijver](https://github.com/ArneDeSchrijver)       | [arne.deschrijver@student.hogent.be](mailto:arne.deschrijver@student.hogent.be) |
| Liam Spitaels     | [liamspitaels](https://github.com/liamspitaels)             | [liam.spitaels@student.hogent.be](mailto:liam.spitaels@student.hogent.be)       |


## 2. De applicatie (Codenaam *Lorem*)


### 2.1 Huidige versie (demo)
> U kunt op elke afbeelding klikken om deze in volledige grootte te zien

#### Inlogscherm
<a href="https://i.imgur.com/sCHOhlF.png">
<figure class="image">
  <img src="https://i.imgur.com/sCHOhlF.png">
</figure>
</a>

#### Kalenderscherm
Op het hoofdscherm krijgt u een overzicht van alle ingeplande sessies in een kalender. Aan de kleurenlegende kunt u zien welke sessies zich in welke staat bevinden. Sessies die aangemaakt zijn, zijn blauw; daarna doorlopen ze groen (open), oranje (gesloten) en rood (afgesloten).

<a href="https://i.imgur.com/6qQgGBY.png">
<figure class="image">
  <img src="https://i.imgur.com/6qQgGBY.png">
</figure>
</a>

Door op een sessie te klikken kunt u een overzichtje van die sessie bekijken. 
Alle nodige informatie is weergegeven, alsook de meest recente aankondiging (indien van toepassing)
<a href="https://i.imgur.com/0in3jA4.png">
<figure class="image">
  <img src="https://i.imgur.com/0in3jA4.png">
</figure>
</a>

<a href="https://i.imgur.com/4sOLnRx.png">
<figure class="image">
  <img src="https://i.imgur.com/4sOLnRx.png">
</figure>
</a>

Als u meer info en alle aankondigingen zou willen raadplegen, kunt u vervolgens op de `Zie meer...` knop drukken. (Deze functionaliteit is nog niet af, zie "2.2 Plannen").


Als de sessie geopend is, kan de gebruiker zich inschrijven. Na het sluiten en afsluiten van de sessie kan de gebruiker zien of hij aanwezig was op deze sessie of niet:

<a href="https://i.imgur.com/HLKEY8n.png">
<figure class="image">
  <img src="https://i.imgur.com/HLKEY8n.png">
</figure>
</a>


#### Sessies Beheren


Als verantwoordelijke of hoofdverantwoordelijke heeft u naast het kalenderzicht nog andere opties: de navigatiebalk toont deze opties enkel als u er toegang tot heeft.

<a href="https://i.imgur.com/b0jPOhE.png">
<figure class="image">
  <img src="https://i.imgur.com/b0jPOhE.png">
</figure>
</a>


De `Sessies Beheren` pagina is enkel toegankelijk voor de (hoofd)verantwoordelijke.

De hoofdverantwoordelijke krijgt hier een overzicht van alle nog niet afgesloten sessies, gesorteerd op datum, te zien. Een gewone verantwoordelijke ziet enkel de sessies die hij/zij zelf heeft aangemaakt en die ook nog niet zijn afgesloten.
Bij elke sessie is er rechts een knop waarop je kan klikken om de sessie van staat te veranderen. U kunt ook elke sessie openklappen om meer info te zien.

<a href="https://i.imgur.com/P9flvbY.png">
<figure class="image">
  <img src="https://i.imgur.com/P9flvbY.png">
</figure>
</a>

<a href="https://i.imgur.com/FyOlveF.png">
<figure class="image">
  <img src="https://i.imgur.com/FyOlveF.png">
</figure>
</a>

<a href="https://i.imgur.com/125iUtV.png">
<figure class="image">
  <img src="https://i.imgur.com/125iUtV.png">
</figure>
</a>


### 2.2 Plannen
We zouden graag de logica voor de aanwezigheden en inschrijvingen te registreren implementeren, hierbij hoort ook een scherm waarmee we de aanwezigheden kunnen valideren en registreren. Verder zal er ook meer naar testen gekeken worden. Ook willen we nog een scherm maken om de details van een sessie te kunnen weergeven alsook alle aankondigingen van die sessie.


<!-- <div style="page-break-after: always;"></div> -->

## 3. Klassendiagram
<a href="https://i.imgur.com/8b138x5.png">
<figure class="image">
  <img src="https://i.imgur.com/8b138x5.png">
</figure>
</a>
