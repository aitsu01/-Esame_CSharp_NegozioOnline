# Progetto C# - Negozio Online Console App

## Descrizione del progetto

Questo progetto è una **Console App sviluppata in C#** che simula il funzionamento di un piccolo negozio online.

L'applicazione prevede due ruoli principali:

* **Utente**
* **Amministratore**

L'utente può visualizzare il catalogo dei prodotti, aggiungere articoli al carrello, modificare le quantità, rimuovere prodotti, confermare un acquisto e consultare il proprio storico acquisti.

L'amministratore può gestire il catalogo prodotti, aggiungere nuovi prodotti, eliminare prodotti, modificare prezzo e quantità disponibile, visualizzare tutti gli acquisti effettuati e consultare un report sui prodotti venduti.

Il progetto è stato realizzato come esercitazione finale per dimostrare l'utilizzo dei concetti fondamentali della **programmazione a oggetti in C#**.

---

## Tecnologie utilizzate

* C#
* .NET Console App
* Visual Studio
* Git
* GitHub

---

## Obiettivi del progetto

L'obiettivo principale del progetto è creare una piccola applicazione console che permetta di simulare la gestione di un negozio online.

Attraverso questo progetto vengono applicati diversi concetti fondamentali della programmazione, tra cui:

* classi e oggetti;
* incapsulamento;
* separazione delle responsabilità;
* gestione di liste di oggetti;
* metodi di ricerca, modifica ed eliminazione;
* gestione del carrello;
* gestione dello storico acquisti;
* validazione degli input da console;
* organizzazione del codice in più classi.

---

## Funzionalità principali

### Funzionalità utente

L'utente può:

* visualizzare il catalogo prodotti;
* aggiungere prodotti al carrello;
* visualizzare il carrello;
* modificare la quantità di un prodotto nel carrello;
* rimuovere prodotti dal carrello;
* svuotare completamente il carrello;
* confermare un acquisto;
* visualizzare il proprio storico acquisti.

---

### Funzionalità amministratore

L'amministratore può:

* visualizzare il catalogo completo;
* aggiungere nuovi prodotti;
* eliminare prodotti dal catalogo;
* modificare il prezzo di un prodotto;
* modificare la quantità disponibile di un prodotto;
* visualizzare tutti gli acquisti effettuati;
* visualizzare un report dei prodotti con quantità iniziale, quantità venduta e quantità disponibile.

---

## Struttura principale del progetto

Il progetto è organizzato in più classi, ognuna con una responsabilità precisa.

---

### Classe `Prodotto`

Rappresenta un prodotto del negozio.

Contiene informazioni come:

* codice prodotto;
* nome;
* prezzo;
* quantità iniziale;
* quantità disponibile.

La classe gestisce anche la modifica del prezzo e della quantità disponibile.

---

### Classe `ElementoCarrello`

Rappresenta un singolo prodotto inserito nel carrello dall'utente.

Contiene:

* prodotto selezionato;
* quantità scelta;
* prezzo unitario;
* subtotale.

---

### Classe `CarrelloUtente`

Gestisce il carrello dell'utente.

Permette di:

* aggiungere prodotti;
* modificare la quantità di un prodotto;
* rimuovere prodotti;
* svuotare il carrello;
* calcolare il totale del carrello.

---

### Classe `CatalogoProdotti`

Gestisce l'elenco dei prodotti disponibili nel negozio.

Permette di:

* aggiungere prodotti;
* cercare un prodotto tramite codice;
* eliminare prodotti;
* modificare il prezzo;
* modificare la quantità disponibile;
* ottenere un report dei prodotti.

---

### Classe `Acquisto`

Rappresenta un acquisto completato da un utente.

Contiene:

* identificativo dell'acquisto;
* nome utente;
* data e ora dell'acquisto;
* prodotti acquistati;
* totale dell'acquisto.

---

### Classe `StoricoAcquisti`

Gestisce la lista degli acquisti effettuati.

Permette di:

* registrare un nuovo acquisto;
* visualizzare tutti gli acquisti;
* visualizzare gli acquisti di uno specifico utente.

---

### Classe `ServizioNegozio`

Coordina le operazioni principali tra catalogo, carrello e storico acquisti.

In particolare gestisce:

* aggiunta prodotto al carrello;
* conferma acquisto;
* aggiornamento del magazzino;
* registrazione dello storico acquisti;
* stampa dei report.

---

### Classe `ApplicazioneNegozio`

Gestisce l'interfaccia console dell'applicazione.

Contiene:

* menu principale;
* scelta del ruolo;
* menu utente;
* menu amministratore;
* lettura degli input da console;
* validazione degli input inseriti dall'utente.

---

## Flusso dell'applicazione

Schema semplificato del funzionamento dell'applicazione:

```text
Avvio programma
      |
      v
Scelta ruolo
      |
      |-- Utente
      |     |
      |     |-- Visualizza catalogo
      |     |-- Gestisce carrello
      |     |-- Conferma acquisto
      |     |-- Visualizza storico acquisti
      |
      |-- Amministratore
            |
            |-- Gestisce prodotti
            |-- Visualizza acquisti
            |-- Visualizza report prodotti
```

