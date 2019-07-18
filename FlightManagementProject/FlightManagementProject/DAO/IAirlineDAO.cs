using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    public interface IAirlineDAO : IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUserame(string userName);
        IList<AirlineCompany> GetAllAirlinesByCountry(int countryId);
        void ChangePassword(AirlineCompany airline);
    }
}
