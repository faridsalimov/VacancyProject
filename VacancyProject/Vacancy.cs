using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployerNamespace;

namespace VacancyNamespace
{
    internal class Vacancy
    {
        public int Id { get; set; }
        public static int StaticId = 1;
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Employer? FromEmployer { get; set; }

        public Vacancy(string? title, string? content, Employer? fromemployer)
        {
            Id = StaticId++;
            Title = title;
            Content = content;
            CreationDateTime = DateTime.Now;
            FromEmployer = fromemployer;
        }

        public override string ToString()
        {
            return $"Vacancy ID: {Id}\nTitle: {Title}\nContent: {Content}\nCreation Date Time: {CreationDateTime}\nFrom Employer: @{FromEmployer.Username}, {FromEmployer.Name} {FromEmployer.Surname}\n";
        }
    }
}