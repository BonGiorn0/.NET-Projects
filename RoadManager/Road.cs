namespace RoadClass
{
    public enum RoadType
    {
        National,
        Regional,
        Oblast,
        Local
    
    }
    public class Road
    {
        public string? Name{get; set;} = "Noname";

        public RoadType Type{get; set;} = RoadType.Local;
        public uint Length{get; set;}  = 1;
        public uint LaneCount{get; set;} = 1;
        public bool HasPavement{get; set;} = false;
        public bool HasLine{get; set;} = false;

        public Road(string name, RoadType type, uint length, uint laneCount, bool hasPavement, bool hasLine){
            Name = name;
            Type = type;
            Length = length;
            LaneCount = laneCount;
            HasPavement = hasPavement;
            HasLine = hasLine;
        }
        public Road(Road other){
            Name = other.Name;
            Type = other.Type;
            Length = other.Length;
            LaneCount = other.LaneCount;
            HasPavement = other.HasPavement;
            HasLine = other.HasLine;
        }


        public Road(){}
    }
}