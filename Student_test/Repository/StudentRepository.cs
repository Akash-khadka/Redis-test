using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Contracts;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class StudentRepository :IStudentRepository
    {
        private readonly DapperContext _context;
        public StudentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<StudentsModel>> GetStudents()
        {
            var query = "Select * from student_test";
            using(var connection = _context.CreateConnection())
            {
                var students= await connection.QueryAsync<StudentsModel>(query);
                return students.ToList();
            }
        }
    }
}
