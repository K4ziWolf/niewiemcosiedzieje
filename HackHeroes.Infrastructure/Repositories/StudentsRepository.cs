using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using HackHeroes.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Infrastructure.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHackHeroesRepository _heroesRepository;

        public StudentsRepository(ApplicationDbContext dbContext, IHackHeroesRepository heroesRepository)
        {
            _dbContext = dbContext;
            _heroesRepository = heroesRepository;
        }

        public async Task Create(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Student student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Student?>> GetAllStudents(string encodedName)
        {           
            var result = await _dbContext.Students.Where(s => s.Class.EncodedName == encodedName).ToListAsync();

            return result;
        }

        public async Task<Domain.Entities.Student?> GetByStudentKey(string studentKey)
        {
            var result = await _dbContext.Students.FirstOrDefaultAsync(s => s.StudentKey == studentKey);

            return result;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Student?>> GetAllStudents()
        {
            var result = await _dbContext.Students.ToListAsync();

            return result;
        }


          public async Task SetStudentsNumber(string encodedName)
        {
            var students = await GetAllStudents(encodedName);

            var sortedStudents = students.OrderBy(u => u!.LastName).ToList();

            
            for (int i = 0; i < sortedStudents.Count; i++)
            {
                sortedStudents[i]!.Number = i + 1; 
            }

            students.ToList().Clear();

            students.ToList().AddRange(sortedStudents);

            foreach (var student in students)
            {
                student!.SetStudentKey();
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
