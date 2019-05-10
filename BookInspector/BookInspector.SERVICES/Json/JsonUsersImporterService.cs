
namespace BookInspector.SERVICES.Json
{
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES.Contracts;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class JsonUsersImporterService : IJsonUsersImporterService
    {
        private readonly IUserService _userService;

        public JsonUsersImporterService(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public List<ApplicationUser> ImportUsers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist", nameof(filePath));
            }
            string fileContent = File.ReadAllText(filePath);
            List<JsonUser> jsonUsersList = JsonConvert.DeserializeObject<List<JsonUser>>(fileContent);


            List<ApplicationUser> result = new List<ApplicationUser>(); 
            foreach (var jsonUser in jsonUsersList)
            {

                var user = _userService.Register(jsonUser.Name);
                result.Add(user);
            }
            return result;
        }     
    }
}
