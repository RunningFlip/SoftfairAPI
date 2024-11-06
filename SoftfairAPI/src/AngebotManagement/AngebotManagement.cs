namespace SoftfairAPI; 

/// <summary>
/// Das AngebotManagement verwaltet Datenset verschiedener Angebote, und legt CRUD Operationen nach außen.
/// Ich habe mich dazu entschieden, die CRUD Operationen generisch zu bauen, um meinen vorherigen Ansatz
/// potentiell verschiedener Angebote weiter auszubauen.
/// Damit ist es nun möglich unterschiedliche Angebotstypen zu verwalten.
/// </summary>
public class AngebotManagement {
    
    //--------------------------------------------------------------------------------
    // Fields
    //--------------------------------------------------------------------------------
    
    /// <summary>
    /// Ich habe mich hier für ein Dictionary entschieden, was einfach aber schnell Beziehungen zwischen einer ID und einem
    /// dazugehörigen Angebot sicherstellen kann.
    /// Eine bessere und aufwendigere Alternative wäre hier eine Datenbank die angesprochen wird über die genau diese
    /// Informationen gespeichert werden können. NoSQL oder SQL würde sich hier anbieten.
    /// Für meine Aufgabe hätte ich hier ebenfalls auf eine JSON Struktur setzen können.
    /// </summary>
    private readonly Dictionary<int, Angebot> idToAngebot = new Dictionary<int, Angebot>();

    //--------------------------------------------------------------------------------
    // Methods
    //--------------------------------------------------------------------------------
    
    public TAngebot[] GetAllAngeboteOfType<TAngebot>() where TAngebot : Angebot {
        return this.idToAngebot.Values.OfType<TAngebot>().ToArray();
    }
    
    //--------------------------------------------------------------------------------
    
    public void Create<TAngebot>(TAngebot angebot) where TAngebot : Angebot {

        if (angebot == null) {
            throw new ArgumentNullException(nameof(angebot));
        }

        if (!this.idToAngebot.TryAdd(angebot.Id, angebot)) {
            throw new Exception(
                $"An '{angebot.GetType()}' object with the id '{angebot.Id}' is already registered. Duplicates are prohibited!");
        }
    }
    
    //--------------------------------------------------------------------------------
    
    public TAngebot? Read<TAngebot>(int id) where TAngebot : Angebot {

        if (this.idToAngebot.TryGetValue(id, out Angebot result)) {

            if (result is TAngebot castedResult) {
                return castedResult;
            }

            throw new Exception($"The requested {typeof(Angebot)} with the id '{id}' is not from type '{typeof(TAngebot)}'.");
        }

        return null;
    }
    
    //--------------------------------------------------------------------------------
    
    public void Update<TAngebot>(int id, TAngebot angebot) where TAngebot : Angebot {
        
        if (angebot.Id < 0) {
            throw new Exception($"Ids are not allowed to be negative! Id was '{angebot.Id}'.");
        }
        
        if (angebot == null) {
            throw new ArgumentNullException(nameof(angebot));
        }
        
        if (this.idToAngebot.ContainsKey(id)) {
            this.idToAngebot[id] = angebot;
        }
        else {
            throw new Exception($"No registered '{angebot.GetType()}' object was found with the given Id '{angebot.Id}'!");
        }
    }
    
    //--------------------------------------------------------------------------------
    
    public void Delete(int id) {
        
        if (id < 0) {
            throw new Exception($"Ids are not allowed to be negative! Given Id was '{id}'.");
        }
        
        this.idToAngebot.Remove(id);
    }
}