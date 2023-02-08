using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationNamespace;
using VacancyNamespace;
using WorkerNamespace;

namespace EmployerNamespace
{
    internal class Employer
    {
        public int Id { get; set; }
        public static int StaticId = 1;
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Sheher { get; set; }
        public int Phone { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; }
        public List<Vacancy> vacancies = new();
        public List<Notification> notifications = new();

        public Employer(string? username, string? name, string? surname, string? sheher, int phone, int age, string? password)
        {
            Id = StaticId++;
            Username = username;
            Name = name;
            Surname = surname;
            Sheher = sheher;
            Phone = phone;
            Age = age;
            Password = password;
        }

        public override string ToString()
        {
            return $"Employer ID: {Id}\nUsername: @{Username}\nName: {Name}\nSurname: {Surname}\nSheher: {Sheher}\nPhone: {Phone}\nAge: {Age}\n";
        }

        public void AddVacancy(Vacancy vc)
        {
            vacancies.Add(vc);
        }

        public void AddNotification(string? title, string? content, int Id, Worker worker)
        {
            foreach (var vc in vacancies)
            {
                if (vc.Id == Id)
                {
                    Notification notification = new(title, content, worker);
                    notifications.Add(notification);
                }
            }
        }

        public void RemoveVacancy(int index)
        {
            vacancies.RemoveAt(index);
        }

        public void ShowVacancies()
        {
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy);
            }
        }

        public void ShowNotifications()
        {
            foreach (var notification in notifications)
            {
                Console.WriteLine(notification);
            }
        }
    }
}
