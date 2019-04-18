using Breeze.Common.Helper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Breeze.Common.DriverWrapper
{
    ///<summary>
    ///Contain properties for web driver.
    ///</summary>
    public class DriverProperties
    {
        private DriverType driverType;
        private bool headless;
        private string downloadLocation;
        private string arguments;
        ///<summary>
        ///Load driver config from json file.
        ///</summary>
        public DriverProperties(string platform, bool headless = false, string downloadLocation = null, string arguments = null)
        {
            setDriverType(platform);
            setHeadless(headless);
            setDownloadLocation(downloadLocation);
            setArguments(arguments);
        }

        public DriverProperties()
        {
        }
        ///<summary>
        ///get driver type.
        ///</summary>
        public DriverType getDriverType()
        {
            return driverType;
        }

        ///<summary>
        ///set driver type.
        ///</summary>
        public void setDriverType(string driverType)
        {   
            switch (driverType.ToLower()) {
                case "firefox":
                    this.driverType = DriverType.Firefox;
                    break;
                case "chrome":
                    this.driverType = DriverType.Chrome;
                    break;
                case "internetexplorer":
                    this.driverType = DriverType.IE;
                    break;
            }
        }
        ///<summary>
        ///Get arguments.
        ///</summary>
        public string getArguments()
        {
            return arguments;
        }
        ///<summary>
        ///Set arguments.
        ///</summary>
        public void setArguments(string arguments)
        {
            this.arguments = arguments;
        }

        ///<summary>
        ///Check if this is headless running.
        ///</summary>
        public bool isHeadless()
        {
            return headless;
        }

        ///<summary>
        ///Set headless mode.
        ///</summary>
        public void setHeadless(bool headless)
        {
            this.headless = headless;
        }

        ///<summary>
        ///Get download location.
        ///</summary>
        public string getDownloadLocation()
        {
            return downloadLocation;
        }
        ///<summary>
        ///Set arguments.
        ///</summary>
        public void setDownloadLocation(string downloadLocation)
        {
            this.downloadLocation = downloadLocation;
        }
    }
}
