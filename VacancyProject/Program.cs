using EmployerNamespace;
using VacancyNamespace;
using WorkerNamespace;
using CVNamespace;
using NotificationNamespace;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;

void addAlert(string? text, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.Write("[!]");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($" {text}\n\n");
    Console.Beep();
}

List<Employer> employers = new();
Employer e1 = new("faridsalimov", "Farid", "Salimov", "Baku", 0507388281, 20, "farid123");
employers.Add(e1);

Vacancy v1 = new("C# Developer Elani", "Herkese salam! Boyuk bir proyekt ucun C# Developeri axtarilir.", e1);
Vacancy v2 = new("Frontend Developer Elani", "Salam! Web-sayt proyekti qururuq, HTML ve CSS biliyi guclu olan axtarilir.", e1);
e1.AddVacancy(v1);
e1.AddVacancy(v2);

List<Worker> workers = new();

// Bu kodları programı yoxlamazdan evvel işe salın. İşe saldıqdan sonra comment edebilersiz.
//Worker w1 = new("fuad38", "Fuad", "Selimov", "Xirdalan", 12412412, 20, "fuad123", new CV("Backend Developer", "Number 9 School", 652, "Uber, Microsoft, Amazon", "C#, C++, Python", true));
//Worker w2 = new("rikoo", "Rinat", "Qedimov", "Shemkir", 5124100, 28, "riko5757", new CV("Frontend Developer", "Number 2 School", 598, "Instagram, Bolt", "HTML, CSS, Java", true));
//workers.Add(w1);
//workers.Add(w2);
//var jsonn = JsonSerializer.Serialize(workers);
//File.WriteAllText("workers.json", jsonn);

var jsonWorkers = File.ReadAllText("workers.json");
workers = JsonSerializer.Deserialize<List<Worker>>(jsonWorkers);

