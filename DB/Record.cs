using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    struct Record
    {
        /// <summary>
        /// Поле "Номер записи"
        /// </summary>
        int recNumber;

        /// <summary>
        /// Поле "Дата создания записи"
        /// </summary>
        DateTime crDate;

        /// <summary>
        /// Поле "Дата операции"
        /// </summary>
        DateTime opDate;

        /// <summary>
        /// Поле "Доход/Расход".
        /// Значение "1" соответствует доходу, "-1" - расходу
        /// </summary>
        byte type;

        /// <summary>
        /// Поле "Сумма"
        /// </summary>
        double sum;

        /// <summary>
        /// Поле "Счет"
        /// </summary>
        string account;

        /// <summary>
        /// Поле "От кого/за что"
        /// </summary>
        string category;

        /// <summary>
        /// Поле "Примечание"
        /// </summary>
        string note;

        
        /// <summary>
        /// Создает новую запись
        /// </summary>
        /// <param name="OpDate">Дата операции</param>
        /// <param name="IsArrival">true - Доход / false - Расход</param>
        /// <param name="Sum">Сумма</param>
        /// <param name="Account">Счет</param>
        /// <param name="Category">Вид дохода/расхода</param>
        /// <param name="Note">Примечание</param>
        public Record(DateTime OpDate, bool IsArrival, double Sum, string Account, string Category, string Note)
        {
            this.recNumber = 0;
            this.crDate = DateTime.Now;
            this.opDate = OpDate;
            this.type = (byte)(IsArrival ? 1 : -1);
            this.sum = Sum;
            this.account = Account;
            this.category = Category;
            this.note = Note;
        }
    }
}
