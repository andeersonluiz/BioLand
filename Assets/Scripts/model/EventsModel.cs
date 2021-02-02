
public class Events
{
    private int _day;
    private Bacteria[] _bacterias;
    private Contaminant _contaminant;
    private VariablesEnviroment _variables;
    private Action _actionEffect;
    private int _score;

    public Events(int day, Bacteria[] bacterias, Contaminant contaminant, VariablesEnviroment variables, Action actionEffect, int score)
    {
        this.day = day;
        this.bacterias = bacterias;
        this.contaminant = contaminant;
        this.variables = variables;
        this.actionEffect = actionEffect;
        this.score = score;
    }

    public int day { get; set; }
    public Bacteria[] bacterias { get; set; }
    public Contaminant contaminant { get; set; }
    public VariablesEnviroment variables { get; set; }
    public Action actionEffect { get; set; }
    public int score { get; set; }
}