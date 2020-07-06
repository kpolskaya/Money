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
        private Record[] records;                   // массив для хранения всех данных в памяти
        private string dbPath;                      // путь к базе данных на диске
        private int index;                          // текущий элемент для добавления в records
        private string[] titles;                    // заголовки полей записей

        /// <summary>
        /// Увеличивает длину текущего массива записей в два раза
        /// </summary>
        private void Resize()
        {
            Array.Resize(ref this.records, this.records.Length * 2);
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
        /// Загружает в память базу данных из файла
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

                    foreach (var item in args)
                    {
                        Console.Write($" {item} ");
                    }
                    Console.WriteLine();

                    Add(new Record(DateTime.Parse(args[1]), DateTime.Parse(args[2]), Convert.ToSByte(args[3]), Convert.ToDouble(args[4]), args[5], args[6], args[7]));
                }
            }
        }

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
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="DbPath">Путь к файлу базы данных</param>
        public Repository(string DbPath)
        {
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

            Load();                      // сразу же при создании происходит загрузка данных из файла
        }
    }
}
