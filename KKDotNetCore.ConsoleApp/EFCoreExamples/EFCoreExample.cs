using KKDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(2);
            //Create("EF", 12);
            //Update(1,"Khun* edited", 12);
            Delete(15);
        }
        private void Read()
        {
            List<UserDataModel> lst = _dbContext.Users.ToList();
            foreach (UserDataModel item in lst)
            {
                Console.WriteLine($"User_Id => {item.User_Id}");
                Console.WriteLine($"User_Name => {item.User_Name}");
                Console.WriteLine($"User_Age => {item.User_Age}");
                Console.WriteLine("-------------------------");
            }
        }
        private void Edit(int id)
        {
            UserDataModel? item = _dbContext.Users.FirstOrDefault(x => x.User_Id == id);

            if (item is null){
                Console.WriteLine("No user found");
                return;
            };


                Console.WriteLine($"User_Id => {item.User_Id}");
                Console.WriteLine($"User_Name => {item.User_Name}");
                Console.WriteLine($"User_Age => {item.User_Age}");
                Console.WriteLine("-------------------------");
        }
        private void Create(string name,int age)
        {
            _dbContext.Users.Add(new UserDataModel
            {
                User_Name = name,
                User_Age = age
            });
            int result = _dbContext.SaveChanges();


            string message = result > 0 ? "Saved user." : "Failed to save";

            Console.Write(message);
        }

        private void Update(int id,string name,int age)
        {
            UserDataModel? item = _dbContext.Users.AsNoTracking().FirstOrDefault(x => x.User_Id == id);
            //AsNoTracking(): Just select the copy data from db. As we can't select while other are inserting.

            if (item is null)
            {
                Console.WriteLine("No user found");
                return;
            }

            item.User_Name = name;
            item.User_Age = age;

            int result = _dbContext.SaveChanges();


            string message = result > 0 ? "Updated user." : "Failed to update";

            Console.Write(message);
        }
        private void Delete(int id)
        {
            UserDataModel? item = _dbContext.Users.FirstOrDefault(x => x.User_Id == id);

            if (item is null)
            {
                Console.WriteLine("No user found");
                return;
            }
            _dbContext.Remove(item);

            int result = _dbContext.SaveChanges();


            string message = result > 0 ? "Deleted user." : "Failed to delete";

            Console.Write(message);
        }
    }
}
