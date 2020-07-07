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
        /// <summary>
        /// Начальная дата
        /// </summary>
        public DateTime FromDate { get; }

        /// <summary>
        /// Конечная дата
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

        /// <summary>
        /// Конструктор фильтра
        /// </summary>
        /// <param name="FromDate">От даты</param>
        /// <param name="EndDate">До даты</param>
        /// <param name="WhatType">Вид операции (расход: -1 / доход: 1</param>
        /// <param name="WhatAcc">По какому счету</param>
        /// <param name="WhatCat">Какая категория дохода/расхода</param>
        public Template(DateTime FromDate, DateTime EndDate, sbyte WhatType, string WhatAcc, string WhatCat)
        {
            this.FromDate = FromDate;
            this.EndDate = EndDate;
            this.WhatType = WhatType;
            this.WhatAcc = WhatAcc;
            this.WhatCat = WhatCat;
        }

    }
}
