using Fnyo.Learn.Jenkins.Entity;
using System;

namespace Fnyo.Learn.Jenkins.Dto
{
    public class StudentScoreDto
    {

        public int Id { get; set; }

        public string Ranking { get; set; } 

        public string StudentName { get; set; }

        public Class Class { get; set; } 


        public string ClassText  => Class.ToString();

        public Level Level { get; set; }

        public string LevelText => Level.ToString();

        public decimal Score { get; set; }

 

        public bool IsBest { get; set; }

        public string IsBestText  => IsBest ? "是" : "否";

        public ExamineType ExamineType { get; set; }

        public string ExamineTypeText => ExamineType.ToString();

        public string ImportTime { get; set; }
    }
}
