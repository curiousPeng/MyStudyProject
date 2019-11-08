using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StudyProject
{
    internal class NewMailEventArgs : EventArgs
    {
        private readonly string m_from, m_to, m_subject;
        public NewMailEventArgs(string from, string to, string subject)
        {
            m_from = from; m_to = to; m_subject = subject;
        }
        public string From { get { return m_from; } }
        public string To { get { return m_to; } }
        public string Subject { get { return m_subject; } }
    }
    internal class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;

        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            Volatile.Read(ref NewMail)?.Invoke(this, e);
        }

        public void SimulateNewMail(string from, string to, string subject)
        {
            NewMailEventArgs e = new NewMailEventArgs(from, to, subject);
            OnNewMail(e);
        }
    }

    internal sealed class Fax
    {
        public Fax(MailManager m)
        {
            m.NewMail += FaxMsg;
        }
        private void FaxMsg(object sender,NewMailEventArgs e)
        {
            Console.Write($"from={e.From},to={e.To},subject={e.Subject}");
        }
        public void UnRegister(MailManager m)
        {
            m.NewMail -= FaxMsg;
        }
    }
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventDelegate)
        {
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);

            if (temp != null)
                temp(sender, e);
        }
    }
}
