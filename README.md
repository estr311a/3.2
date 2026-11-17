# Notatnik — zadanie 3.2

## Uruchomienie

Projekt wymaga .NET 9 i Avalonia. Szablon Avalonia wygenerował cel .NET 10, dlatego w `notatnik.csproj` ustawiono `net9.0`, zgodnie z SDK dostępnym w środowisku.

```powershell
dotnet run
```

Notatki trafiają do `notatki.md` w bieżącym katalogu. Zapis jest w UTF-8, najnowszy wpis jest na górze, a treść może zawierać wiele linii.

Format wpisu:

```text
Data: RRRR-MM-DD GG:MM:SS
Tytuł: tytuł notatki
Treść:
treść notatki
```

Wpisy rozdziela jedna pusta linia. Wszystkie pliki rund używają tego samego zestawu dwóch notatek testowych; w rundzie 4 dodatkowe przykłady były częścią prośby, nie dodatkowymi wpisami testowymi.

Przykłady dołączone do prośby z rundy 4:

1. Jednolinijkowa: tytuł `Zakupy`, treść `Kupić chleb i mleko.`
2. Wielolinijkowa: tytuł `Plan na weekend`, treść `Pójść na spacer.` oraz `Zabrać aparat i wodę.`
3. Polskie znaki i myślnik w tytule: tytuł `Łódź - plan na dziś`, treść `Sprawdzić wystawę w muzeum.` oraz `Zadzwonić do przyjaciół.`

## Rundy i powtórzenie

Pliki `rundy/runda-1.md`–`rundy/runda-4.md` przedstawiają przykładowy wynik dla kolejnych poziomów szczegółowości: prośba ogólna, jawny format, przykład formatu i kilka różnych przykładów. `rundy/runda-4-powtorzenie.md` pokazuje powtórzenie tego samego zestawu treści; różnią się znaczniki czasu, a tytuły i treści pozostają takie same. `rundy/runda-5.md` zawiera wynik dla doprecyzowanej prośby.

**Ograniczenie metody:** pliki w `rundy/` są przygotowanymi przykładami wyjściowymi, a nie kopiami z pięciu faktycznie przeprowadzonych, świeżych wątków agenta. W tym środowisku nie mogłem wiarygodnie wykonać i porównać osobnych sesji modelu. Z tego powodu powtórzenie nie mierzy losowości modelu. Nie da się rzetelnie przypisać konkretnej liczby różnic między rundami 1–4 losowości: polecenia w każdej rundzie zmieniały się, więc główną oczekiwaną przyczyną zmian są dodatkowe wymagania i przykłady. Losowość może wpływać na szczegóły niewynikające z polecenia, ale wymagałoby to powtórzeń przy niezmienionym promptcie i tym samym modelu.

## Ulepszona prośba (runda 5)

> Zbuduj w istniejącym projekcie aplikację desktopową Avalonia do zapisywania notatek. Interfejs ma zawierać osobne pola na tytuł i wielowierszową treść, przycisk „Zapisz”, listę zapisanych notatek i pole statusu. Zapisuj wszystkie notatki w pliku `notatki.md` w katalogu roboczym, kodując tekst jako UTF-8. Przy uruchomieniu wczytaj wcześniejsze wpisy. Nowe wpisy dodawaj na początku, nie usuwając starszych. Każdy wpis ma mieć lokalną datę i godzinę w formacie `yyyy-MM-dd HH:mm:ss`, tytuł oraz treść; rozdziel wpisy jedną pustą linią. Zachowuj polskie znaki i podziały wierszy treści. Po poprawnym zapisie pokaż „Zapisano”; w razie wyjątku pokaż „Nie udało się zapisać: …” i nie czyść pól. Zademonstruj format na przykładach: wpis jednolinijkowy, wielolinijkowy oraz tytuł z polskimi znakami i myślnikiem. Nie zmieniaj niepowiązanych plików.

## Obserwacje i weryfikacja

- Rundy 1–4 pokazują, jak jawny format i przykłady ograniczają niejednoznaczność. Samo określenie „zapis do pliku” nie definiuje kolejności, kodowania ani separatorów.
- Powtórzenie zestawu nie zmieniło przykładowych tytułów ani treści. To porównanie ilustracyjne, nie empiryczny pomiar odpowiedzi modelu.
- `dotnet build notatnik.csproj --no-restore` kończy się powodzeniem. Środowisko zgłasza ostrzeżenie, że Windows Application Control blokuje ładowanie analizatora Avalonia (`0x800711C7`); nie ma błędów kompilacji.
- Próba uruchomienia zbudowanej aplikacji również została zablokowana przez Windows Application Control. Nie mogłem więc wykonać interaktywnego testu pliku tylko do odczytu ani potwierdzić komunikatu GUI. Kod obsługi wyjątku został dodany, ale ten test należy wykonać lokalnie po uruchomieniu aplikacji.

## Dodatkowa porada producenta modelu

OpenAI zaleca doprecyzować instrukcje i w razie potrzeby podać przykłady; dokumentacja zwraca też uwagę na niedeterministyczność generowania i potrzebę mierzenia zachowania promptów.

- Źródło: [OpenAI — Prompt engineering](https://developers.openai.com/api/docs/guides/prompt-engineering)
- Data dostępu: 2026-11-17
- Wpływ na notatnik: przykład z konkretnymi etykietami, formatem daty, separatorem, sortowaniem i treścią wielowierszową zmniejsza pole do różnych interpretacji. Trzy typy przykładów pomagają sprawdzić, czy format działa poza najprostszą notatką.

## GitHub

Commity są lokalne. W chwili przygotowania README repozytorium nie miało skonfigurowanego zdalnego adresu GitHub, więc nie wykonano `git push`.
