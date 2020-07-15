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
        ///  Загружает в структуру записи из файла
        /// </summary>
        private void Load()
        {
            if (!File.Exists(this.dbPath)) return;

            using (StreamReader dbStream = new StreamReader(this.dbPath))
            {
                //this.titles = 
                dbStream.ReadLine();

                while (!dbStream.EndOfStream)
                {
                    string[] args = dbStream.ReadLine().Split(';');



                    Add(new Record(DateTime.Parse(args[1]), DateTime.Parse(args[2]), Convert.ToSByte(args[3]), Convert.ToDouble(args[4]), args[5], args[6], args[7]));
                }
            }

        }


            /// <summary>
            /// Добавляет текущую запись в базу данных
            /// </summary>
            /// <param name="currentRecord"></param>
        public void Add(Record currentRecord)
        {
            if (this.index >= this.records.Length)
                Resize();
            this.records[index] = currentRecord;
            this.records[index].RecNumber = index;
            this.index++;
        }

        

        /// <summary>
        /// Проверяет запись с указанным номером на соответствие заданным условиям
        /// </summary>
        /// <param name="num">номер записи</param>
        /// <param name="filter">шаблон с условиями</param>
        /// <returns>true - если запись соответствует,  false - если нет, или такая запись отстутствует</returns>
        private bool Match(int num, Template filter)
        {
            if (num < 0 || num >= this.index) return false;
            if (this.records[num].Deleted) return false;

            bool crDateOK = this.records[num].CrDate >= filter.CrFromDate && this.records[num].CrDate <= filter.CrEndDate;
            bool dateOK = this.records[num].OpDate >= filter.FromDate && this.records[num].OpDate <= filter.EndDate;
            bool typeOK = filter.WhatType == 0 || this.records[num].OpType == filter.WhatType;
            bool accOK = filter.WhatAcc == "" || this.records[num].Account == filter.WhatAcc;
            bool catOK = filter.WhatCat == "" || this.records[num].Category == filter.WhatCat;

            return crDateOK && dateOK && typeOK && accOK && catOK;
        }

        #endregion


        #region Публичные Методы
        /// <summary>
        /// Сохраняет в файл все записи, которые не помечены для удаления
        /// </summary>
        public void Save()
        {
            if (File.Exists(this.dbPath))
                File.Delete(this.dbPath);
            string temp = String.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                                        this.titles[0],
                                        this.titles[1],
                                        this.titles[2],
                                        this.titles[3],
                                        this.titles[4],
                                        this.titles[5],
                                        this.titles[6],
                                        this.titles[7]);

            File.WriteAllText(this.dbPath, $"{temp}\n");
            for (int i = 0; i < index; i++)
            {
                if (this.records[i].Deleted) continue;

                temp = String.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                                        this.records[i].RecNumber,
                                        this.records[i].CrDate,
                                        this.records[i].OpDate,
                                        this.records[i].OpType,
                                        this.records[i].OpSum,
                                        this.records[i].Account,
                                        this.records[i].Category,
                                        this.records[i].Note);
                File.AppendAllText(dbPath, $"{temp}\n");
                this.LastSavingTime = DateTime.Now;

            }
        }
        
        /// <summary>
        /// Помечает запись как удаленную из базы
        /// </summary>
        /// <param name="num">номер записи</param>
        public void Delete(int num)
        {
            if (num < index && num >= 0)
                this.records[num].Deleted = true;
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

        #endregion



    }
}
