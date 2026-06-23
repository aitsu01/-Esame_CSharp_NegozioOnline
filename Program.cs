using System;
using System.Collections.Generic;
using System.Linq;

/*
 * TEMPLATE ESAME C# - NEGOZIO ONLINE
 *
 * Regola scelta per il template:
 * - i metodi di visualizzazione sono già implementati, così lo studente può concentrarsi
 *   sulle operazioni richieste dalla traccia.
 * - i metodi operazionali contengono TODO guidati: lo studente deve completarli senza
 *   modificare firma, nome, parametri o tipo di ritorno.
 *
 * Vincolo richiesto: tutto il codice è in un unico file .cs e senza namespace.
 */

public class Program
{
    public static void Main()
    {
        ApplicazioneNegozio applicazione = new ApplicazioneNegozio();
        applicazione.Avvia();

        // TestNegozioOnline.EseguiTuttiITest();
    }
}

public class ApplicazioneNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;
    private readonly ServizioNegozio servizioNegozio;

    public ApplicazioneNegozio()
    {
        catalogoProdotti = new CatalogoProdotti();
        carrelloUtente = new CarrelloUtente();
        storicoAcquisti = new StoricoAcquisti();
        servizioNegozio = new ServizioNegozio(catalogoProdotti, carrelloUtente, storicoAcquisti);

        CaricaDatiIniziali();
    }

    public void Avvia()
    {
        bool continuaProgramma = true;

        Console.WriteLine("========================================");
        Console.WriteLine(" BENVENUTO NEL NEGOZIO ONLINE C#");
        Console.WriteLine("========================================");

        while (continuaProgramma)
        {
            string ruoloScelto = ScegliRuolo();

            switch (ruoloScelto)
            {
                case "utente":
                    GestisciMenuUtente();
                    break;

                case "amministratore":
                    GestisciMenuAmministratore();
                    break;

                case "esci":
                    continuaProgramma = false;
                    Console.WriteLine("Uscita dal programma. Arrivederci!");
                    break;

                default:
                    Console.WriteLine("Scelta non valida. Riprova.");
                    break;
            }
        }
    }

    private void CaricaDatiIniziali()
    {
        // Metodo già implementato: fornisce prodotti di partenza per testare subito il sistema.
        catalogoProdotti.AggiungiProdotto(new Prodotto("P001", "Tastiera meccanica", 79.90m, 10));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P002", "Mouse wireless", 24.50m, 25));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P003", "Monitor 24 pollici", 149.99m, 7));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P004", "Cavo USB-C", 9.99m, 40));
    }

    private string ScegliRuolo()
    {
        Console.WriteLine();
        Console.WriteLine("=== SCELTA RUOLO ===");
        Console.WriteLine("1 - Utente");
        Console.WriteLine("2 - Amministratore");
        Console.WriteLine("0 - Esci");
        Console.Write("Scegli un'opzione: ");

        string? scelta = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(scelta))
        {
            return "";
        }

        scelta = scelta.Trim().ToLower();

        switch (scelta)
        {
            case "1":
            case "utente":
                return "utente";

            case "2":
            case "amministratore":
            case "admin":
                return "amministratore";

            case "0":
            case "esci":
                return "esci";

            default:
                return "";
        }
    }

    private void GestisciMenuUtente()
    {
        Console.Write("Inserisci il tuo nome utente: ");
        string? nomeUtente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nomeUtente))
        {
            Console.WriteLine("Nome utente non valido. Ritorno al menu principale.");
            return;
        }

        Utente utente = new Utente(nomeUtente);
        bool continuaMenuUtente = true;

        while (continuaMenuUtente)
        {
            Console.WriteLine();
            Console.WriteLine("=== MENU UTENTE ===");
            Console.WriteLine("1 - Visualizza catalogo");
            Console.WriteLine("2 - Aggiungi prodotto al carrello");
            Console.WriteLine("3 - Visualizza carrello");
            Console.WriteLine("4 - Modifica quantità nel carrello");
            Console.WriteLine("5 - Rimuovi prodotto dal carrello");
            Console.WriteLine("6 - Svuota carrello");
            Console.WriteLine("7 - Conferma acquisto");
            Console.WriteLine("8 - Visualizza storico acquisti");
            Console.WriteLine("0 - Torna al menu principale");
            Console.Write("Scegli un'opzione: ");

            string? scelta = Console.ReadLine();

            switch (scelta)
            {
                case "1":
                    MostraCatalogo();
                    break;

                case "2":
                    MostraCatalogo();

                    Console.Write("Inserisci codice prodotto da aggiungere: ");
                    string? codiceDaAggiungere = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(codiceDaAggiungere))
                    {
                        Console.WriteLine("Codice prodotto non valido.");
                        break;
                    }

                    int quantitaDaAggiungere = LeggiInteroPositivo("Inserisci quantità da aggiungere: ");

                    bool aggiunto = servizioNegozio.AggiungiProdottoAlCarrello(
                        codiceDaAggiungere.Trim(),
                        quantitaDaAggiungere);

                    if (aggiunto)
                    {
                        Console.WriteLine("Prodotto aggiunto al carrello.");
                    }
                    else
                    {
                        Console.WriteLine("Impossibile aggiungere il prodotto. Controlla codice o disponibilità.");
                    }

                    break;

                case "3":
                    MostraCarrello();
                    break;

                case "4":
                    MostraCarrello();

                    Console.Write("Inserisci codice prodotto da modificare: ");
                    string? codiceDaModificare = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(codiceDaModificare))
                    {
                        Console.WriteLine("Codice prodotto non valido.");
                        break;
                    }

                    int nuovaQuantita = LeggiInteroPositivo("Inserisci nuova quantità: ");

                    bool modificato = carrelloUtente.ModificaQuantitaNelCarrello(
                        codiceDaModificare.Trim(),
                        nuovaQuantita);

                    if (modificato)
                    {
                        Console.WriteLine("Quantità aggiornata nel carrello.");
                    }
                    else
                    {
                        Console.WriteLine("Impossibile modificare la quantità. Controlla codice o disponibilità.");
                    }

                    break;

                case "5":
                    MostraCarrello();

                    Console.Write("Inserisci codice prodotto da rimuovere: ");
                    string? codiceDaRimuovere = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(codiceDaRimuovere))
                    {
                        Console.WriteLine("Codice prodotto non valido.");
                        break;
                    }

                    bool rimosso = carrelloUtente.RimuoviDalCarrello(codiceDaRimuovere.Trim());

                    if (rimosso)
                    {
                        Console.WriteLine("Prodotto rimosso dal carrello.");
                    }
                    else
                    {
                        Console.WriteLine("Prodotto non trovato nel carrello.");
                    }

                    break;

                case "6":
                    carrelloUtente.SvuotaCarrello();
                    Console.WriteLine("Carrello svuotato.");
                    break;

                case "7":
                    try
                    {
                        Acquisto acquisto = servizioNegozio.ConfermaAcquisto(utente);
                        Console.WriteLine("Acquisto confermato con successo.");
                        servizioNegozio.StampaAcquisto(acquisto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Errore durante la conferma dell'acquisto: " + ex.Message);
                    }

                    break;

                case "8":
                    List<Acquisto> acquistiUtente = storicoAcquisti.OttieniAcquistiPerUtente(utente.Nome);

                    Console.WriteLine();
                    Console.WriteLine("=== STORICO ACQUISTI DI " + utente.Nome + " ===");

                    if (acquistiUtente.Count == 0)
                    {
                        Console.WriteLine("Nessun acquisto trovato.");
                    }
                    else
                    {
                        foreach (Acquisto acquisto in acquistiUtente)
                        {
                            servizioNegozio.StampaAcquisto(acquisto);
                        }
                    }

                    break;

                case "0":
                    continuaMenuUtente = false;
                    break;

                default:
                    Console.WriteLine("Scelta non valida. Riprova.");
                    break;
            }
        }
    }
    private void GestisciMenuAmministratore()
    {
        bool continuaMenuAmministratore = true;

        while (continuaMenuAmministratore)
        {
            Console.WriteLine();
            Console.WriteLine("=== MENU AMMINISTRATORE ===");
            Console.WriteLine("1 - Visualizza catalogo completo");
            Console.WriteLine("2 - Aggiungi prodotto");
            Console.WriteLine("3 - Elimina prodotto");
            Console.WriteLine("4 - Modifica prezzo prodotto");
            Console.WriteLine("5 - Modifica quantità disponibile");
            Console.WriteLine("6 - Visualizza tutti gli acquisti");
            Console.WriteLine("7 - Visualizza report prodotti");
            Console.WriteLine("0 - Torna al menu principale");
            Console.Write("Scegli un'opzione: ");

            string? scelta = Console.ReadLine();

            switch (scelta)
            {
                case "1":
                    MostraCatalogo();
                    break;

                case "2":
                    Console.Write("Inserisci codice prodotto: ");
                    string? codiceNuovoProdotto = Console.ReadLine();

                    Console.Write("Inserisci nome prodotto: ");
                    string? nomeNuovoProdotto = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(codiceNuovoProdotto) || string.IsNullOrWhiteSpace(nomeNuovoProdotto))
                    {
                        Console.WriteLine("Codice o nome prodotto non valido.");
                        break;
                    }

                    decimal prezzoNuovoProdotto = LeggiPrezzoPositivo("Inserisci prezzo prodotto: ");
                    int quantitaNuovoProdotto = LeggiInteroPositivo("Inserisci quantità disponibile: ");

                    try
                    {
                        Prodotto nuovoProdotto = new Prodotto(
                            codiceNuovoProdotto.Trim(),
                            nomeNuovoProdotto.Trim(),
                            prezzoNuovoProdotto,
                            quantitaNuovoProdotto);

                        catalogoProdotti.AggiungiProdotto(nuovoProdotto);
                        Console.WriteLine("Prodotto aggiunto al catalogo.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Errore durante l'aggiunta del prodotto: " + ex.Message);
                    }

                    break;

                case "3":
                    MostraCatalogo();

                    Console.Write("Inserisci codice prodotto da eliminare: ");
                    string? codiceDaEliminare = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(codiceDaEliminare))
                    {
                        Console.WriteLine("Codice prodotto non valido.");
                        break;
                    }

                    bool eliminato = catalogoProdotti.EliminaProdotto(codiceDaEliminare.Trim());

                    if (eliminato)
                    {
                        Console.WriteLine("Prodotto eliminato dal catalogo.");
                    }
                    else
                    {
                        Console.WriteLine("Prodotto non trovato.");
                    }

                    break;

                case "4":
                    MostraCatalogo();

                    Console.Write("Inserisci codice prodotto da modificare: ");
                    string? codicePrezzo = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(codicePrezzo))
                    {
                        Console.WriteLine("Codice prodotto non valido.");
                        break;
                    }

                    decimal nuovoPrezzo = LeggiPrezzoPositivo("Inserisci nuovo prezzo: ");

                    try
                    {
                        bool prezzoModificato = catalogoProdotti.ModificaPrezzoProdotto(
                            codicePrezzo.Trim(),
                            nuovoPrezzo);

                        if (prezzoModificato)
                        {
                            Prodotto? prodottoAggiornato = catalogoProdotti.CercaProdottoPerCodice(codicePrezzo.Trim());

                            if (prodottoAggiornato != null)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Prodotto aggiornato correttamente:");
                                Console.WriteLine("Codice: " + prodottoAggiornato.CodiceProdotto);
                                Console.WriteLine("Nome: " + prodottoAggiornato.Nome);
                                Console.WriteLine("Nuovo prezzo: " + prodottoAggiornato.Prezzo.ToString("0.00") + " euro");
                                
                                Console.WriteLine("Quantita disponibile: " + prodottoAggiornato.QuantitaDisponibile);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Prodotto non trovato.");
                        }
                    
                   


            }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Errore durante la modifica della quantità: " + ex.Message);
                    }

                    break;

                case "5":
                    MostraCatalogo();

                    Console.Write("Inserisci codice prodotto da modificare: ");
                    string? codiceQuantita = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(codiceQuantita))
                    {
                        Console.WriteLine("Codice prodotto non valido.");
                        break;
                    }

                    Console.WriteLine("Inserisci una variazione di quantità.");
                    Console.WriteLine("Esempio: 5 per aumentare, -3 per diminuire.");
                    Console.Write("Variazione quantità: ");

                    string? inputVariazione = Console.ReadLine();

                    if (!int.TryParse(inputVariazione, out int variazioneQuantita))
                    {
                        Console.WriteLine("Variazione non valida.");
                        break;
                    }

                    try
                    {
                        bool quantitaModificata = catalogoProdotti.ModificaQuantitaProdotto(
                            codiceQuantita.Trim(),
                            variazioneQuantita);

                        if (quantitaModificata)
                        {
                            Prodotto? prodottoAggiornato = catalogoProdotti.CercaProdottoPerCodice(codiceQuantita.Trim());

                            if (prodottoAggiornato != null)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Prodotto aggiornato correttamente:");
                                Console.WriteLine("Codice: " + prodottoAggiornato.CodiceProdotto);
                                Console.WriteLine("Nome: " + prodottoAggiornato.Nome);
                                Console.WriteLine("Prezzo: " + prodottoAggiornato.Prezzo.ToString("0.00") + " euro");


                                Console.WriteLine("Quantita disponibile aggiornata: " + prodottoAggiornato.QuantitaDisponibile);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Prodotto non trovato.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Errore durante la modifica della quantità: " + ex.Message);
                    }

                    break;

                case "6":
                    List<Acquisto> tuttiGliAcquisti = storicoAcquisti.OttieniTuttiGliAcquisti();

                    Console.WriteLine();
                    Console.WriteLine("=== TUTTI GLI ACQUISTI ===");

                    if (tuttiGliAcquisti.Count == 0)
                    {
                        Console.WriteLine("Nessun acquisto registrato.");
                    }
                    else
                    {
                        foreach (Acquisto acquisto in tuttiGliAcquisti)
                        {
                            servizioNegozio.StampaAcquisto(acquisto);
                        }
                    }

                    break;

                case "7":
                    servizioNegozio.StampaReportProdotti();
                    break;

                case "0":
                    continuaMenuAmministratore = false;
                    break;

                default:
                    Console.WriteLine("Scelta non valida. Riprova.");
                    break;
            }
        }
    }

    private void MostraCatalogo()
    {
        // Metodo già implementato: mostra a video tutti i prodotti del catalogo.
        List<Prodotto> prodotti = catalogoProdotti.OttieniTuttiIProdotti();

        Console.WriteLine();
        Console.WriteLine("=== CATALOGO PRODOTTI ===");

        if (prodotti.Count == 0)
        {
            Console.WriteLine("Il catalogo è vuoto.");
            return;
        }

        foreach (Prodotto prodotto in prodotti)
        {
            Console.WriteLine(
                prodotto.CodiceProdotto + " - " +
                prodotto.Nome + " - " +
                prodotto.Prezzo.ToString("0.00") + " euro - " +
                "Disponibili: " + prodotto.QuantitaDisponibile);
        }
    }

    private void MostraCarrello()
    {
        // Metodo già implementato: mostra contenuto del carrello e totale corrente.
        List<ElementoCarrello> elementi = carrelloUtente.OttieniElementi();

        Console.WriteLine();
        Console.WriteLine("=== CARRELLO ===");

        if (elementi.Count == 0)
        {
            Console.WriteLine("Il carrello è vuoto.");
            return;
        }

        foreach (ElementoCarrello elemento in elementi)
        {
            Console.WriteLine(
                elemento.ProdottoSelezionato.CodiceProdotto + " - " +
                elemento.ProdottoSelezionato.Nome + " - " +
                "Quantità: " + elemento.QuantitaScelta + " - " +
                "Prezzo unitario: " + elemento.PrezzoUnitario.ToString("0.00") + " euro - " +
                "Parziale: " + elemento.CalcolaTotaleParziale().ToString("0.00") + " euro");
        }

        Console.WriteLine("Totale carrello: " + carrelloUtente.CalcolaTotale().ToString("0.00") + " euro");
    }

    private void MostraStoricoUtente()
    {
        // Metodo già implementato: chiede un nome e mostra gli acquisti collegati.
        Console.Write("Inserisci nome utente: ");
        string? nomeUtente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nomeUtente))
        {
            Console.WriteLine("Nome utente non valido.");
            return;
        }

        List<Acquisto> acquistiUtente = storicoAcquisti.OttieniAcquistiPerUtente(nomeUtente);

        Console.WriteLine();
        Console.WriteLine("=== STORICO ACQUISTI DI " + nomeUtente.Trim() + " ===");

        if (acquistiUtente.Count == 0)
        {
            Console.WriteLine("Nessun acquisto trovato per questo utente.");
            return;
        }

        foreach (Acquisto acquisto in acquistiUtente)
        {
            servizioNegozio.StampaAcquisto(acquisto);
        }
    }

    private int LeggiInteroPositivo(string messaggio)
    {
        int valore;

        do
        {
            Console.Write(messaggio);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out valore) && valore > 0)
            {
                return valore;
            }

            Console.WriteLine("Valore non valido. Inserisci un numero intero maggiore di zero.");

        } while (true);
    }

    private decimal LeggiPrezzoPositivo(string messaggio)
    {
        decimal valore;

        do
        {
            Console.Write(messaggio);
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out valore) && valore > 0)
            {
                return valore;
            }

            Console.WriteLine("Prezzo non valido. Inserisci un numero maggiore di zero.");

        } while (true);
    }
}

