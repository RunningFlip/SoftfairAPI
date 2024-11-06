using Microsoft.AspNetCore.Mvc;

//--------------------------------------------------------------------------------

namespace SoftfairAPI; 

public static class Progam {
    
    //--------------------------------------------------------------------------------
    // Main
    //--------------------------------------------------------------------------------
    
    public static void Main(string[] args) {
    
        AngebotManagement management = new AngebotManagement();

        Versicherungsangebot angebotA = new Versicherungsangebot() { Anbieter="Hook", VersicherungsTyp= VersicherungsTyp.Haftpflicht, MonatlichePraemie=30.99m, Deckungssumme=200000 };
        Versicherungsangebot angebotB = new Versicherungsangebot() { Anbieter="Allianz", VersicherungsTyp=VersicherungsTyp.KFZ, MonatlichePraemie=27.99m, Deckungssumme=100000 };
        Versicherungsangebot angebotC = new Versicherungsangebot() { Anbieter="DVB", VersicherungsTyp=VersicherungsTyp.Hausrat, MonatlichePraemie=10.0m, Deckungssumme=100700 };
        Versicherungsangebot angebotD = new Versicherungsangebot() { Anbieter="Wüstenrot", VersicherungsTyp=VersicherungsTyp.Haftpflicht, MonatlichePraemie=99.0m, Deckungssumme=500000 };
        
        management.Create(angebotA);
        management.Create(angebotB);
        management.Create(angebotC);
        management.Create(angebotD);

        VersicherungsRequestController requestController = new VersicherungsRequestController(management);
        
        // Alle Angebote
        ActionResult<IEnumerable<Versicherungsangebot>> allAngeboteResult = requestController.GetAngebote();
        Versicherungsangebot[] allAngebote = UnwrapOkObjectResult<Versicherungsangebot[], IEnumerable<Versicherungsangebot>>(allAngeboteResult);
        Console.WriteLine($"Angebotanzahl: {allAngebote?.Length}");
        
        // Bestes Angebot
        ActionResult<Versicherungsangebot> bestAngebotResult = requestController.GetAngebot();
        Versicherungsangebot bestAngebot = UnwrapOkObjectResult<Versicherungsangebot, Versicherungsangebot>(bestAngebotResult);
        Console.WriteLine($"Günstigster Anbieter: {bestAngebot.Anbieter} mit einer mon. Prämie: {bestAngebot.MonatlichePraemie} & einer Deckungssumme: {bestAngebot.Deckungssumme}");

        // Fügt ein neues günstiges Angebot hinzu
        requestController.PostAngebot(new Versicherungsangebot() { Anbieter="Hook", VersicherungsTyp=VersicherungsTyp.Haftpflicht, MonatlichePraemie=17.99m, Deckungssumme=800000 });
        allAngeboteResult = requestController.GetAngebote();
        allAngebote = UnwrapOkObjectResult<Versicherungsangebot[], IEnumerable<Versicherungsangebot>>(allAngeboteResult);
        Console.WriteLine($"Angebotanzahl: {allAngebote?.Length}");
        
        // Checkt ob das neue Angebot nun das günstigste ist
        bestAngebotResult = requestController.GetAngebot();
        bestAngebot = UnwrapOkObjectResult<Versicherungsangebot, Versicherungsangebot>(bestAngebotResult);
        Console.WriteLine($"Günstigster Anbieter: {bestAngebot.Anbieter} mit einer mon. Prämie: {bestAngebot.MonatlichePraemie} & einer Deckungssumme: {bestAngebot.Deckungssumme}");
    }
    
    //--------------------------------------------------------------------------------
    // Helpers
    //--------------------------------------------------------------------------------

    private static TResult UnwrapOkObjectResult<TResult, TResultType>(ActionResult<TResultType> actionResult) {

        if (actionResult.Result is OkObjectResult objectResult) {

            if (objectResult.Value != null && objectResult.Value is TResult result) {
                return result;
            }
        }

        return default;
    }
}