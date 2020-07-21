using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBF
{

    public struct Repository
    {
        #region Поля
        /// <summary>
        /// Массив для хранения всех данных в памяти
        /// </summary>
        private Record[] records;                   

        /// <summary>
        /// Путь к базе данных на диске
        /// </summary>
        private string dbPath;                      

        /// <summary>
        /// Индекс текущего элемента для добавления в records
        /// </summary>
        private int index;                          

        /// <summary>
        /// Заголовки таблицы записей
        /// </summary>
        private string[] titles;

        /// <summary>
        /// Остаток денежных средств по всем счетам
        /// </summary>
        private double balance;

        #endregion

        #region Свойства
        /// <summary>
        /// Путь к файлу с данными
        /// </summary>
        public string DbPath { get { return this.dbPath; } }

        /// <summary>
        /// Количество записей в базе
        /// </summary>
        public int Count { get { return this.index; } }
        
       
        /// <summary>
        /// Время последнего сохранения базы данных в файл
        /// </summary>
        public DateTime LastSavingTime { get; set; }

        /// <summary>
        /// Остаток денежных средств по всем счетам
        /// </summary>
        public double Balance { get { return this.balance; } }

        #endregion


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="DbPath">Путь к файлу базы данных</param>
        public Repository(string DbPath)
        {
            this.LastSavingTime = DateTime.Now;     // когда в последний раз были сохранены данные
            this.dbPath = DbPath;                   // получение пути к файлу с данными
            this.index = 0;                         // первый элемент базы имеет нулевой индекс
            this.balance = 0;                       // начальный баланс
            this.titles = new string[8]             // задание строк заголовка
            {
                                "No",
                                "Дата записи",
                                "Дата операции",
                                "Вид операции",
                                "Сумма операции",
                                "Счет",
                                "Категория",
                                "Примечание"
            };
            this.records = new Record[1];           // длина массива при инициализации - 1 запись 

            Load();                                 // сразу же при создании происходит загрузка данных из файла
        }


        #region Частные Методы

        /// <summary>
        /// Увеличивает длину текущего массива записей в два раза
        /// </summary>
        private void Resize()
        {
            Array.Resize(ref this.records, this.records.Length * 2);
        }

        /// <summary>
        ///  Загружает в хранилище все записи из файла по умолчанию
        /// </summary>
        private void Load()
        {
           Load(this.dbPath, new Template(DateTime.MinValue, DateTime.MaxValue, (sbyte)0));
        }


        /// <summary>
        /// Проверяет запись с указанным номером на соответствие заданным условиям
        /// </summary>
        /// <param name="num">номер записи</param>
        /// <param name="filter">шаблон с условиями</param>
        /// <returns>true - если запись соответствует,  false - если нет, или такая запись отстутствует</returns>
        private bool Match(int num, Template filter)
        {
            return Match(this.records[num], filter);
        }

        #endregion
        /// <summary>
        /// Проверяет запись на соответствие заданным условиям
        /// </summary>
        /// <param name="record">запись</param>
        /// <param name="filter">шаблон с условиями</param>
        /// <returns></returns>
        private bool Match(Record record, Template filter)
        {
            if (record.Deleted) return false;

            bool crDateOK = record.CrDate >= filter.CrFromDate && record.CrDate <= filter.CrEndDate;
            bool dateOK = record.OpDate >= filter.FromDate && record.OpDate <= filter.EndDate;
            bool typeOK = filter.WhatType == 0 || record.OpType == filter.WhatType;
            bool accOK = filter.WhatAcc == "" || record.Account == filter.WhatAcc;
            bool catOK = filter.WhatCat == "" || record.Category == filter.WhatCat;

            return crDateOK && dateOK && typeOK && accOK && catOK;
        }


        #region Публичные Методы

        /// <summary>
        /// Загружает из файла в хранилище все записи, соответствующие заданным условиям
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void Load(string path, Template filter)
        {
            if (!File.Exists(path)) return;

            using (StreamReader dbStream = new StreamReader(path))
            {
               
                dbStream.ReadLine();  // пропускаем заголовки

                while (!dbStream.EndOfStream)
                {
                    string[] args = dbStream.ReadLine().Split(';');

                    Record temp = new Record(DateTime.Parse(args[1]), DateTime.Parse(args[2]), Convert.ToSByte(args[3]), Convert.ToDouble(args[4]), args[5], args[6], args[7]);

                    if (Match(temp, filter))
                        Add(temp); 
                } 
            }

        }


        /// <summary>
        /// Сохраняет в файл записи, которые не помечены для удаления и сответствуют заданному условию
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void Save(string path, Template filter)
        {
            if (File.Exists(path))
                File.Delete(path);
            string temp = String.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                                        this.titles[0],
                                        this.titles[1],
                                        this.titles[2],
                                        this.titles[3],
                                        this.titles[4],
                                        this.titles[5],
                                        this.titles[6],
                                        this.titles[7]);

            File.WriteAllText(path, $"{temp}\n");
            for (int i = 0; i < index; i++)
            {
                if (!this.records[i].Deleted && Match(i, filter))
                {
                    temp = String.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                                            this.records[i].RecNumber,
                                            this.records[i].CrDate,
                                            this.records[i].OpDate,
                                            this.records[i].OpType,
                                            this.records[i].OpSum,
                                            this.records[i].Account,
                                            this.records[i].Category,
                                            this.records[i].Note);
                    File.AppendAllText(path, $"{temp}\n");
                    this.LastSavingTime = DateTime.Now;
                }    
            }
        }

        /// <summary>
        /// Сохраняет в файл по умолчанию все записи, не помеченные как удаленные
        /// </summary>
        public void Save()
        {
            Save(this.dbPath, new Template(DateTime.MinValue, DateTime.MaxValue, (sbyte)0));
        }
        
        /// <summary>
        /// Помечает запись как удаленную из базы
        /// </summary>
        /// <param name="num">номер записи</param>
        public void Delete(int num)
        {
            if (num < index && num >= 0)
                this.records[num].Deleted = true;
            this.balance -= this.records[num].Sum; 
        }

        /// <summary>
        /// Создает из содержания записи массив строк
        /// </summary>
        /// <param name="num">номер записи</param>
        /// <returns>Массив строк или  null, если такой записи не существует</returns>
        public string[] ToText(int num)
        {
            
            
            if (num < 0 || num >= this.index) return null;
            if (this.records[num].Deleted) return null;
            
            string[] recordText = new string[]
            {
                                            this.records[num].RecNumber.ToString("d"),
                                            this.records[num].OpDate.ToString("d"),
                                            (this.records[num].OpType > 0) ? "Доход" : "Расход",
                                            this.records[num].OpSum.ToString("f"),
                                            this.records[num].Account,
                                            this.records[num].Category,
                                            this.records[num].Note
            };
            return recordText;
        }

   
        /// <summary>
        /// Отбирает записи по заданным в Template параметрам
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <returns>массив номеров записей, удовлетворяющих заданным параметрам
        /// или массив нулевой длины, если нечего не найдено</returns>
        public int[] Select(Template filter)
        {
            int[] selection = new int[this.index];
            int count = 0;
            
            for (int i = 0; i < this.index; i++)
            {
                if (Match(i, filter))
                {
                    selection[count] = this.records[i].RecNumber;
                    count++;
                }

            }
            Array.Resize(ref selection, count);
            return selection;
        }

        /// <summary>
        /// Выбирает из хранилища все записи, отвечающие заданным условиям
        /// </summary>
        /// <param name="filter"> Условия отбора</param>
        /// <returns>Массив записей</returns>
        public Record[] FilteredList (Template filter)
        {
            int[] selected = Select(filter);
            Record[] lst = new Record[selected.Length];
            for (int i = 0; i < lst.Length; i++)
            {
                lst[i] = this.records[selected[i]];
            }
            return lst;
        }

        /// <summary>
        /// Добавляет текущую запись в базу данных
        /// </summary>
        /// <param name="currentRecord"></param>
        public void Add(Record currentRecord)
        {
            if (this.index >= this.records.Length)
                Resize();
            this.records[this.index] = currentRecord;
            this.records[this.index].RecNumber = index;
            this.balance += this.records[this.index].Sum;
            this.index++;
            
        }

        #endregion



    }
}
