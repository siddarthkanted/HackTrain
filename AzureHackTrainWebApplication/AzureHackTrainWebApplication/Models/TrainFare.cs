using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AzureHackTrainWebApplication.Models
{
    public class TrainFare
    {
        public string status { get; set; }
        public TrainFareResult result { get; set; }
    }

    public class Fare
    {
        public string cls { get; set; }
        public string fare { get; set; }
    }


    public class TrainFareResult
    {
        public string trainno { get; set; }
        public string type { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string age { get; set; }
        public string quota { get; set; }
        public List<Fare> fare { get; set; }
    }
}