---

## Come avviare il progetto

Aprire il terminale nella cartella principale del progetto ed eseguire:

```bash
dotnet run
```

In alternativa, è possibile aprire il progetto con **Visual Studio** e avviare l'applicazione direttamente dall'ambiente di sviluppo.

---

## Modalità di esecuzione

Nel metodo `Main` è possibile scegliere se avviare l'applicazione console oppure eseguire i test manuali.

### Avvio dell'applicazione console

```csharp
public static void Main()
{
    ApplicazioneNegozio applicazione = new ApplicazioneNegozio();
    applicazione.Avvia();

    // TestNegozioOnline.EseguiTuttiITest();
}
```

### Esecuzione dei test manuali

```csharp
public static void Main()
{
    // ApplicazioneNegozio applicazione = new ApplicazioneNegozio();
    // applicazione.Avvia();

    TestNegozioOnline.EseguiTuttiITest();
}
```

---

## Esempio di utilizzo utente

```text
1 - Utente

Inserisci nome utente: Gianni

1 - Visualizza catalogo
2 - Aggiungi prodotto al carrello
3 - Visualizza carrello
4 - Modifica quantità prodotto
5 - Rimuovi prodotto dal carrello
6 - Svuota carrello
7 - Conferma acquisto
8 - Visualizza storico acquisti
0 - Torna indietro
```

---

## Esempio di utilizzo amministratore

```text
2 - Amministratore

1 - Visualizza catalogo completo
2 - Aggiungi prodotto
3 - Elimina prodotto
4 - Modifica prezzo prodotto
5 - Modifica quantità disponibile
6 - Visualizza tutti gli acquisti
7 - Visualizza report prodotti
0 - Torna indietro
```

---

## Test manuali

Il progetto include una classe dedicata ai test manuali chiamata `TestNegozioOnline`.

I test permettono di verificare alcune funzionalità fondamentali dell'applicazione, come:

* modifica prezzo valido;
* rifiuto di prezzo non valido;
* modifica quantità disponibile;
* rifiuto di quantità negativa;
* aggiunta prodotto al carrello;
* modifica quantità nel carrello;
* rimozione prodotto dal carrello;
* conferma acquisto;
* aggiornamento della disponibilità di magazzino.

Esempio di output dei test:

```text
=== TEST NEGOZIO ONLINE ===

[PASS] Prodotto: modifica prezzo valido
[PASS] Prodotto: rifiuta prezzo non valido
[PASS] Prodotto: modifica quantità e calcola venduto
[PASS] Prodotto: rifiuta magazzino negativo
```

---

## Gestione Git e GitHub

Il progetto è stato versionato con **Git** e pubblicato su **GitHub**.

Sono stati effettuati commit progressivi per documentare le varie fasi di sviluppo:

```text
Initial commit - progetto C# negozio online
Implementata logica catalogo carrello e acquisti
Implementati menu console utente e amministratore
Migliorato feedback aggiornamento prodotti admin
Corretto formato prezzo nel menu amministratore
Rimossi caratteri accentati dai messaggi console
```

Questa organizzazione permette di mostrare l'evoluzione del progetto e le modifiche effettuate durante lo sviluppo.

---

## Nota sui caratteri speciali

Durante i test su console Windows, alcuni caratteri speciali come il simbolo euro e le lettere accentate potevano essere visualizzati in modo non corretto.

Per rendere l'output più stabile e leggibile, nei messaggi della console sono state preferite scritte semplici come:

```text
euro
Quantita
Disponibilita
```

invece di:

```text
€
Quantità
Disponibilità
```

Questa scelta è stata fatta per evitare problemi di visualizzazione nella console e rendere l'applicazione più compatibile con diversi ambienti Windows.

---

## Uso dell'intelligenza artificiale

Durante lo sviluppo è stato utilizzato ChatGPT come supporto per:

* analizzare la traccia dell'esame;
* individuare i metodi da completare;
* organizzare la struttura del progetto;
* migliorare la logica del carrello e degli acquisti;
* preparare una spiegazione chiara del codice;
* correggere piccoli bug di visualizzazione nella console;
* preparare la documentazione finale del progetto.

L'intelligenza artificiale è stata utilizzata come supporto allo studio e alla revisione, mantenendo la struttura del progetto e completando il codice secondo i requisiti della traccia.

---

## Possibili miglioramenti futuri

Il progetto potrebbe essere ampliato con ulteriori funzionalità, come:

* salvataggio dei dati su file;
* gestione di più utenti registrati;
* login con credenziali;
* gestione di categorie prodotto;
* ricerca prodotti per nome o prezzo;
* esportazione report in formato testo;
* interfaccia grafica desktop;
* collegamento a un database.

---

## Conclusione

Questo progetto rappresenta una simulazione completa di un piccolo negozio online realizzato tramite Console App in C#.

L'applicazione dimostra l'utilizzo della programmazione a oggetti, la gestione di classi separate, la manipolazione di collezioni di oggetti e la creazione di un flusso applicativo completo per utente e amministratore.

Il progetto è stato sviluppato come prova finale per consolidare le competenze acquisite durante il percorso di studio.
