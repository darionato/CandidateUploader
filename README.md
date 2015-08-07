# README #

Progetto di test per provare AngularJs con ASP.NET Mvc.

### Configurazione iniziale ###

* Modificare la stringa di connessione sul web.config (il database verrà creato se non esiste)
* Compilare il progetto affinchè vengano scaricati tutti i pacchetti neccessari da NuGet
* Aprire il Package Manager Console ed eseguire il comando: update-database
* Avviare il progetto

### Caratteristiche ###

* Pagina di upload
* Limite di 1000000 uploads configurabile sul web.config
* Limite di 10 MB di upload per le immagini (configurabile sul web.config)
* Limite di 60" per i video (configurabile sul web.config)
* Limite massimo elaborati per utente (configurabile sul web.config)
* Salvataggio su sql server
* Ridimensionamento immagini dopo l'upload
* Pagina di approvazione
* Pagina di visualizzazione elaborati con orinamento personalizzato

### Implementazioni mancanti ###

* La grafica non è stata curata
* Non ci sono i loader nelle chiamate AJAX
* Refactoring generale
