using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudyProject.Example.Excel.Model
{
    [Description("导入模板")]
    public class InportModel
    {
        [Display(Name = "年度"), Required]
        public int Year { get; set; }
        [Display(Name = "指标名称"), Required]
        public string itemName { get; set; }
        [Display(Name = "指标编码"), Required]
        public string itemCode { get; set; }
        [Display(Name = "车辆编码"), Required]
        public string VehicleCode { get; set; }
    }
}
