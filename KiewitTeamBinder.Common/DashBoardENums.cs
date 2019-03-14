using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common
{
    public class DashBoardENums
    {
        public enum SymbolMenu
        {
            [Description("mn-setting")]
            GlobalSettings,
            [Description("mn-panels")]
            ChoosePanels
        }

        public enum SubMenu
        {
            [Description("Add Page")]
            AddPage,
            [Description("Create Profile")]
            CreateProfile,
            [Description("Create Panel")]
            CreatePanel,
            [Description("Edit")]
            Edit,
            [Description("Delete")]
            Delete
        }

        public enum Dialog
        {
            [Description("New Page")]
            AddNewPage,
            [Description("Edit Page")]
            EditPage,
            [Description("Add New Panel")]
            AddNewPanel,
            [Description("Edit Panel")]
            EditPanel
        }

        public enum CheckValue
        {
            [Description("Default")]
            Default,
            [Description("Yes")]
            Yes,
            [Description("No")]
            No
        }
    }
}
