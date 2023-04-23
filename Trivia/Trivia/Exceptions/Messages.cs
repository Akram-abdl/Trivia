namespace Trivia.Exceptions;

public class Messages
{
    public const string NotEnoughPlayerException =
        "Il n'y a pas assez de joueurs pour démarrer une partie, ajoutez-en au moins 2.";

    public const string TooManyPlayerException = "Une partie ne peut pas démarrer avec plus de 6 joueurs";

    public const string MinimumGoldRequirement6 = " Il faut mettre en paramètre au minimum 6 pièces d'or pour pouvoir gagner la partie";

}