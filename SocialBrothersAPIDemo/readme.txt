Voor het opstarten van het project is alleen vs2019 nodig en het aanzetten van IIS Express.

Deel 1: alles staat al gereed tijdens het aanzetten van de applicatie. De API met alle crud funcionaliteit staat gereed in een swaggerUI.

Deel 2: helaas is niet alles gelukt, alle data staat in een tabel en kan op gezocht worden, alleen heb ik het niet voor elkaar gekregen om het sortable te maken. 
Mijn idee was om met het drukken van het knop van welke rij je wilde sorteren de tabelrijnaam mee te geven met daarbij de vorige sort mogelijkheid (dus 'geen sort', 'asc', 'desc') zodat daar doorheen gedrukt kon worden. 
De data kon verwerkt worden binnen de controller en weer mee terug gestuurd worden naar de view pagina.

Deel 3: In de swagger UI moet je het volgende kopieren en plakken om daaruit informatie te krijgen.

{
  "adres1": {
    "adresId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "straat": "Arkelstraat",
    "huisnummer": "15",
    "postcode": "4201 KA",
    "plaats": "Gorinchem",
    "land": "Nederland"
  },
  "adres2": {
    "adresId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "straat": "Weeshuissteeg",
    "huisnummer": "19",
    "postcode": "1441 CS",
    "plaats": "Purmerend",
    "land": "Nederland"
  }
}



Meest tevreden: API's. na best wel een lange tijd sinds mijn stage is het me weer gelukt om een API op te zetten. daar ben ik best trots op en volgens mij is het goed gelukt.

Minder tevreden: Tabel. ik merkte dat ik niet veel van razorpages en views wist waardoor ik snel verkneld raakte. al met al heb ik wel een tabel met zoekfunctie kunnen schrijven.