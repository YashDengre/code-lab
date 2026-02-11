using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Utility
{
    public class BulkUserCreation
    {
        static Guid FileName = Guid.NewGuid();
        public static void DownloadFile()
        {
            ExcelService.GenerateExcelWithGroupDropdown($"{Environment.CurrentDirectory}/Sample{FileName}.xlsx");
        }
        public static void UploadFile()
        {
            ExcelService.ProcessUploadedFile($"{Environment.CurrentDirectory}/Sample{FileName}.xlsx");
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid GroupId { get; set; }
    }

    public class Group
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public static class GroupRepository
    {
        // Simulating fetching groups from a database
        public static List<Group> GetAllGroups()
        {
            return new List<Group>
            {
                new Group { Id = Guid.NewGuid() , Name = "Admin" },
                new Group { Id = Guid.NewGuid(), Name = "User" },
                new Group { Id = Guid.NewGuid(), Name = "Guest" },
                new Group { Id = Guid.NewGuid(), Name = "Manager" }
            };
        }
    }

}
