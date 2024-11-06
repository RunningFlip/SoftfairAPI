using Microsoft.AspNetCore.Mvc;

//--------------------------------------------------------------------------------

namespace SoftfairAPI; 

/// <summary>
/// Der Request Controller deklariert die gewünschten GET, POST, PUT & DELETE Methoden,
/// die dann von einer Kindklasse definiert werden müssen.
/// Die Klasse selber ist wieder generisch um verschiedene Angebotstypen unterstützen zu können.
/// </summary>
/// <typeparam name="T">Angebots Typ der vom unterstützt werden soll.</typeparam>
public abstract class RequestController<T> : ControllerBase where T : Angebot {
    
    //--------------------------------------------------------------------------------
    // Fields
    //--------------------------------------------------------------------------------

    protected readonly AngebotManagement management;
    
    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------
    
    protected RequestController(AngebotManagement management) {
        this.management = management;
    }
    
    //--------------------------------------------------------------------------------
    // Methods
    //--------------------------------------------------------------------------------

    [HttpGet]
    public abstract ActionResult<IEnumerable<T>> GetAngebote();
    
    [HttpPost]
    public abstract ActionResult PostAngebot(T angebot);
    
    [HttpPut("{id}")]
    public abstract ActionResult PutAngebot(int id, T angebot);

    [HttpDelete("{id}")]
    public abstract ActionResult DeleteAngebot(int id);
    
    [HttpGet("{vergleiche}")]
    public abstract ActionResult<T> GetAngebot();
}