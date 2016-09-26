using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZbClassLibrary
{
    public class GSWordModel
    {
        public List<SectionBidModelWord> SModel { get; set; }
        public string ZBR { get; set; }
        public string ZBDLJG { get; set; }
        public string ZBDLJGDH { get; set; }
    }

    public class SectionBidModelWord
    {
        public int Section_Id { get; set; }
        public string SectionName { get; set; }
        public List<BidRankWord> BidRankWord { get; set; }
    }
    public class BidRankWord
    {
        public int Id { get; set; }
        public string ProjectLeader { get; set; }
        public string CompanyName { get; set; }
        public bool IsAbolish { get; set; }
        public int CompanyId { get; set; }
        public string AbolishReason { get; set; }
        public string OpenScore { get; set; }
        public string DesignMoney { get; set; }
        public string CyclePromise { get; set; }
        public string QualityPromise { get; set; }
        public bool IsBond { get; set; }
        public string TimeLimit { get; set; }
        public int section_Id { get; set; }
    }
}
