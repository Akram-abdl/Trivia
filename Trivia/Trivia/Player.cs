namespace Trivia;

public class Player
{
    public string name { get; }
    public int nbJoker {get; set;}
    public bool use {get; set;}
    public int Place { get; set; }
    public int Purse { get; set; }
    public bool InPenaltyBox { get; set; }
    public int CorrectAnswersRow { get; set; }
    public bool askYesQuestion { get; set; }
    

    public Player(string name)
    {
        this.name = name;
        nbJoker = 1;
        use = false;
    }
    
    public Player(string name, bool askYesQuestion)
    {
        this.name = name;
        this.askYesQuestion = askYesQuestion;
        nbJoker = 1;
        use = false;
    }

    public override string ToString()
    {
        return name;
    }
}