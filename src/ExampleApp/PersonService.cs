using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp
{
    public class PersonService
    {
        public int SavePerson(Person person)
        {
            //Succeeded. Return 1.
            return 1;
        }

        public List<Person> GetPeople(string location)
        {
            var results = new List<Person>();

            var person1 = new Person();
            person1.Name = "Jamie Zawinski";
            person1.Age = 44;
            person1.Location = "San Francisco, CA";
            person1.Books = new List<string>(new[] { "Coders at Work - Peter Seibel" });
            results.Add(person1);

            var person2 = new Person();
            person2.Name = "Richard Stallman";
            person2.Age = 60;
            person2.Location = "New York City, NY";
            person2.Books = new List<string>(new[] { "Free Software, Free Society" });
            results.Add(person2);

            var person3 = new Person();
            person3.Name = "Eric S. Raymond";
            person3.Age = 56;
            person3.Location = "Boston, MA";
            person3.Books = new List<string>(new[] { "The Cathedral and the Bazaar", "The Art of Unix Programming" });
            results.Add(person3);

            var person4 = new Person();
            person4.Name = "Alan Cox";
            person4.Age = 45;
            person4.Location = "Swansea, Wales";
            person4.Books = new List<string>(new[] { "Open Source Pioneers" });
            results.Add(person4);

            return results;
        }


        public Person GetPerson(string location)
        {
            var person1 = new Person();
            person1.Name = "Jamie Zawinski";
            person1.Age = 44;
            person1.Location = "San Francisco, CA";
            person1.Books = new List<string>(new[] { "Coders at Work - Peter Seibel" });
            return person1;
        }
    
    }
}
