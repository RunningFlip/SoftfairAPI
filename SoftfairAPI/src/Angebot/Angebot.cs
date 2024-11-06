namespace SoftfairAPI;

/// <summary>
/// Um für mögliche Erweiterungszwecke verschiedene Angebotsmöglichkeiten zu eröffnen,
/// habe ich mich dazu entschieden eine Base einzuziehen, die mindestens eine ID besitzt,
/// um entsprechend Angebote unterscheiden zu können.
/// </summary>
public abstract class Angebot {
    
    // Um die IDs nicht selber händisch zu pflegen, habe ich mich entschieden GUIDs zu verwenden
    // und deren HashCode als ID abzuspeichern. Da die Anforderung war, die ID als Integer anzulegen,
    // habe ich hier von abgesehen direkt eine GUID als string zu speichern.
    public int Id { get; } = Math.Abs(Guid.NewGuid().GetHashCode());
}