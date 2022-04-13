using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fnyo.Learn.Jenkins.Entity
{
    [Table("tms_score")]
    public class StudentScore
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("student_name")]
        public string StudentName { get; set; }
        [Column("class")]
        public Class Class { get; set; }
        [Column("level")]
        public Level Level { get; set; }

        [Column("score")]
        public decimal Score { get; set; }

        [Column("is_best")]
        public bool IsBest { get; set; }
        [Column("ranking")]
        public int Ranking { get; set; }
        /// <summary>
        /// 考试类型
        /// </summary>
        [Column("examine_type")]
        public ExamineType ExamineType { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        [Column("import_time")]
        public DateTime ImportTime { get; set; }
    }


    public enum Class
    {
        语文 = 1,
        数学 = 2,
        英语 = 3
    }

    public enum Level
    {
        优秀 = 1,
        良好 = 2,
        不及格 = 3
    }
    public enum ExamineType
    {
        期中考试 =1,
        期末考试 = 2
    }
}
