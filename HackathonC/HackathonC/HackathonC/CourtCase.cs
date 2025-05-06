using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonC
{
    public class CourtCase
    {
        public string CaseNumber { get; set; } = string.Empty;
        public string SearchCaseNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}