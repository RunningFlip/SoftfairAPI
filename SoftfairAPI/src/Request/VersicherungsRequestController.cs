using Microsoft.AspNetCore.Mvc;

//--------------------------------------------------------------------------------

namespace SoftfairAPI; 

/// Diese Klasse gibt die Implementierung für Vergleichsrechner-Requests an.
/// Hierüber finden sich alle Request-Methoden, die aktuell noch über eine feste Route verlaufen.
/// Weitere Implementierungen brauchen entsprechend eine andere Route, um gefunden zu werden.
///
/// Dieser Punkt war für mich neu, da ich bisher noch keine API-Schnittstellen gebaut habe,
/// die GET, PUT, POST & DELETE implementieren.
/// Hier bin ich mir auch nicht sicher, wie der Aufruf letztendlich in der Klasse landet und wie ich eigentlich
/// testen kann, ob der Controller via Request das tut, was er soll.
/// Ich habe mich versucht, darüber schlau zu machen, welche ActionResults in welcher Situation am besten wären
/// und bin mir bewusst, dass es definitiv Edgecases gibt, die man weiter verfolgen kann.
/// Außerdem kann man, bei Bedarf, die Methoden jeweils async implementieren und entsprechend den aktuellen Return-Typ
/// mit einem Task wrappen.
///
/// Ich bin hier sehr auf Feedback gespannt und habe Lust, neue Aspekte zu erlernen.
///
/// Ich habe mir hierzu bereits Postman angeschaut und verschiedene Test-Frameworks, die ggf. gut korrespondieren würden,
/// habe es bisher aber nicht geschafft, den Controller korrekt anzusprechen. Da ich in meinem Zeitlimit bleiben möchte,
/// belasse ich es hierbei.
/// </summary>
[Route("api/angebote")]
public class VersicherungsRequestController : RequestController<Versicherungsangebot> {
    
    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------
    
    public VersicherungsRequestController(AngebotManagement management) : base(management) { }
    
    //--------------------------------------------------------------------------------
    // Methods
    //--------------------------------------------------------------------------------

    [HttpGet]
    public override ActionResult<IEnumerable<Versicherungsangebot>> GetAngebote() {
        return this.Ok(this.management.GetAllAngeboteOfType<Versicherungsangebot>());
    }
    
    //--------------------------------------------------------------------------------
    
    [HttpPost]
    public override ActionResult PostAngebot(Versicherungsangebot angebot) {

        try {
            this.management.Create(angebot);
        }
        catch (Exception e) {
            return this.Conflict(e.Message);
        }

        return this.NoContent();
    }
    
    //--------------------------------------------------------------------------------

    [HttpPut("{id}")]
    public override ActionResult PutAngebot(int id, Versicherungsangebot angebot) {

        try {
            this.management.Update(id, angebot);
        }
        catch (Exception e) {
            return this.Conflict(e.Message);
        }
        
        return this.NoContent();
    }
    
    //--------------------------------------------------------------------------------

    [HttpDelete("{id}")]
    public override ActionResult DeleteAngebot(int id) {

        try {
            this.management.Delete(id);
        }
        catch (Exception e) {
            return this.Conflict(e.Message);
        }
        
        return this.NoContent();
    }
    
    //--------------------------------------------------------------------------------

    /// <summary>
    /// Hier war ich mir nicht ganz sicher wie die Anforderung gedacht war.
    /// Die Liste von Angeboten kann ich natürlich als Übergabeparameter mit übergeben,
    /// da alle anderen Methoden aber intern auf den Daten des AngebotManagements arbeiten,
    /// wollte ich hier nicht nach außen ermöglich, dass fremde Daten in die Methode gegeben werden können
    /// und stattdessen sicherstellen, dass auf den Daten des Managements gearbeitet wird.
    /// </summary>
    /// <returns></returns>
    [HttpGet("{vergleiche}")]
    public override ActionResult<Versicherungsangebot> GetAngebot() {
        
        Versicherungsangebot[] allAngebote = this.management.GetAllAngeboteOfType<Versicherungsangebot>();
        KostenNutzenVergleichsrechner vergleichsrechner = new KostenNutzenVergleichsrechner();

        try {

            Versicherungsangebot? result = vergleichsrechner.VergleicheAngebote(allAngebote.ToList());
            return result == null ? this.NoContent() : this.Ok(result);
        }
        catch (Exception e) {
            return this.Conflict(e.Message);
        }
    }
}