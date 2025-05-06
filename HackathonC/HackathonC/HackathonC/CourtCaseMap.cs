using CsvHelper.Configuration;
using HackathonC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class CourtCaseMap : ClassMap<CourtCase>
{
    public CourtCaseMap()
    {
        Map(m => m.CaseNumber).Name("case_number");
        Map(m => m.SearchCaseNumber).Name("search_case_number");
        Map(m => m.Date).Name("date");
        Map(m => m.CaseTitle).Name("case_title");
        Map(m => m.Text).Name("text");
    }
}