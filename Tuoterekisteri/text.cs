﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuoterekisteri
{
public class L
{
public static int nr = 1; // Setting initial language
public static int tot = 3; //setting language amount
}
public class T
{
public static string[,] txt =
{

//  T.txt[0, L.nr]   is the syntax for the variable,
//  '0' replaced with actual number

{"ENG", "FIN", "SWE" }, // 0
{"Login", "Kirjaudu", "Logga in" }, // 1
{"Username", "Käyttäjänimi", "Användarnamn" }, // 2
{"Error", "Virhe","Fel"}, // 3
{"Password", "Salasana", "Lösenord" }, // 4
{"Menu", "Valikko", "Meny" }, // 5
{"Logout", "Kirjaudu ulos", "Logga ut"}, // 6
{"Products", "Tuotteet", "Produkter"}, // 7
{"An error occurred.", "Tapahtui virhe", "Ett fel uppstod"}, // 8
{"Create", "Lisää", "Skapa"}, // 9
{"Delete", "Poista", "Radera"}, // 10
{"Edit", "Muuta", "Ändra"}, // 11
{"Users", "Käyttäjät", "Användare"}, //12
{"Locations", "Paikat", "Platser"}, // 13
{"Location", "Paikka", "Plats"}, // 14
{"Back to List", "Takaisin listaan", "Tillbaka till listan"}, //15
{"Index", "Indeksi", "Index"}, // 16
{"Create New", "Luo uusi", "Lägg till ny"}, // 17
{"Details", "Tiedot", "Detaljer"}, // 18
{"Are you sure you want to delete this?",
"Oletko varma että haluat poistaa tämän?",
"Är du säker att du vill radera detta"},  //19
{"Product Groups", "Tuoteryhmät", "Produktgrupper"}, //20
{"Email", "S-posti", "E-post"}, // 21
{"Fisrtname", "Etunimi", "Förnamn"}, // 22
{"Lastname", "Sukunimi", "Släktnamn"}, // 23
{"1 for admin, otherwise leave blank",
"1 jos admin, muuten jätetään tyhjäksi",
"1 för admin, annars lämna tomt"}, // 24
{"Save", "Talenna", "Spara"}, // 25
{"Invalid username/password",
"Virheellinen käyttäjänimi/salasana",
"Felaktigt användarnamn/lösenord"}, // 26
{"Error. User does not sexist?",
"Virhe. Käyttäjä ei ole olemassa?",
"Fel. Användaren finns inte?"}, // 27
{"Lastseen", "Nähty", "Setts till"}, // 28
{"Intended use", "Käyttötarkoitus", "Anv Ändamål"}, // 29
{"Productgroup", "Tuoteryhmä", "Produktgrupp"}, // 30
{"Product", "Tuote", "Produkt"}, // 31
{"Location Name", "Paikan Nimi", "Plats Namn"}, // 32
{"Location Data", "Paikan Tiedot", "Platsens Data"}, // 33
{"Barcode", "Viivakoodi", "Streckkod"}, // 34
{"Productname", "Tuotenimi", "Produktnamn"}, // 35
{"Productdata", "Tuotetieto", "Produktdata"}, // 36
{"Logged In", "Kirjautunut", "Inloggad"}, // 37
{"Loans", "Lainat", "Lån"}, // 38
{"Loaned", "Lainattu", "Lånat"}, // 39
{"Loan", "Laina", "Lån"}, // 40
{"Loandisk", "Lainatiski", "Lånedisken"}, // 41
{"Return", "Palauta", "Returnera"}, // 42
{"Loan", "Lainaa", "Låna"}, // 43
{"Allowed Return", "Palautuspaikka",
"Återlämningsplats"}, // 44
{"Bad Username / Password",
"Väärä Käyttäjänimi / Salasana",
"Fel Användarnamn / Lösenord"}, // 45
{"Leave blank if you do not wish to change password!",
"Jätetään tyhjäksi ellei salasanaa muuteta!",
"Lämnas tomt ifall lösenoret inte ska bytas!"}, // 46
{"Username taken!", "Käyttäjänimi varattu!",
"Användarnamnet är reserverat!"}, // 47
{"The loan is registered!", "Laina on rekisteröity!",
"Lånet är registrerat!"}, // 48
{"The loan is returned", "Laina on palautettu",
"Lånet är returnerat"}, // 49
{"-- ALL --", "-- KAIKKI --", "-- ALLA --"}, // 50
{" ", " ", " "}, // 51
{" ", " ", " "}, // 52
{" ", " ", " "}, // 53
{" ", " ", " "}, // 54

};
}
}

