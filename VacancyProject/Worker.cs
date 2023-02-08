using NotificationNamespace;
using CVNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerNamespace
{
    internal class Worker
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
        public CV? Cv { get; set; }

        public Worker(string? username, string? name, string? surname, string? sheher, int phone, int age, string? password, CV? cv)
        {
            Id = StaticId++;
            Username = username;
            Name = name;
            Surname = surname;
            Sheher = sheher;
            Phone = phone;
            Age = age;
            Password = password;
            Cv = cv;
        }

        public override string ToString()
        {
            return $"Worker ID: {Id}\nUsername: @{Username}\nName: {Name}\nSurname: {Surname}\nSheher: {Sheher}\nPhone: {Phone}\nAge: {Age}\n>> CV:\nSpecialty: {Cv.Specialty}\nSchool: {Cv.School}\nAcceptance Score: {Cv.AcceptanceScore}\nCompanies: {Cv.Companies}\nSkills: {Cv.Skills}\nDiploma: {(Cv.Diploma ? "YES\n" : "NO\n")}";
        }
    }
}
