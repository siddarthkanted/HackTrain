using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Http.Results;
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

        //http://localhost:64548/getTrainListOnStationCode?fromStationCode=cbe&toStationCode=hyb
        [Route("getTrainListOnStationCode")]
        public JsonResult<List<Train>> GetTrainListOnStationCode(string fromStationCode, string toStationCode)
        {
            //trains which are passing fromStation and ToStation
            List<Train> trainList =
                trains.Train.FindAll(
                    x => x.TrainRecord.TrainScheduleItem.Exists(y => y.StationCode.Equals(fromStationCode)) &&
                         x.TrainRecord.TrainScheduleItem.Exists(y => y.StationCode.Equals(toStationCode)));

            //fromStation DayTime should be less than  ToStation DayTime
            trainList =
                trainList.FindAll(x =>
                    (IsFromStationDayTimeLessThanToStationDayTime(
                        x.TrainRecord.TrainScheduleItem.Find(y => y.StationCode.Equals(fromStationCode)),
                        x.TrainRecord.TrainScheduleItem.Find(y => y.StationCode.Equals(toStationCode)))));

            return Json(trainList);
        }

        private bool IsFromStationDayTimeLessThanToStationDayTime(TrainScheduleItem fromStationTrainScheduleItem, TrainScheduleItem toStationTrainScheduleItem)
        {
            if (fromStationTrainScheduleItem.DepartureTime.Equals("Destination")) return false;
            if (toStationTrainScheduleItem.DepartureTime.Equals("Destination")) return true;
            if (fromStationTrainScheduleItem.Day > toStationTrainScheduleItem.Day) return false;
            if (fromStationTrainScheduleItem.Day < toStationTrainScheduleItem.Day) return true;

            TimeSpan fromStationDepatureTime = DateTime.Parse(fromStationTrainScheduleItem.DepartureTime).TimeOfDay;
            TimeSpan toStationDepatureTime = DateTime.Parse(toStationTrainScheduleItem.DepartureTime).TimeOfDay;

            return TimeSpan.Compare(fromStationDepatureTime, toStationDepatureTime) < 0;
        }

        //http://localhost:64548/getTrainOnTrainNumber?trainNumber=12647
        [Route("getTrainOnTrainNumber")]
        public JsonResult<Train> GetTrainOnTrainNumber(string trainNumber)
        {
            Train train = trains.Train.Find(x => x.TrainDetails.TrainNumber.Equals(trainNumber));
            return Json(train);
        }



        private Trains ConvertTrainsXmlToTrainsObject()
        {
            XmlSerializer trainsXmlSerializer = new XmlSerializer(typeof(Trains));
            TextReader trainsTextReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(@"~/Data/Trains_Hyd_SC_KCG.xml"));
            Trains trains = (Trains)trainsXmlSerializer.Deserialize(trainsTextReader);
            trainsTextReader.Close();
            return trains;
        }
    }
}
