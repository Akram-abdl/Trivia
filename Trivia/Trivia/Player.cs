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
    // 0: null, 1: yes question asked, 2: no question asked
    public int askYesQuestion { get; set; }
    // 0: null, 1: yes question asked, 2: no question asked
    public int reGameQuestion { get; set; }
    

    public Player(string name)
    {
        this.name = name;
        nbJoker = 1;
        use = false;
        askYesQuestion = 0;
        reGameQuestion = 0;
    }
    
    public Player(string name, int askYesQuestion, int reGameQuestion)
    {
        this.name = name;
        this.askYesQuestion = askYesQuestion;
        this.reGameQuestion = reGameQuestion;
        nbJoker = 1;
        use = false;
    }

    public override string ToString()
    {
        return name;
    }
}