public interface IGestioneCatalogo
{
    void AggiungiProdotto(Prodotto prodotto);
    bool EliminaProdotto(string codiceProdotto);
    Prodotto? CercaProdottoPerCodice(string codiceProdotto);
    List<Prodotto> OttieniTuttiIProdotti();
    bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo);
    bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita);
}

public interface IGestioneCarrello
{
    bool AggiungiAlCarrello(Prodotto prodotto, int quantita);
    bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita);
    bool RimuoviDalCarrello(string codiceProdotto);
    void SvuotaCarrello();
    decimal CalcolaTotale();
    List<ElementoCarrello> OttieniElementi();
}

public interface IGestioneAcquisti
{
    void RegistraAcquisto(Acquisto acquisto);
    List<Acquisto> OttieniTuttiGliAcquisti();
    List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente);
}

public class Utente
{
    public string Nome { get; private set; }

    public Utente(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Il nome utente non può essere vuoto.");
        }

        Nome = nome.Trim();
    }
}

public class Prodotto
{
    public string CodiceProdotto { get; private set; }
    public string Nome { get; private set; }
    public decimal Prezzo { get; private set; }
    public int QuantitaDisponibile { get; private set; }
    public int QuantitaIniziale { get; private set; }

    public Prodotto(string codiceProdotto, string nome, decimal prezzo, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        Nome = nome;
        Prezzo = prezzo;
        QuantitaDisponibile = quantitaDisponibile;
        QuantitaIniziale = quantitaDisponibile;
    }

