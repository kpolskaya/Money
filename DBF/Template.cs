using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBF
{
    /// <summary>
    /// Структура для фильтрации записей по условиям
    /// </summary>
    public struct Template
    {
        #region Автосвойства

        /// <summary>
        /// Начальная дата создания
        /// </summary>
        public DateTime CrFromDate { get; }

        /// <summary>
        /// Конечная дата создания
        /// </summary>
        public DateTime CrEndDate { get; }

        /// <summary>
        /// Начальная дата операции
        /// </summary>
        public DateTime FromDate { get; }

        /// <summary>
        /// Конечная дата операции
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// Вид операции
        /// </summary>
        public sbyte WhatType { get; }

        /// <summary>
        /// Счет
        /// </summary>
        public string WhatAcc { get; }

        /// <summary>
        /// Категория
        /// </summary>
        public string WhatCat { get; }
        #endregion


        /// <summary>
        /// Конструктор фильтра
        /// </summary>
        /// <param name="FromDate">От даты</param>
        /// <param name="EndDate">До даты</param>
        /// <param name="WhatType">Вид операции (расход: -1 / доход: 1 / все: 0</param>
        /// <param name="WhatAcc">По какому счету (все - пустая строка)</param>
        /// <param name="WhatCat">Какая категория дохода/расхода (все - пустая строка)</param>
        public Template(DateTime CrFromDate, DateTime CrEndDate, DateTime FromDate, DateTime EndDate, sbyte WhatType, string WhatAcc, string WhatCat)
        {

            this.CrFromDate = CrFromDate;
            this.CrEndDate = CrEndDate;
            this.FromDate = FromDate;
            this.EndDate = EndDate;
            this.WhatType = WhatType;
            this.WhatAcc = WhatAcc;
            this.WhatCat = WhatCat;
        }

        /// <summary>
        /// Конструктор фильтра записей, сделанных в определенные даты
        /// </summary>
        /// <param name="CrFromDate">Начальная дата создания записи</param>
        /// <param name="CrEndDate">Конечная дата создания записей</param>
        public Template(DateTime CrFromDate, DateTime CrEndDate)
        {

            this.CrFromDate = CrFromDate;
            this.CrEndDate = CrEndDate;
            this.FromDate = DateTime.MinValue;
            this.EndDate = DateTime.MaxValue;
            this.WhatType = (sbyte)0;
            this.WhatAcc = "";
            this.WhatCat = "";
        }

        /// <summary>
        /// Конструктор фильтра записей без учета даты создания
        /// </summary>
        /// <param name="FromDate">Начальная дата операции</param>
        /// <param name="EndDate">Конечная дата операции</param>
        /// <param name="WhatType">Тип операции (-1 - расход, 1 - доход, 0 - все)</param>
        /// <param name="WhatAcc">Счет операции (пустая строка - все)</param>
        /// <param name="WhatCat">Категория расхода/дохода (пустая строка - все)</param>
        public Template(DateTime FromDate, DateTime EndDate, sbyte WhatType, string WhatAcc, string WhatCat)
        {

            this.CrFromDate = DateTime.MinValue;
            this.CrEndDate = DateTime.MinValue;
            this.FromDate = FromDate;
            this.EndDate = EndDate;
            this.WhatType = WhatType;
            this.WhatAcc = WhatAcc;
            this.WhatCat = WhatCat;
        }

    }
}
