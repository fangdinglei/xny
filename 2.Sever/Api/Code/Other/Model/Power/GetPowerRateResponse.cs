using System.Collections.Generic; 

namespace XNYAPI.Response.Power
{
    public class PowerRate {
        public uint DeviceID;
        public float Rate;

        public PowerRate(uint deviceID, float rate)
        {
            DeviceID = deviceID;
            Rate = rate;
        }
    }
    public class GetPowerRateResponse :  DataListResponse<PowerRate>
    {
        public GetPowerRateResponse(List<PowerRate> pr):base(pr)
        {

        }
    }
}