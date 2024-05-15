using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UTMNStudentsExamAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public FilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<FilesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //POST api/files/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile excelData)
        {
            if (excelData == null || excelData.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine("uploads", excelData.FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await excelData.CopyToAsync(stream);
            }

            var pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "loader_script_v2.py");
            var result = RunPythonScript(pythonScriptPath, filePath, _configuration.GetConnectionString("ExamDatabasePython"));

            return Ok(new { result });
        }


        [NonAction]
        private string RunPythonScript(string scriptPath, string excelFilePath, string dbConnection)
        {
            var pythonInterpreterPath = _configuration["PythonInterpreter"];

            var start = new ProcessStartInfo
            {
                FileName = pythonInterpreterPath,
                Arguments = $"\"{scriptPath}\" \"{excelFilePath}\" \"{dbConnection}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                CreateNoWindow = true
            };

            using (var process = new Process())
            {
                process.StartInfo = start;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new InvalidOperationException($"Python script exited with code {process.ExitCode}: {error}");
                }

                return output;
            }
        }
    }
}
