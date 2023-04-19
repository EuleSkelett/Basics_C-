using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LR2
{
    interface IRateAndCopy // Определить интерфейс IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
    enum Frequency { Weekly, Monthly, Yearly }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("1"); // Создать два объекта типа Edition с совпадающими данными и проверить, что ссылки на объекты не равны, а объекты равны, вывести значения хэш- кодов для объектов.
            Edition edition1 = new Edition("HAZI", new DateTime(2000, 8, 25), 10000);
            Edition edition2 = new Edition("HAZI", new DateTime(2000, 8, 25), 10000);
            Console.WriteLine(edition1.Equals(edition2));
            Console.WriteLine(String.Format("Edition1 hashcode: {0}\nEdition2 hashcode: {1}", edition1.GetHashCode(), edition2.GetHashCode()));
            Console.WriteLine();

            Console.WriteLine("2"); // В блоке try/catch присвоить свойству с тиражом издания некорректное значение, в обработчике исключения вывести сообщение, переданное через объект-исключение.
            try
            {
                edition1.GetSetCount = -1;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Incorrect value");
            }
            Console.WriteLine();

            Console.WriteLine("3"); // Создать объект типа Magazine, добавить элементы в списки статей и редакторов журнала и вывести данные объекта Magazine
            Magazine magazine = new Magazine();
            magazine.AddEditors(
                new Person("Albert", "Einstein", new DateTime(1879, 3, 14)),
                new Person("Isaac", "Newton", new DateTime(1642, 12, 25)),
                new Person("Charles", "Darwin", new DateTime(1809, 2, 12)),
                new Person("Nikola", "Tesla", new DateTime(1856, 7, 10)));
            magazine.AddArticles(
                new Article(new Person("Albert", "Einstein", new DateTime(1879, 3, 14)), "The Whole Package", 5),
                new Article(new Person("Isaac", "Newton", new DateTime(1642, 12, 25)), "The Man Who Defined Science on a Bet", 4.8),
                new Article(new Person("Charles", "Darwin", new DateTime(1809, 2, 12)), "Delivering the Evolutionary Gospel", 4.5),
                new Article(new Person("Nikola", "Tesla", new DateTime(1856, 7, 10)), "Wizard of the Industrial Revolution", 5));
            Console.WriteLine(magazine.ToString());
            Console.WriteLine();

            Console.WriteLine("4");
            Console.WriteLine(magazine.GetSetEditionType);
            Console.WriteLine();

            Console.WriteLine("5"); // С помощью метода DeepCopy() создать полную копию объекта Magazine. Изменить данные в исходном объекте Magazine и вывести копию и исходный объект, полная копия исходного объекта должна остаться без изменений.
            Magazine magazine1 = (Magazine)magazine.DeepCopy();
            magazine1.GetSetEditionName = "MCMK";
            magazine1.GetSetRelease = new DateTime(2020, 1, 1);
            magazine1.GetSetCount = 0;
            Console.WriteLine(magazine.ToShortString());
            Console.WriteLine(magazine1.ToShortString());
            Console.WriteLine();

            Console.WriteLine(6); // С помощью оператора foreach для итератора с параметром типа double вывести список всех статей с рейтингом больше некоторого заданного значения
            foreach (Article article in magazine.HigherRating(4.5))
                Console.WriteLine(article);
            Console.WriteLine();

            Console.WriteLine(7); // С помощью оператора foreach для итератора с параметром типа string вывести список статей, в названии которых есть заданная строка
            foreach (Article article in magazine.SubstrArticle("Who"))
                Console.WriteLine(article);
        }

    }
    class Person // новые версии классов Person
    {
        private string firstname; // закрытое поле типа string, в котором хранится имя
        private string lastname; // закрытое поле типа string, в котором хранится фамилия
        private DateTime birthDate; // закрытое поле типа System.DateTime для даты рождения

        public Person(string firstname, string lastname, DateTime birthDate) // конструктор c тремя параметрами типа string, string, DateTime для инициализации всех полей класса
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.birthDate = birthDate;
        }
        public string GetFirstName => firstname;
        public string GetLastName => lastname;
        public override bool Equals(object obj) // переопределить (override) виртуальный метод bool Equals (object obj);
        {
            Person objPerson = obj as Person;
            return firstname == objPerson.firstname && lastname == objPerson.lastname && birthDate == objPerson.birthDate;
        }
        public static bool operator ==(Person p1, Person p2) // определить операцию ==
        {
            return p1.Equals(p2);
        }
        public static bool operator !=(Person p1, Person p2) // определить операцию !=
        {
            return !p1.Equals(p2);
        }
        public override int GetHashCode() // Переопределение виртуального метода int GetHashCode()
        {
            int hashcode = 0;
            char[] charArr = (firstname + lastname + birthDate.Day + birthDate.Month + birthDate.Year).ToCharArray();
            foreach (char ch in charArr)
                hashcode += Convert.ToInt32(ch);
            return hashcode;
        }
        public virtual object DeepCopy() // определить метод object DeepCopy()
        {
            return new Person(firstname, lastname, birthDate);
        }
    }
    class Article : IRateAndCopy // новые версии классов Article
    {
        private Person author;
        private string title;
        private double Rating;

        public Article(Person author, string title, double Rating)
        {
            this.author = author;
            this.title = title;
            this.Rating = Rating;
        }
        public Article()
        {
            author = new Person("Marsel", "Nazirov", new DateTime(2000, 8, 25));
            title = "Kotlin";
            Rating = 5;
        }
        double IRateAndCopy.Rating // реализовать интерфейс IRateAndCopy
        {
            get
            {
                return Rating;
            }
        }
        public Person GetSetAuthor
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }
        public double SetRating
        {
            set
            {
                Rating = value;
            }
        }
        public string GetSetTitle
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public Article(double Rating)
        {
            this.Rating = Rating;
        }
        public object DeepCopy() // определить метод object DeepCopy()
        {
            return new Article(Rating);
        }
        public override string ToString()
        {
            return String.Format("Title: {0}, Rating: {1}", title, Rating);
        }
    }
    class Edition
    {
        protected string editionName; // защищенное(protected) поле типа string c названием издания
        protected DateTime release; // защищенное поле типа DateTime c датой выхода издания
        protected int count; // защищенное поле типа int с тиражом издания
        public Edition(string editionName, DateTime release, int count) // конструктор с параметрами типа string, DateTime, int для инициализации соответствующих полей класса
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
    class Magazine : Edition, IRateAndCopy //новые версии классов Magazine
    {
        private Frequency period; // закрытое поле типа Frequency с информацией о периодичности выхода журнала
        private ArrayList Editors = new ArrayList(); // закрытое поле типа System.Collections.ArrayList со списком редакторов журнала (объектов типа Person).
        private ArrayList Articles = new ArrayList(); // закрытое поле типа System.Collections.ArrayList, в котором хранится список статей в журнале (объектов типа Article).
        private double magazineRating; // закрытое поле типо string с рейтингом журнала

        public Magazine(string editionName, Frequency period, DateTime release, int count) // конструктор с параметрами типа string, Frequency, DateTime, int для инициализации соответствующих полей класса
        {
            this.editionName = editionName;
            this.period = period;
            this.release = release;
            this.count = count;
        }
        public Magazine() // конструктор без параметров для инициализации по умолчанию
        {
            editionName = "ABC";
            period = Frequency.Monthly;
            release = new DateTime(2008, 10, 10);
            count = 10000;
            Articles.Add(new Article());
        }
        public ArrayList ListOfArticles => Articles; // свойство типа System.Collections.ArrayList для доступа к полю со списком статей в журнале
        public double SrRating // свойство типа double (только с методом get), в котором вычисляется среднее значение рейтинга статей в журнале
        {
            get
            {
                double rtng = 0;
                foreach (Article article in Articles)
                    rtng += ((IRateAndCopy)article).Rating;
                return Articles.Count != 0 ? rtng / Articles.Count : 0;
            }
        }
        public void AddArticles(params Article[] args) // метод void AddArticles (params Article[]) для добавления элементов в список статей в журнале
        {
            Articles.AddRange(args);
        }
        public ArrayList ListOfEditors => Editors; // свойство типа System.Collections.ArrayList для доступа к списку редакторов журнала
        public void AddEditors(params Person[] args) // метод void AddEditors (params Person[]) для добавления элементов в список редакторов
        {
            Editors.AddRange(args);
        }
        public override string ToString() // перегруженная версия виртуального метода string ToString() для формирования строки со значениями всех полей класса, включая список статей и список редакторов
        {
            string articles = "";
            foreach (Article article in Articles)
                articles += "\n" + article.GetSetTitle;
            string editors = "";
            foreach (Person editor in Editors)
                editors += "\n" + editor.GetFirstName + " " + editor.GetLastName;
            return String.Format("Edition name is: {0}\nPeriod: {1}\nRelease date: {2}\nEdition count: {3}\nArticles: {4}\nEditors: {5}", editionName, period, release, count, articles, editors);
        }
        public virtual string ToShortString()
        {
            return String.Format("Edition name is: {0}\nPeriod: {1}\nRelease date: {2}\nEdition count: {3}\nAverage rating: {4}", editionName, period, release, count, SrRating);
        }
        public override object DeepCopy() // перегруженная (override) версия виртуального метода object DeepCopy()
        {
            Magazine magazine = new Magazine(editionName, period, release, count);
            magazine.Editors = Editors;
            magazine.Articles = Articles;
            return magazine;
        }
        double IRateAndCopy.Rating // реализовать интерфейс IRateAndCopy
        {
            get
            {
                return magazineRating;
            }
        }
        public Edition GetSetEditionType // свойство типа Edition; метод get свойства возвращает объект типа Edition, данные которого совпадают с данными подобъекта базового класса, метод set присваивает значения полям из подобъекта базового класса
        {
            get
            {
                return new Edition(editionName, release, count);
            }
            set
            {
                editionName = value.GetSetEditionName;
                release = value.GetSetRelease;
                count = value.GetSetCount;
            }
        }
        public IEnumerable<Article> HigherRating(double rating) // итератор с параметром типа double для перебора статей с рейтингом больше некоторого заданного значения
        {
            foreach (Article article in Articles)
                if (((IRateAndCopy)article).Rating > rating)
                    yield return article;
        }
        public IEnumerable<Article> SubstrArticle(string subStr) // итератор с параметром типа string для перебора статей, в названии которых есть заданная строка
        {
            foreach (Article article in Articles)
                if (article.GetSetTitle.Contains(subStr))
                    yield return article;
        }

    }
}