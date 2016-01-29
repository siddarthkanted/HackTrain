using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AzureHackTrainWebApplication.Models
{
    [XmlRoot(ElementName = "TrainScheduleItem")]
    public class TrainScheduleItem
    {
        [XmlElement(ElementName = "serialNumber")]
        public string SerialNumber { get; set; }
        [XmlElement(ElementName = "stationCode")]
        public string StationCode { get; set; }
        [XmlElement(ElementName = "stationName")]
        public string StationName { get; set; }
        [XmlElement(ElementName = "arrivalTime")]
        public string ArrivalTime { get; set; }
        [XmlElement(ElementName = "departureTime")]
        public string DepartureTime { get; set; }
        [XmlElement(ElementName = "day")]
        public int Day { get; set; }
        [XmlElement(ElementName = "halt")]
        public string Halt { get; set; }
        [XmlElement(ElementName = "distance")]
        public string Distance { get; set; }
        [XmlElement(ElementName = "latitude")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "longitude")]
        public string Longitude { get; set; }
        [XmlElement(ElementName = "isHalting")]
        public string IsHalting { get; set; }
    }

    [XmlRoot(ElementName = "TrainRecord")]
    public class TrainRecord
    {
        [XmlElement(ElementName = "Provider")]
        public string Provider { get; set; }
        [XmlElement(ElementName = "TrainName")]
        public string TrainName { get; set; }
        [XmlElement(ElementName = "TrainNumber")]
        public string TrainNumber { get; set; }
        [XmlElement(ElementName = "TrainScheduleItem")]
        public List<TrainScheduleItem> TrainScheduleItem { get; set; }
    }

    [XmlRoot(ElementName = "DaysRunning")]
    public class DaysRunning
    {
        [XmlElement(ElementName = "Day")]
        public List<string> Day { get; set; }
    }

    [XmlRoot(ElementName = "TrainAttributes")]
    public class TrainAttributes
    {
        [XmlElement(ElementName = "isPantryAvailable")]
        public string IsPantryAvailable { get; set; }
        [XmlElement(ElementName = "isCateringAvailable")]
        public string IsCateringAvailable { get; set; }
        [XmlElement(ElementName = "hasPrepaidTaxiService")]
        public string HasPrepaidTaxiService { get; set; }
        [XmlElement(ElementName = "hasPrepaidAutoService")]
        public string HasPrepaidAutoService { get; set; }
        [XmlElement(ElementName = "DaysRunning")]
        public DaysRunning DaysRunning { get; set; }
        [XmlElement(ElementName = "trainType")]
        public string TrainType { get; set; }
        [XmlElement(ElementName = "fareType")]
        public string FareType { get; set; }
        [XmlElement(ElementName = "pairTrainNumber")]
        public string PairTrainNumber { get; set; }
        [XmlElement(ElementName = "pairTrainName")]
        public string PairTrainName { get; set; }
        [XmlElement(ElementName = "ClassesAvailable")]
        public ClassesAvailable ClassesAvailable { get; set; }
        [XmlElement(ElementName = "RakeShareTrainNumbers")]
        public RakeShareTrainNumbers RakeShareTrainNumbers { get; set; }
        [XmlElement(ElementName = "CoachComposition")]
        public CoachComposition CoachComposition { get; set; }
    }

    [XmlRoot(ElementName = "TrainDetails")]
    public class TrainDetails
    {
        [XmlElement(ElementName = "Provider")]
        public string Provider { get; set; }
        [XmlElement(ElementName = "TrainName")]
        public string TrainName { get; set; }
        [XmlElement(ElementName = "TrainNumber")]
        public string TrainNumber { get; set; }
        [XmlElement(ElementName = "TrainAttributes")]
        public TrainAttributes TrainAttributes { get; set; }
    }

    [XmlRoot(ElementName = "Train")]
    public class Train
    {
        [XmlElement(ElementName = "TrainRecord")]
        public TrainRecord TrainRecord { get; set; }
        [XmlElement(ElementName = "TrainDetails")]
        public TrainDetails TrainDetails { get; set; }
    }

    [XmlRoot(ElementName = "ClassesAvailable")]
    public class ClassesAvailable
    {
        [XmlElement(ElementName = "Class")]
        public List<string> Class { get; set; }
    }

    [XmlRoot(ElementName = "RakeShareTrainNumbers")]
    public class RakeShareTrainNumbers
    {
        [XmlElement(ElementName = "RakeShareTrainNumber")]
        public List<string> RakeShareTrainNumber { get; set; }
    }

    [XmlRoot(ElementName = "CoachAttributes")]
    public class CoachAttributes
    {
        [XmlElement(ElementName = "CarNumber")]
        public string CarNumber { get; set; }
        [XmlElement(ElementName = "CarType")]
        public string CarType { get; set; }
        [XmlElement(ElementName = "CarName")]
        public string CarName { get; set; }
    }

    [XmlRoot(ElementName = "CoachComposition")]
    public class CoachComposition
    {
        [XmlElement(ElementName = "CoachAttributes")]
        public List<CoachAttributes> CoachAttributes { get; set; }
    }

    [XmlRoot(ElementName = "Trains")]
    public class Trains
    {
        [XmlElement(ElementName = "Train")]
        public List<Train> Train { get; set; }
    }
}
