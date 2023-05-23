using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using StudentsDetails.DbContexts;
using StudentsDetails.Dto;
using StudentsDetails.Models;


namespace StudentsDetails;



public class StudentRepository : IStudentRepository
{
    private readonly studentContext _Context;

    public StudentRepository(studentContext Context)
    {
        _Context = Context;
    }


    public async Task<List<StudentsDetailDto>> GetStudent()
    {
        return await _Context.StudentsDetails.ToListAsync();

    }

    public async Task<StudentsDetailDto> GetStudentById(int id)
    {
        var student = await _Context.StudentsDetails.FindAsync(id);
        return student;
    }
    public async Task<StudentsDetailDto> AddStudentAsync(StudentsDetailDto student)
    {
        _Context.StudentsDetails.Add(student);
        var s = await _Context.SaveChangesAsync();


        return student;
    }
    public async Task<StudentsDetailDto> UpdateStudentAsync(int id, StudentsDetailDto student)
    {
        var studentQuery = await GetStudentById(id);
        if (studentQuery == null)
        {
            return studentQuery;
        }

        _Context.Entry(studentQuery).CurrentValues.SetValues(student);
        await _Context.SaveChangesAsync();

        return studentQuery;
    }

    public async Task<StudentsDetailDto> DeleteStudentAsync(int id)
    {
        var student = await GetStudentById(id);
        if (student == null)
        {
            return student;
        }
        _Context.StudentsDetails.Remove(student);
        await _Context.SaveChangesAsync();

        return student;
    }

}