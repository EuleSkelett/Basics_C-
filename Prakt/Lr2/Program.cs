using System;
using System.Collections;
using System.Collections.Generic;
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
            var rectArray = new Article[1000, 1000];
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
        //переопределить метод virtial bool Equals (object obj)
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        //переопределить виртуальный метод int GetHashCode()
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //определить операции == и != так, чтобы равенство объектов типа Person 
        //трактовалось как совпадение всех данных объектов, 
        //а не ссылок на объекты Person
        public static bool operator ==(Person p1, Person p2)
 
        {
 
            bool result = false;
 
            if (p1.Name.Equals(p2.Name))
 
                if (p1.Secondname.Equals(p2.Secondname))
 
                    if (p1.Date.Equals(p2.Date))
 
                        result = true;
 
            return result;
 
        }
        public static bool operator !=(Person p1, Person p2)
 
        {
 
            bool result = true;
 
            if (p1.Name.Equals(p2.Name))
 
                if (p1.Secondname.Equals(p2.Secondname))
 
                    if (p1.Date.Equals(p2.Date))
 
                        result = false;
 
            return result;
 
        }
        public object DeepCopy()
 
        {
 
            return new Person(Name, Secondname, Date);
 
            
 
        }
    }
    enum Frequency { Weekly, Monthly, Yearly }

    //Опеделить класс Article, который имеет три открытых автореализуемых свойства, доступных для чтения и записи
    class Article : IRateAndCopy
    {
        public Person Author { get; set; }
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
        double IRateAndCopy.Top // реализовать интерфейс IRateAndCopy
        {
            get
            {
                return Top;
            }
        }
        public Person GetSetAuthor
        {
            get
            {
                return Author;
            }
            set
            {
                Author = value;
            }
        }
        public double SetTop
        {
            set
            {
                Top = value;
            }
        }
        public string GetSetTitle
        {
            get
            {
                return Title;
            }
            set
            {
                Title = value;
            }
        } 
        public Article(double Top)
        {
            this.Top = Top;
        }

        // перегруженная (override) версия виртуального метода string ToString()
        public override string ToString()
             => $"{Title} с рейтингом {Top} от {Author}";

        public object DeepCopy()
        {
            return new Article(Top);
        }
    }
    class Edition
    {
        protected string editionName; // защищенное(protected) поле типа string c названием издания
        protected DateTime release; // защищенное поле типа DateTime c датой выхода издания
        protected int count; // защищенное поле типа int с тиражом издания
        public Edition(string editionName, DateTime release, int count) 
        // конструктор с параметрами типа string, DateTime, 
        //int для инициализации соответствующих полей класса
        {
            this.editionName = editionName;
            this.release = release;
            this.count = count;
        }
        public Edition() // конструктор без параметров для инициализации по умолчанию;
        {
            editionName = "ABC";
            release = new DateTime(2008, 10, 10);
            count = 10000;
        }
        public string GetSetEditionName // свойство типа string для доступа к полю с названием журнала
        {
            get
            {
                return editionName;
            }
            set
            {
                editionName = value;
            }
        }
        public DateTime GetSetRelease // свойство типа DateTime для доступа к полю c датой выхода журнала
        {
            get
            {
                return release;
            }
            set
            {
                release = value;
            }
        }
        public int GetSetCount // свойство типа int для доступа к полю с тиражом журнала
        {
            get
            {
                return count;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Count cannot be negative");
                else
                    count = value;
            }
        }
        public virtual object DeepCopy() // виртуальный метод object DeepCopy()
        {
            return new Edition(editionName, release, count);
        }
        public override bool Equals(object obj) // переопределить метод Equals (object obj)
        {
            Edition objEdition = obj as Edition;
            return editionName == objEdition.editionName && release == objEdition.release && count == objEdition.count;            
        }
        public static bool operator ==(Edition ed1, Edition ed2) // определить операцию ==
        {
            return ed1.Equals(ed2);
        }
        public static bool operator !=(Edition ed1, Edition ed2) // определить операцию !=
        {
            return !ed1.Equals(ed2);
        }
        public override int GetHashCode() // виртуальный метод int GetHashCode()
        {
            int hashcode = 0;
            Char[] charArr = (editionName + count + release.Year + release.Month + release.Day).ToCharArray();
            foreach (char ch in charArr)
                hashcode += Convert.ToInt32(ch);
            return hashcode;
        }
        public override string ToString() // перегруженная версия виртуального метода string ToString() для формирования строки со значениями всех полей класса
        {
            return String.Format("Edition name is: {0}\nRelease date: {1}\nEdition count: {2}", editionName, release, count);
        }
    }

    class Magazine : Edition, IRateAndCopy
    {
        private string title; //закрытое поле типа string c названием журнала
        private ArrayList Editors = new ArrayList(); // закрытое поле типа System.Collections.ArrayList со списком редакторов журнала (объектов типа Person).
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
        public ArrayList ListOfArticles => Articles; // свойство типа System.Collections.ArrayList для доступа к полю со списком статей в журнале
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
        public double GetAvgTop()
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
            + $"\nAvg Top = {GetAvgTop()}";

    }
    interface IRateAndCopy
    { 
        double Top { get;}
        object DeepCopy();
    }
}