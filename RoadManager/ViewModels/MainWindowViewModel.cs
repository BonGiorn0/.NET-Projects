using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using System.Diagnostics;
using System.Reactive;
using System.Text;
using ReactiveUI;
using RoadClass;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RoadManager.Views;
namespace RoadManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _RoadToAddName = "Name";
    private uint _RoadToAddTypeIndex;
    private RoadType _RoadToAddType;
    private uint _RoadToAddLength;
    private uint _RoadToAddLaneCount;
    private bool _RoadToAddHasPavement;
    private bool _RoadToAddHasLine;
    private string _MenuOpenFileText = "in.txt";

    [Required]
    public string MenuOpenFileText
    {
        get => _MenuOpenFileText;
        set => this.RaiseAndSetIfChanged(ref _MenuOpenFileText, value);
    }
    //private string _RoadToAddName;

    public uint RoadToAddTypeIndex
    {
        get => _RoadToAddTypeIndex; 
        set => this.RaiseAndSetIfChanged(ref _RoadToAddTypeIndex, value);
    }
    public uint RoadToAddLaneCount
    {
        get => _RoadToAddLaneCount; 
        set => this.RaiseAndSetIfChanged(ref _RoadToAddLaneCount, value);
    }
    public uint RoadToAddLength
    {
        get => _RoadToAddLength; 
        set => this.RaiseAndSetIfChanged(ref _RoadToAddLength, value);
    }
    [Required]
    public string RoadToAddName
    {
        get => _RoadToAddName;
        set
        {
        if(value == "")
        {
            //throw new ArgumentNullException("", "This field is required");
        }
        this.RaiseAndSetIfChanged(ref _RoadToAddName, value);
        }
    }
    public bool RoadToAddHasPavement
    {
        get => _RoadToAddHasPavement;
        set => this.RaiseAndSetIfChanged(ref _RoadToAddHasPavement, 
                                        _RoadToAddHasPavement ^= true);
    }
    public bool RoadToAddHasLine
    {
        get => _RoadToAddHasLine;
        set => this.RaiseAndSetIfChanged(ref _RoadToAddHasLine, 
                                        _RoadToAddHasLine ^= true);
    }
    
    //private bool[] ItemVisibility = new bool[10];

    public ObservableCollection<bool> ItemVisibility { get; } = new ObservableCollection<bool>(Enumerable.Repeat(false, 10));


    public bool AddRoadVisible
    {
        get => ItemVisibility[0];
        set
        { 
            for(int i = 0; i < ItemVisibility.Count; i++){
                ItemVisibility[i] = false;
            }
           ItemVisibility[0] = true; 
        }
    }

    public void AddRoadVisibleSetFalse(){
        ItemVisibility[0] = false;
    }
    public bool SecondTaskVisible
    {
        get => ItemVisibility[1];
        set
        { 
            for(int i = 0; i < ItemVisibility.Count; i++){
                ItemVisibility[i] = false;
                //this.RaiseAndSetIfChanged(ref ItemVisibility[i], false);
            }
            ItemVisibility[1] = true;
            //this.RaiseAndSetIfChanged(ref ItemVisibility[1], true);
            
        }
    }

    public bool ThirdTaskVisible
    {
        get => ItemVisibility[2];
        set
        { 
            for(int i = 0; i < ItemVisibility.Count; i++){
                ItemVisibility[i] = false;
                //this.RaiseAndSetIfChanged(ref ItemVisibility[i], false);
            }
            ItemVisibility[2] = true;
            //this.RaiseAndSetIfChanged(ref ItemVisibility[1], true);
            
        }
    }
    public bool FourthTaskVisible
    {
        get => ItemVisibility[3];
        set
        { 
            for(int i = 0; i < ItemVisibility.Count; i++){
                ItemVisibility[i] = false;
                //this.RaiseAndSetIfChanged(ref ItemVisibility[i], false);
            }
            ItemVisibility[3] = true;
            //this.RaiseAndSetIfChanged(ref ItemVisibility[1], true);
            
        }
    }
    public bool FifthTaskVisible
    {
        get => ItemVisibility[4];
        set
        { 
            for(int i = 0; i < ItemVisibility.Count; i++){
                ItemVisibility[i] = false;
                //this.RaiseAndSetIfChanged(ref ItemVisibility[i], false);
            }
            ItemVisibility[4] = true;
            //this.RaiseAndSetIfChanged(ref ItemVisibility[1], true);
            
        }
    }
    public void SetRoadToAddType()
    {
        switch(RoadToAddTypeIndex){
        case 0:
            _RoadToAddType = RoadType.National;
            return;
        case 1:
            _RoadToAddType = RoadType.Regional;
            return;
        case 2:
            _RoadToAddType = RoadType.Oblast;
            return;
        case 3:
            _RoadToAddType = RoadType.Local;
            return;
        }
    }
    public ObservableCollection<Road> Roads { get; } = new();
    public ObservableCollection<Road> RequestedRoads { get; } = new();

        
        public void EditMenuAddRoad()
        {
            AddRoadVisible = true; 
        }

        public MainWindowViewModel()
        {
            
            //Roads = new ObservableCollection<Road>(ReadRoadsFromFile("in.txt"));
        }

        public void AddRoad()
        {
            SetRoadToAddType();
            Roads.Add(new()
            {
                Name = _RoadToAddName, 
                Type = _RoadToAddType,
                Length = _RoadToAddLength,
                LaneCount = _RoadToAddLaneCount,
                HasLine = _RoadToAddHasLine,
                HasPavement = _RoadToAddHasPavement
            });
        }

        public void SortRoadsByLength()
        {
            if (Roads == null || Roads.Count <= 1)
            {
                // Return if the input list has 0 or 1 elements
                return;
            }

            // Bubble Sort implementation for sorting Roads by Length
            int n = Roads.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Swap if the current road's Length is greater than the next road's Length
                    if (Roads[j].Length > Roads[j + 1].Length)
                    {
                        Road temp = Roads[j];
                        Roads[j] = Roads[j + 1];
                        Roads[j + 1] = temp;
                    }
                }
            }
        }

        public void GetRoadsForTask2()
        {
            RequestedRoads.Clear();
            if (Roads == null || Roads.Count == 0)
            {
                // Return an empty list if the input list is null or empty
                return;
            }

            uint smallestLength = uint.MaxValue;
            uint largestLaneCount = 0;

            // Find the smallest length and largest lane count
            foreach (var road in Roads)
            {
                smallestLength = Math.Min(smallestLength, road.Length);
                largestLaneCount = Math.Max(largestLaneCount, road.LaneCount);
            }

            // Find Roads with the smallest length and largest lane count
            foreach (var road in Roads)
            {
                if (road.Length == smallestLength || road.LaneCount == largestLaneCount)
                {
                    RequestedRoads.Add(road);
                }
            }
            SecondTaskVisible = true;

        }
        public void GetRoadsForTask3()
        {
            RequestedRoads.Clear();

            if (Roads == null || Roads.Count == 0)
            {
                // Return an empty list if the input list is null or empty
                return;
            }

            // Find Roads with LaneCount > 2 and HasLine is true
            foreach (var road in Roads)
            {
                if (road.LaneCount > 2 && road.HasLine)
                {
                    RequestedRoads.Add(road);
                }
            }

            ThirdTaskVisible = true;
        }

        public void GetRoadTypeForTask4()
        {
            RequestedRoads.Clear();
            if (Roads == null || Roads.Count == 0)
            {
                // Return if the input list is null or empty
                return;
            }

            // Find type of roads with the biggest length and HasPavement is true
            Road tmp = null;
            RoadType selectedType = RoadType.National; // Assuming a default value if no matching road is found
            uint maxLength = 0;

            foreach (var road in Roads)
            {
                if (road.HasPavement && road.Length > maxLength)
                {
                    maxLength = road.Length;
                    selectedType = road.Type;
                    tmp = road;
                }
            }
            if (tmp != null)
            {
                RequestedRoads.Add(tmp);
            }

            FourthTaskVisible = true;
            // Now 'selectedType' contains the type of roads with the biggest length and HasPavement is true
            Console.WriteLine($"Type of roads with the biggest length and HasPavement is true: {selectedType}");
        }

        public void GetRoadsForTask5()
        {
            RequestedRoads.Clear();

            if (Roads == null || Roads.Count == 0)
            {
                // Return an empty list if the input list is null or empty
                return;
            }

            // Find Roads with the biggest LaneCount, HasPavement is true, and Type is Regional
            Road selectedRoad = null;

            foreach (var road in Roads)
            {
                if (road.Type == RoadType.Regional && road.HasPavement)
                {
                    if (selectedRoad == null || road.LaneCount > selectedRoad.LaneCount)
                    {
                        selectedRoad = road;
                    }
                }
            }

            if (selectedRoad != null)
            {
                RequestedRoads.Add(selectedRoad);
            }

            FifthTaskVisible = true;
        }

        public void MenuOpenFile()
        {
            Roads.Clear();
            //Roads = new ObservableCollection<Road>(ReadRoadsFromFile(MenuOpenFileText));
            ReadRoadsFromFile(Roads, MenuOpenFileText);

        }
        public void MenuResetTable()
        {
            Roads.Clear();
            //Roads = new ObservableCollection<Road>(ReadRoadsFromFile(MenuOpenFileText));
            ReadRoadsFromFile(Roads, "in.txt");
        }


        void  ReadRoadsFromFile(ObservableCollection<Road> roads, string filePath, char delimiter = ',')
        {
            //List<Road> roads = new List<Road>();

            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    // Split each line based on the delimiter
                    string[] roadProperties = line.Split(delimiter);

                    // Ensure the line has enough elements
                    if (roadProperties.Length >= 6)
                    {
                        // Parse properties and create a Road object
                        string name = roadProperties[0].Trim();
                        RoadType type = Enum.Parse<RoadType>(roadProperties[1].Trim());
                        uint length = uint.Parse(roadProperties[2].Trim());
                        uint laneCount = uint.Parse(roadProperties[3].Trim());
                        bool hasPavement = bool.Parse(roadProperties[4].Trim());
                        bool hasLine = bool.Parse(roadProperties[5].Trim());

                        Road road = new Road(name, type, length, laneCount, hasPavement, hasLine);
                        roads.Add(road);
                    }
                    else
                    {
                        // Log or handle cases where the line doesn't have enough elements
                        Console.WriteLine($"Invalid line: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, format issues, etc.)
                Console.WriteLine($"Error reading file: {ex.Message}");
                MenuOpenFileText = "Could not open file!";
                //throw new ArgumentNullException(nameof(MenuOpenFileText), ex.Message);
            }

            //return roads;
        }
}
