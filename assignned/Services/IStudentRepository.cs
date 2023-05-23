using Microsoft.AspNetCore.JsonPatch;
using StudentsDetails.Dto;
using StudentsDetails.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsDetails;
public interface IStudentRepository
{
    Task<List<StudentsDetailDto>> GetStudent();

    Task<StudentsDetailDto> GetStudentById(int id);

    Task<StudentsDetailDto> AddStudentAsync(StudentsDetailDto student);

    Task<StudentsDetailDto> DeleteStudentAsync(int id);

    Task<StudentsDetailDto> UpdateStudentAsync(int id, StudentsDetailDto student);

    //Task<Student> UpdateStudentPatchAsync(int id, JsonPatchDocument student);
}