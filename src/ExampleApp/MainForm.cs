using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var person = new Person();
            person.Name = nameTextBox.Text;
            person.Age = Convert.ToInt32(ageTextBox.Text);
            person.Location = locationTextBox.Text;
            person.Books = new List<string>();
            person.Books.Add(book1TextBox.Text);
            person.Books.Add(book2TextBox.Text);

            var personService = new PersonService();
            personService.SavePerson(person);
        }

        private void loadPeopleButton_Click(object sender, EventArgs e)
        {
            var personService = new PersonService();
            var people = personService.GetPeople(loadPeopleLocationTextBox.Text);
            int i = 1;
            foreach (var person in people)
            {
                peopleTextBox.Text += string.Format("{0}   {1}({2}) from {3}. Books: {4}\r\n", i, person.Name, person.Age, person.Location, string.Join(", ", person.Books.ToArray()));
                i++;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void loadPeopleLocationTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void loadPersonButton_Click(object sender, EventArgs e)
        {
            var personService = new PersonService();
            var person = personService.GetPerson(loadPersonLocationTextBox.Text);
            personTextBox.Text = string.Format("{0}\r\nAge: {1}\r\nLocation: {2}\r\nBooks: {3}\r\n", person.Name, person.Age, person.Location, string.Join(", ", person.Books.ToArray()));
        }
    }
}
