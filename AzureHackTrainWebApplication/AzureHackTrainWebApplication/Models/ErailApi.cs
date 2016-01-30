using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using Newtonsoft.Json;

namespace AzureHackTrainWebApplication.Models
{
    public class ErailApi
    {
        private List<Train> trainList;
        private string fromStationCode;
        private string toStationCode;

        public ErailApi(List<Train> trainList, string fromStationCode, string toStationCode)
        {
            this.trainList = trainList;
            this.fromStationCode = fromStationCode;
            this.toStationCode = toStationCode;
        }

        public void AddTrainFare_SeatAvailability()
        {
            foreach (var Train in trainList)
            {
                AddTrainFare(Train);
                AddSeatAvailability(Train);
            }
        }

        //call http://api.erail.in/fare/?key=API_KEY&trainno=12138&stnfrom=NDLS&stnto=CSTM&age=AD&quota=GN&date=05-SEP-2014      
        private void AddTrainFare(Train train)
        {
            string url = "http://api.erail.in/fare/";
            var nameValueCollection = new NameValueCollection();
            CommonNameValueCollection(nameValueCollection, train);
            nameValueCollection["age"] = "AD";
            //string response = CallErailApi(url, nameValueCollection);
            string response =
                "{ 'status' : 'OK', 'result' : { 'trainno' : '12952', 'type' : 'RAJDHANI', 'from' : 'NDLS', 'to' : 'BCT', 'age' : '30', 'quota' : 'GN', 'fare' : [ { 'cls' : '1A', 'fare' : '4680' }, { 'cls' : '2A', 'fare' : '2810' }, { 'cls' : '3A', 'fare' : '2030' } ] } }";
                train.TrainFare = JsonConvert.DeserializeObject<TrainFare>(response);
        }

        //call http://api.erail.in/seats/?key=API_KEY&trainno=12138&stnfrom=NDLS&stnto=CSTM&quota=GN&class=SL&date=05-SEP-2014
        private void AddSeatAvailability(Train train)
        {
            string url = "http://api.erail.in/seats/";
            var nameValueCollection = new NameValueCollection();
            CommonNameValueCollection(nameValueCollection, train);
            nameValueCollection["class"] = "SL";
            //string response = CallErailApi(url, nameValueCollection);
            string response =
                "{ 'status' : 'OK', 'result' : { 'trainno' : '12138', 'from' : 'NDLS', 'to' : 'BCT', 'cls' : 'SL', 'quota' : 'GN', 'error' : '', 'seats' : [ { 'date' : '10-Sep-14', 'seat' : 'TRAIN DEPARTED' }, { 'date' : '11-Sep-14', 'seat' : 'GNWL91/WL22' }, ] } }";
            train.TrainSeatAvailability = JsonConvert.DeserializeObject<TrainSeatAvailability>(response);
        }

        private void CommonNameValueCollection(NameValueCollection nameValueCollection, Train train)
        {
            nameValueCollection["key"] = "ojxcd2635";
            nameValueCollection["trainno"] = train.TrainRecord.TrainNumber;
            nameValueCollection["stnfrom"] = fromStationCode;
            nameValueCollection["stnto"] = toStationCode;
            nameValueCollection["quota"] = "GN";
            nameValueCollection["date"] = GetNextTrainRunningDate(train);
        }

        //format dd-MMM-yyyy (ex :05-SEP-2014)
        //if train runs on every monday. Get the date of coming monday.
        private string GetNextTrainRunningDate(Train train)
        {
            DateTime today = DateTime.Today;
            int dayOfWeek = (int) today.DayOfWeek + 1;
            while (true)
            {
                DateTime nextDay = today.AddDays(dayOfWeek);
                if (train.TrainDetails.TrainAttributes.DaysRunning.Day.Contains(nextDay.DayOfWeek.ToString()))
                {
                    break;
                }
                dayOfWeek++;
            }

            DateTime nextTrainRunningDate = today.AddDays(dayOfWeek);
            return nextTrainRunningDate.ToString("dd-MMM-yyyy");
        }


        private string CallErailApi(string url, NameValueCollection nameValueCollection)
        {
            string response = MemoryCache.Default.Get(url) as string;
            if (!String.IsNullOrEmpty(response))
            {
                return response;
            }
            using (var client = new WebClient())
            {
                var responseByte = client.UploadValues(url, nameValueCollection);
                string responseString = Encoding.Default.GetString(responseByte);
                return responseString;
            } 
        }
    }
}
