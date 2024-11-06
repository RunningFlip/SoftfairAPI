namespace SoftfairAPI; 

/// <summary>
/// Ein Interface erschien mir hier sinnig, damit verschiedene Vergleichsrechner mit der selben Methode
/// <see cref="IVergleichsrechner{T}.VergleicheAngebote"/>> implementiert werden können.
/// Möglicherweise inkludieren andere Vergleichsrechner weitere Logik oder können anderweitig angesteuert werden.
/// </summary>
/// <typeparam name="TAngebot">Angebots Typ der vom Vergleichsrechner verbearbeitet werden soll.</typeparam>
public interface IVergleichsrechner<TAngebot> where TAngebot : Angebot {
    TAngebot? VergleicheAngebote(List<TAngebot> angebote);
}