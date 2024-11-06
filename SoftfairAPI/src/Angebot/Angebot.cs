namespace SoftfairAPI;

/// <summary>
/// Um für mögliche Erweiterungszwecke verschiedene Angebotsmöglichkeiten zu eröffnen,
/// habe ich mich dazu entschieden eine Base einzuziehen, die mindestens eine ID besitzt,
/// um entsprechend Angebote unterscheiden zu können.
/// </summary>
public abstract class Angebot {
    public int Id { get; set; }
}