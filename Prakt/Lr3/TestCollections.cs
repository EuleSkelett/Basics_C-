using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DashaLabs

{

    class TestCollections<TKey, TValue>

    {

        private List<TKey> keyList = new List<TKey>();

        private List<string> stringKeyList = new List<string>();

        private Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        private Dictionary<string, TValue> dictWithStringKey = new Dictionary<string, TValue>();

        private GenerateElement<TKey, TValue> generator;

        public TestCollections(int count, GenerateElement<TKey, TValue> generator)

        {

            this.generator = generator;

            for (int i = 0; i < count; i++)

            {

                KeyValuePair<TKey, TValue> generated = this.generator(i);

                keyList.Add(generated.Key);

                stringKeyList.Add(generated.Key.ToString());

                dict.Add(generated.Key, generated.Value);

                dictWithStringKey.Add(generated.Key.ToString(), generated.Value);

            }

        }

        public void RunTest()

        {

            Console.WriteLine("поиск первого элемента");

            FindFirstInKeyList();

            FindFirstInStringKeyList();

            FindFirstInDict();

            FindFirstInStringKeyDict();

            FindFirstInDictWithContainsValue();

            Console.WriteLine("\nпоиск последнего элемента");

            FindLastInKeyList();

            FindLastInStringKeyList();

            FindLastInDict();

            FindLastInStringKeyDict();

            FindLastInDictWithContainsValue();

            Console.WriteLine("\nпоиск среднего элемента");

            FindMiddleInKeyList();

            FindMiddleInStringKeyList();

            FindMiddleInDict();

            FindMiddleInStringKeyDict();

            FindMiddleInDictWithContainsValue();

            Console.WriteLine("\nпоиск несуществующего элемента");

            FindUnexistsInKeyList();

            FindUnexistsInStringKeyList();

            FindUnexistsInDict();

            FindUnexistsInStringKeyDict();

            FindUnexistsInDictWithContainsValue();

        }

        public KeyValuePair<TKey, TValue> UnexistsValue

        {

            get;

            set;

        }

        public void FindFirstInKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в keyList");

            TKey findingEl = generator(0).Key;

            stopwatch.Start();

            keyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindFirstInStringKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в stringKeyList");

            string findingEl = generator(0).Key.ToString();

            stopwatch.Start();

            stringKeyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindFirstInDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict");

            KeyValuePair<TKey, TValue> findingEl = generator(0);

            stopwatch.Start();

            dict.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindFirstInStringKeyDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dictWithStringKey");

            KeyValuePair<TKey, TValue> el = generator(0);

            KeyValuePair<string, TValue> findingEl = new KeyValuePair<string, TValue>(el.Key.ToString(), el.Value);

            stopwatch.Start();

            dictWithStringKey.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindFirstInDictWithContainsValue()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict с помощью ContainsValue");

            KeyValuePair<TKey, TValue> findingEl = generator(0);

            stopwatch.Start();

            dict.ContainsValue(findingEl.Value);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindLastInKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в keyList");

            TKey findingEl = generator(keyList.Count - 1).Key;

            stopwatch.Start();

            keyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindLastInStringKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в stringKeyList");

            string findingEl = generator(keyList.Count - 1).Key.ToString();

            stopwatch.Start();

            stringKeyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindLastInDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict");

            KeyValuePair<TKey, TValue> findingEl = generator(keyList.Count - 1);

            stopwatch.Start();

            dict.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindLastInStringKeyDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dictWithStringKey");

            KeyValuePair<TKey, TValue> el = generator(keyList.Count - 1);

            KeyValuePair<string, TValue> findingEl = new KeyValuePair<string, TValue>(el.Key.ToString(), el.Value);

            stopwatch.Start();

            dictWithStringKey.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindLastInDictWithContainsValue()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict с помощью ContainsValue");

            KeyValuePair<TKey, TValue> findingEl = generator(keyList.Count - 1);

            stopwatch.Start();

            dict.ContainsValue(findingEl.Value);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindMiddleInKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в keyList");

            TKey findingEl = generator(keyList.Count / 2).Key;

            stopwatch.Start();

            keyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindMiddleInStringKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в stringKeyList");

            string findingEl = generator(keyList.Count / 2).Key.ToString();

            stopwatch.Start();

            stringKeyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindMiddleInDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict");

            KeyValuePair<TKey, TValue> findingEl = generator(keyList.Count / 2);

            stopwatch.Start();

            dict.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindMiddleInStringKeyDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dictWithStringKey");

            KeyValuePair<TKey, TValue> el = generator(keyList.Count / 2);

            KeyValuePair<string, TValue> findingEl = new KeyValuePair<string, TValue>(el.Key.ToString(), el.Value);

            stopwatch.Start();

            dictWithStringKey.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindMiddleInDictWithContainsValue()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict с помощью ContainsValue");

            KeyValuePair<TKey, TValue> findingEl = generator(keyList.Count / 2);

            stopwatch.Start();

            dict.ContainsValue(findingEl.Value);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindUnexistsInKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в keyList");

            TKey findingEl = generator(keyList.Count + 10).Key;

            stopwatch.Start();

            keyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindUnexistsInStringKeyList()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в stringKeyList");

            string findingEl = generator(keyList.Count + 10).Key.ToString();

            stopwatch.Start();

            stringKeyList.Contains(findingEl);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindUnexistsInDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict");

            KeyValuePair<TKey, TValue> findingEl = generator(keyList.Count + 10);

            stopwatch.Start();

            dict.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindUnexistsInStringKeyDict()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dictWithStringKey");

            KeyValuePair<TKey, TValue> el = generator(keyList.Count + 10);

            KeyValuePair<string, TValue> findingEl = new KeyValuePair<string, TValue>(el.Key.ToString(), el.Value);

            stopwatch.Start();

            dictWithStringKey.ContainsKey(findingEl.Key);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

        public void FindUnexistsInDictWithContainsValue()

        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Поиск в dict с помощью ContainsValue");

            KeyValuePair<TKey, TValue> findingEl = generator(keyList.Count + 10);

            stopwatch.Start();

            dict.ContainsValue(findingEl.Value);

            stopwatch.Stop();

            Console.WriteLine("Время: " + stopwatch.ElapsedTicks + " тиков процессора");

        }

    }

}