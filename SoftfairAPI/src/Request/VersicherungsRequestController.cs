using Microsoft.AspNetCore.Mvc;

//--------------------------------------------------------------------------------

namespace SoftfairAPI; 

/// <summary>
/// Diese Klasse gibt die Implementierung für Vergleichsrechner Requests an.
/// Hierrüber finden sich alle Request Methoden, die aktuell noch über eine feste Route verlaufen.
/// Weitere Implementierungen brauchen entsprechend eine andere Route um gefunden zu werden.
///
/// Dieser Punkt war für mich neu, da ich mich bisher nicht mit solchen API Schnittstellen auseinandersetzen musste.
/// Hier bin ich mir auch nicht sicher, wie der Aufruf letztendlich in der Klasse landet und wie ich eigentlich
/// testen kann, ob der Controller via Request das tut, was er soll.
/// Ich habe mich versucht darüber schlau zu machen, welche ActionResults in welcher Situation am besten wären
/// und bin mir bewusst, dass es definitiv Edgecases gibt, die man weiter verfolgen kann.
///
/// Ich bin hier sehr auf Feedback gespannt und habe Lust neue Aspekte zu erlernen.
///
/// Ich habe mir hierzu bereits Postman engeschaut und verschiedene Test-Frameworks die ggf. gut korrespondieren würden,
/// habe es bisher aber nicht geschafft, den Controller korrekt anzusprechen. Da ich in meinem Zeitlimit bleiben möchte
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