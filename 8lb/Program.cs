using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8lb
{
    class Program
    {
        static void SortAdd(LinkedList<App> apps, App app)
        {
            if (apps == null)
            {
                throw new Exception("Список пуст.");
            }
            else
            {
                if (app != null)
                {
                    LinkedListNode<App> itemnode = apps.First;
                    App item;
                    if (itemnode == null)
                    {
                        apps.AddFirst(app);
                        return;
                    }
                    while (itemnode != null)
                    {
                        item = itemnode.Value;
                        if((itemnode == apps.First) && item.Sort(app))
                        {
                            apps.AddFirst(app);
                            return;
                        }
                        else if((itemnode == apps.Last) && !item.Sort(app))
                        {
                            apps.AddLast(app);
                            return;
                        }
                        else
                        {
                            if (item.Sort(app))
                            {
                                apps.AddBefore(itemnode, app);
                                return;
                            }
                            else
                            {
                                itemnode = itemnode.Next;
                            }
                        }
                    }
                }
                else throw new Exception();
            }
        }

        static App GetApp(LinkedList<App> apps, int index)
        {
            if (apps == null)
            {
                throw new Exception("Список пуст.");
            }
            if ((index < 0) || (index > apps.Count))
            {
                throw new Exception("Индекс за пределами списка.");
            }
            else
            {
                LinkedListNode<App> listNode = apps.First;
                App app;
                int i = 0;
                while (i<index)
                {
                    app = listNode.Value;
                    if (i == index - 1)
                    {
                        return app;
                    }
                    else
                    {
                        listNode = listNode.Next;
                        i++;
                        continue;
                    }
                }
                return null;
            }
        }

        static int[] CheckAvailApp(LinkedList<App> apps, string name)
        {
            if (apps == null)
            {
                throw new Exception("Список пуст.");
            }
            LinkedListNode<App> listNode = apps.First;
            App app;
            int size = 0, i = 0;
            while (listNode != null)
            {
                app = listNode.Value;
                if (name.CompareTo(app.Name) == 0)
                {
                    size++;
                    listNode = listNode.Next;
                    continue;
                }
                else
                {
                    listNode = listNode.Next;
                    continue;
                }
            }
            if (size==0)
            {
                int[] e = new int[1];
                size++;
                e[0] = -1;
                return e;
            }
            int[] mas = new int[size];
            listNode = apps.First;
            while (listNode != null)
            {
                app = listNode.Value;
                if (name.CompareTo(app.Name) == 0)
                {
                    mas[i] = i + 1;
                    i++;
                    listNode = listNode.Next;
                    continue;
                }
                else
                {
                    listNode = listNode.Next;
                    continue;
                }
            }
            return mas;
        }

        static string[] UniqueNames(LinkedList<App> apps)
        {
            if (apps == null)
            {
                throw new Exception("Список пуст.");
            }
            LinkedListNode<App> listNode = apps.First;
            App app;
            int k = 0, usize = 0;
            string[] allnames = new string[apps.Count];
            while(listNode != null)
            {
                app = listNode.Value;
                allnames[k] = app.Prouzvod;
                k++;
                listNode = listNode.Next;
            }
            bool f = true;
            for(int i=1; i< apps.Count; i++)
            {
                f = true;
                for(int j=i-1; j>=0; j--)
                {
                    if (allnames[i] == allnames[j])
                    {
                        f = true;
                        break;
                    }
                    else
                    {
                        f = false;
                    }
                }
                if (!f)
                {
                    usize++;
                }
            }
            string[] uniqnames = new string[usize + 1];
            uniqnames[0] = allnames[0];
            k = 1;
            for (int i = 1; i < apps.Count; i++)
            {
                f = true;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (allnames[i] == allnames[j])
                    {
                        f = true;
                        break;
                    }
                    else
                    {
                        f = false;
                    }
                }
                if (!f)
                {
                    uniqnames[k] = allnames[i];
                    k++;
                }
            }
            return uniqnames;
        }

        static void Main(string[] args)
        {
            LinkedList<App> apps = new LinkedList<App>();
            int n;
            Console.Write("Введите количество программ: ");
            try
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n <= 0) throw new Exception();
            }
            catch (Exception ex)
            {
                EventLog myLog = new EventLog();
                myLog.Source = "MySource";
                myLog.WriteEntry("Число программ должно быть больше нуля.");
                Console.WriteLine("Число программ должно быть больше нуля.");
                Console.ReadKey();
                return;
            }
            App[] Applications = new App[n];
            for(int i=0; i<n; i++)
            {
                Applications[i] = new App();
            }
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введите наименование программы {i+1}:");
                Applications[i].Name = Console.ReadLine();
                Console.Write($"Введите наименование производителя программы {i + 1}: ");
                Applications[i].Prouzvod = Console.ReadLine();
                Console.Write($"Введите размер на диске для программы (в Гб) {i + 1}: ");
                Applications[i].Razmer = Convert.ToDouble(Console.ReadLine());
                try
                {
                    if (Applications[i].Razmer <= 0) throw new Exception();
                }
                catch (Exception ex)
                {
                    EventLog myLog = new EventLog();
                    myLog.Source = "MySource";
                    myLog.WriteEntry("\"Размер на диске\" не может иметь такое значение.");
                    Console.WriteLine("\"Размер на диске\" не может иметь такое значение.");
                    Console.ReadKey();
                    return;
                }
                Console.Write($"Введите цену прогаммы (в рублях) {i+1}: ");
                Applications[i].Zena = Convert.ToDouble(Console.ReadLine());
                try
                {
                    if (Applications[i].Zena < 0) throw new Exception();
                }
                catch (Exception ex)
                {
                    EventLog myLog = new EventLog();
                    myLog.Source = "MySource";
                    myLog.WriteEntry("\"Цена программы\" не может иметь такое значение.");
                    Console.WriteLine("\"Цена программы\" не может иметь такое значение.");
                    Console.ReadLine();
                    return;
                }
            }
            try
            {
                foreach (App item in Applications)
                {
                    SortAdd(apps, item);
                }                            
                foreach (App item in apps)
                {
                    Console.WriteLine(item.Name + " " + item.Prouzvod + " " + item.Razmer + " " + item.Zena);
                }
                Console.Write("Введите индекс для удаления элемента: ");
                int index = Convert.ToInt32(Console.ReadLine());
                apps.Remove(GetApp(apps, index));
                foreach (App item in apps)
                {
                    Console.WriteLine(item.Name + " " + item.Prouzvod + " " + item.Razmer + " " + item.Zena);
                }
                Console.Write("Введите индекс для нахождения элемента: ");
                index = Convert.ToInt32(Console.ReadLine());
                App help = apps.Find(GetApp(apps, index)).Value;
                Console.WriteLine(help.Name + " " + help.Prouzvod + " " + help.Razmer + " " + help.Zena);
                Console.Write("Введите Наименование программы для нахождения индекса(ов): ");
                string strname = Console.ReadLine();
                int[] mas = CheckAvailApp(apps, strname);
                for(int i=0; i<mas.Length; i++)
                {
                    Console.WriteLine(mas[i]);
                }
                string[] unames = UniqueNames(apps);
                foreach(string item in unames)
                {
                    Console.WriteLine(item);
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ReadKey();
        }
    }
}
