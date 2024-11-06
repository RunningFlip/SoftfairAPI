namespace SoftfairAPI; 

/// <summary>
/// Erbt von Angebot und erweitert die Klasse um die angeforderten spezifischen
/// Versicherungsdetails.
/// </summary>
public class Versicherungsangebot : Angebot {
    
    public string Anbieter { get; set; }
    
    // Unknown schien mir hier besser, um initial ein leeres Objekt darzustellen.
    public VersicherungsTyp VersicherungsTyp { get; set; } = VersicherungsTyp.Unknown; 
    public decimal MonatlichePraemie { get; set; }
    public decimal Deckungssumme { get; set; }
}