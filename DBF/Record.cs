using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBF
{   /// <summary>
    /// Структура записи в базе данных
    /// </summary>
    public struct Record
    {
        #region Поля

        /// <summary>
        /// Поле "Дата операции"
        /// </summary>
        DateTime opDate;

        /// <summary>
        /// Поле "Доход/Расход".
        /// Значение "1" соответствует доходу, "-1" - расходу
        /// </summary>
        sbyte opType;

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
        #endregion

        #region Свойства

        /// <summary>
        /// Номер записи
        /// </summary>
        public int RecNumber { get; set; }

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
        public sbyte OpType { get { return opType; } set { this.opType = value; } }

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

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get { return note; } set { this.note = value; } }

        /// <summary>
        /// Пометка на удаление
        /// </summary>
        public bool Deleted { get; set; }
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="RecNumber">Номер записи</param>>
        /// <param name="OpDate">Дата операции</param>
        /// <param name="OpType">1 - Доход / -1 - Расход </param>
        /// <param name="OpSum">Сумма</param>
        /// <param name="Account">Счет</param>
        /// <param name="Category">Вид дохода/расхода</param>
        /// <param name="Note">Примечание</param>
        public Record(DateTime CrDate, DateTime OpDate, sbyte OpType, double OpSum, string Account, string Category, string Note)
        {
            this.RecNumber = 0;
            this.CrDate = CrDate;
            this.opDate = OpDate;
            this.opType = OpType;
            this.opSum = OpSum;
            this.account = Account;
            this.category = Category;
            this.note = Note;
            this.Deleted = false;
        }

        /// <summary>
        /// Конструктор с фиксацией текущего времени как даты создания записи
        /// </summary>
        /// <param name="OpDate">Дата операции</param>
        /// <param name="OpType">1 - Доход / -1 - Расход</param>
        /// <param name="OpSum">Сумма</param>
        /// <param name="Account">Счет</param>
        /// <param name="Category">Вид дохода/расхода</param>
        /// <param name="Note">Примечание</param>
        public Record(DateTime OpDate, sbyte OpType, double OpSum, string Account, string Category, string Note) :
            this(DateTime.Now, OpDate, OpType, OpSum, Account, Category, Note)
        {
            this.RecNumber = 0;                 // нужны ли эти присваивания тут?
            this.Deleted = false;
        }


    }
}
