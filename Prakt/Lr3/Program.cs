using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace DashaLabs

{

    class Program

    {

        static void Main(string[] args)

        {

            TestMagazineSort();

            TestMagazineCollection();

            TestCollectionTest();

            Console.ReadKey();

        }

        public static void TestMagazineSort()

        {

            Magazine magazine = new Magazine("Magazine", Frequency.Monthly, new DateTime(2019, 12, 12), 10);

            Person p1 = new Person("Ivan", "Ivanov", new DateTime(1998, 12, 23));

            Person p2 = new Person("Harry", "Potter", new DateTime(1984, 6, 11));

            Person p3 = new Person("Albus", "Dumbledore", new DateTime(1943, 5, 3));

            Article a1 = new Article(p1, "Art of Programming", 5.64);

            Article a2 = new Article(p1, "Art of War", 4.51);

            Article a3 = new Article(p3, "Fantastic Beasts", 8.54);

            Article a4 = new Article(p2, "Harry Potter and the Prisoner of Azkaban", 9.59);

            Article a5 = new Article(p2, "Artemis Fawl", 10);

            Article a6 = new Article(p1, "Tanya Grotter", 1.41);

            magazine.AddArticles(a1, a2, a3, a4, a5, a6);

            Console.WriteLine("Сортировка по названию");

            magazine.OrderByTitle();

            Console.WriteLine(magazine);

            Console.WriteLine("Сортировка по Имени автора");

            magazine.OrderByEditorName();

            Console.WriteLine(magazine);

            Console.WriteLine("Сортировка по рейтингу");

            magazine.OrderByScore();

            Console.WriteLine(magazine);

        }

        public static void TestMagazineCollection()

        {

            Magazine m1 = new Magazine("Crime and Destiny", Frequency.Monthly, new DateTime(2019, 12, 12), 10);

            Magazine m2 = new Magazine("Jealous cats and dogs", Frequency.Yearly, new DateTime(2012, 12, 12), 60);

            Magazine m3 = new Magazine("Games and Thrones", Frequency.Weekly, new DateTime(2013, 12, 12), 30);

            Person p1 = new Person("Ivan", "Ivanov", new DateTime(1998, 12, 23));

            Person p2 = new Person("Harry", "Potter", new DateTime(1984, 6, 11));

            Person p3 = new Person("Albus", "Dumbledore", new DateTime(1943, 5, 3));

            Article a1 = new Article(p1, "Art of Programming", 5.64);

            Article a2 = new Article(p1, "Art of War", 4.51);

            Article a3 = new Article(p3, "Fantastic Beasts", 8.54);

            Article a4 = new Article(p2, "Harry Potter and the Prisoner of Azkaban", 9.59);

            Article a5 = new Article(p2, "Artemis Fawl", 10);

            Article a6 = new Article(p1, "Tanya Grotter", 1.41);

            m1.AddArticles(a1, a2);

            m2.AddArticles(a3, a4);

            m3.AddArticles(a5, a6);

            MagazineCollection<string> magazineCollection = new MagazineCollection<string>(x => x.Name);

            magazineCollection.AddMagazines(m1, m2, m3);

            Console.WriteLine(magazineCollection);

            Console.WriteLine("Группировка по периодичности");

            IEnumerable<IGrouping<Frequency, KeyValuePair<string, Magazine>>> groups = magazineCollection.Groups;

            foreach (IGrouping<Frequency, KeyValuePair<string, Magazine>> entry in groups)

            {

                Console.WriteLine(entry.Key);

                foreach (KeyValuePair<string, Magazine> mag in entry)

                {

                    Console.WriteLine(mag);

                }

            }

            Console.WriteLine("Ежемесячные журналы");

            IEnumerable<KeyValuePair<string, Magazine>> monthlyMagazines = magazineCollection.FrequencyGroup(Frequency.Monthly);

            foreach (KeyValuePair<string, Magazine> entry in monthlyMagazines)

            {

                Console.WriteLine(entry.Value);

            }

            Console.WriteLine("Рейтинги");

            Console.WriteLine(magazineCollection.MaxScore);

        }

        public static void TestCollectionTest()

        {

            GenerateElement<Edition, Magazine> generator = x =>

            {

                Edition edition = new Edition("edition " + x, DateTime.Now, x * 2);

                Magazine magazine = new Magazine("magazine by edition " + x, (Frequency)(x % 3), DateTime.Now, x * 3);

                return new KeyValuePair<Edition, Magazine>(edition, magazine);

            };

            TestCollections<Edition, Magazine> testCollection = new TestCollections<Edition, Magazine>(10, generator);

            testCollection.RunTest();

        }

    }
    
}