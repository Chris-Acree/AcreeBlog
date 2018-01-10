using System.Threading.Tasks;

namespace AcreeBlog.Services
{
    public interface IImageService
  {
    Task<string> SaveImage(byte[] bytes, string filename);
  }
}
