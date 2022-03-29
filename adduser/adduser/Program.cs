using System;
using System.DirectoryServices;

class adduser
{
    static void Adduser(string username,string password ,string group) {
        //string username = "";
        //string password = "";
        //string group = "";

        try
        {
            DirectoryEntry AD = new DirectoryEntry("WinNT://" +
            Environment.MachineName + ",computer");
            DirectoryEntry NewUser = AD.Children.Add(username, "user");
            NewUser.Invoke("SetPassword", new object[] { password });
            NewUser.Invoke("Put", new object[] { "Description", "Test User from .NET" });
            NewUser.CommitChanges();
            DirectoryEntry grp;

            grp = AD.Children.Find(group, "group");
            if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
            Console.WriteLine("Account Created Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }


    static void Main(string[] args)
    {
        Console.WriteLine("author：Kat");
        if (args.Length != 3)
        {
            Console.WriteLine("Usage：adduser.exe <username> <password> <group>");
        }
        else {
            string username = args[0];
            string password = args[1];
            string group = args[2];
            Adduser(username, password, group);
        
        }
    
        
    }
}