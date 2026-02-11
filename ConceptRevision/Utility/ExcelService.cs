using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
namespace ConceptRevision.Utility
{
    public class ExcelService
    {
        static ExcelService()
        {
            //ExcelPackage.LicenseContext = ExcelPackage.License..NonCommercialPersonal;
            ////ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage.License.SetNonCommercialPersonal("NonCommericialPersonal");

        }
        public static void GenerateExcelWithGroupDropdown(string filePath)
        {
            // Fetch available groups
            var groups = GroupRepository.GetAllGroups();
            var groupNames = string.Join(",", groups.ConvertAll(g => g.Name)); // CSV string of group names for dropdown

            // Create Excel package
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("User Data");

                // Add headers
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Email";
                worksheet.Cells[1, 3].Value = "Group (Visible)";
                worksheet.Cells[1, 4].Value = "Group (Hidden)";

                for (int row = 2; row <= 10; row++)
                {
                    worksheet.Cells[row, 1].Value = $"User {row}";  // Name
                    worksheet.Cells[row, 2].Value = $"user{row}@example.com";  // Email

                    // Populate Group (Visible) with group names
                    worksheet.Cells[row, 3].Value = groups[row % groups.Count].Name;  // Group Name (Visible)
                    worksheet.Cells[row, 4].Formula = $"VLOOKUP(C{row},$G$2:$H${groups.Count + 1},2,FALSE)";  // Group ID (Hidden)

                    // This formula will dynamically update the Group ID based on the selected Group Name in column C
                }

                var groupColumn = worksheet.Cells[2, 3, 10, 3];  // Group Name (Visible Column)
                var validation = groupColumn.DataValidation.AddListDataValidation();
                validation.Formula.ExcelFormula = $"\"{string.Join(",", groups.Select(g => g.Name))}\"";

                // Hide the "Group (Hidden)" column (the column where the IDs are stored)
                worksheet.Column(4).Hidden = true;

                // Add a lookup table in columns G and H for VLOOKUP reference (Group Name - Group ID)
                worksheet.Cells[2, 7].Value = "Group Name";
                worksheet.Cells[2, 8].Value = "Group ID";

                for (int i = 0; i < groups.Count; i++)
                {
                    worksheet.Cells[i + 3, 7].Value = groups[i].Name;  // Group Name
                    worksheet.Cells[i + 3, 8].Value = groups[i].Id;    // Group ID
                }

                // Save the file
                package.Save();
            }

            Console.WriteLine("Excel file with dropdown saved to " + filePath);
        }

        public static void ProcessUploadedFile(string filePath)
        {
            try
            {
                // Open the Excel file for reading
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    // Read and process the data (example: print all rows)
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string name = worksheet.Cells[row, 1].Text;  // Name
                        string email = worksheet.Cells[row, 2].Text; // Email
                        string groupName = worksheet.Cells[row, 3].Text; // Group Name (Visible)
                        var guid = worksheet.Cells[row, 4].Text;
                        Guid groupId = Guid.Parse(guid); // Group ID (Hidden)

                        // Process the group ID (for example, find the group in the database)
                        Console.WriteLine($"Name: {name}, Email: {email}, Group: {groupName} (ID: {groupId})");
                    }
                }

                Console.WriteLine("File processed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }

    }
}





