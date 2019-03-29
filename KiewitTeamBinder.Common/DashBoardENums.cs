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
        public enum MainMenu
        {
            [Description("mn-setting")]
            GlobalSettings,
            [Description("mn-panels")]
            ChoosePanels,
            [Description("Administer")]
            Administer
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
            Delete,
            [Description("Data Profiles")]
            DataProfiles,
            [Description("Panels")]
            Panels
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

        public enum LinkButton
        {
            [Description("Add New")]
            AddNew,
            [Description("Delete")]
            Delete,
            [Description("Check All")]
            CheckAll,
            [Description("UnCheck All")]
            UnCheckAll,
            [Description("Edit")]
            Edit,
            [Description("Save as")]
            SaveAs
        }

        public enum RadioButton
        {
            //Panel dialog
            [Description("Chart")]
            Chart,
            [Description("Indicator")]
            Indicator,
            [Description("Report")]
            Report,
            [Description("Heat Map")]
            HeatMap,
            [Description("radPlacementNone")]
            None,
            [Description("radPlacementTop")]
            Top,
            [Description("radPlacementRight")]
            Right,
            [Description("radPlacementBottom")]
            Bottom,
            [Description("radPlacementLeft")]
            Left,
            [Description("rdoChartStyle2D")]
            Style2D,
            [Description("rdoChartStyle3D")]
            Style3D
        }

        public enum ComboBox
        {
            [Description("cbbProfile")]
            DataProfiles,
            [Description("cbbSeriesField")]
            Series,
            [Description("cbbChartType")]
            ChartType,
            [Description("cbbCategoryField")]
            Category
        }

        public enum Textbox
        {
            [Description("txtDisplayName")]
            DisplayName,
            [Description("txtChartTitle")]
            ChartTitle,
            [Description("txtCategoryXAxis")]
            Caption1,
            [Description("txtValueYAxis")]
            Caption2
        }

        public enum Checkbox
        {
            [Description("chkShowTitle")]
            ShowTitle,
            [Description("chkSeriesName")]
            Series,
            [Description("chkCategoriesName")]
            Categories,
            [Description("chkValue")]
            Values,
            [Description("chkPercentage")]
            Percentage
        }

        public enum Button
        {
            [Description("Next")]
            Next,
            [Description("Finish")]
            Finish,
            [Description("Cancel")]
            Cancel,
            [Description("Back")]
            Back,
            [Description("OK")]
            OK,
            [Description("Create new panel")]
            CreateNewPanel,
            [Description("Hide")]
            Hide
        }

    }
}
