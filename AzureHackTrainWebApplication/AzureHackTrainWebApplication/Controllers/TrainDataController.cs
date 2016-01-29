using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Xml.Serialization;
using AzureHackTrainWebApplication.Models;

namespace AzureHackTrainWebApplication.Controllers
{
    public class TrainDataController : ApiController
    {

        private Trains trains;

        public TrainDataController()
        {
            trains = ConvertTrainsXmlToTrainsObject();
        }

        //http://localhost:64548/getTrainListOnStationCode?fromStationCode=be&toStationCode=mb
        [Route("getTrainListOnStationCode")]
        public List<Train> GetTrainListOnStationCode(string fromStationCode, string toStationCode)
        {
            //trains which are passing fromStation and ToStation
            List<Train> trainList =
                trains.Train.FindAll(
                    x => x.TrainRecord.TrainScheduleItem.Exists(y => y.StationCode.Equals(fromStationCode)) &&
                         x.TrainRecord.TrainScheduleItem.Exists(y => y.StationCode.Equals(toStationCode)));

            //fromStation day should be less than  ToStation day
            trainList =
                trainList.FindAll(
                    x =>
                        (x.TrainRecord.TrainScheduleItem.Find(y => y.StationCode.Equals(fromStationCode)).Day <=
                         x.TrainRecord.TrainScheduleItem.Find(y => y.StationCode.Equals(toStationCode)).Day));


            //fromStation time should be less than  ToStation time
            trainList =
                trainList.FindAll(x =>
                    (IsFromStationTimeLessThanToStationTime(
                        x.TrainRecord.TrainScheduleItem.Find(y => y.StationCode.Equals(fromStationCode))
                            .DepartureTime,
                        x.TrainRecord.TrainScheduleItem.Find(y => y.StationCode.Equals(toStationCode))
                            .DepartureTime))
                    );

            return trainList;
        }

        private bool IsFromStationTimeLessThanToStationTime(string fromStationDepatureTimeString, string toStationDepatureTimeString)
        {
            if (fromStationDepatureTimeString.Equals("Destination")) return false;
            if (toStationDepatureTimeString.Equals("Destination")) return true;

            TimeSpan fromStationDepatureTime = DateTime.Parse(fromStationDepatureTimeString).TimeOfDay;
            TimeSpan toStationDepatureTime = DateTime.Parse(toStationDepatureTimeString).TimeOfDay;

            return TimeSpan.Compare(fromStationDepatureTime, toStationDepatureTime) < 0;
        }

        //http://localhost:64548/getTrainOnTrainNumber?trainNumber=1234
        [Route("getTrainOnTrainNumber")]
        public Train GetTrainOnTrainNumber(string trainNumber)
        {
            Train train = trains.Train.Find(x => x.TrainDetails.TrainNumber.Equals(trainNumber));
            return train;
        }



        private Trains ConvertTrainsXmlToTrainsObject()
        {
            XmlSerializer trainsXmlSerializer = new XmlSerializer(typeof(Trains));
            TextReader trainsTextReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(@"~/Data/TrainData.xml"));
            Trains trains = (Trains)trainsXmlSerializer.Deserialize(trainsTextReader);
            trainsTextReader.Close();
            return trains;
        }
    }
}