    public void CambiaPrezzo(decimal nuovoPrezzo)
    {
        // Metodo già implementato: centralizza la validazione del prezzo.
        if (nuovoPrezzo <= 0)
        {
            throw new ArgumentException("Il prezzo deve essere maggiore di zero.");
        }

        Prezzo = nuovoPrezzo;
    }

    public void CambiaQuantita(int variazioneQuantita)
    {
        // Metodo già implementato: impedisce di portare il magazzino sotto zero.
        int nuovaQuantita = QuantitaDisponibile + variazioneQuantita;

        if (nuovaQuantita < 0)
        {
            throw new InvalidOperationException("La quantità disponibile non può diventare negativa.");
        }

        QuantitaDisponibile = nuovaQuantita;
    }

    public int CalcolaQuantitaVenduta()
    {
        // Metodo già implementato: serve per il report amministratore.
        return QuantitaIniziale - QuantitaDisponibile;
    }
}

public class ElementoCarrello
{
    public Prodotto ProdottoSelezionato { get; private set; }
    public int QuantitaScelta { get; private set; }
    public decimal PrezzoUnitario { get; private set; }

    public ElementoCarrello(Prodotto prodottoSelezionato, int quantitaScelta)
    {
        ProdottoSelezionato = prodottoSelezionato;
        QuantitaScelta = quantitaScelta;
        PrezzoUnitario = prodottoSelezionato.Prezzo;
    }

