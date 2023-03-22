using System;
using System.Linq;

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
            
            magazine.Name = "Тест 2";
            magazine.Frequency = Frequency.Yearly;
            magazine.PublishDate = magazine.PublishDate.AddDays(-1);
            magazine.Edition = 3;
            magazine.Articles = new Article[]
            {
                new Article("Статья 1", 1, new Person("Семен")),
                new Article("Статья 2", 2, new Person("Валера"))
            };
            Console.WriteLine(magazine);
            Console.WriteLine();
            
            magazine.AddArticles(
                new Article("Статья 3", 3, new Person("Алекс")),
                new Article("Статья 4", 4, new Person("Алла"))
            );
            Console.WriteLine(magazine);
            Console.WriteLine();
        }
    }

enum Frequency { Weekly, Monthly, Yearly }
 
sealed class Article
{
    public Article(string name, double rating, Person author)
    {
        Name = name;
        Rating = rating;
        Author = author;
    }
 
    public Article()
        : this("Без названия", 0, new Person("Нет автора"))
    {
    }
 
    public string Name { get; set; }
    public double Rating { get; set; }
    public Person Author { get; set; }
 
    public override string ToString()
        => $"{Name} с рейтингом {Rating} от {Author}";
}
 
sealed class Person
{
    public Person(string name)
    {
        Name = name;
    }
 
    public string Name { get; set; }
}
 
class Magazine
{
    private string name;
    private Frequency frequency;
    private DateTime publishDate;
    private int edition;
    private Article[] articles;
 
    public Magazine(
        string name,
        Frequency frequency,
        DateTime publishDate,
        int edition)
    {
        this.name = name;
        this.frequency = frequency;
        this.publishDate = publishDate;
        this.edition = edition;
    }
 
    public Magazine()
    {
    }
 
    public string Name
    {
        get => name;
        set => name = value;
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
 
    public int Edition
    {
        get => edition;
        set => edition = value;
    }
 
    public Article[] Articles
    {
        get => articles;
        set => articles = value;
    }
 
    public double GetAvgRating()
        => articles?.Average(x => x.Rating) ?? 0;
 
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
        => $"Name = {Name}"
        + $"\nFrequency = {Frequency}"
        + $"\nPublishDate = {PublishDate}"
        + $"\nEdition = {Edition}"
        + $"\nArticles = {string.Join<Article>("\n", Articles)}";
 
    public virtual string ToShortString()
        => $"Name = {Name}"
        + $"\nFrequency = {Frequency}"
        + $"\nPublishDate = {PublishDate}"
        + $"\nEdition = {Edition}"
        + $"\nAvg rating = {GetAvgRating()}";
}