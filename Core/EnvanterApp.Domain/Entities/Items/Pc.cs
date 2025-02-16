using EnvanterApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Domain.Entities.Items
{
    public class Pc : BaseItem
    {
        public string PcName { get; set; } = null!;
        public string CoreModel { get; set; } = null!;
        public string CoreSpeed { get; set; } = null!;
        public string OperatingSystem { get; set; } = null!;
        public Language Language { get; set; } = Language.English;
        public string OfficeEdition { get; set; } = null!;
        public string Antivirus { get; set; } = null!;
        public bool CdDvdRoom { get; set; } = false;
        public string Disk1 { get; set; } = null!;
        public string Disk2 { get; set; } = null!;
        public string Disk3 { get; set; } = null!;
        public string MotherBoardModel { get; set; } = null!;
        public string DMM1Memory { get; set; } = null!;

    }
}