    public decimal CalcolaTotaleParziale()
    {
        // Metodo già implementato: evita di duplicare il calcolo del parziale.
        return PrezzoUnitario * QuantitaScelta;
    }

    public void CambiaQuantitaScelta(int nuovaQuantita)
    {
        if (nuovaQuantita <= 0)
        {
            throw new ArgumentException("La quantità scelta deve essere maggiore di zero.");
        }

        QuantitaScelta = nuovaQuantita;
    }
}

public class Acquisto
{
    public Utente Utente { get; private set; }
    public string NomeUtente
    {
        get { return Utente.Nome; }
    }

    public List<ElementoAcquistato> ProdottiAcquistati { get; private set; }
    public decimal TotaleOrdine { get; private set; }
    public DateTime DataAcquisto { get; private set; }

    public Acquisto(Utente utente, List<ElementoAcquistato> prodottiAcquistati)
    {
        Utente = utente;
        ProdottiAcquistati = prodottiAcquistati;
        DataAcquisto = DateTime.Now;
        TotaleOrdine = CalcolaTotaleOrdine();
    }

    private decimal CalcolaTotaleOrdine()
    {
        // Metodo già implementato: somma tutti i parziali dei prodotti acquistati.
        return ProdottiAcquistati.Sum(prodotto => prodotto.TotaleParziale);
    }
}

