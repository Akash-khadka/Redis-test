using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Contracts
{
    public interface IStudentRepository
    {
        public Task<List<StudentsModel>> GetStudents();
    }
}