while (true)
{
    try
    {
        Console.ForegroundColor = ConsoleColor.White;
    label:
        Console.WriteLine("1 - Employer\n2 - Worker");
        ConsoleKeyInfo key = Console.ReadKey();

        if (key.Key == ConsoleKey.D1)
        {
            Console.Clear();

            string? username;
            Console.Write("Enter username: ");
            username = Console.ReadLine();

            string? password;
            Console.Write("Enter password: ");
            password = Console.ReadLine();

            bool empfound = false;

            foreach (var employer in employers)
            {
                if (employer.Username == username && employer.Password == password)
                {
                    empfound = true;
                    Console.Clear();
                label2:
                    Console.WriteLine("1 - Show Vacancies\n2 - Show Notifications\n3 - Show Workers\n4 - Add Vacancy\n5 - Add Worker\n6 - Remove Vacancy\n7 - Remove Worker\n8 - Search\n9 - Logout");
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.D1)
                    {
                    showvaci:
                        if (e1.vacancies.Count != 0)
                        {
                            Console.Clear();
                            e1.ShowVacancies();

                            Console.WriteLine("B - Back");
                            key = Console.ReadKey();

                            if (key.Key == ConsoleKey.B)
                            {
                                Console.Clear();
                                goto label2;
                            }

                            else
                            {
                                Console.Clear();
                                addAlert("Invilad select!", ConsoleColor.Red);
                                goto showvaci;
                            }
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("No Vacancy.", ConsoleColor.Red);
                            goto label2;
                        }
                    }

                    else if (key.Key == ConsoleKey.D2)
                    {
                    shownoti:
                        if (e1.notifications.Count != 0)
                        {
                            Console.Clear();
                            e1.ShowNotifications();

                            Console.WriteLine("A - Select Request");
                            Console.WriteLine("B - Back");
                            key = Console.ReadKey();

                            if (key.Key == ConsoleKey.A)
                            {
                                Console.Clear();
                                int id;
                                Console.Write("Enter Notification ID: ");
                                id = Convert.ToInt32(Console.ReadLine());

                                if (e1.notifications.Count >= id)
                                {
                                choose:
                                    Console.Clear();
                                    foreach (var notification in e1.notifications)
                                    {
                                        if (notification.Id == id)
                                        {
                                            Console.WriteLine($"Worker ID: {notification.FromWorker.Id}\nUsername: @{notification.FromWorker.Username}\nName: {notification.FromWorker.Name}\nSurname: {notification.FromWorker.Surname}\nPhone: {notification.FromWorker.Phone}\nAge: {notification.FromWorker.Age}\n>> CV:\nSpecialty: {notification.FromWorker.Cv.Specialty}\nSchool: {notification.FromWorker.Cv.School}\nAcceptance Score: {notification.FromWorker.Cv.AcceptanceScore}\nCompanies: {notification.FromWorker.Cv.Companies}\nSkills: {notification.FromWorker.Cv.Skills}\nDiploma: {(notification.FromWorker.Cv.Diploma ? "YES" : "NO")}");
                                        }
                                    }
                                    Console.WriteLine("\nA - Accept\nD - Decline");
                                    key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.A)
                                    {
                                        foreach (var notification in e1.notifications)
                                        {
                                            if (notification.Id == id)
                                            {
                                                e1.notifications.RemoveAt(id - 1);
                                                workers.Remove(notification.FromWorker);
                                                var json = JsonSerializer.Serialize(workers);
                                                File.WriteAllText("workers.json", json);
                                                Console.Clear();
                                                addAlert("Request successfully accepted.", ConsoleColor.Green);
                                                goto label2;
                                            }
                                        }
                                    }
                                    else if (key.Key == ConsoleKey.D)
                                    {
                                        Console.Clear();
                                        addAlert("Request successfully declined.", ConsoleColor.Green);
                                        goto label2;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        addAlert("Invilad select!", ConsoleColor.Red);
                                        goto choose;
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    addAlert("No such Notification.", ConsoleColor.Red);
                                    goto label2;
                                }
                            }

                            else if (key.Key == ConsoleKey.B)
                            {
                                Console.Clear();
                                goto label2;
                            }

                            else
                            {
                                Console.Clear();
                                addAlert("Invilad select!", ConsoleColor.Red);
                                goto shownoti;
                            }
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("No Notification.", ConsoleColor.Red);
                            goto label2;
                        }
                    }

                    else if (key.Key == ConsoleKey.D3)
                    {
                    showworkers:
                        if (workers.Count != 0)
                        {
                            Console.Clear();
                            foreach (var worker in workers)
                            {
                                Console.WriteLine(worker + "\n");
                            }

                            Console.WriteLine("B - Back");
                            key = Console.ReadKey();

                            if (key.Key == ConsoleKey.B)
                            {
                                Console.Clear();
                                goto label2;
                            }

                            else
                            {
                                Console.Clear();
                                addAlert("Invilad select!", ConsoleColor.Red);
                                goto showworkers;
                            }
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("No Worker.", ConsoleColor.Red);
                            goto label2;
                        }
                    }

                    else if (key.Key == ConsoleKey.D4)
                    {
                    addvacy:
                        Console.Clear();

                        string? title;
                        Console.Write("Enter title: ");
                        title = Console.ReadLine();

                        string? content;
                        Console.Write("Enter content: ");
                        content = Console.ReadLine();

                        Vacancy vc = new(title, content, e1);
                        e1.AddVacancy(vc);

                        Console.Clear();
                        addAlert("Vacancy successfully added.", ConsoleColor.Green);
                        goto label2;
                    }

                    else if (key.Key == ConsoleKey.D5)
                    {
                        Console.Clear();
                        string? usernamee;
                        Console.Write("Enter username: ");
                        usernamee = Console.ReadLine();

                        string? namee;
                        Console.Write("Enter name: ");
                        namee = Console.ReadLine();

                        string? surnamee;
                        Console.Write("Enter surname: ");
                        surnamee = Console.ReadLine();

                        string? sheherr;
                        Console.Write("Enter sheher: ");
                        sheherr = Console.ReadLine();

                        int numberr;
                        Console.Write("Enter number: ");
                        numberr = Convert.ToInt32(Console.ReadLine());

                        int agee;
                        Console.Write("Enter age: ");
                        agee = Convert.ToInt32(Console.ReadLine());

                        string? passwordd;
                        Console.Write("Enter password: ");
                        passwordd = Console.ReadLine();

                        string? specialtyy;
                        Console.Write("Enter specialty: ");
                        specialtyy = Console.ReadLine();

                        string? schooll;
                        Console.Write("Enter school: ");
                        schooll = Console.ReadLine();

                        int score;
                        Console.Write("Enter acceptance score: ");
                        score = Convert.ToInt32(Console.ReadLine());

                        string? companiess;
                        Console.Write("Enter companies: ");
                        companiess = Console.ReadLine();

                        string? skillss;
                        Console.Write("Enter skills: ");
                        skillss = Console.ReadLine();

                        Console.WriteLine("Diploma:");
                    diplomaselect:
                        bool diplomaa;
                        Console.WriteLine("1 - Yes\n2 - No");
                        key = Console.ReadKey();

                        if (key.Key == ConsoleKey.D1)
                        {
                            diplomaa = true;
                        }

                        else if (key.Key == ConsoleKey.D2)
                        {
                            diplomaa = false;
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("Invilad select!", ConsoleColor.Red);
                            goto diplomaselect;
                        }

                        workers.Add(new Worker(usernamee, namee, surnamee, sheherr, numberr, agee, passwordd, new CV(specialtyy, schooll, score, companiess, skillss, diplomaa)));
                        var json = JsonSerializer.Serialize(workers);
                        File.WriteAllText("workers.json", json);

                        Console.Clear();
                        addAlert("Worker successfully added.", ConsoleColor.Green);
                        goto label2;
                    }

                    else if (key.Key == ConsoleKey.D6)
                    {
                    removevacy:
                        if (e1.vacancies.Count != 0)
                        {
                            Console.Clear();
                            int index;
                            Console.Write("Enter Vacancy ID: ");
                            index = Convert.ToInt32(Console.ReadLine());

                            if (e1.vacancies.Count < index)
                            {
                                Console.Clear();
                                addAlert("No such Vacancy.", ConsoleColor.Red);
                                goto label2;
                            }

                            else
                            {
                                e1.RemoveVacancy(index - 1);
                                Console.Clear();
                                addAlert("Vacancy successfully removed.", ConsoleColor.Green);
                                goto label2;
                            }
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("No Vacancy.", ConsoleColor.Red);
                            goto label2;
                        }
                    }

                    else if (key.Key == ConsoleKey.D7)
                    {
                    removeworker:
                        if (workers.Count != 0)
                        {
                            Console.Clear();
                            int index;
                            Console.Write("Enter Worker ID: ");
                            index = Convert.ToInt32(Console.ReadLine());

                            if (workers.Count < index)
                            {
                                Console.Clear();
                                addAlert("No such Worker.", ConsoleColor.Red);
                                goto label2;
                            }

                            else
                            {
                                workers.RemoveAt(index - 1);
                                var json = JsonSerializer.Serialize(workers);
                                File.WriteAllText("workers.json", json);

                                Console.Clear();
                                addAlert("Worker successfully removed.", ConsoleColor.Green);
                                goto label2;
                            }
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("No Worker.", ConsoleColor.Red);
                            goto label2;
                        }
                    }

                    else if (key.Key == ConsoleKey.D8)
                    {
                        Console.Clear();
                    search:
                        if (workers.Count != 0)
                        {
                            Console.WriteLine("1 - Name Search\n2 - Surname Search\n3 - Sheher Search\n4 - Age Search\n5 - Specialty Search\n6 - Acceptance Score Search\n7 - Skill Search\n8 - Diploma Search\n9 - Company Search\n0 - Back");
                            key = Console.ReadKey();

                            if (key.Key == ConsoleKey.D1)
                            {
                                Console.Clear();

                                string? nameSearch;
                                Console.Write("Enter name: ");
                                nameSearch = Console.ReadLine();

                                Console.Clear();
                                var result = workers.Where(wk => wk.Name.Contains(nameSearch)).ToList();
                                result.ForEach(wk => Console.WriteLine(wk));
                                goto search;
                            }

                            else if (key.Key == ConsoleKey.D2)
                            {
                                Console.Clear();

                                string? surnameSearch;
                                Console.Write("Enter surname: ");
                                surnameSearch = Console.ReadLine();

                                Console.Clear();
                                var result = workers.Where(wk => wk.Surname.Contains(surnameSearch)).ToList();
                                result.ForEach(wk => Console.WriteLine(wk));
                                goto search;
                            }
                            
                            else if (key.Key == ConsoleKey.D3)
                            {
                                Console.Clear();

                                string? sheherSearch;
                                Console.Write("Enter surname: ");
                                sheherSearch = Console.ReadLine();

                                Console.Clear();
                                var result = workers.Where(wk => wk.Sheher.Contains(sheherSearch)).ToList();
                                result.ForEach(wk => Console.WriteLine(wk));
                                goto search;
                            }
                            
                            else if (key.Key == ConsoleKey.D4)
                            {
                                Console.Clear();

                                int searchAge;
                                Console.Write("Enter age: ");
                                searchAge = Convert.ToInt32(Console.ReadLine());

                            boyukkicik:
                                Console.WriteLine($"1 - Greater than {searchAge}\n2 - Less than {searchAge}\n3 - Equal to {searchAge}");
                                key = Console.ReadKey();

                                if (key.Key == ConsoleKey.D1)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Age > searchAge).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else if (key.Key == ConsoleKey.D2)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Age < searchAge).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else if (key.Key == ConsoleKey.D3)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Age == searchAge).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else
                                {
                                    Console.Clear();
                                    addAlert("Invilad select!", ConsoleColor.Red);
                                    goto boyukkicik;
                                }
                            }
                            
                            else if (key.Key == ConsoleKey.D5)
                            {
                                Console.Clear();

                                string? specialtySearch;
                                Console.Write("Enter specialty: ");
                                specialtySearch = Console.ReadLine();

                                Console.Clear();
                                var result = workers.Where(wk => wk.Cv.Specialty.Contains(specialtySearch)).ToList();
                                result.ForEach(wk => Console.WriteLine(wk));
                                goto search;
                            }
                            
                            else if (key.Key == ConsoleKey.D6)
                            {
                                Console.Clear();

                                int searchScore;
                                Console.Write("Enter acceptance score: ");
                                searchScore = Convert.ToInt32(Console.ReadLine());

                            boyukkicik:
                                Console.WriteLine($"1 - Greater than {searchScore}\n2 - Less than {searchScore}\n3 - Equal to {searchScore}");
                                key = Console.ReadKey();

                                if (key.Key == ConsoleKey.D1)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Cv.AcceptanceScore > searchScore).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else if (key.Key == ConsoleKey.D2)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Cv.AcceptanceScore < searchScore).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else if (key.Key == ConsoleKey.D3)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Cv.AcceptanceScore == searchScore).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else
                                {
                                    Console.Clear();
                                    addAlert("Invilad select!", ConsoleColor.Red);
                                    goto boyukkicik;
                                }
                            }
                            
                            else if (key.Key == ConsoleKey.D7)
                            {
                                Console.Clear();

                                string? skillSearch;
                                Console.Write("Enter skill: ");
                                skillSearch = Console.ReadLine();

                                Console.Clear();
                                var result = workers.Where(wk => wk.Cv.Skills.Contains(skillSearch)).ToList();
                                result.ForEach(wk => Console.WriteLine(wk));
                                goto search;
                            }
                            
                            else if (key.Key == ConsoleKey.D8)
                            {
                            diplomasearch:
                                Console.Clear();

                                Console.WriteLine("1 - With a diploma\n2 - Without a diploma");
                                key = Console.ReadKey();

                                if (key.Key == ConsoleKey.D1)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Cv.Diploma == true).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else if (key.Key == ConsoleKey.D2)
                                {
                                    Console.Clear();
                                    var result = workers.Where(wk => wk.Cv.Diploma == false).ToList();
                                    result.ForEach(wk => Console.WriteLine(wk));
                                    goto search;
                                }

                                else
                                {
                                    Console.Clear();
                                    addAlert("Invilad select!", ConsoleColor.Red);
                                    goto diplomasearch;
                                }
                            }
                            
                            else if (key.Key == ConsoleKey.D9)
                            {
                                Console.Clear();

                                string? companySearch;
                                Console.Write("Enter company name: ");
                                companySearch = Console.ReadLine();

                                Console.Clear();
                                var result = workers.Where(wk => wk.Cv.Companies.Contains(companySearch)).ToList();
                                result.ForEach(wk => Console.WriteLine(wk));
                                goto search;
                            }
                            
                            else if (key.Key == ConsoleKey.D0)
                            {
                                Console.Clear();
                                goto label2;
                            }

                            else
                            {
                                Console.Clear();
                                addAlert("Invilad select!", ConsoleColor.Red);
                                goto search;
                            }
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("No Worker.", ConsoleColor.Red);
                            goto label2;
                        }
                    }

                    else if (key.Key == ConsoleKey.D9)
                    {
                        Console.Clear();
                        goto label;
                    }

                    else
                    {
                        Console.Clear();
                        addAlert("Invilad select!", ConsoleColor.Red);
                        goto label2;
                    }
                }
            }

            if (!empfound)
            {
                Console.Clear();
                addAlert("Wrong username or password!", ConsoleColor.Red);
                goto label;
            }
        }

        else if (key.Key == ConsoleKey.D2)
        {
            Console.Clear();

            string? username;
            Console.Write("Enter username: ");
            username = Console.ReadLine();

            string? password;
            Console.Write("Enter password: ");
            password = Console.ReadLine();

            bool workerfound = false;

            foreach (var worker in workers)
            {
                if (worker.Username == username && worker.Password == password)
                {
                    workerfound = true;
                    Console.Clear();
                label22:
                    Console.WriteLine("1 - Show Vacancies\n2 - Logout");
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.D1)
                    {
                    showvaci2:
                        if (e1.vacancies.Count != 0)
                        {
                            Console.Clear();
                            e1.ShowVacancies();

                            Console.WriteLine("A - Select Vacancy");
                            Console.WriteLine("B - Back");
                            key = Console.ReadKey();

                            if (key.Key == ConsoleKey.A)
                            {
                                Console.Clear();

                                int id;
                                Console.Write("Enter Vacancy ID: ");
                                id = Convert.ToInt32(Console.ReadLine());

                                string? title;
                                Console.Write("Enter title: ");
                                title = Console.ReadLine();

                                string? content;
                                Console.Write("Enter content: ");
                                content = Console.ReadLine();

                                e1.AddNotification(title, content, id, worker);

                                Console.Clear();
                                addAlert("Vacancy sent successfully.", ConsoleColor.Green);
                                goto label22;
                            }

                            else if (key.Key == ConsoleKey.B)
                            {
                                Console.Clear();
                                goto label22;
                            }

                            else
                            {
                                Console.Clear();
                                addAlert("Invilad select!", ConsoleColor.Red);
                                goto showvaci2;
                            }
                        }

                        else
                        {
                            Console.Clear();
                            addAlert("No Vacancy.", ConsoleColor.Red);
                            goto label22;
                        }
                    }

                    else if (key.Key == ConsoleKey.D2)
                    {
                        Console.Clear();
                        goto label;
                    }

                    else
                    {
                        Console.Clear();
                        addAlert("Invilad select!", ConsoleColor.Red);
                        goto label22;
                    }
                }
            }

            if (!workerfound)
            {
                Console.Clear();
                addAlert("Wrong username or password!", ConsoleColor.Red);
                goto label;
            }
        }

        else
        {
            Console.Clear();
            addAlert("Invilad select!", ConsoleColor.Red);
        }
    }

    catch (Exception ex)
    {
        Console.Clear();
        Console.WriteLine(">> Error: " + ex.Message + "\n");
    }
}
