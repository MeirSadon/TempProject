using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    //POCO Class With Login Token.
    public class AirlineCompany : IPoco,IUser
    {
        public long Id { get; set; }
        public string Airline_Name { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public long Country_Code { get; set; }

        // This Function Override The Real Operator == And Check If This.Id And Other.Id Are Equals.
        static public bool operator ==(AirlineCompany me, AirlineCompany other)
        {
            if (ReferenceEquals(me, other) || ReferenceEquals(me, null) && ReferenceEquals(other, null))
                return true;
            return false;
        }

        // This Function Override The Real Operator != And Check If This.Id And Other.Id Are NOT Equals.
        static public bool operator !=(AirlineCompany me, AirlineCompany other)
        {
            return !(me == other);
        }

        // This Function Override The Real Function Equals And Compair Between This.Id And Other.Id.
        public override bool Equals(object obj)
        {
            AirlineCompany otherAirline = obj as AirlineCompany;
            return (this.Id == otherAirline.Id);
        }

        // This Function Override The Real HashCode And Return this Id.
        public override int GetHashCode()
        {
            return (int)this.Id;
        }
    }
}