public class ElementoAcquistato
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaAcquistata { get; private set; }
    public decimal PrezzoUnitario { get; private set; }
    public decimal TotaleParziale { get; private set; }

    public ElementoAcquistato(string codiceProdotto, string nomeProdotto, int quantitaAcquistata, decimal prezzoUnitario)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaAcquistata = quantitaAcquistata;
        PrezzoUnitario = prezzoUnitario;
        TotaleParziale = prezzoUnitario * quantitaAcquistata;
    }
}

public class CatalogoProdotti : IGestioneCatalogo
{
    private readonly List<Prodotto> prodotti;

    public CatalogoProdotti()
    {
        prodotti = new List<Prodotto>();
    }

    public void AggiungiProdotto(Prodotto prodotto)
    {
        // Metodo già implementato: evita codici duplicati nel catalogo.
        bool codiceGiaPresente = prodotti.Any(p => p.CodiceProdotto == prodotto.CodiceProdotto);

        if (codiceGiaPresente)
        {
            throw new InvalidOperationException("Esiste già un prodotto con lo stesso codice.");
        }

        prodotti.Add(prodotto);
    }

    public bool EliminaProdotto(string codiceProdotto)
    {
        Prodotto? prodottoDaEliminare = CercaProdottoPerCodice(codiceProdotto);

        if (prodottoDaEliminare == null)
        {
            return false;
        }

        prodotti.Remove(prodottoDaEliminare);
        return true;
    }

