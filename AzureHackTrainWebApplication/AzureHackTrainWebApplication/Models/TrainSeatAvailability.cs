using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureHackTrainWebApplication.Models
{
    public class TrainSeatAvailability
    {
        public string status { get; set; }
        public TrainSeatAvailabilityResult result { get; set; }

    }
    public class Seat
    {
        public string date { get; set; }
        public string seat { get; set; }
    }

    public class TrainSeatAvailabilityResult
    {
        public string trainno { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string cls { get; set; }
        public string quota { get; set; }
        public string error { get; set; }
        public List<Seat> seats { get; set; }
    }
}
