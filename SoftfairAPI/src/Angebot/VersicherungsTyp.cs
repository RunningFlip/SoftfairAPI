namespace SoftfairAPI; 

/// <summary>
/// Ein Enum das die verschiedenen Typen von Versicherungen beinhält.
/// Optimaler wäre wahrscheinlich ein JSON File, welches lokal oder im Backend auf einem Server liegt.
/// Dieses ist dann nicht mehr Bestandteil des Codes und kann extern besser gepflegt werde,
/// sollte das Portfolio erweitert werden.
/// </summary>
public enum VersicherungsTyp {
    
    Unknown = 0,
    
    Haftpflicht = 1,
    Hausrat = 2,
    Rechtschutz = 3,
    KFZ = 4,
    Leben = 5,
    Kranken = 6,
}