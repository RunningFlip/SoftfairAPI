namespace SoftfairAPI; 

/// <summary>
/// Hier der gewünschte Vergleichsrechner der das beste Angebot auf Basis der monatlichen Prämie sowie der Deckungssumme
/// ermittelt.
/// </summary>
public class KostenNutzenVergleichsrechner : IVergleichsrechner<Versicherungsangebot> {
    
    /// <summary>
    /// Ich habe mich hier für eine einfache Variante entschieden, da noch nicht klar ist, wie viele Angebote es eigentlich geben wird.
    /// Optimierung kommt im besten Falle immer zum Schluss, weshalb genau der Punkt hier ein Kandidat dafür wäre.
    /// Bei einer sehr großen Liste, kann eine Alternative zum MinBy Aufruf attraktiv werden.
    /// </summary>
    /// <param name="angebote">Die Liste der übergebenen Angebote die vergleichen werden soll.</param>
    /// <returns>Das beste Ergebnis im Vergleich der monatlichen Prämie sowie der Deckungssumm</returns>
    public Versicherungsangebot? VergleicheAngebote(List<Versicherungsangebot> angebote) {
        return angebote.MinBy(candidate => candidate.MonatlichePraemie / candidate.Deckungssumme);
    }
}