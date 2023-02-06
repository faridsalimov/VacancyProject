using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerNamespace;

namespace NotificationNamespace
{
    internal class Notification
    {
        public int Id { get; set; }
        public static int StaticId = 1;
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Worker? FromWorker { get; set; }
        public DateTime DateTime { get; set; }

        public Notification(string? title, string? content, Worker? fromworker)
        {
            Id = StaticId++;
            Title = title;
            Content = content;
            FromWorker = fromworker;
            DateTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Notification ID: {Id}\nTitle: {Title}\nContent: {Content}\nDate Time: {DateTime}\nFrom: @{FromWorker.Username}, {FromWorker.Name} {FromWorker.Surname}\n";
        }
    }
}