using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace DapperExampleInsert
{
    public class Groups
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
      
    }

    interface Users
    {
        List<Groups> GetAll();
        bool Add(Groups groups);
        Groups GetById(int GroupId);
        bool Update(Groups groups,String ColumnWidth);
        bool Delete(int GroupId);
    }

class UsersGroups :Users
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings['DefaultConnectionString'].ConnectionString);
       
        //Get Groups Record By Id
        public Contacts GetById(int GroupId)
        {
            return this.db.Query<Groups>('Select * From Groups Where GroupId=@Id', new { Id = id}).FirstOrDefault();
        }
        
        //Retreive the data from the table.
        public List<Groups> GetAll()
        {
            return this.db.Query<Contacts>('Select * From Groups').ToList();
        }
        //Add Groups Data
        public bool Add(Groups groups)
        {
            try
            {
                string sql = 'INSERT INTO Groupss(GroupName) values(@GroupName); SELECT CAST(SCOPE_IDENTITY() as int)';
                var returnId = this.db.Query<int>(sql, groups).SingleOrDefault();
                groups.Id = returnId;
            }
            catch (Exception ex)
            {
               
                return false;
            }
            return true;
        }
        //Update The Groups Record
        public bool Update(Groups groups,String ColumnName)
        {
            string query = 'update Groups set ' + ColumnName + '=@' + ColumnName + ' Where GroupId=@Id';
            var count = this.db.Execute(query,groups);
            return count > 0;
        }
        public bool Delete(int id)
        {
            var affectedrows = this.db.Execute('Delete from Groupss where GroupId=@Id', new {GroupId=id });
            return affectedrows > 0;
        }
    }


 class Program
    {
        public static UserG userg = new UserG();
        public void InsertData()
        {
            Console.WriteLine(new string('*', 20));
            Console.WriteLine('Enter the GroupName Of Groups');
            Console.Write('Group Name : ');
            String fName = Console.ReadLine();
            
            //inserting
            Contacts contacts1 = new Contacts 
            {
                GroupName = gName,
               
            };
            userg.Add(groups1);
            ShowData();
            
        }
        public void ShowData()
        {
            Console.WriteLine(new string('*', 20));
            List<Groups> groups = userg.GetAll();
            foreach (var cont in groups)
            {
                Console.WriteLine(cont.Id+' ' + cont.GroupName );
            }
        }
        public void UpdatingData()
        {
            Console.WriteLine(new string('*', 20));
            //Updating
            Console.WriteLine('What Id do you want to Update ');
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine('What do you want to Update....');
            Console.Write('Group Name press 1 ');
           
            int ch = Convert.ToInt32(Console.ReadLine());
            Groups groups = userg.GetById(id);
            String Name = null;
            switch (ch)
            {
                case 1:
                    Console.WriteLine('Group Name : ');
                    string gName = Console.ReadLine();
                    contacts.GroupName = gName;
                    Name = 'GroupName';
                    userg.Update(groups, Name);
                    GetByID(id);
                    break;
                
                default:
                    Console.WriteLine('Please select a choice atleast');
                    break;
            }            
        }
        //Get By ID Method
        public void GetByID(int id)
        {
            Console.WriteLine(new string('*', 20));
            Groups groups2 = userg.GetById(id);
            if (groups2 != null)
            {
                Console.WriteLine(groups2.GroupId + ' ' + groups2.GroupName );
            }
        }
        //Delete Method
        public void DeleteData()
        {
            Console.WriteLine(new string('*', 20));
            ShowData();
            Console.WriteLine(new string('*', 20));
            
            //Deletion
            Console.Write('What id do you want to delete :');
            int id = Convert.ToInt32(Console.ReadLine());
            userg.Delete(id);
            Groups con = userg.GetById(3);
            if(con==null)
            {
                Console.WriteLine('Groups record is deleted already');
            }
            Console.WriteLine(new string('*', 20));
            ShowData();
        }
        public void SelectOption()
        {
            Console.WriteLine(new string('*', 20));
           
            Console.WriteLine('Welcome To Example :');
            Console.WriteLine(new string('*', 20));
            Console.WriteLine('For...');
            Console.WriteLine('Show Data Select 1');
            Console.WriteLine('Insert Data Select 2');
            Console.WriteLine('Update Data Select 3');
            Console.WriteLine('Delete Data Select 4');
            Console.WriteLine();
            Console.Write('Your Selection :  ');
            int selection = Convert.ToInt32(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    ShowData();
                    break;
                case 2:
                    InsertData();
                    break;
                case 3:
                    UpdatingData();
                    break;
                case 4:
                    DeleteData();
                    break;
                default:                    
                    break;
            }
           
            Console.WriteLine(new string('*', 20));
        }
            
        static void Main(string[] args)
        {
            Program p = new Program();
            p.SelectOption();
            
           
            Console.ReadLine();
        }
    }




}