    public Prodotto? CercaProdottoPerCodice(string codiceProdotto)
    {
        // Metodo già implementato: ricerca case-insensitive per rendere più comodo l'input da console.
        return prodotti.FirstOrDefault(prodotto =>
            prodotto.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));
    }

    public List<Prodotto> OttieniTuttiIProdotti()
    {
        // Metodo già implementato: restituisce una copia per proteggere la lista interna.
        return new List<Prodotto>(prodotti);
    }

    public bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo)
    {
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);

        if (prodotto == null)
        {
            return false;
        }

        prodotto.CambiaPrezzo(nuovoPrezzo);
        return true;
    }

    public bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita)
    {
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);

        if (prodotto == null)
        {
            return false;
        }

        prodotto.CambiaQuantita(variazioneQuantita);
        return true;
    }
}

public class CarrelloUtente : IGestioneCarrello
{
    private readonly List<ElementoCarrello> elementiCarrello;

    public CarrelloUtente()
    {
        elementiCarrello = new List<ElementoCarrello>();
    }

    public bool AggiungiAlCarrello(Prodotto prodotto, int quantita)
    {
        if (quantita <= 0 || quantita > prodotto.QuantitaDisponibile)
        {
            return false;
        }

        ElementoCarrello? elementoEsistente = elementiCarrello.FirstOrDefault(elemento =>
            elemento.ProdottoSelezionato.CodiceProdotto.Equals(prodotto.CodiceProdotto, StringComparison.OrdinalIgnoreCase));

        if (elementoEsistente != null)
        {
            int nuovaQuantitaTotale = elementoEsistente.QuantitaScelta + quantita;

            if (nuovaQuantitaTotale > prodotto.QuantitaDisponibile)
            {
                return false;
            }

            elementoEsistente.CambiaQuantitaScelta(nuovaQuantitaTotale);
            return true;
        }

        elementiCarrello.Add(new ElementoCarrello(prodotto, quantita));
        return true;
    }

