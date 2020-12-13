
public class AStarGridElement : GridElement
{
    public float HCost { get; set; }
    public float GCost { get; set; }
    public float FCost
    {
        get
        {
            return HCost + GCost;
        }
    }
    public AStarGridElement Parent { get; set; }

    public AStarGridElement(GridElement element) : base(element) { }
}