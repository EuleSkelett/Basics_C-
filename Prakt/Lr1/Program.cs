using System;
using System.Linq;
using System.Diagnostics;

namespace Lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            var magazine = new Magazine("Тест", Frequency.Weekly, DateTime.Now, 2);
            Console.WriteLine(magazine.ToShortString());
            Console.WriteLine();

            Console.WriteLine(magazine[Frequency.Weekly]);
            Console.WriteLine(magazine[Frequency.Monthly]);
            Console.WriteLine(magazine[Frequency.Yearly]);
            Console.WriteLine();
            magazine.Title = "Тест 2";
            magazine.Frequency = Frequency.Yearly;
            magazine.PublishDate = magazine.PublishDate.AddDays(-1);
            magazine.Circulation = 3;
            magazine.Articles = new Article[]
            {
                new Article(new Person("Семен", "Куринов", new DateTime(2001, 6, 10)), "Статья 1", 1 ),
                new Article(new Person("Валера", "Рукин", new DateTime(2001, 7, 20)),"Статья 2", 2 )
            };
            Console.WriteLine(magazine);
            Console.WriteLine();

            magazine.AddArticles(
                new Article(new Person("Алекс", "Свон", new DateTime(2000, 4, 10)), "Статья 3", 3),
                new Article(new Person("Алла", "Теник", new DateTime(2000, 8, 15)), "Статья 4", 4)
            );
            Console.WriteLine(magazine);
            Console.WriteLine();

            //Cравнить время, необходимое для выполнения операций с элементами массивов
            var linearArray = new Article[1000000];
            var rectArray = new Article[1000,1000];
            var jaggedArray = new Article[1000][];
 
            for (int i = 0; i < jaggedArray.Length; i++)
                jaggedArray[i] = new Article[1000];
 
            //test1
            var sw = Stopwatch.StartNew();
 
            for (int i = 0; i < 1000000; i++)
                linearArray[i] = null;
 
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
 
            //test2
            sw = Stopwatch.StartNew();
 
            for (int i = 0; i < 1000; i++)
            for (int j = 0; j < 1000; j++)
                rectArray[i, j] = null;
 
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
 
            //test3
            sw = Stopwatch.StartNew();
 
            for (int i = 0; i < 1000; i++)
            for (int j = 0; j < 1000; j++)
                jaggedArray[i][j] = null;
 
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
 
            Console.ReadKey();
        }
    }
    class Person
    {
        private string name;
        private string secondname;
        private DateTime date;

        public Person(string name, string secondname, System.DateTime date)
        {
            Name = name;
            Secondname = secondname;
            Date = date;
        }

        public Person()
        { }
        public string Name { get; set; }
        public string Secondname { get; set; }
        DateTime Date { get; set; }
        int intstddate
        {
            get
            {
                return Convert.ToInt32(date);
            }

            set
            {
                date = Convert.ToDateTime(value);
            }
        }
        public override string ToString()
            => $"{Name}{Secondname} день роджения: {Date}";
        public string ToShortString()
            => $"{Name}{Secondname}";
    }
    enum Frequency { Weekly, Monthly, Yearly }

    //Опеделить класс Article, который имеет три открытых автореализуемых свойства, доступных для чтения и записи
    class Article
    {
        public Person Author{ get; set; }
        public string Title { get; set; }
        public double Top { get; set; }

        //Конструктор с параметрами типа Person, string, double для инициализации всех свойств класса
        public Article(Person author, string title, double top)
        {
            Author = author;
            Title = title;
            Top = top;
        }

        //Конструктор без параметров
        public Article()
            : this(new Person(), "Без названия", 0)
        {
        }

        // перегруженная (override) версия виртуального метода string ToString()
        public override string ToString()
             => $"${Title} с рейтингом {Top} от {Author}";
    }
    class Magazine
    {
        private string title; //закрытое поле типа string c названием журнала
        private Frequency frequency; //закрытое поле типа Frequency с информацией о периодичности выхода журнала
        private DateTime publishDate; //закрытое поле типа DateTime c датой выхода журнала
        private int circulation; //закрытое поле типа int с тиражом журнала
        private Article[] articles; //закрытое поле типа Article [] со списком статей в журнале

        //конструктор с параметрами типа string, Frequency, DateTime, int для инициализации соответствующих полей класса

        public Magazine(string title, Frequency frequency, DateTime publishDate, int circulation)
        {
            this.title = title;
            this.frequency = frequency;
            this.publishDate = publishDate;
            this.circulation = circulation;
        }

        //конструктор без параметров, инициализирующий поля класса значениями по умолчанию
        private Magazine()
        { 
        }

        //В классе Magazine определить свойства c методами get и set:
        //private string title;// Свойство типа string для доступа к полю с названием журнала
        public string Title
        {
            get => title;
            set => title = value;
        }

        public Frequency Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        public DateTime PublishDate
        {
            get => publishDate;
            set => publishDate = value;
        }

        public int Circulation
        {
            get => circulation;
            set => circulation = value;
        }

        public Article[] Articles
        {
            get => articles;
            set => articles = value;
        }
        public double GetAvgRating()
            => articles?.Average(x => x.Top) ?? 0;
        //Cвойство типа double ( только с методом get), в котором вычисляется среднее значение рейтинга в списке статей
        public bool this[Frequency frequency]
        {
            get => Frequency == frequency;
        }
        public void AddArticles(params Article[] newArticles)
        {
            if (newArticles?.Length == 0)
            {
                return;
            }

            if (articles == null)
            {
                articles = Array.Empty<Article>();
            }

            int oldLength = articles.Length;
            Array.Resize(ref articles, articles.Length + newArticles.Length);
            Array.Copy(newArticles, 0, articles, oldLength, newArticles.Length);
        }

        public override string ToString()
            => $"Title = {Title}"
            + $"\nFrequency = {Frequency}"
            + $"\nPublishDate = {PublishDate}"
            + $"\nCirculation = {Circulation}"
            + $"\nArticles = {string.Join<Article>("\n", Articles)}";

        public virtual string ToShortString()
            => $"Title = {Title}"
            + $"\nFrequency = {Frequency}"
            + $"\nPublishDate = {PublishDate}"
            + $"\nCirculation = {Circulation}"
            + $"\nAvg rating = {GetAvgRating()}";

    }
}