    public bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita)
    {
        ElementoCarrello? elemento = elementiCarrello.FirstOrDefault(elementoCarrello =>
            elementoCarrello.ProdottoSelezionato.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));

        if (elemento == null || nuovaQuantita <= 0 || nuovaQuantita > elemento.ProdottoSelezionato.QuantitaDisponibile)
        {
            return false;
        }

        elemento.CambiaQuantitaScelta(nuovaQuantita);
        return true;
    }

    public bool RimuoviDalCarrello(string codiceProdotto)
    {
        ElementoCarrello? elemento = elementiCarrello.FirstOrDefault(elementoCarrello =>
            elementoCarrello.ProdottoSelezionato.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));

        if (elemento == null)
        {
            return false;
        }

        elementiCarrello.Remove(elemento);
        return true;
    }

    public void SvuotaCarrello()
    {
        // Metodo già implementato: cancella tutti gli elementi del carrello.
        elementiCarrello.Clear();
    }

    public decimal CalcolaTotale()
    {
        // Metodo già implementato: ricalcola sempre il totale dai parziali correnti.
        return elementiCarrello.Sum(elemento => elemento.CalcolaTotaleParziale());
    }

    public List<ElementoCarrello> OttieniElementi()
    {
        // Metodo già implementato: restituisce una copia per evitare modifiche esterne dirette.
        return new List<ElementoCarrello>(elementiCarrello);
    }
}

public class StoricoAcquisti : IGestioneAcquisti
{
    private readonly List<Acquisto> acquisti;

    public StoricoAcquisti()
    {
        acquisti = new List<Acquisto>();
    }

    public void RegistraAcquisto(Acquisto acquisto)
    {
        // Metodo già implementato: conserva l'acquisto in memoria durante l'esecuzione.
        acquisti.Add(acquisto);
    }

    public List<Acquisto> OttieniTuttiGliAcquisti()
    {
        // Metodo già implementato: restituisce una copia dello storico.
        return new List<Acquisto>(acquisti);
    }

