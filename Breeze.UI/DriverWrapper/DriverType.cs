using System.ComponentModel;

namespace Breeze.UI.DriverWrapper
{
    ///<summary>
    ///Type of Web driver
    ///</summary>
    public enum DriverType {
        [Description("chrome")]
        Chrome,
        [Description("firefox")]
        Firefox,
        [Description("internetexplorer")]
        IE
    }   

}
