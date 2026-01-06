# Neptun Backend System

Ez a projekt a GarageClciker rendszer backend része, amely C# (.NET) nyelven íródott, PostgreSQL adatbázist használ, és Docker-konténerben fut.

## 🌐 Funkciók

* Hallgatók és oktatók kezelése
* Tárgyak kezelése
* Bejelentkezés / jelszó-módosítás
* Órák létrehozása, jelentkezés, vizsga jelentkezés, osztályozás
* EF Core-alapú migrációk és adattöltés
* Swagger UI a dokumentációhoz és API teszteléshez
* Token alapú hitelesítés
* Jogosultságkezelés

---

## ⚡ Előfeltételek

A következőknek telepítve kell, hogy legyenek a szmáítógépre.:

* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Docker + Docker Compose)
* [Rider](https://www.jetbrains.com/rider/) vagy [Visual Studio 2022+](https://visualstudio.microsoft.com/)

---

## 📁 Projekt inicializálása

1. Készítsen egy `.env` fájlt a `.env.example` minta alapján.

   Tartalma kb. ilyesmi lesz:

   ```env
   PG_USER=root
   PG_PASSWORD=root
   PG_DATABASE=garage
   PG_PORT=3254
   PG_HOST=localhost

   ConnectionStrings__DefaultConnection=Host=localhost;Port=3254;Database=garage;Username=root;Password=root
   Jwt__Key=VfBDQqXtCSxd5WAjK6ehJZkEvHnTR79GU8wM4sNYarB3Rr8NcqvtdSxE2nzPbw5pV9mUjXQF6ZGWKMCLYuHaNxeuSGPRaXhJm8ZzCArD4jFQ5KYp7VksWEbL6gyUBMLr2GZv6gsb3MfYdA7nSHuaB8zejQmFPwhxWqtCcyXJzSNPVZ6u43KM95yBWwgmxqbvTaCnUrdXDRE8eYh7JA
   ```

2. Futtassa az `init.bat` scriptet.

   Ez:

   * Letörli a korábbi konténereket és volume-okat (ha vannak)
   * Elindítja a PostgreSQL konténert Dockerben
   * Végrehajtja az adatbázis migrációkat

---

## ▶️ Az alkalmazás futtatása

1. Nyissa meg a projektet Riderben vagy Visual Studio-ban
2. Állítsa be az indító projektet (ha kell)
3. Indítsa el (F5 vagy Run)
4. A Swagger UI elérhető lesz a [http://localhost](http://localhost:PORT/swagger)[:PORT](http://localhost:PORT/swagger)[/swagger](http://localhost:PORT/swagger) címen (a PORT a projekt portja)

---

## 🛡️ .env biztonság

* A `.env` fájl NEM kerül be a verzókövetésbe (benne van a `.gitignore`-ban)
* A `.env.example` segít a paraméterek előkészítésében

---

## 🎓 Projekt felépítése

* `Models/` - EF Core entitás osztályok (Student, Teacher, Course, Exam, Administrator)
* `Controllers/` - REST API kontrollerek
* `Data/` - DbContext, konfigurációk
* `Services/` - üzleti logika / jelszókezelés / beléptetés

---

Ha bármilyen hibába ütközik, nyugodtan nyisson issue-t vagy dobjon egy üzenetet. 🙂
