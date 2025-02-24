
# E nevoie de `.NET8` si `Visual Studio 2022`, altfel niste feature-uri pe care le folosesc nu merg.

# Trebe sa implementati in principal chestii din folderul 'Evenimente', dar puteti modifica oriunde obv

# Proiect CSD
Cred ca trebe fiecare sa instalam `OpenSSL` ***separat***, nush cum l-as pune ca dependenta.  
Recomand extensia pt Visual Studio *Markdown Editor v2* sa editati usor README.md in Visual Studio.

Actiunile de la elementele GUI apeleaza cate o functie din fisierele aflate in folderul `Evenimente`.  
De acolo, faceti ce fisiere/foldere vreti pentru codul vostru. Recomand totusi sa faceti fisiere separate pt baza de date, criptografie, lucru cu fisiere idk etc.  
Incercati sa scrieti un `///<summary>` macar sa ziceti ce face functia si cand e apelata.  
Evenimentele furnizeaza o clasa `EntryData` unde se afla informatiile despre elementul de adaugat/sters/modificat. Uitativa in cod sa vedeti ce contine.

Ca sa apelati functii din/modificati interfata, apelati `Orchestrator.Functia_Dorita`. Pana acum sunt functiile:
- `Orchestrator.Refresh();`

Documentatia lor se afla in cod.

### Login - Pui la parola 'r' pentru regular user si 'a' pentru admin
Inca nu am implementat tebela pt admini, asa ca inca nu este nicio dif intre cele 2

## Facut Pana acum
- ![Mascota](https://pbs.twimg.com/profile_images/775573562434256897/tq_qo0uc_400x400.jpg "Mascota")

## De Facut
- Arhitectura codului [matei]
- Elemente de gui [matei]: 
    - login [v]
    - tabel de utilizatori pentru admin [todo]
    - overview (tabel, butoane pt operatiile **`CRUD`**) [v]
    - fereastra de manage users (potential drepturi) [todo]
- design baza de date [parca erich]
    - ...
    - ...
- encriptarea efectiva cu openssl [parca victor]
    - ...
    - ...
- ...
- documentatia [vedem]