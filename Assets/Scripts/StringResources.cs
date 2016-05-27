namespace Assets.Scripts
{
    /// <summary>
    /// Class containing UI string constants to facilitate wording changes.
    /// </summary>
    public class StringResources
    {
        // common texts
        public const string BackButtonText = "Zurück";
        public const string GoButtonText = "Los!";

        // start screen
        public const string HelpButtonDefaultText = "Hilfe";
        public const string GameButtonText = "QR-Schnitzeljagd spielen";
        public const string WifiButtonText = "Wo bin ich gerade?";
        public const string NextEvent = "In {0} Minuten startet hier die Veranstaltung {1}.";
        public const string AlgEvent = "{0}: In {1} Minuten startet hier die Veranstaltung {2}.";

        // game-scene texts
        public const string GameHeading = "QR-Schnitzeljagd";
        public const string HelpButtonText = "Anleitung";
        public const string QuestionProgressText = "Fragen: {0}/{1}";
        public const string CoinProgressText = "Münzen: {0}/{1}";


        // help-scene texts
        public const string HelpText = "In den Gebäuden der Fachhochschule sind verschiedene QR-Codes versteckt.\nEs gibt Fragen- und Münz-QR-Codes.\nFinde die Fragen-QR-Codes und beantworte die Fragen, um die Münz-QR-Codes freizuschalten.\nNachdem eine Frage eine Münze freigeschaltet hat, sammle diese ein.\nFinde und sammle alle Münzen, um einen Preis zu gewinnen.";
        public const string HelpSceneHeading = "Anleitung";

        // help-scene dafault texts
        public const string HelpDefaultText = "Wähle einen Ort aus, um die dort stattfindenden Veranstaltungen anzuzeigen. Klicke anschließend auf Los und scanne Positions-QR-Codes, um dir den Weg zu diesem Ort mit Hilfe von Pfeilen anzeigen zu lassen. Es kann nötig sein, auf dem Weg zum Ziel weitere QR-Codes zu scannen, um den Weg zu finden. \nOder wechsel zur Ansicht der QR-Schnitzeljagd, wenn du bei einem Such- und Ratespiel die Fachhochschule erkunden möchtest.";
        public const string HelpSceneDefaultHeading = "Hilfe";

        // camera scene texts
        public const string WinToastMessage = "Herzlichen Glückwunsch! Du hast alle Münzen gesammelt. Hole dir deinen Preis am Stand ab.";

        public const string QuestionAlreadyAnsweredToastMessage = "Du hast diese Frage bereits beantwortet.";
        public const string CoinAlreadyCollectedToastMessage = "Du hast diese Münze bereits eingesammelt.";
        public const string TapCoinToCollectToastMessage = "Berühre den Bildschirm um diese Münze einzusammeln.";

        public const string AnswerQuestionFirstToastMessage = "Diese Münze ist noch gesperrt. Suche und beantworte zunächst die richtige Frage.";
        public const string NoDestinationToastMessage = "Navigationspunkt. Wähle ein Ziel beim Menüpunkt 'FH Navigation' aus, um den Weg zu diesem angezeigt zu bekommen.";

        // position describtion texts
        public const string Mediabuilding = "Sie befinden sich im Mediengebäude. Im Untergeschoss befinden sich das Audimax, das VR-Labor und ds Fotolabor. Im Obergeschoss befindet sich das Medienrechenzentrum.";
        public const string South = "Sie befinden sich im Südes des Neubaus. Hier befinden sich die Cafeteria mit Terrase, das Rechenzentrum 5 und die Hörsäle 3 und 4.";
        public const string Old =  "Sie befinden sich im Altbau. In den oberen Stockwerken befindet sich die Elektro- und Digitaltechnik, im Keller befindet sich die Optik. Im Erdgeschoss befinden sich diverse Seminarräume und die Verwaltung mit dem Sekräteriat.";
        public const string Passage = "Sie befinden sich in dem Korridor, der Alt- und Neubau verbindet. Hier befinden sich das Rechnernetzelabor und diverse Arbeitsplätze.";
        public const string Overbuilding = "Sie befinden sich im Überbau. Hier befinden sich im Erdgeschoss die Hörsäle 1 und 2, im 1. Obergeschoss die Fertigungstechnik und im 2. Obergeschoss die Bibliothek und der Hörsaal 6.";
        public const string Entrance = "Sie befinden sich im Eingangsbereich. Hier befindet sich die Studentenvertretung AStA, der Hörsaal 4 und der Seminarraum 11.";
        public const string Passage2 = "Sie befinden sich im Zwischengang. Hier befindet sich Hörsaal 5 und die Rechenzentren 1 und 4.";
        public const string North = "Sie befinden sich im Norden des Neubaus. Hier befinden sich im Erdgeschoss die Rechenzentren 1 bis 3 und im Obergeschoss der Seminarraum 9 sowie die Robotik.";
        public const string NoWifi = "Die aktuelle Position kann gerade nicht festgestellt werden.";

    }
}