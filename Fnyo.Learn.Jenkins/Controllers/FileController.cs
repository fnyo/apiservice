using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    public class FileController:ControllerBase
    {
        [HttpPost("file/post")]
        public async Task<dynamic> PostFile([FromForm] IFormFile file)
        {
            // 拿到你传给我的文件，保存至我的文件服务器

            // saveFile(file);
            var response = new
            {
                FileName = file.FileName,
                Result = true
            };
            return await Task.FromResult(response);

        }
    }
}
