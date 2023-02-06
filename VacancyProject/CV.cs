using NotificationNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVNamespace
{
    internal class CV
    {
        public string? Specialty { get; set; }
        public string? School { get; set; }
        public int AcceptanceScore { get; set; }
        public string? Companies { get; set; }
        public string? Skills { get; set; }
        public bool Diploma { get; set; }

        public CV(string? specialty, string? school, int acceptancescore, string? companies, string? skills, bool diploma)
        {
            Specialty = specialty;
            School = school;
            AcceptanceScore = acceptancescore;
            Companies = companies;
            Skills = skills;
            Diploma = diploma;
        }

        public override string ToString()
        {
            return $"Specialty: {Specialty}\nSchool: {School}\nAcceptance Score: {AcceptanceScore}\nCompanies: {Companies}\nSkills: {Skills}\nDiploma: {(Diploma ? "YES" : "NO")}";
        }
    }
}