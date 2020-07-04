using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    struct Record
    {

        /// <summary>
        /// Поле "Дата операции"
        /// </summary>
        DateTime opDate;

        /// <summary>
        /// Поле "Доход/Расход".
        /// Значение "1" соответствует доходу, "-1" - расходу
        /// </summary>
        byte opType;

        /// <summary>
        /// Поле "Сумма"
        /// </summary>
        double opSum;

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
        /// <param name="RecNumber">Номер записи</param>>
        /// <param name="OpDate">Дата операции</param>
        /// <param name="IsArrival">true - Доход / false - Расход</param>
        /// <param name="OpSum">Сумма</param>
        /// <param name="Account">Счет</param>
        /// <param name="Category">Вид дохода/расхода</param>
        /// <param name="Note">Примечание</param>
        public Record(int RecNumber, DateTime OpDate, bool IsArrival, double OpSum, string Account, string Category, string Note)
        {
            this.RecNumber = RecNumber;
            this.CrDate = DateTime.Now;
            this.opDate = OpDate;
            this.opType = (byte)(IsArrival ? 1 : -1);
            this.opSum = OpSum;
            this.account = Account;
            this.category = Category;
            this.note = Note;
        }

        /// <summary>
        /// Номер записи
        /// </summary>
        public int RecNumber { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CrDate { get; }

        /// <summary>
        /// Дата операции
        /// </summary>
        public DateTime OpDate { get { return opDate; } set { this.opDate = value; } }

        /// <summary>
        /// Тип операции (доход/расход)
        /// </summary>
        public byte OpType { get { return opType; } set { this.opType = value; } }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public double OpSum { get { return opSum; } set { this.opSum = value; } }

        /// <summary>
        /// Сумма операции со знаком +/- в соответствии с направлением движения средств
        /// </summary>
        public double Sum
        {
            get
            { return opType * opSum; }
        }

        /// <summary>
        /// Счет поступления/выбытия средств
        /// </summary>
        public string Account { get { return account; } set { this.account = value; } }

        /// <summary>
        /// Вид дохода/расхода
        /// </summary>
        public string Category { get { return category; } set { this.category = value; } }


        public string Note { get { return note; } set { this.note = value; } }

    }
}
