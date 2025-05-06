using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonC
{
    public class TextJuridiqueResult
    {
        public string CaseNumber { get; set; } = string.Empty;
        public List<string> PhrasesSubjectives { get; set; }
        public float DegreDeSubjectvite { get; set; }
    }

    public class PhraseSubjectives
    {

        public string CaseNumber { get; set; }
        public string ContenuHeading { get; set; }
        public string Contenu { get; set; }
        public float IntensiteDeSubjectivite { get; set; }
    }


    public class SentenceAnalysis
    {
        public string Sentence { get; set; }
        public string Explanation { get; set; }

        [JsonProperty("confiance")]
        public double Confiance { get; set; }

        [JsonProperty("intensity")]
        public double Intensity { get; set; }
    }
}