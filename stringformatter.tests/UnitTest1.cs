using NUnit.Framework;
using System;

namespace stringformatter.tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void CorrectTemplate()
        {
            User user = new User("Bob", "Dylan");
            string result = StringFormatter.Shared.Format("FirstName {FirstName} LastName {LastName}", user);
            Assert.That($"FirstName {user.FirstName} LastName {user.LastName}".Equals(result));
        }

        [Test]
        public void EscapingBraces()
        {
            User user = new User("Robert", "Zimmerman");
            string result = StringFormatter.Shared.Format("FirstName {{{FirstName}}} LastName {{{LastName}}}", user);
            Assert.That($"FirstName {{{user.FirstName}}} LastName {{{user.LastName}}}".Equals(result));
        }

        [Test]
        public void IncorrectTemplateArray()
        {
            User user = new User("John", "Lemon", new string[] { "sleep" });
           
            Assert.Catch<ArgumentException>(() =>
            {
                string result = StringFormatter.Shared.Format("FirstName {FirstName} LastName {LastName} array {strings[0]}", user);
            });
        }

        [Test]
        public void IncorrectTemplateSpaces()
        {
            User user = new User("Henry", "Cow");

            Assert.Catch<ArgumentException>(() =>
            {
                string result = StringFormatter.Shared.Format("FirstName {Fi rstName} LastName {LastName} ", user);
            });
        }

        [Test]
        public void IncorrectTemplateBraces()
        {
            User user = new User("Curly", "Bracket");

            Assert.Catch<ArgumentException>(() =>
            {
                string result = StringFormatter.Shared.Format("FirstName {FirstName LastName {LastName} ", user);
            });
        }
    }

    public class User
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string[] Strings { get; }

        public User(string firstName, string lastName, string[] strings)
        {
            FirstName = firstName;
            LastName = lastName;
            Strings = strings;
        }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}