    public List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente)
    {
        return acquisti
            .Where(acquisto => acquisto.NomeUtente.Equals(nomeUtente.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}

public class ServizioNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;

    public ServizioNegozio(CatalogoProdotti catalogoProdotti, CarrelloUtente carrelloUtente, StoricoAcquisti storicoAcquisti)
    {
        this.catalogoProdotti = catalogoProdotti;
        this.carrelloUtente = carrelloUtente;
        this.storicoAcquisti = storicoAcquisti;
    }

    public bool AggiungiProdottoAlCarrello(string codiceProdotto, int quantita)
    {
        Prodotto? prodotto = catalogoProdotti.CercaProdottoPerCodice(codiceProdotto);

        if (prodotto == null)
        {
            return false;
        }

        return carrelloUtente.AggiungiAlCarrello(prodotto, quantita);
    }

    public Acquisto ConfermaAcquisto(Utente utente)
    {
        List<ElementoCarrello> elementiCarrello = carrelloUtente.OttieniElementi();

        if (elementiCarrello.Count == 0)
        {
            throw new InvalidOperationException("Impossibile confermare un acquisto con carrello vuoto.");
        }

        foreach (ElementoCarrello elemento in elementiCarrello)
        {
            if (elemento.QuantitaScelta <= 0 || elemento.QuantitaScelta > elemento.ProdottoSelezionato.QuantitaDisponibile)
            {
                throw new InvalidOperationException("Quantità non disponibile per il prodotto: " + elemento.ProdottoSelezionato.Nome);
            }
        }

        List<ElementoAcquistato> prodottiAcquistati = elementiCarrello
            .Select(elemento => new ElementoAcquistato(
                elemento.ProdottoSelezionato.CodiceProdotto,
                elemento.ProdottoSelezionato.Nome,
                elemento.QuantitaScelta,
                elemento.PrezzoUnitario))
            .ToList();

        foreach (ElementoCarrello elemento in elementiCarrello)
        {
            elemento.ProdottoSelezionato.CambiaQuantita(-elemento.QuantitaScelta);
        }

        Acquisto acquisto = new Acquisto(utente, prodottiAcquistati);
        storicoAcquisti.RegistraAcquisto(acquisto);
        carrelloUtente.SvuotaCarrello();

        return acquisto;
    }

    public List<ReportProdotto> CreaReportProdotti()
    {
        // Metodo già implementato: prepara il report richiesto per l'amministratore.
        return catalogoProdotti.OttieniTuttiIProdotti()
            .Select(prodotto => new ReportProdotto(
                prodotto.CodiceProdotto,
                prodotto.Nome,
                prodotto.QuantitaIniziale,
                prodotto.CalcolaQuantitaVenduta(),
                prodotto.QuantitaDisponibile))
            .ToList();
    }

    public void StampaAcquisto(Acquisto acquisto)
    {
        // Metodo già implementato: mostra i dettagli di un acquisto completato.
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Utente: " + acquisto.NomeUtente);
        Console.WriteLine("Data: " + acquisto.DataAcquisto.ToString("dd/MM/yyyy HH:mm"));
        Console.WriteLine("Prodotti acquistati:");

        foreach (ElementoAcquistato elemento in acquisto.ProdottiAcquistati)
        {
            Console.WriteLine(
                "- " + elemento.CodiceProdotto + " - " +
                elemento.NomeProdotto + " - " +
                "Quantità: " + elemento.QuantitaAcquistata + " - " +
                "Prezzo unitario: " + elemento.PrezzoUnitario.ToString("0.00") + " euro - " +
                "Parziale: " + elemento.TotaleParziale.ToString("0.00") + " euro");
        }

        Console.WriteLine("Totale ordine: " + acquisto.TotaleOrdine.ToString("0.00") + " euro");
    }

    public void StampaReportProdotti()
    {
        // Metodo già implementato: mostra il report quantità richiesto all'amministratore.
        List<ReportProdotto> report = CreaReportProdotti();

        Console.WriteLine();
        Console.WriteLine("=== REPORT PRODOTTI ===");

        if (report.Count == 0)
        {
            Console.WriteLine("Nessun prodotto presente nel catalogo.");
            return;
        }

        foreach (ReportProdotto riga in report)
        {
            Console.WriteLine(
                riga.CodiceProdotto + " - " +
                riga.NomeProdotto + " - " +
                "Iniziale: " + riga.QuantitaIniziale + " - " +
                "Venduta: " + riga.QuantitaVenduta + " - " +
                "Disponibile: " + riga.QuantitaDisponibile);
        }
    }
}

public class ReportProdotto
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaIniziale { get; private set; }
    public int QuantitaVenduta { get; private set; }
    public int QuantitaDisponibile { get; private set; }

    public ReportProdotto(string codiceProdotto, string nomeProdotto, int quantitaIniziale, int quantitaVenduta, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaIniziale = quantitaIniziale;
        QuantitaVenduta = quantitaVenduta;
        QuantitaDisponibile = quantitaDisponibile;
    }
}
