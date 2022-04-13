using System;
using System.Collections.Generic;

namespace Fnyo.Learn.Jenkins.Dto
{
    public class BookDto
    {

        public int Id { get; set; }


        /// <summary>
        /// 书籍名称
        /// </summary>
        public string BookName { get; set; }


        /// <summary>
        /// 出版社
        /// </summary>
        public string Press { get; set; }


        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }



        /// <summary>
        /// 出版时期
        /// </summary>
        public string Date { get; set; }



        /// <summary>
        /// 价格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// json数组 存储书籍类型
        /// </summary>
        public List<string> Types { get; set; }
    }
}
