using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    //POCO Class.
    public class Flight : IPoco
    {
        public long Id { get; set; }
        public long AirLineCompany_Id { get; set; }
        public long Origin_Country_Code { get; set; }
        public long Destination_Country_Code { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Landing_Time { get; set; }
        public int Remaining_Tickets { get; set; }


        // This Function Override The Real Operator == And Check If This.Id And Other.Id Are Equals.
        static public bool operator ==(Flight me, Flight other)
        {
            if (ReferenceEquals(me, other) || ReferenceEquals(me, null) && ReferenceEquals(other, null))
                return true;
            return false;
        }

        // This Function Override The Real Operator != And Check If This.Id And Other.Id Are NOT Equals.
        static public bool operator !=(Flight me, Flight other)
        {
            return !(me == other);
        }

        // This Function Override The Real Function Equals And Compair Between This.Id And Other.Id.
        public override bool Equals(object obj)
        {
            Flight otherFlight = obj as Flight;
            return (this.Id == otherFlight.Id);
        }

        // This Function Override The Real HashCode And Return this Id.
        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Flight Id: {Id}. Airline Company Id: {AirLineCompany_Id}. Origin Country: {Origin_Country_Code}. Destination Country: {Destination_Country_Code}. Departure Time: {Departure_Time}. Landing Time: {Landing_Time}. Remaining Tickets: {Remaining_Tickets}.";
        }
    }
}
