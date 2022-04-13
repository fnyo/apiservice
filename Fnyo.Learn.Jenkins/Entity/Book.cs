using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fnyo.Learn.Jenkins.Entity
{

    [Table("tms_book")]
    public class Book
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } 


        /// <summary>
        /// 书籍名称
        /// </summary>

        [Column("book_name")]
        public string BookName { get; set; }   


        /// <summary>
        /// 出版社
        /// </summary>
        [Column("press")]
        public string Press { get;set; }


        /// <summary>
        /// 作者
        /// </summary>
        [Column("author")]
        public string Author { get; set; }



        /// <summary>
        /// 出版时期
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }  



        /// <summary>
        /// 价格
        /// </summary>
        [Column("price")]
        public double Price { get; set; }

        /// <summary>
        /// json数组 存储书籍类型
        /// </summary>
        [Column("types")]
        public string Types { get; set; }

    }
}
